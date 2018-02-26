using System;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using static System.Diagnostics.Process;
using System.Management;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using Be.Windows.Forms;
using System.Drawing;

namespace ChameleonMiniGUI
{
    public partial class frm_main : Form
    {
        private SerialPort _comport = null;
        private string[] _modesArray = null;
        private string[] _buttonModesArray = null;
        private string[] _buttonLongModesArray = null;
        private string _cmdExtension = "MY";
        private System.Windows.Forms.Timer timer1;
        private bool isConnected = false;
        private bool disconnectPressed = false;

        private string _current_comport = string.Empty;
        private string _deviceIdentification;
        private string _firmwareVersion;

        public string FirmwareVersion
        {
            get { return _firmwareVersion; }
            set
            {
                _firmwareVersion = value;
                tb_firmware.Text = _firmwareVersion;
            }
        }

        public frm_main()
        {
            InitializeComponent();

            txt_output.SelectionStart = 0;
        }

        #region Event Handlers
        private void frm_main_Load(object sender, EventArgs e)
        {
            // Find the COM port of the Chameleon (wait reply of VERSIONMY? from every available port)
            //ConnectToChameleon();
            OpenChameleonSerialPort();

            if (_comport != null && _comport.IsOpen)
            {
                DeviceConnected();

                // Get all available modes and populate the dropdowns
                GetSupportedModes();

                // Refresh all
                RefreshAllSlots();
            }

            // Select no tag slot
            foreach (var cb in FindControls<CheckBox>(Controls, "checkBox"))
            {
                cb.Checked = false;
            }

            // Load the saved settings
            LoadSettings();

            // Initialize timer
            InitTimer();
        }

        private void LoadSettings()
        {
            // Set the default download path if not empty and exists
            if (!string.IsNullOrEmpty(Properties.Settings.Default.DownloadDumpPath))
            {
                if (Directory.Exists(Properties.Settings.Default.DownloadDumpPath))
                {
                    txt_defaultdownload.Text = Properties.Settings.Default.DownloadDumpPath;
                } // else create folder?
            }

            // Set the keep alive options
            chk_keepalive.Checked = Properties.Settings.Default.EnableKeepAlive;
            if (Properties.Settings.Default.KeepAliveInterval > 0)
            {
                txt_interval.Text = Properties.Settings.Default.KeepAliveInterval.ToString();
            }
            else
            {
                // set the default value
                // should be a setting aswell 
                txt_interval.Text = "2000";
            }
        }

        private void frm_main_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Stop the timer
            if (timer1 != null)
            {
                timer1.Stop();
            }

            // Close the port if still open
            if (_comport != null && _comport.IsOpen)
            {
                _comport.Close();
            }

            // Save the download path if exists
            if (!string.IsNullOrEmpty(txt_defaultdownload.Text))
            {
                if (Directory.Exists(txt_defaultdownload.Text))
                {
                    Properties.Settings.Default.DownloadDumpPath = txt_defaultdownload.Text;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void btn_apply_Click(object sender, EventArgs e)
        {
            // Get all selected indices
            foreach (var cb in FindControls<CheckBox>(Controls, "checkBox"))
            {
                if (!cb.Checked) continue;

                var tagslotIndex = int.Parse(cb.Name.Substring(cb.Name.Length - 1));
                if (tagslotIndex <= 0) continue;

                //SETTINGMY=tagslotIndex-1
                SendCommandWithoutResult($"SETTING{_cmdExtension}=" + (tagslotIndex - 1));

                //SETTINGMY? -> SHOULD BE "NO."+tagslotIndex
                var selectedSlot = SendCommand($"SETTING{_cmdExtension}?").ToString();
                if (!selectedSlot.Contains((tagslotIndex - 1).ToString())) return;


                var selectedMode = string.Empty;

                // Set the mode of the selected slot
                var cb_mode = FindControls<ComboBox>(Controls, $"cb_mode{tagslotIndex}").FirstOrDefault();
                if (cb_mode != null)
                {
                    //CONFIGMY=cb_mode.SelectedItem
                    SendCommandWithoutResult($"CONFIG{_cmdExtension}={cb_mode.SelectedItem}");
                    selectedMode = cb_mode.SelectedItem.ToString();
                }

                // Set the button mode of the selected slot
                var cb_button = FindControls<ComboBox>(Controls, $"cb_button{tagslotIndex}").FirstOrDefault();
                if (cb_button != null)
                {
                    //BUTTONMY=cb_buttonMode.SelectedItem
                    SendCommandWithoutResult($"BUTTON{_cmdExtension}={cb_button.SelectedItem}");
                }

                // Set the button long mode of the selected slot
                var cb_buttonlong = FindControls<ComboBox>(Controls, $"cb_buttonlong{tagslotIndex}").FirstOrDefault();
                if (cb_buttonlong != null)
                {
                    //BUTTONLONGMY=cb_buttonMode.SelectedItem
                    SendCommandWithoutResult($"BUTTONLONG{_cmdExtension}={cb_buttonlong.SelectedItem}");
                }

                // Set the UID
                var txtUid = FindControls<TextBox>(Controls, $"txt_uid{tagslotIndex}").FirstOrDefault();
                if (txtUid != null)
                {
                    string uid = txtUid.Text;
                    // always set UID,  either with user provided or random. Is that acceptable?
                    if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(selectedMode) && IsUidValid(uid, selectedMode))
                    {
                        SendCommandWithoutResult($"UID{_cmdExtension}={uid}");
                    }
                    else
                    {
                        // set a random UID
                        SendCommandWithoutResult($"UID{_cmdExtension}=?");
                    }
                }

                RefreshSlot(tagslotIndex - 1);
            }
        }

        private void btn_bootmode_Click(object sender, EventArgs e)
        {
            SendCommandWithoutResult("UPGRADE" + _cmdExtension);

            try
            {
                _comport.Close();
            }
            catch (Exception) { }

            _comport = null;

            DeviceDisconnected();
        }

        private void btn_exitboot_Click(object sender, EventArgs e)
        {
            bool failed = true;

            // Run the bootloader exe
            try
            {
                var bootloaderPath = ConfigurationManager.AppSettings["BOOTLOADER_PATH"];
                var bootloaderFileName = ConfigurationManager.AppSettings["BOOTLOADER_EXE"];
                var flashFileName = ConfigurationManager.AppSettings["FLASH_BINARY"];
                var eepromFileName = ConfigurationManager.AppSettings["EEPROM_BINARY"];

                var fullBootloaderPath = Path.Combine(bootloaderPath, bootloaderFileName);
                var fullFlashBinaryPath = Path.Combine(bootloaderPath, flashFileName);
                var fullEepromBinaryPath = Path.Combine(bootloaderPath, eepromFileName);

                if (File.Exists(fullBootloaderPath) && File.Exists(fullFlashBinaryPath) && File.Exists(fullEepromBinaryPath))
                {
                    Start(fullBootloaderPath);
                    failed = false;
                }
                else
                {
                    MessageBox.Show("Unable to find all the required files to exit the boot mode", "Exit Boot Mode failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                //
            }

            if (failed)
            {
                MessageBox.Show("Unable to exit the bootloader mode", "Exit Boot Mode failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            foreach (var cb in FindControls<CheckBox>(Controls, "checkBox"))
            {
                if (!cb.Checked) continue;

                var tagslotIndex = int.Parse(cb.Name.Substring(cb.Name.Length - 1));
                if (tagslotIndex <= 0) continue;

                // select the corresponding slot
                SendCommandWithoutResult($"SETTING{_cmdExtension}={tagslotIndex - 1}");

                // Open dialog
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    var dumpFilename = openFileDialog1.FileName;

                    // Load the dump
                    LoadDump(dumpFilename);

                    // Refresh slot
                    RefreshSlot(tagslotIndex - 1);
                }

                break; // We can only upload a single dump at a time
            }
        }

        private void btn_download_Click(object sender, EventArgs e)
        {
            var downloadPath = Application.StartupPath;

            // Try to use the default download path if exists
            if (!string.IsNullOrEmpty(txt_defaultdownload.Text))
            {
                if (Directory.Exists(txt_defaultdownload.Text))
                {
                    downloadPath = txt_defaultdownload.Text;
                }
            }

            // Get all selected indices
            foreach (var cb in FindControls<CheckBox>(Controls, "checkBox"))
            {
                if (!cb.Checked) continue;

                var tagslotIndex = int.Parse(cb.Name.Substring(cb.Name.Length - 1));
                if (tagslotIndex <= 0) continue;

                // select the corresponding slot
                SendCommandWithoutResult("SETTING" + _cmdExtension + "=" + (tagslotIndex - 1));

                if (btn_upload.Enabled)
                {
                    // Only one tag slot is selected, show the save dialog

                    saveFileDialog1.InitialDirectory = downloadPath;

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        var dumpFilename = saveFileDialog1.FileName;

                        // Add extension if missing
                        dumpFilename = !dumpFilename.ToLower().Contains(".bin") ? dumpFilename + ".bin" : dumpFilename;

                        // Save the dump
                        SaveDump(dumpFilename);
                    }

                    break; // no need to check the others
                }
                else
                {
                    // Get UID first
                    var uid = SendCommand("UID" + _cmdExtension + "?").ToString();

                    if (!string.IsNullOrEmpty(uid))
                    {
                        var varFullDownloadPath = Path.Combine(downloadPath, uid + ".bin");
                        SaveDump(varFullDownloadPath);
                    }
                }
            }
        }

        private void btn_mfkey_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            // Get all selected indices
            foreach (var cb in FindControls<CheckBox>(Controls, "checkBox"))
            {
                if (!cb.Checked) continue;

                var tagslotIndex = int.Parse(cb.Name.Substring(cb.Name.Length - 1));
                if (tagslotIndex <= 0) return;

                txt_output.Text += $"[Tag slot {tagslotIndex}]{Environment.NewLine}";

                //SETTINGMY=tagslotIndex-1
                SendCommandWithoutResult($"SETTING{_cmdExtension}=" + (tagslotIndex - 1));

                var data = SendCommand($"DETECTION{_cmdExtension}?") as byte[];

                var result = MfKeyAttacks.Attack(data);

                txt_output.Text += result;
            }
            this.Cursor = Cursors.Default;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            // Get all selected indices
            foreach (var cb in FindControls<CheckBox>(Controls, "checkBox"))
            {
                if (cb.Checked)
                {
                    var tagslotIndex = int.Parse(cb.Name.Substring(cb.Name.Length - 1));
                    if (tagslotIndex <= 0) return;

                    //SETTINGMY=tagslotIndex-1
                    SendCommandWithoutResult($"SETTING{_cmdExtension}=" + (tagslotIndex - 1));

                    // DETECTIONMY = CLOSED
                    SendCommandWithoutResult($"DETECTION{_cmdExtension}=CLOSED");

                    // CLEARMY
                    SendCommandWithoutResult($"CLEAR{_cmdExtension}");

                    // Set every field to a default value
                    var cb_mode = FindControls<ComboBox>(Controls, $"cb_mode{tagslotIndex}").FirstOrDefault();
                    if (cb_mode != null)
                    {
                        SendCommandWithoutResult($"CONFIG{_cmdExtension}={cb_mode.Items[0]}");
                    }

                    var cb_button = FindControls<ComboBox>(Controls, $"cb_button{tagslotIndex}").FirstOrDefault();
                    if (cb_button != null)
                    {
                        SendCommandWithoutResult($"BUTTON{_cmdExtension}={cb_button.Items[0]}");
                    }

                    var cb_buttonlong = FindControls<ComboBox>(Controls, $"cb_buttonlong{tagslotIndex}").FirstOrDefault();
                    if (cb_buttonlong != null)
                    {
                        SendCommandWithoutResult($"BUTTONLONG{_cmdExtension}={cb_buttonlong.Items[0]}");
                    }

                    SendCommandWithoutResult($"UID{_cmdExtension}=?");

                    // Refresh
                    RefreshSlot(tagslotIndex - 1);
                }
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            // Get all selected indices
            foreach (var cb in FindControls<CheckBox>(Controls, "checkBox"))
            {
                if (!cb.Checked) continue;

                var tagslotIndex = int.Parse(cb.Name.Substring(cb.Name.Length - 1));
                if (tagslotIndex <= 0) continue;

                RefreshSlot(tagslotIndex - 1);
            }
        }

        private void btn_setactive_Click(object sender, EventArgs e)
        {
            foreach (var cb in FindControls<CheckBox>(Controls, "checkBox"))
            {
                if (!cb.Checked) continue;

                var tagslotIndex = int.Parse(cb.Name.Substring(cb.Name.Length - 1));
                if (tagslotIndex <= 0) continue;

                SendCommandWithoutResult($"SETTING{_cmdExtension}={tagslotIndex -1}");

                break; // Only one can be set as active
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Check if online with version cmd
            if (_comport != null)
            {
                // Check if keep alive is set
                if (chk_keepalive.Checked)
                {
                    var version = SendCommand($"VERSION{_cmdExtension}?").ToString();
                    if (!string.IsNullOrEmpty(version))
                    {
                        DeviceConnected();
                    }
                    else
                    {
                        DeviceDisconnected();
                    }
                }
            }
            else
            {
                DeviceDisconnected();

                // If disconnect button hasn't be pressed
                if (!disconnectPressed)
                {
                    // try to connect
                    OpenChameleonSerialPort();
                }
            }
        }

        private void btn_selectall_Click(object sender, EventArgs e)
        {
            SetCheckBox(true);
        }

        private void btn_selectnone_Click(object sender, EventArgs e)
        {
            SetCheckBox(false);
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkCount = GetNumberOfChecked();

            if (checkCount == 1)
            {
                // Enable all buttons
                btn_apply.Enabled = true;
                btn_refresh.Enabled = true;
                btn_clear.Enabled = true;
                btn_setactive.Enabled = true;
                btn_keycalc.Enabled = true;
                btn_upload.Enabled = true;
                btn_download.Enabled = true;
            }
            else if (checkCount > 1)
            {
                // Enable most buttons
                btn_apply.Enabled = true;
                btn_refresh.Enabled = true;
                btn_clear.Enabled = true;
                btn_setactive.Enabled = false;
                btn_keycalc.Enabled = true;
                btn_upload.Enabled = false;
                btn_download.Enabled = true;
            }
            else
            {
                // Disable all
                btn_apply.Enabled = false;
                btn_refresh.Enabled = false;
                btn_clear.Enabled = false;
                btn_setactive.Enabled = false;
                btn_keycalc.Enabled = false;
                btn_upload.Enabled = false;
                btn_download.Enabled = false;
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            // Send the RESETMY command
            SendCommandWithoutResult("RESETMY");
        }

        private void btn_rssirefresh_Click(object sender, EventArgs e)
        {
            // Send the RSSIMY command
            var rssi = SendCommand($"RSSI{_cmdExtension}?");
            txt_rssi.Text = rssi.ToString();
        }

        private void btn_browsedownloads_Click(object sender, EventArgs e)
        {
            // Open dialog
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                var selectedFolder = folderBrowserDialog1.SelectedPath;

                if (selectedFolder != null)
                {
                    txt_defaultdownload.Text = selectedFolder;
                    // Save setting
                    Properties.Settings.Default.DownloadDumpPath = selectedFolder;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void btn_setInterval_Click(object sender, EventArgs e)
        {
            var keepAliveInterval = 0;

            if (!string.IsNullOrEmpty(txt_interval.Text))
            {
                int.TryParse(txt_interval.Text, out keepAliveInterval);
            }

            if (keepAliveInterval > 0)
            {
                if (chk_keepalive.Checked)
                {
                    timer1.Stop();
                    timer1.Interval = keepAliveInterval;
                    timer1.Start();
                }

                // Save in settings
                Properties.Settings.Default.EnableKeepAlive = chk_keepalive.Checked;
                Properties.Settings.Default.KeepAliveInterval = keepAliveInterval;
                Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show("Only positive numbers are allowed for the interval of the keep-alive setting", "Interval not valid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            if (!isConnected) return;

            if (_comport != null && _comport.IsOpen)
            {
                _comport.Close();
                _comport = null;

                // Set that the disconnect button was pressed
                disconnectPressed = true;
            }
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (isConnected) return;

            // Try to connect
            OpenChameleonSerialPort();

            if (_comport == null || !_comport.IsOpen)
            {
                MessageBox.Show("Unable to connect to the Chameleon device", "Connection failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btn_open1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var fileName = openFileDialog1.FileName;

                OpenFile(fileName, hexBox1);
            }
        }

        private void btn_save1_Click(object sender, EventArgs e)
        {
            SaveFile(hexBox1);
        }


        private void btn_open2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var fileName = openFileDialog1.FileName;

                OpenFile(fileName, hexBox2);
            }
        }

        private void btn_save2_Click(object sender, EventArgs e)
        {
            SaveFile(hexBox2);
        }

        private void tabPage3_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void tabPage3_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            int fileCount = 0;

            if ((files != null) && (files.Length > 0))
            {
                // TODO: Should check if files are open instead of closing both files
                CloseFile(hexBox1);
                CloseFile(hexBox2);

                foreach (string file in files)
                {
                    fileCount++;
                    switch (fileCount)
                    {
                        case 1:
                            OpenFile(file, hexBox1);
                            break;
                        case 2:
                            OpenFile(file, hexBox2);
                            break;
                    }
                }
            }
        }

        private void btn_compare_Click(object sender, EventArgs e)
        {
            if (hexBox1.ByteProvider != null && hexBox2.ByteProvider != null)
            {
                if (hexBox1.ByteProvider.Length == hexBox2.ByteProvider.Length)
                {
                    // Clear the last highlights
                    hexBox1.ClearHighlights();
                    hexBox2.ClearHighlights();

                    for (int i = 0; i < hexBox1.ByteProvider.Length; i++)
                    {
                        if (hexBox1.ByteProvider.ReadByte(i) != hexBox2.ByteProvider.ReadByte(i))
                        {
                            Console.WriteLine("Byte " + i + " is different.");
                            hexBox1.AddHighlight(i, 1, Color.Red, Color.White);
                            hexBox2.AddHighlight(i, 1, Color.Red, Color.White);
                        }
                    }

                    hexBox1.Invalidate();
                    hexBox2.Invalidate();
                }
            }
        }

        private void byteWidthCheckBoxes_CheckedChanged(Object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                RadioButton rb = (RadioButton)sender;
                var byteWidthStr = rb.Name.Substring(rb.Name.Length - 2);
                int bytesPerLine = int.Parse(byteWidthStr) * 4;
                hexBox1.BytesPerLine = bytesPerLine;
                hexBox2.BytesPerLine = bytesPerLine;
                if (bytesPerLine == 4 * 4)
                {
                    hexBox1.Size = new System.Drawing.Size(200, 370);
                    btn_open2.Location = new System.Drawing.Point(btn_open1.Location.X + 420, btn_open1.Location.Y);
                    btn_save2.Location = new System.Drawing.Point(btn_open2.Location.X + 56, btn_open1.Location.Y);
                    hexBox2.Location = new System.Drawing.Point(hexBox1.Location.X + 420, hexBox1.Location.Y);
                    hexBox2.Size = new System.Drawing.Size(200, 370);
                }
                else if (bytesPerLine == 4 * 8)
                {
                    hexBox1.Size = new System.Drawing.Size(300, 370);
                    btn_open2.Location = new System.Drawing.Point(btn_open1.Location.X + 420, btn_open1.Location.Y);
                    btn_save2.Location = new System.Drawing.Point(btn_open2.Location.X + 56, btn_open1.Location.Y);
                    hexBox2.Location = new System.Drawing.Point(hexBox1.Location.X + 420, hexBox1.Location.Y);
                    hexBox2.Size = new System.Drawing.Size(300, 370);
                }
                else if (bytesPerLine == 4 * 16)
                {
                    hexBox1.Size = new System.Drawing.Size(480, 180);
                    btn_open2.Location = new System.Drawing.Point(hexBox1.Location.X, hexBox1.Location.Y + 180 + 10);
                    btn_save2.Location = new System.Drawing.Point(btn_save1.Location.X, btn_open2.Location.Y);
                    hexBox2.Location = new System.Drawing.Point(hexBox1.Location.X, btn_save2.Location.Y + btn_save2.Size.Height + 5);
                    hexBox2.Size = new System.Drawing.Size(480, 180);
                }
            }
        }
        #endregion

        #region Helper methods

        private void DisplayText()
        {
            var ident = string.Empty;
            if (!string.IsNullOrWhiteSpace(_deviceIdentification))
            {
                ident = $"({_deviceIdentification})";
            }
            this.Text = $"Connected {_current_comport} [{ident}] - {Environment.OSVersion.VersionString}";
        }

        private void SetCheckBox(bool value)
        {
            foreach (var cb in FindControls<CheckBox>(Controls, "checkBox"))
            {
                cb.Checked = value;
            }
        }

        private int GetNumberOfChecked()
        {
            return FindControls<CheckBox>(Controls, "checkBox").Count(cb => cb.Checked);
        }

        private void DeviceDisconnected()
        {
            if (!isConnected) return;

            isConnected = false;
            try
            {
                _comport.Close();
            }
            catch (Exception) { }
            finally
            {
                _comport = null;
            }

            this.Text = "Device disconnected";

            txt_constatus.Text = "NOT CONNECTED";
            txt_constatus.BackColor = System.Drawing.Color.Red;
            txt_constatus.ForeColor = System.Drawing.Color.White;
            txt_constatus.SelectionStart = 0;

            // Disable all tag slots and don't select any tag slot
            foreach (var cb in FindControls<CheckBox>(Controls, "checkBox"))
            {
                cb.Enabled = false;
                cb.Checked = false;
            }

            // Disable controls
            btn_selectall.Enabled = false;
            btn_selectnone.Enabled = false;

            btn_apply.Enabled = false;
            btn_refresh.Enabled = false;
            btn_clear.Enabled = false;
            btn_setactive.Enabled = false;
            btn_keycalc.Enabled = false;
            btn_upload.Enabled = false;
            btn_download.Enabled = false;

            btn_reset.Enabled = false;
            btn_bootmode.Enabled = false;
            btn_rssirefresh.Enabled = false;
            btn_setInterval.Enabled = false;
            btn_disconnect.Enabled = false;

            btn_connect.Enabled = true;
        }

        private void DeviceConnected()
        {
            if (isConnected) return;

            this.Cursor = Cursors.WaitCursor;

            isConnected = true;

            DisplayText();

            // Enable all tag slots but don't select any tag slot
            foreach (var cb in FindControls<CheckBox>(Controls, "checkBox"))
            {
                cb.Enabled = true;
                cb.Checked = false;
            }

            txt_constatus.Text = "CONNECTED!";
            txt_constatus.BackColor = System.Drawing.Color.Green;
            txt_constatus.ForeColor = System.Drawing.Color.White;
            txt_constatus.SelectionLength = 0;

            if (_modesArray == null || _modesArray.Length == 0)
            {
                GetSupportedModes();
            }

            RefreshAllSlots();

            // Enable controls
            btn_selectall.Enabled = true;
            btn_selectnone.Enabled = true;

            btn_apply.Enabled = false;
            btn_refresh.Enabled = false;
            btn_clear.Enabled = false;
            btn_setactive.Enabled = false;
            btn_keycalc.Enabled = false;
            btn_upload.Enabled = false;
            btn_download.Enabled = false;

            btn_reset.Enabled = true;
            btn_bootmode.Enabled = true;
            btn_rssirefresh.Enabled = true;
            btn_setInterval.Enabled = true;
            btn_disconnect.Enabled = true;

            btn_connect.Enabled = false;

            this.Cursor = Cursors.Default;
        }

        /*
        private void ConnectToChameleon()
        {
            var portNames = SerialPort.GetPortNames();

            foreach (string port in portNames)
            {
                _comport = new SerialPort(port, 115200);

                _comport.ReadTimeout = 4000;
                _comport.WriteTimeout = 6000;

                try
                {
                    _comport.Open();

                    if (_comport.IsOpen)
                    {
                        // try without the "MY" extension first
                        string version = SendCommand("VERSION?") as string;
                        if (!string.IsNullOrEmpty(version) && version.Contains("Chameleon"))
                        {
                            _cmdExtension = "";
                            break;
                        }

                        version = SendCommand("VERSIONMY?") as string;
                        if (!string.IsNullOrEmpty(version) && version.Contains("Chameleon"))
                        {
                            _cmdExtension = "MY";
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Problem with: " + port);
                    _comport = null;
                }
            }

            if (_comport != null && _comport.IsOpen)
            {
                this.Text = "Device connected";
            }
        }
        */

        private void OpenChameleonSerialPort()
        {
            this.Cursor = Cursors.WaitCursor;
            txt_output.Text = string.Empty;

            //var searcher = new ManagementObjectSearcher("select DeviceID from Win32_SerialPort where Description = \"ChameleonMini Virtual Serial Port\"");
            var searcher = new ManagementObjectSearcher("select Name,DeviceID from Win32_SerialPort ");
            foreach (var obj in searcher.Get())
            {
                var comPortStr = obj["DeviceID"].ToString();

                _comport = new SerialPort(comPortStr, 115200)
                {
                    ReadTimeout = 4000,
                    WriteTimeout = 6000
                };

                try
                {
                    _comport.Open();
                    var name = obj["Name"].ToString();
                    txt_output.Text += $"Connecting to {name} at {comPortStr}{Environment.NewLine}";
                }
                catch (Exception)
                {
                    txt_output.Text = $"Failed {comPortStr}{Environment.NewLine}";
                }

                if (_comport.IsOpen)
                {
                    // try without the "MY" extension first
                    FirmwareVersion = SendCommand("VERSION?") as string;
                    if (!string.IsNullOrEmpty(_firmwareVersion) && _firmwareVersion.Contains("Chameleon"))
                    {
                        _cmdExtension = string.Empty;
                        _deviceIdentification = "Firmware Official";
                        txt_output.Text = $"Success{Environment.NewLine}Found Chameleon Mini device {comPortStr} with {_deviceIdentification} installed{Environment.NewLine}";
                        txt_output.Text += $"---------------------------------------------------------------{Environment.NewLine}";
                        _current_comport = comPortStr;
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    FirmwareVersion = SendCommand("VERSIONMY?") as string;
                    if (!string.IsNullOrEmpty(_firmwareVersion) && _firmwareVersion.Contains("Chameleon"))
                    {
                        _cmdExtension = "MY";
                        _deviceIdentification = "Firmware RevE rebooted";
                        txt_output.Text = $"Success{Environment.NewLine}Found Chameleon Mini device {comPortStr} with {_deviceIdentification} installed{Environment.NewLine}";
                        txt_output.Text += $"---------------------------------------------------------------{Environment.NewLine}";
                        _current_comport = comPortStr;
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    // wrong comport.
                    _comport.Close();
                    txt_output.Text += $"Didn't find a Chameleon on {comPortStr}{Environment.NewLine}";
                }
            }
            _current_comport = string.Empty;
            this.Cursor = Cursors.Default;
            txt_output.Text += $"Didn't find any Chameleon Mini device connected{Environment.NewLine}";
            txt_output.Text += $"---------------------------------------------------------------{Environment.NewLine}";
        }

        private void SendCommandWithoutResult(string cmdText)
        {
            if (string.IsNullOrWhiteSpace(cmdText)) return;
            if (_comport == null || !_comport.IsOpen) return;

            try
            {
                // send command
                var tx_data = Encoding.ASCII.GetBytes(cmdText);
                _comport.Write(tx_data, 0, tx_data.Length);
                _comport.Write("\r\n");
            }
            catch(Exception)
            { }
        }

        private object SendCommand(string cmdText)
        {
            if (string.IsNullOrWhiteSpace(cmdText)) return string.Empty;
            if (_comport == null || !_comport.IsOpen) return string.Empty;

            try
            {
                // send command
                var tx_data = Encoding.ASCII.GetBytes(cmdText);
                _comport.Write(tx_data, 0, tx_data.Length);
                _comport.Write("\r\n");

                // wait to make sure data is transmitted
                Thread.Sleep(100);

                var rx_data = new byte[275];

                // read the result
                var read_count = _comport.Read(rx_data, 0, rx_data.Length);
                if (read_count <= 0) return string.Empty;

                if (cmdText.Contains("DETECTIONMY?"))
                {
                    var foo = new byte[read_count];
                    Array.Copy(rx_data, 8, foo, 0, read_count - 7);
                    return foo;
                }
                else
                {
                    var s = new string(Encoding.ASCII.GetChars(rx_data, 0, read_count));
                    return s.Replace("101:OK WITH TEXT", "").Replace("100:OK", "").Replace("\r\n", "");
                }
            }
            catch(Exception)
            {
                return string.Empty;
            }
        }

        private bool IsUidValid(string uid, string selectedMode)
        {
            if (!Regex.IsMatch(uid, @"\A\b[0-9a-fA-F]+\b\Z")) return false;

            // TODO: We could also find out the UID size with the UIDSIZEMY cmd
            // and there exists 4,7,10 uid lengths.

            // if mode is classic then UID must be 4 bytes (8 hex digits) long
            if (selectedMode.StartsWith("MF_CLASSIC"))
            {
                if (uid.Length == 8)
                {
                    return true;
                }
            }

            // if mode is ul then UID must be 7 bytes (14 hex digits) long
            if (selectedMode.StartsWith("MF_ULTRALIGHT"))
            {
                if (uid.Length == 14)
                {
                    return true;
                }
            }

            return false;
        }

        private void RefreshAllSlots()
        {
            progressBar1.Value = 0;

            for (int i = 0; i < 8; i++)
            {
                RefreshSlot(i);
                progressBar1.Value = (int)((i + 1) * 12.5);
            }

            progressBar1.Value = 0;
        }

        private void RefreshSlot(int slotIndex)
        {
            //SETTINGMY=i
            SendCommandWithoutResult($"SETTING{_cmdExtension}={slotIndex}");

            //SETTINGMY? -> SHOULD BE "NO."+i
            var selectedSlot = SendCommand($"SETTING{_cmdExtension}?").ToString();
            if ((!selectedSlot.Contains(slotIndex.ToString()))) return;

            //CONFIGMY? -> RETURNS THE CONFIGURATION MODE
            var slotMode = SendCommand($"CONFIG{_cmdExtension}?").ToString();
            if (IsModeValid(slotMode))
            {
                // set the combobox value of the i+1 cb_mode
                var cbMode = FindControls<ComboBox>(Controls, "cb_mode" + (slotIndex + 1) );
                foreach (var box in cbMode)
                {
                    if (slotMode.Equals("MF_CLASSIC_4K") && box.Name != "cb_mode1")
                        continue;
                    box.SelectedItem = slotMode;
                }
            }

            //UIDMY? -> RETURNS THE UID
            var slotUid = SendCommand($"UID{_cmdExtension}?").ToString();
            if ( !string.IsNullOrWhiteSpace(slotUid) )
            {
                // set the textbox value of the i+1 txt_uid
                var tbs = FindControls<TextBox>(Controls, "txt_uid" + (slotIndex + 1));
                foreach( var box in tbs)
                {
                    box.Text = slotUid;
                }
            }

            //BUTTONMY? -> RETURNS THE MODE OF THE BUTTON
            var slotButtonMode = SendCommand($"BUTTON{_cmdExtension}?").ToString();
            if (IsButtonModeValid(slotButtonMode))
            {
                // set the combobox value of the i+1 cb_button
                var cbButton = FindControls<ComboBox>(Controls, "cb_button" + (slotIndex + 1));
                foreach (var box in cbButton)
                {
                    box.SelectedItem = slotButtonMode;
                }
            }

            //BUTTONLONGMY? -> RETURNS THE MODE OF THE BUTTON LONG
            var slotButtonLongMode = SendCommand($"BUTTONLONG{_cmdExtension}?").ToString();
            if (IsButtonModeValid(slotButtonLongMode))
            {
                // set the combobox value of the i+1 cb_buttonlong
                var cbButtonLong = FindControls<ComboBox>(Controls, "cb_buttonlong" + (slotIndex + 1));
                foreach (var box in cbButtonLong)
                {
                    box.SelectedItem = slotButtonLongMode;
                }
            }

            //MEMSIZEMY? -> RETURNS THE SIZE OF THE OCCUPIED MEMORY
            var slotMemSize = SendCommand($"MEMSIZE{_cmdExtension}?").ToString();
            if (!string.IsNullOrEmpty(slotMemSize))
            {
                // set the textbox value of the i+1 txt_size
                var txtMemSize = FindControls<TextBox>(Controls, "txt_size" + (slotIndex + 1));
                foreach (var box in txtMemSize)
                {
                    box.Text = slotMemSize;
                }
            }
        }

        private bool IsButtonModeValid(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return false;
            return _buttonModesArray.Contains(s);
        }

        private bool IsModeValid(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return false;
            return _modesArray.Contains(s);
        }

        private static List<T> FindControls<T>(ICollection ctrls, string searchname) where T : Control
        {
            var list = new List<T>();

            // make sure we have controls to search for.
            if (ctrls == null || ctrls.Count == 0) return list;
            if (string.IsNullOrWhiteSpace(searchname)) return list;


            foreach (Control cb in ctrls)
            {                
                if (cb.HasChildren)
                {
                    list.AddRange(FindControls<T>(cb.Controls, searchname));
                }

                if (cb.Name.StartsWith(searchname))
                    list.Add(cb as T);
            }

            return list;
        }

        private void GetSupportedModes()
        {
            var modesStr = SendCommand($"CONFIG{_cmdExtension}").ToString();

            if (!string.IsNullOrEmpty(modesStr))
            {
                // split by comma
                _modesArray = modesStr.Split(',');
                if (_modesArray.Any())
                {
                    // populate all dropdowns
                    foreach (var cb in FindControls<ComboBox>(Controls,"cb_mode"))
                    {
                        cb.Items.Clear();
                        cb.Items.AddRange(_modesArray);

                        // We can set the MF_CLASSIC_4K mode only on the first tag slot
                        if (cb.Name != "cb_mode1")
                        {
                            cb.Items.Remove("MF_CLASSIC_4K");
                        }
                    }
                }
            }

            // Get button modes
            var buttonModesStr = SendCommand($"BUTTON{_cmdExtension}").ToString();
            if (string.IsNullOrEmpty(buttonModesStr)) return;

            // split by comma
            _buttonModesArray = buttonModesStr.Split(',');
            if (!_buttonModesArray.Any()) return;

            // populate all dropdowns
            foreach (var cb in FindControls<ComboBox>(Controls, "cb_button"))
            {
                cb.Items.Clear();
                cb.Items.AddRange(_buttonModesArray);
            }

            // Get button long modes
            var buttonLongModesStr = SendCommand($"BUTTONLONG{_cmdExtension}").ToString();
            if (string.IsNullOrEmpty(buttonLongModesStr)) return;

            // split by comma
            _buttonLongModesArray = buttonLongModesStr.Split(',');
            if (!_buttonLongModesArray.Any()) return;

            // populate all dropdowns
            foreach (var cb in FindControls<ComboBox>(Controls, "cb_buttonlong"))
            {
                cb.Items.Clear();
                cb.Items.AddRange(_buttonLongModesArray);
            }
        }

        private static byte[] ReadFileIntoByteArray(string filename)
        {
            if (File.Exists(filename))
                return File.ReadAllBytes(filename);
            return null;
        }

        internal void LoadDump(string filename)
        {
            // Load the file into a memory block
            var bytes = ReadFileIntoByteArray(filename);

            // Set up an XMODEM object
            var xmodem = new XMODEM(_comport, XMODEM.Variants.XModemChecksum);

            // First send the upload command
            SendCommandWithoutResult($"UPLOAD{_cmdExtension}");
            _comport.ReadLine(); // For the "110:WAITING FOR XMODEM" text

            int numBytesSuccessfullySent = xmodem.Send(bytes);

            if (numBytesSuccessfullySent == bytes.Length &&
                xmodem.TerminationReason == XMODEM.TerminationReasonEnum.EndOfFile)
            {
                var msg = $"[+] File upload ok{Environment.NewLine}";
                Console.WriteLine(msg);
                txt_output.Text += msg;
            }
            else
            {
                var msg = $"[!] Failed to upload file{Environment.NewLine}";
                MessageBox.Show(msg);
                txt_output.Text += msg;
            }
        }

        internal void SaveDump(string filename)
        {
            // Set up an XMODEM object
            var xmodem = new XMODEM(_comport, XMODEM.Variants.XModemChecksum);

            // First get the current memory size of the slot
            var memsizeStr = SendCommand($"MEMSIZE{_cmdExtension}?");

            int memsize = 4096; // Default value

            if (!string.IsNullOrEmpty((string)memsizeStr))
            {
                int.TryParse((string)memsizeStr, out memsize);
            }

            // Also check if the tag is UL to save the counters too
            var configStr = SendCommand($"CONFIG{_cmdExtension}?") as string;
            if ((configStr != null) && (configStr.Contains("ULTRALIGHT")))
            {
                if (memsize < 4069)
                {
                    memsize += 3 * 4; // 3 more pages
                }
            }

            // Then send the download command
            SendCommandWithoutResult($"DOWNLOAD{_cmdExtension}");

            // For the "110:WAITING FOR XMODEM" text
            _comport.ReadLine();

            var ms = new MemoryStream();
            var reason = xmodem.Receive(ms);

            if (reason == XMODEM.TerminationReasonEnum.EndOfFile)
            {
                var msg = $"[+] File download from device ok{Environment.NewLine}";
                Console.WriteLine(msg);
                txt_output.Text += msg;

                // Transfer successful, so convert MemoryStream to byte array
                var bytes = ms.ToArray();

                // Strip away the SUB (byte value 26) padding bytes
                bytes = xmodem.TrimPaddingBytesFromEnd(bytes);

                byte[] neededBytes = bytes;

                if (bytes.Length > memsize)
                {
                    // Create a new array same size as memsize
                    neededBytes = new byte[memsize];

                    Array.Copy(bytes, neededBytes, neededBytes.Length);
                }

                // Write the actual file
                File.WriteAllBytes(filename, neededBytes);

                msg = $"[+] File saved to {filename}{Environment.NewLine}";
                Console.WriteLine(msg);
                txt_output.Text += msg;
            }
            else
            {
                // Something went wrong during the transfer
                var msg = $"[!] Failed to save dump{Environment.NewLine}";
                MessageBox.Show(msg);
                txt_output.Text += msg;
            }
        }

        private void InitTimer()
        {
            int tickInterval = 0;
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);

            int.TryParse(txt_interval.Text, out tickInterval);

            if (tickInterval > 0)
            {
                timer1.Interval = tickInterval;
            }
            else
            {
                timer1.Interval = 2000; // default value
            }

            timer1.Start();
        }

        void SaveFile(HexBox hexBox)
        {
            if (hexBox.ByteProvider == null)
                return;

            try
            {
                DynamicFileByteProvider dynamicFileByteProvider = hexBox.ByteProvider as DynamicFileByteProvider;
                dynamicFileByteProvider.ApplyChanges();
            }
            catch (Exception)
            {
                var msg = $"[!] Failed to save file{Environment.NewLine}";
                MessageBox.Show(msg);
                txt_output.Text += msg;
            }
        }

        public void OpenFile(string fileName, HexBox hexBox)
        {
            if (!File.Exists(fileName))
            {
                var msg = $"[!] Failed to open - File does not exist{Environment.NewLine}";
                MessageBox.Show(msg);
                txt_output.Text += msg;
                return;
            }

            if (CloseFile(hexBox) == DialogResult.Cancel)
                return;

            DynamicFileByteProvider dynamicFileByteProvider;
            try
            {
                // try to open in write mode
                dynamicFileByteProvider = new DynamicFileByteProvider(fileName);
                hexBox.ByteProvider = dynamicFileByteProvider;
            }
            catch (IOException) // write mode failed
            {
                // file cannot be opened
                var msg = $"[!] Failed to open file{Environment.NewLine}";
                MessageBox.Show(msg);
                txt_output.Text += msg;
            }
        }

        DialogResult CloseFile(HexBox hexBox)
        {
            if (hexBox.ByteProvider == null)
                return DialogResult.OK;

            if (hexBox.ByteProvider != null && hexBox.ByteProvider.HasChanges())
            {
                DialogResult res = MessageBox.Show("There are unsaved changes. Do you want to save them?",
                    "File changed",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if (res == DialogResult.Yes)
                {
                    SaveFile(hexBox);
                    CleanUp(hexBox);
                }
                else if (res == DialogResult.No)
                {
                    CleanUp(hexBox);
                }
                else if (res == DialogResult.Cancel)
                {
                    return res;
                }

                return res;
            }
            else
            {
                CleanUp(hexBox);
                return DialogResult.OK;
            }
        }

        void CleanUp(HexBox hexBox)
        {
            if (hexBox.ByteProvider != null)
            {
                IDisposable byteProvider = hexBox.ByteProvider as IDisposable;
                if (byteProvider != null)
                    byteProvider.Dispose();
                hexBox.ByteProvider = null;
            }
        }
        #endregion

    }
}
