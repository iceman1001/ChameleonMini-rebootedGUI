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
using System.Collections.Generic;
using System.Diagnostics;
using Be.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using ChameleonMiniGUI.Json;

namespace ChameleonMiniGUI
{
    public partial class frm_main : Form
    {
        private SerialPort _comport;
        private string[] _modesArray;
        private string[] _lbuttonModesArray;
        private string[] _lbuttonLongModesArray;
        private string[] _rbuttonModesArray;
        private string[] _rbuttonLongModesArray;
        private string[] _LEDGreenModesArray;
        private string[] _LEDRedModesArray;
        private string _cmdExtension = "";
        private System.Windows.Forms.Timer timer1;
        private bool isConnected;
        private bool disconnectPressed;
    
        private string _current_comport = string.Empty;
        private string _deviceIdentification;
        private string _firmwareVersion;

        private int _tagslotIndexOffset = 0;

        private const int REVGDefaultComboWidth = 80;
        private const int REVEDefaultComboWidth = 175;

        private bool lockFlag = false;
        private DeviceType _CurrentDevType = DeviceType.RevG;

        private enum DeviceType {RevE, RevG};

        public string FirmwareVersion
        {
            get { return _firmwareVersion; }
            set
            {
                _firmwareVersion = value;
                tb_firmware.Text = _firmwareVersion;
            }
        }

        private List<string> AvailableCommands { get; set; }

        public frm_main()
        {
            InitializeComponent();

            var software_version = Properties.Settings.Default.version;
            // 
            this.Text = $"Chameleon Mini GUI - {software_version} - iceman edition 冰人";

            AvailableCommands = new List<string>();
        }

        #region Event Handlers
        private void frm_main_Load(object sender, EventArgs e)
        {
            txt_output.SelectionStart = 0;

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

                GetAvailableCommands();
                InitHelp();
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
            SplashScreen.CloseForm();
        }

        private void InitHelp()
        {
            if ( !AvailableCommands.Any())
            {
                tbSerialHelp.Text = "N/A";
            }
            else
            {
                var txt = string.Empty;
                var nl = Environment.NewLine;
                txt = AvailableCommands.Aggregate(txt, (current, c) => current + $"* {c}{nl}");
                tbSerialHelp.Text = txt.Replace("*", "\u2022");
            }
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

            var ml = new MultiLanguage();
            var languages = ml.GetLanguages();
            if (languages.Any())
            {
                lockFlag = true;
                bsLanguages.DataSource = languages;
                cb_languages.DisplayMember = "Key";
                cb_languages.ValueMember = "Value";
                lockFlag = false;
            }

            // load prefered language
            var lang = Properties.Settings.Default.Language;
            if (!string.IsNullOrWhiteSpace(lang))
            {
                ml.LoadLanguage(this.Controls, lang);

                // select lang in combobox
                lockFlag = true;
                foreach (KeyValuePair<string, string> i in cb_languages.Items)
                {
                    if (i.Value == lang)
                    {
                        cb_languages.SelectedItem = i;
                        break;
                    }
                }
                lockFlag = false;
            }

            var t = new Templating();
            var templates = t.GetTemplates();
            if (templates.Any())
            {
                lockFlag = true;
                bsTemplates.DataSource = templates;
                cb_templateA.DisplayMember = "Key";
                cb_templateA.ValueMember = "Value";
                lockFlag = false;
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

        private void cb_languages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lockFlag)
                return;

            // load language
            if (cb_languages?.SelectedItem == null) return;

            var o = (KeyValuePair<string, string>)cb_languages.SelectedItem;

            var ml = new MultiLanguage();
            ml.LoadLanguage(this.Controls, o.Value);

            // Save language
            if (!string.IsNullOrEmpty(o.Value))
            {
                Properties.Settings.Default.Language = o.Value;
                Properties.Settings.Default.Save();
            }
        }

        private void cb_templateA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lockFlag)
                return;

            var c = (ComboBox)sender;

            if (c?.SelectedItem == null) return;
            if (c.SelectedIndex == 0)
            {
                ucLegend1.Items = null;
                PerformComparison();
                return;
            }

            var o = (KeyValuePair<string, string>)c.SelectedItem;

            var items = new List<IlegendItem>();
            var t = new Templating();
            t.LoadTemplate(hexBox1, o.Value, items);
            t.LoadTemplate(hexBox2, o.Value, items);

            ucLegend1.Items = items;

        }

        private void btn_apply_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            // Get all selected indices
            foreach (var cb in FindControls<CheckBox>(Controls, "checkBox"))
            {
                if (!cb.Checked) continue;

                var tagslotIndex = int.Parse(cb.Name.Substring(cb.Name.Length - 1));
                if (tagslotIndex <= 0) continue;

                //SETTINGMY=tagslotIndex-1
                SendCommandWithoutResult($"SETTING{_cmdExtension}=" + (tagslotIndex - _tagslotIndexOffset));
                
                //SETTINGMY? -> SHOULD BE "NO."+tagslotIndex
                var selectedSlot = SendCommand($"SETTING{_cmdExtension}?").ToString();
                if (!selectedSlot.Contains((tagslotIndex - _tagslotIndexOffset).ToString())) return;


                var selectedMode = string.Empty;

                // Set the mode of the selected slot
                var cb_mode = FindControls<ComboBox>(Controls, $"cb_mode{tagslotIndex}").FirstOrDefault();
                if (cb_mode != null)
                {
                    //CONFIGMY=cb_mode.SelectedItem
                    SendCommandWithoutResult($"CONFIG{_cmdExtension}={cb_mode.SelectedItem}");
                    selectedMode = cb_mode.SelectedItem.ToString();
                }

                if (_CurrentDevType == DeviceType.RevG)
                {
                    // Set the button mode of the selected slot
                    var cb_Lbutton = FindControls<ComboBox>(Controls, $"cb_Lbutton{tagslotIndex}").FirstOrDefault();
                    if (cb_Lbutton != null)
                    {
                        //BUTTONMY=cb_buttonMode.SelectedItem
                        SendCommandWithoutResult($"LBUTTON{_cmdExtension}={cb_Lbutton.SelectedItem}");
                    }

                    // Set the button mode of the selected slot
                    var cb_Rbutton = FindControls<ComboBox>(Controls, $"cb_Rbutton{tagslotIndex}").FirstOrDefault();
                    if (cb_Rbutton != null)
                    {
                        //BUTTONMY=cb_buttonMode.SelectedItem
                        SendCommandWithoutResult($"RBUTTON{_cmdExtension}={cb_Rbutton.SelectedItem}");
                    }

                    // Set the button long mode of the selected slot
                    var cb_Lbuttonlong = FindControls<ComboBox>(Controls, $"cb_Lbuttonlong{tagslotIndex}").FirstOrDefault();
                    if (cb_Lbuttonlong != null)
                    {
                        //BUTTON_LONGMY=cb_buttonMode.SelectedItem
                        SendCommandWithoutResult($"LBUTTON_LONG{_cmdExtension}={cb_Lbuttonlong.SelectedItem}");
                    }

                    // Set the button long mode of the selected slot
                    var cb_Rbuttonlong = FindControls<ComboBox>(Controls, $"cb_Rbuttonlong{tagslotIndex}").FirstOrDefault();
                    if (cb_Rbuttonlong != null)
                    {
                        //BUTTON_LONGMY=cb_buttonMode.SelectedItem
                        SendCommandWithoutResult($"RBUTTON_LONG{_cmdExtension}={cb_Rbuttonlong.SelectedItem}");
                    }
                }
                else if (_CurrentDevType == DeviceType.RevE)
                {
                    // Set the button mode of the selected slot
                    var cb_Lbutton = FindControls<ComboBox>(Controls, $"cb_Lbutton{tagslotIndex}").FirstOrDefault();
                    if (cb_Lbutton != null)
                    {
                        //BUTTONMY=cb_buttonMode.SelectedItem
                        SendCommandWithoutResult($"BUTTON{_cmdExtension}={cb_Lbutton.SelectedItem}");
                    }

                    // Set the button long mode of the selected slot
                    var cb_Lbuttonlong = FindControls<ComboBox>(Controls, $"cb_Lbuttonlong{tagslotIndex}").FirstOrDefault();
                    if (cb_Lbuttonlong != null)
                    {
                        //BUTTON_LONGMY=cb_buttonMode.SelectedItem
                        SendCommandWithoutResult($"BUTTON_LONG{_cmdExtension}={cb_Lbuttonlong.SelectedItem}");
                    }
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

                // Set LEDs
                if (_CurrentDevType == DeviceType.RevG)
                {
                    // Set the button mode of the selected slot
                    var cb_LEDgreen = FindControls<ComboBox>(Controls, $"cb_ledgreen{tagslotIndex}").FirstOrDefault();
                    if (cb_LEDgreen != null)
                    {
                        //BUTTONMY=cb_buttonMode.SelectedItem
                        SendCommandWithoutResult($"LEDGREEN{_cmdExtension}={cb_LEDgreen.SelectedItem}");
                    }

                    // Set the button mode of the selected slot
                    var cb_LEDred = FindControls<ComboBox>(Controls, $"cb_ledred{tagslotIndex}").FirstOrDefault();
                    if (cb_LEDred != null)
                    {
                        //BUTTONMY=cb_buttonMode.SelectedItem
                        SendCommandWithoutResult($"LEDRED{_cmdExtension}={cb_LEDred.SelectedItem}");
                    }

                }

                    RefreshSlot(tagslotIndex);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btn_bootmode_Click(object sender, EventArgs e)
        {
            SendCommandWithoutResult($"UPGRADE{_cmdExtension}");

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

                var fullpath = Path.Combine(Application.StartupPath, bootloaderPath);

                var flasher = Path.Combine(fullpath, bootloaderFileName);
                var firmware = Path.Combine(fullpath, flashFileName);
                var eeprom = Path.Combine(fullpath, eepromFileName);

                if (File.Exists(flasher) && File.Exists(firmware) && File.Exists(eeprom))
                {
                    var ps = new ProcessStartInfo
                    {
                        UseShellExecute = true,
                        WorkingDirectory = fullpath,
                        FileName = flasher
                    };
                    Start(ps);
                    failed = false;
                }
                else
                {
                    MessageBox.Show("Unable to find all the required files to exit the boot mode", "Exit Boot Mode failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                //
                Console.WriteLine(ex.Message);
            }

            if (failed)
            {
                MessageBox.Show("Unable to exit the bootloader mode", "Exit Boot Mode failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            var ActiveselectedSlot = SendCommand($"SETTING{_cmdExtension}?").ToString();

            Cursor.Current = Cursors.WaitCursor;
            foreach (var cb in FindControls<CheckBox>(Controls, "checkBox"))
            {
                if (!cb.Checked) continue;

                var tagslotIndex = int.Parse(cb.Name.Substring(cb.Name.Length - 1));
                if (tagslotIndex <= 0) continue;

                // select the corresponding slot
                SendCommandWithoutResult($"SETTING{_cmdExtension}={tagslotIndex - _tagslotIndexOffset}");

                // Open dialog
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    var dumpFilename = openFileDialog1.FileName;

                    // Load the dump
                    UploadDump(dumpFilename);

                    // Refresh slot
                    RefreshSlot(tagslotIndex);
                }

                break; // We can only upload a single dump at a time
            }

            SendCommandWithoutResult($"SETTING{_cmdExtension}={ActiveselectedSlot}".ToString());
            HighlightActiveSlot();
            Cursor.Current = Cursors.Default;
        }

        private void btn_download_Click(object sender, EventArgs e)
        {
            var downloadPath = Application.StartupPath;
            Cursor.Current = Cursors.WaitCursor;
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
                SendCommandWithoutResult($"SETTING{_cmdExtension}={tagslotIndex - _tagslotIndexOffset}");

                if (btn_upload.Enabled)
                {
                    // Only one tag slot is selected, show the save dialog

                    saveFileDialog1.InitialDirectory = downloadPath;

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        var dumpFilename = saveFileDialog1.FileName;

                        // Add extension if missing
                        switch (saveFileDialog1.FilterIndex)
                        {
                            case 1:
                                dumpFilename = !dumpFilename.ToLower().Contains(".bin") ? dumpFilename + ".bin" : dumpFilename;
                                break;
                            case 2:
                                dumpFilename = !dumpFilename.ToLower().EndsWith(".json") ? dumpFilename + ".json" : dumpFilename;
                                break;
                            default:
                                break;
                        }

                        // Save the dump
                        DownloadAndSaveDump(dumpFilename);
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
                        DownloadAndSaveDump(varFullDownloadPath);
                    }
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void btn_mfkey_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            var ActiveselectedSlot = SendCommand($"SETTING{_cmdExtension}?").ToString();

            // Get all selected indices
            var results = FindControls<CheckBox>(Controls, "checkBox")
                .Where(cb => cb.Checked)
                .Select(cb =>
                {
                    var tagslotIndex = int.Parse(cb.Name.Substring(cb.Name.Length - 1));

                    //SETTINGMY=tagslotIndex-1
                    SendCommandWithoutResult($"SETTING{_cmdExtension}={tagslotIndex - _tagslotIndexOffset}");

                    var data = SendCommand($"DETECTION{_cmdExtension}?") as byte[];
                    return new KeyValuePair<int, byte[]>(tagslotIndex, data);
                })
                .OrderBy(pair => pair.Key)
                .AsParallel()
                .AsOrdered()
                .Select(pair =>
                {
                    var result = MfKeyAttacks.Attack(pair.Value);
                    if (string.IsNullOrWhiteSpace(result))
                        result = $"mfkey32 attack failed, no keys found{Environment.NewLine}";

                    result = $"[Tag slot {pair.Key}]{Environment.NewLine}" + result;
                    return result;
                });
            txt_output.AppendText(string.Join(string.Empty, results));

            SendCommandWithoutResult($"SETTING{_cmdExtension}={ActiveselectedSlot}".ToString());
            HighlightActiveSlot();

            this.Cursor = Cursors.Default;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            // Get all selected indices
            foreach (var cb in FindControls<CheckBox>(Controls, "checkBox"))
            {
                if (cb.Checked)
                {
                    var tagslotIndex = int.Parse(cb.Name.Substring(cb.Name.Length - 1));
                    if (tagslotIndex <= 0) return;

                    //SETTINGMY=tagslotIndex-1
                    SendCommandWithoutResult($"SETTING{_cmdExtension}=" + (tagslotIndex - _tagslotIndexOffset));

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
                        SendCommandWithoutResult($"BUTTON_LONG{_cmdExtension}={cb_buttonlong.Items[0]}");
                    }

                    SendCommandWithoutResult($"UID{_cmdExtension}=?");

                    // Refresh
                    RefreshSlot(tagslotIndex);
                }
            }

            HighlightActiveSlot();

            Cursor.Current = Cursors.Default;
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            // Get all selected indices
            foreach (var cb in FindControls<CheckBox>(Controls, "checkBox"))
            {
                if (!cb.Checked) continue;

                var tagslotIndex = int.Parse(cb.Name.Substring(cb.Name.Length - 1));
                if (tagslotIndex <= 0) continue;

                RefreshSlot(tagslotIndex);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btn_setactive_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            foreach (var cb in FindControls<CheckBox>(Controls, "checkBox"))
            {
                if (!cb.Checked) continue;

                var tagslotIndex = int.Parse(cb.Name.Substring(cb.Name.Length - 1));
                if (tagslotIndex <= 0) continue;

                SendCommandWithoutResult($"SETTING{_cmdExtension}={tagslotIndex - _tagslotIndexOffset}");

                break; // Only one can be set as active
            }

            HighlightActiveSlot();

            Cursor.Current = Cursors.Default;
        }

        private void HighlightActiveSlot()
        {
            // Determine which slot is active finally
            var ActSetting = SendCommand($"SETTING{ _cmdExtension}?");

            int Slot = Convert.ToInt32(ActSetting.ToString());

            foreach (var gb in FindControls<GroupBoxEnhanced>(Controls, "gb_tagslot"))
            {
                var tagslotIndex = int.Parse(gb.Name.Substring(gb.Name.Length - 1));
                if (tagslotIndex <= 0) continue;

                gb.BorderColor = System.Drawing.SystemColors.ControlLight;
                gb.BorderColorLight = System.Drawing.SystemColors.ControlLightLight;
                gb.BorderWidth = 1;
            }


            var gb_active = FindControls<GroupBoxEnhanced>(Controls, $"gb_tagslot{Slot}").FirstOrDefault();
            gb_active.BorderColor = System.Drawing.Color.Green;
            gb_active.BorderColorLight = System.Drawing.Color.LightGreen;
            gb_active.BorderWidth = 2;
            GroupBoxEnhanced.RedrawGroupBoxDisplay(tpOperation);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Check if online with version cmd
            if (_comport != null)
            {
                // Check if keep alive is set
                if (!chk_keepalive.Checked) return;

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
            else
            {
                DeviceDisconnected();

                // If disconnect button hasn't be pressed
                if (!disconnectPressed)
                {
                    // try to connect
                    OpenChameleonSerialPort();                    
                    if (_comport == null || !_comport.IsOpen)
                        return;

                    DeviceConnected();
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
                return;
            }

            GetAvailableCommands();
            InitHelp();
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

            if ((files != null) && (files.Length > 0))
            {
                if (files.Length == 1)
                {
                    // Check if dropped to a hexbox
                    var clientPoint = tpDump.PointToClient(new Point(e.X, e.Y));
                    var dropControl = FindControlAtPoint(tpDump, clientPoint);

                    var hb = (dropControl is HexBox) ? (HexBox)dropControl : hexBox1;

                    OpenFile(files[0], hb);
                }
                else
                {
                    OpenFile(files[0], hexBox1);
                    OpenFile(files[1], hexBox2);
                }
            }
            hexBox1.Focus();
        }

        private void byteWidthCheckBoxes_CheckedChanged(Object sender, EventArgs e)
        {
            var rb = sender as RadioButton;
            if (rb == null) return;

            if (!rb.Checked) return;

            var byteWidthStr = rb.Name.Substring(rb.Name.Length - 2);
            ApplyByteWidthChange(byteWidthStr);
        }

        private void hexBox_ByteProviderWriteFinished(object sender, EventArgs e)
        {
            // TODO: Add the index of the written byte to the event and compare only that
            PerformComparison();
        }

        private void hexBox1_VScrollBarChanged(object sender, VScrollEventArgs e)
        {
            if (chkSyncScroll.Checked)
            {
                if (e.Pos != hexBox2.CurrentLine)
                {
                    hexBox2.PerformScrollToLine(e.Pos);
                }
            }
        }

        private void hexBox2_VScrollBarChanged(object sender, VScrollEventArgs e)
        {
            if (chkSyncScroll.Checked)
            {
                if (e.Pos != hexBox1.CurrentLine)
                {
                    hexBox1.PerformScrollToLine(e.Pos);
                }
            }
        }

        private void tabPage3_MouseEnter(object sender, EventArgs e)
        {
            if (!hexBox1.Focused && !hexBox2.Focused)
            {
                hexBox1.Focus();
            }
        }

        private void menuScroll_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "toolStripMenuItem1")
            {
                chkSyncScroll.Checked = !chkSyncScroll.Checked;
            }
            else if (e.ClickedItem.Name == "toolStripMenuItem2")
            {
                CloseFile(hexBox1);
                CloseFile(hexBox2);
            }
        }

        private void toggleSyncScrollPressed(object sender, EventArgs e)
        {
            chkSyncScroll.Checked = !chkSyncScroll.Checked;
        }

        private void hexBox_MouseEnter(object sender, EventArgs e)
        {
            var hb = (HexBox) sender;

            if (!hb.Focused)
                hb.Focus();
        }

        private void btnStartlocation_Click(object sender, EventArgs e)
        {
            Start(Application.StartupPath);
        }

        private void btnClearCmd_Click(object sender, EventArgs e)
        {
            tbSerialCmd.Text = string.Empty;
            tbSerialOutput.Text = string.Empty;
        }

        private void linkRevE_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkRevE.LinkVisited = true;
            Start("https://github.com/iceman1001/ChameleonMini-rebooted/wiki/Terminal-Commands");
        }

        private void linkRevG_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkRevG.LinkVisited = true;
            Start("https://rawgit.com/emsec/ChameleonMini/master/Doc/Doxygen/html/_page__command_line.html");
        }

        private void menuClear_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "tsmi_copy")
            {
                Clipboard.SetText(tbSerialOutput.Text);
            }

            if (e.ClickedItem.Name == "tsmi_clear")
            {
                tbSerialOutput.Text = string.Empty;
            }
        }

        private void tbSerialCmd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Return) return;

            var tb = (TextBox)sender;
            var cmd = tb.Text.Trim();
            if (string.IsNullOrWhiteSpace(cmd)) return;

            Send(cmd);
        }

        private void btnSerialSend_Click(object sender, EventArgs e)
        {
            var cmd = tbSerialCmd.Text.Trim();
            if (string.IsNullOrWhiteSpace(cmd)) return;

            Send(cmd);
        }

        private void btn_close1_Click(object sender, EventArgs e)
        {
            CloseFile(hexBox1);
        }

        private void btn_close2_Click(object sender, EventArgs e)
        {
            CloseFile(hexBox2);
        }


        #endregion

        #region Helper methods

        private async void Send(string cmd)
        {
            var prompt = "--> ";

            tbSerialOutput.Text += $"{Environment.NewLine}{prompt}{cmd}";

            //determine if command has return data? 
            var res = await SendCommand_ICE(cmd);
            tbSerialOutput.Text += $"{Environment.NewLine}{res}";
        }

        private void ApplyByteWidthChange(int width)
        {
            hexBox1.BytesPerLine = width;
            hexBox2.BytesPerLine = width;
        }

        private void ApplyByteWidthChange(string width_str)
        {
            int width;
            if (!int.TryParse(width_str, out width))
            {
                width = 16;
            }
            ApplyByteWidthChange(width);
        }
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
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                _comport = null;
            }

            this.Text = "Device disconnected";

            pb_device.Image = pb_device.InitialImage;
            FirmwareVersion = string.Empty;

            txt_constatus.Text = "NOT CONNECTED";
            txt_constatus.BackColor = Color.Red;
            txt_constatus.ForeColor = Color.White;
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

            // tab Serial
            btnSerialSend.Enabled = false;
            tbSerialCmd.Enabled = false;
            tbSerialHelp.Text = "N/A";
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
            txt_constatus.BackColor = Color.Green;
            txt_constatus.ForeColor = Color.White;
            txt_constatus.SelectionLength = 0;

            GetSupportedModes();

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

            // tab Serial
            btnSerialSend.Enabled = true;
            tbSerialCmd.Enabled = true;
            GetAvailableCommands();
            InitHelp();

            this.Cursor = Cursors.Default;
        }

        private void OpenChameleonSerialPort()
        {
            this.Cursor = Cursors.WaitCursor;
            pb_device.Image = pb_device.InitialImage;
            txt_output.Text = string.Empty;

            //var searcher = new ManagementObjectSearcher("select DeviceID from Win32_SerialPort where Description = \"ChameleonMini Virtual Serial Port\"");
            var searcher = new ManagementObjectSearcher("select Name, DeviceID, PNPDeviceID from Win32_SerialPort ");

            // first check if there is a known VID/PID found - this increases startup-speed significantly

            foreach (var obj in searcher.Get())
            {
                var comPortStr = obj["DeviceID"].ToString();
                var pnpId = obj["PNPDeviceID"].ToString();

                if (pnpId.Contains("VID_03EB&PID_2044") || (pnpId.Contains("VID_16D0&PID_04B2")))
                {
                    _comport = new SerialPort(comPortStr, 115200)
                    {
                        ReadTimeout = 4000,
                        WriteTimeout = 6000,
                        DtrEnable = true,
                        RtsEnable = true,
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
                        if (pnpId.Contains("VID_03EB&PID_2044"))
                        {
                            // revE
                            _deviceIdentification = "Firmware RevE rebooted";
                            pb_device.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("chamRevE");
                            _CurrentDevType = DeviceType.RevE;
                            _tagslotIndexOffset = 1;
                            ConfigHMIForRevE();

                            // try without the "MY" extension first
                            FirmwareVersion = SendCommand("VERSION?") as string;
                            if (!string.IsNullOrEmpty(_firmwareVersion) && _firmwareVersion.Contains("Chameleon"))
                            {
                                _cmdExtension = string.Empty;
                                txt_output.Text = $"Success, found Chameleon Mini device on '{comPortStr}' with {_deviceIdentification} installed{Environment.NewLine}";
                                _current_comport = comPortStr;
                                this.Cursor = Cursors.Default;
                                return;
                            }

                            FirmwareVersion = SendCommand("VERSIONMY?") as string;
                            if (!string.IsNullOrEmpty(_firmwareVersion) && _firmwareVersion.Contains("Chameleon"))
                            {
                                _cmdExtension = "MY";
                                txt_output.Text = $"Success, found Chameleon Mini device on '{comPortStr}' with {_deviceIdentification} installed{Environment.NewLine}";
                                _current_comport = comPortStr;
                                this.Cursor = Cursors.Default;
                                return;
                            }
                        }
                        else if (pnpId.Contains("VID_16D0&PID_04B2"))
                        {
                            // revG
                            _deviceIdentification = "Firmware RevG Official";
                            pb_device.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("chamRevG1");
                            _CurrentDevType = DeviceType.RevG;
                            _tagslotIndexOffset = 0;
                            ConfigHMIForRevG();

                            // try without the "MY" extension first
                            FirmwareVersion = SendCommand("VERSION?") as string;
                            if (!string.IsNullOrEmpty(_firmwareVersion) && _firmwareVersion.Contains("Chameleon"))
                            {
                                _cmdExtension = string.Empty;
                                txt_output.Text = $"Success, found Chameleon Mini device on '{comPortStr}' with {_deviceIdentification} installed{Environment.NewLine}";
                                _current_comport = comPortStr;
                                this.Cursor = Cursors.Default;
                                return;
                            }

                        }

                    }

                }
            }

            // OK, no known USB HW-ID's found, then go "brute-force" ....
            foreach (var obj in searcher.Get())
            {
                var comPortStr = obj["DeviceID"].ToString();
                var pnpId = obj["PNPDeviceID"].ToString();

                _comport = new SerialPort(comPortStr, 115200)
                {
                    ReadTimeout = 4000,
                    WriteTimeout = 6000,
                    DtrEnable = true,
                    RtsEnable = true,
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
                   
                    _deviceIdentification = "Unknown Version";
                    pb_device.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("warning");
                    _CurrentDevType = DeviceType.RevE;
                    _tagslotIndexOffset = 1;
                    ConfigHMIForRevE();
                   

                    // try without the "MY" extension first
                    FirmwareVersion = SendCommand("VERSION?") as string;
                    if (!string.IsNullOrEmpty(_firmwareVersion) && _firmwareVersion.Contains("Chameleon"))
                    {
                        _cmdExtension = string.Empty;
                        txt_output.Text = $"Success, found Chameleon Mini device on '{comPortStr}' with {_deviceIdentification} installed{Environment.NewLine}";
                        _current_comport = comPortStr;
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    FirmwareVersion = SendCommand("VERSIONMY?") as string;
                    if (!string.IsNullOrEmpty(_firmwareVersion) && _firmwareVersion.Contains("Chameleon"))
                    {
                        _cmdExtension = "MY";
                        txt_output.Text = $"Success, found Chameleon Mini device on '{comPortStr}' with {_deviceIdentification} installed{Environment.NewLine}";
                        _current_comport = comPortStr;
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    // wrong comport.
                    _comport.Close();
                    txt_output.Text += $"Didn't find a Chameleon on '{comPortStr}'{Environment.NewLine}";
                }
            }
            _current_comport = string.Empty;
            this.Cursor = Cursors.Default;
            txt_output.Text += $"Didn't find any Chameleon Mini device connected{Environment.NewLine}";
        }

        private void SendCommandWithoutResult(string cmdText)
        {
            if (!SendCommandPossible(cmdText)) return;
         
            try
            {
                // send command
                var tx_data = Encoding.ASCII.GetBytes(cmdText);
                _comport.Write(tx_data, 0, tx_data.Length);
                _comport.Write("\r\n");
            }
            catch (Exception)
            { }
        }

        private object SendCommand(string cmdText)
        {
            if (!SendCommandPossible(cmdText)) return string.Empty;
     
            try
            {
                _comport.DiscardInBuffer();
                // send command
                SendCommandWithoutResult(cmdText);

                if (cmdText.Contains("DETECTIONMY?"))
                {
                    // wait to make sure data is transmitted
                    Thread.Sleep(100);

                    var rx_data = new byte[275];

                    // read the result
                    var read_count = _comport.Read(rx_data, 0, rx_data.Length);
                    if (read_count <= 0) return string.Empty;

                    var foo = new byte[read_count];
                    Array.Copy(rx_data, 8, foo, 0, read_count - 7);
                    return foo;
                }
                else
                {
                    string read_response = "";
                    DateTime start = DateTime.Now;

                    while (((read_response == "") || (read_response == null)) && (DateTime.Now.Subtract(start).TotalMilliseconds < 1000))
                    {
                        read_response = _comport.ReadLine();
                        read_response = read_response.Replace("101:OK WITH TEXT", "").Replace("100:OK", "").Replace("\r", "");
                    }
                    return read_response;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        // Some commands are expected to give multiline response, like IDENTIFY
        private object SendCommandWithMultilineResponse(string cmdText)
        {
            if (!SendCommandPossible(cmdText)) return string.Empty;
            string full_response = "";
            try
            {
                _comport.DiscardInBuffer();
                // send command
                SendCommandWithoutResult(cmdText);
                string read_response ="";
                bool receptionStarted = false;

                DateTime start = DateTime.Now;
                DateTime segment = DateTime.Now;

                // Read until no more data is received for 50ms
                while ( (DateTime.Now.Subtract(start).TotalMilliseconds < 5000) || (receptionStarted && (DateTime.Now.Subtract(segment).TotalMilliseconds < 100) ) )
                {
                    read_response = _comport.ReadExisting();
                    if ((read_response != null) && (read_response != "" ))
                    {
                        full_response += read_response;
                        segment = DateTime.Now;
                        receptionStarted = true;
                    }
                }

                full_response = full_response.Replace("101:OK WITH TEXT", "").Replace("100:OK", "");
                return full_response;
            }
            catch
            {
                return string.Empty;
            }
        }


        private bool SendCommandPossible(string cmdText)
        {
            bool retVal = true;

            if ((_comport == null) || (!_comport.IsOpen) || string.IsNullOrWhiteSpace(cmdText))
            {
                retVal = false;
            }

            return retVal;
        }

        private async Task<object> SendCommand_ICE(string cmdText)
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

                int blockLimit = 275;
                var cts = new CancellationTokenSource();
                var rx_data = new byte[blockLimit];

                var bytesread = await _comport.BaseStream.ReadAsync(rx_data, 0, blockLimit, cts.Token);

                if (bytesread <= 0) return string.Empty;


                var received = new byte[bytesread];
                Buffer.BlockCopy(rx_data, 0, received, 0, bytesread);

                if (cmdText.Contains("DETECTIONMY?"))
                {
                    var foo = new byte[bytesread];
                    Array.Copy(rx_data, 8, foo, 0, bytesread - 7);
                    return foo;
                }
                var s = new string(Encoding.ASCII.GetChars(received));
                return s;
            }
            catch (Exception)
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
            if (selectedMode.StartsWith("MF_CLASSIC") || selectedMode.StartsWith("MF_DETECTION"))
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
            for (int i = 1; i < 9; i++)
            {
                RefreshSlot(i);
            }
        }

        private void RefreshSlot(int slotIndex)
        {
            // Keep initial Active Slot when refreshing
            var ActiveselectedSlot = SendCommand($"SETTING{_cmdExtension}?").ToString();

            //SETTINGMY=i
            SendCommandWithoutResult($"SETTING{_cmdExtension}={slotIndex - _tagslotIndexOffset}");

            //SETTINGMY? -> SHOULD BE "NO."+i
            var selectedSlot = SendCommand($"SETTING{_cmdExtension}?").ToString();
            if (!selectedSlot.Contains((slotIndex - _tagslotIndexOffset).ToString())) return;

            //UIDMY? -> RETURNS THE UID
            var slotUid = SendCommand($"UID{_cmdExtension}?").ToString();
            if (!string.IsNullOrWhiteSpace(slotUid))
            {
                // set the textbox value of the i+1 txt_uid
                var tbs = FindControls<TextBox>(Controls, $"txt_uid{slotIndex}");
                foreach (var box in tbs)
                {
                    box.Text = slotUid;
                }
            }

            //MEMSIZEMY? -> RETURNS THE SIZE OF THE OCCUPIED MEMORY
            var slotMemSize = SendCommand($"MEMSIZE{_cmdExtension}?").ToString();
            if (!string.IsNullOrEmpty(slotMemSize))
            {
                // set the textbox value of the i+1 txt_size
                var txtMemSize = FindControls<TextBox>(Controls, $"txt_size{slotIndex}");
                foreach (var box in txtMemSize)
                {
                    box.Text = slotMemSize;
                }
            }


            // SWAP TYPES
            if (_CurrentDevType == DeviceType.RevG)
            {
                //CONFIGMY? -> RETURNS THE CONFIGURATION MODE
                var slotMode = SendCommand($"CONFIG{_cmdExtension}?").ToString();
                if (IsModeValid(slotMode))
                {
                    // set the combobox value of the i+1 cb_mode
                    var cbMode = FindControls<ComboBox>(Controls, $"cb_mode{slotIndex}");
                    foreach (var box in cbMode)
                    {
                        box.SelectedItem = slotMode;
                    }
                }

                //BUTTONMY? -> RETURNS THE MODE OF THE BUTTON
                var slotLButtonMode = SendCommand($"LBUTTON{_cmdExtension}?").ToString();
                if (IsLButtonModeValid(slotLButtonMode))
                {
                    // set the combobox value of the i+1 cb_button
                    var cbLButton = FindControls<ComboBox>(Controls, $"cb_lbutton{slotIndex}");
                    foreach (var box in cbLButton)
                    {
                        box.SelectedItem = slotLButtonMode;
                    }
                }

                //BUTTON_LONGMY? -> RETURNS THE MODE OF THE BUTTON LONG
                var slotLButtonLongMode = SendCommand($"LBUTTON_LONG{_cmdExtension}?").ToString();
                if (IsLButtonModeValid(slotLButtonLongMode))
                {
                    // set the combobox value of the i+1 cb_buttonlong
                    var cbLButtonLong = FindControls<ComboBox>(Controls, $"cb_lbuttonlong{slotIndex}");
                    foreach (var box in cbLButtonLong)
                    {
                        box.SelectedItem = slotLButtonLongMode;
                    }
                }

                //BUTTONMY? -> RETURNS THE MODE OF THE BUTTON
                var slotRButtonMode = SendCommand($"RBUTTON{_cmdExtension}?").ToString();
                if (IsRButtonModeValid(slotRButtonMode))
                {
                    // set the combobox value of the i+1 cb_button
                    var cbButton = FindControls<ComboBox>(Controls, $"cb_rbutton{slotIndex}");
                    foreach (var box in cbButton)
                    {
                        box.SelectedItem = slotRButtonMode;
                    }
                }

                //BUTTON_LONGMY? -> RETURNS THE MODE OF THE BUTTON LONG
                var slotRButtonLongMode = SendCommand($"RBUTTON_LONG{_cmdExtension}?").ToString();
                if (IsRButtonLongModeValid(slotRButtonLongMode))
                {
                    // set the combobox value of the i+1 cb_buttonlong
                    var cbRButtonLong = FindControls<ComboBox>(Controls, $"cb_rbuttonlong{slotIndex + _tagslotIndexOffset}");
                    foreach (var box in cbRButtonLong)
                    {
                        box.SelectedItem = slotRButtonLongMode;
                    }
                }

                var slotLEDGreen = SendCommand($"LEDGREEN{_cmdExtension}?").ToString();
                if (IsLEDGreenModeValid(slotLEDGreen))
                {
                    // set the combobox value of the i+1 cb_button
                    var cbLEDGreen = FindControls<ComboBox>(Controls, $"cb_ledgreen{slotIndex}");
                    foreach (var box in cbLEDGreen)
                    {
                        box.SelectedItem = slotLEDGreen;
                    }
                }

                var slotLEDRed = SendCommand($"LEDRED{_cmdExtension}?").ToString();
                if (IsLEDRedModeValid(slotLEDRed))
                {
                    // set the combobox value of the i+1 cb_button
                    var cbLEDRed = FindControls<ComboBox>(Controls, $"cb_ledred{slotIndex}");
                    foreach (var box in cbLEDRed)
                    {
                        box.SelectedItem = slotLEDRed;
                    }
                }
            }
            else if (_CurrentDevType == DeviceType.RevE)
            {
                //CONFIGMY? -> RETURNS THE CONFIGURATION MODE
                var slotMode = SendCommand($"CONFIG{_cmdExtension}?").ToString();
                if (IsModeValid(slotMode))
                {
                    // set the combobox value of the i+1 cb_mode
                    var cbMode = FindControls<ComboBox>(Controls, $"cb_mode{slotIndex}");
                    foreach (var box in cbMode)
                    {
                        if (slotMode.Equals("MF_CLASSIC_4K") && box.Name != "cb_mode1")
                            continue;
                        box.SelectedItem = slotMode;
                    }
                }

                //BUTTONMY? -> RETURNS THE MODE OF THE BUTTON
                var slotLButtonMode = SendCommand($"BUTTON{_cmdExtension}?").ToString();
                if (IsLButtonModeValid(slotLButtonMode))
                {
                    // set the combobox value of the i+1 cb_button
                    var cbLButton = FindControls<ComboBox>(Controls, $"cb_lbutton{slotIndex}");
                    foreach (var box in cbLButton)
                    {
                        box.SelectedItem = slotLButtonMode;
                    }
                }

                //BUTTON_LONGMY? -> RETURNS THE MODE OF THE BUTTON LONG
                var slotLButtonLongMode = SendCommand($"BUTTON_LONG{_cmdExtension}?").ToString();
                if (IsLButtonModeValid(slotLButtonLongMode))
                {
                    // set the combobox value of the i+1 cb_buttonlong
                    var cbLButtonLong = FindControls<ComboBox>(Controls, $"cb_lbuttonlong{slotIndex}");
                    foreach (var box in cbLButtonLong)
                    {
                        box.SelectedItem = slotLButtonLongMode;
                    }
                }
            }

            SendCommandWithoutResult($"SETTING{_cmdExtension}={ActiveselectedSlot}".ToString());
            HighlightActiveSlot();
        }

        private bool IsLButtonModeValid(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return false;
            return _lbuttonModesArray.Contains(s);
        }

        private bool IsRButtonModeValid(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return false;
            return _rbuttonModesArray.Contains(s);
        }

        private bool IsLButtonLongModeValid(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return false;
            return _lbuttonLongModesArray.Contains(s);
        }

        private bool IsRButtonLongModeValid(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return false;
            return _rbuttonLongModesArray.Contains(s);
        }

        private bool IsLEDGreenModeValid(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return false;
            return _LEDGreenModesArray.Contains(s);
        }

        private bool IsLEDRedModeValid(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return false;
            return _LEDRedModesArray.Contains(s);
        }

        private bool IsModeValid(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return false;
            return _modesArray.Contains(s);
        }

        private void SetRevGButtons()
        {
            var modesStr = SendCommand($"CONFIG{_cmdExtension}?").ToString();

            if (!string.IsNullOrEmpty(modesStr))
            {
                // split by comma
                _modesArray = modesStr.Split(',');
                if (_modesArray.Any())
                {
                    // populate all dropdowns
                    foreach (var cb in FindControls<ComboBox>(Controls, "cb_mode"))
                    {
                        cb.Items.Clear();
                        cb.Items.AddRange(_modesArray);
                    }
                }
            }
            // Get button modes
            var lbuttonModesStr = SendCommand($"LBUTTON{_cmdExtension}=?").ToString();
            if (string.IsNullOrEmpty(lbuttonModesStr)) return;

            // split by comma
            _lbuttonModesArray = lbuttonModesStr.Split(',');
            if (!_lbuttonModesArray.Any()) return;

            // populate all dropdowns
            foreach (var cb in FindControls<ComboBox>(Controls, "cb_Lbutton"))
            {
                cb.Items.Clear();
                cb.Items.AddRange(_lbuttonModesArray);
            }

            // Get button modes
            var rbuttonModesStr = SendCommand($"RBUTTON{_cmdExtension}=?").ToString();
            if (string.IsNullOrEmpty(rbuttonModesStr)) return;

            // split by comma
            _rbuttonModesArray = rbuttonModesStr.Split(',');
            if (!_rbuttonModesArray.Any()) return;

            // populate all dropdowns
            foreach (var cb in FindControls<ComboBox>(Controls, "cb_Rbutton"))
            {
                cb.Items.Clear();
                cb.Items.AddRange(_rbuttonModesArray);
            }

            // Get button long modes
            var lbuttonLongModesStr = SendCommand($"LBUTTON_LONG{_cmdExtension}=?").ToString();
            if (string.IsNullOrEmpty(lbuttonLongModesStr)) return;

            if (lbuttonLongModesStr.ToLower().StartsWith("200"))
            {
                // disable all dropdowns
                foreach (var cb in FindControls<ComboBox>(Controls, "cb_Lbuttonlong"))
                {
                    cb.Items.Clear();
                    cb.Enabled = false;
                }
            }
            else
            {
                // split by comma
                _lbuttonLongModesArray = lbuttonLongModesStr.Split(',');

                if (!_lbuttonLongModesArray.Any()) return;

                // populate all dropdowns
                foreach (var cb in FindControls<ComboBox>(Controls, "cb_lbuttonlong"))
                {
                    cb.Enabled = true;
                    cb.Items.Clear();
                    cb.Items.AddRange(_lbuttonLongModesArray);
                }

                // Get button long modes
                var rbuttonLongModesStr = SendCommand($"RBUTTON_LONG{_cmdExtension}=?").ToString();
                if (string.IsNullOrEmpty(rbuttonLongModesStr)) return;

                if (rbuttonLongModesStr.ToLower().StartsWith("200"))
                {
                    // disable all dropdowns
                    foreach (var cb in FindControls<ComboBox>(Controls, "cb_rbuttonlong"))
                    {
                        cb.Items.Clear();
                        cb.Enabled = false;
                    }
                }
                else
                {
                    // split by comma
                    _rbuttonLongModesArray = rbuttonLongModesStr.Split(',');

                    if (!_rbuttonLongModesArray.Any()) return;

                    // populate all dropdowns
                    foreach (var cb in FindControls<ComboBox>(Controls, "cb_rbuttonlong"))
                    {
                        cb.Enabled = true;
                        cb.Items.Clear();
                        cb.Items.AddRange(_rbuttonLongModesArray);
                    }
                }
            }

            // Get led modes
            var LEDGreenModesStr = SendCommand($"LEDGREEN{_cmdExtension}=?").ToString();
            if (string.IsNullOrEmpty(LEDGreenModesStr)) return;

            // split by comma
            _LEDGreenModesArray = LEDGreenModesStr.Split(',');
            if (!_LEDGreenModesArray.Any()) return;

            // populate all dropdowns
            foreach (var cb in FindControls<ComboBox>(Controls, "cb_ledgreen"))
            {
                cb.Items.Clear();
                cb.Items.AddRange(_LEDGreenModesArray);
            }

            // Get led modes
            var LEDRedModesStr = SendCommand($"LEDRED{_cmdExtension}=?").ToString();
            if (string.IsNullOrEmpty(LEDRedModesStr)) return;

            // split by comma
            _LEDRedModesArray = LEDRedModesStr.Split(',');
            if (!_LEDRedModesArray.Any()) return;

            // populate all dropdowns
            foreach (var cb in FindControls<ComboBox>(Controls, "cb_ledred"))
            {
                cb.Items.Clear();
                cb.Items.AddRange(_LEDRedModesArray);
            }
        }
        private void SetRevEButtons()
        {
            var modesStr = SendCommand($"CONFIG{_cmdExtension}").ToString();

            if (!string.IsNullOrEmpty(modesStr))
            {
                // split by comma
                _modesArray = modesStr.Split(',');
                if (_modesArray.Any())
                {
                    // populate all dropdowns
                    foreach (var cb in FindControls<ComboBox>(Controls, "cb_mode"))
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
            var lbuttonModesStr = SendCommand($"BUTTON{_cmdExtension}").ToString();
            if (string.IsNullOrEmpty(lbuttonModesStr)) return;

            // split by comma
            _lbuttonModesArray = lbuttonModesStr.Split(',');
            if (!_lbuttonModesArray.Any()) return;

            // populate all dropdowns
            foreach (var cb in FindControls<ComboBox>(Controls, "cb_Lbutton"))
            {
                cb.Items.Clear();
                cb.Items.AddRange(_lbuttonModesArray);
            }

            // Get button long modes
            var lbuttonLongModesStr = SendCommand($"BUTTON_LONG{_cmdExtension}").ToString();
            if (string.IsNullOrEmpty(lbuttonLongModesStr)) return;

            if (lbuttonLongModesStr.ToLower().StartsWith("200"))
            {
                // disable all dropdowns
                foreach (var cb in FindControls<ComboBox>(Controls, "cb_Lbuttonlong"))
                {
                    cb.Items.Clear();
                    cb.Enabled = false;
                }
            }
            else
            {
                // split by comma
                _lbuttonLongModesArray = lbuttonLongModesStr.Split(',');

                if (!_lbuttonLongModesArray.Any()) return;

                // populate all dropdowns
                foreach (var cb in FindControls<ComboBox>(Controls, "cb_lbuttonlong"))
                {
                    cb.Enabled = true;
                    cb.Items.Clear();
                    cb.Items.AddRange(_lbuttonLongModesArray);
                }
            }
        }

        private void GetSupportedModes()
        {
            switch (_CurrentDevType)
            {
                case DeviceType.RevG:
                    SetRevGButtons();
                    break;
                default:
                    SetRevEButtons();
                    break;                

            }
        }

        private void GetAvailableCommands()
        {
            var cmd = $"HELP{_cmdExtension}";
          
            var result = SendCommand(cmd).ToString();
            if (string.IsNullOrEmpty(result))
                return;
            
            // split by comma
            var helpArray = result.Split(',');
            if (!helpArray.Any()) return;

            // Set 
            AvailableCommands.Clear();
            AvailableCommands.AddRange(helpArray);            
        }

        private static byte[] ReadFileIntoByteArray(string filename)
        {
            var fi = new FileInfo(filename);
            if (fi.Exists)
            {
                switch (fi.Extension.ToLower())
                {
                    case ".bin":
                    case ".dump":
                    case ".mfd":
                    case ".hex":
                        return File.ReadAllBytes(filename);
                    case ".json":
                        using (var fs = fi.OpenRead())
                        {
                            var settings = new DataContractJsonSerializerSettings();
                            settings.DataContractSurrogate = new BlockSurrogate();
                            settings.KnownTypes = new List<Type> { typeof(Dictionary<string, string>) };
                            settings.UseSimpleDictionaryFormat = true;
                            var ser = new DataContractJsonSerializer(typeof(MifareClassicModel), settings);
                            var mfc = ser.ReadObject(fs) as MifareClassicModel;
                            return mfc.ToByteArray();
                        }
                    default:
                        break;
                }
            }
            return null;
        }

        internal void UploadDump(string filename)
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

        internal void DownloadAndSaveDump(string filename)
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
                if (Path.GetExtension(filename).ToLower() == ".json")
                {
                    var mfc = new MifareClassicModel()
                    {
                        Created = "ChameleonMiniGUI",
                        FileType = "mfcard",
                        Blocks = MifareClassicModel.ToNestedByteArray(neededBytes)
                    };
                    using (var fs = File.Open(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    using (var writer = JsonReaderWriterFactory.CreateJsonWriter(fs, Encoding.UTF8, true, true, "  "))
                    {
                        var settings = new DataContractJsonSerializerSettings();
                        settings.DataContractSurrogate = new BlockSurrogate();
                        settings.KnownTypes = new List<Type> { typeof(Dictionary<string, string>) };
                        settings.UseSimpleDictionaryFormat = true;
                        var ser = new DataContractJsonSerializer(typeof(MifareClassicModel), settings);
                        ser.WriteObject(writer, mfc);
                        writer.Flush();
                    }
                }
                else
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
            timer1.Tick += timer1_Tick;

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
            if (hexBox.ByteProvider == null) return;

            var idx = int.Parse(hexBox.Name.Substring(hexBox.Name.Length - 1));
            var l = FindControls<Label>(Controls, $"lbl_hbfilename{idx}").FirstOrDefault();

            try
            {
                var dynamicFileByteProvider = hexBox.ByteProvider;
                dynamicFileByteProvider?.ApplyChanges();

                txt_output.Text += $"[+] Saved file {l?.Text}{Environment.NewLine}";
            }
            catch (Exception)
            {
                var msg = $"[!] Failed to save file {l?.Text}{Environment.NewLine}";
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

            var fi = new FileInfo(fileName);

            //
            rbtn_bytewidth04.Checked = false;
            rbtn_bytewidth08.Checked = false;
            rbtn_bytewidth16.Checked = false;

            // iclass dumps should be 8bytes width
            if (fileName.ToLower().Contains("iclass"))
            {
                rbtn_bytewidth08.Checked = true;
            }
            else
            {
                // generic rule, larger than 256bytes,  16byte width
                if (fi.Length >= 256)
                {
                    rbtn_bytewidth16.Checked = true;
                }
                else
                {
                    rbtn_bytewidth04.Checked = true;
                }
            }

            try
            {
                CleanUpHexBox(hexBox);

                // try to open in write mode
                IByteProvider dynamicFileByteProvider = null;
                switch (fi.Extension.ToLower())
                {
                    case ".bin":
                    case ".dump":
                    case ".mfd":
                    case ".hex":
                        dynamicFileByteProvider = new DynamicFileByteProvider(fi.Open(FileMode.Open, FileAccess.ReadWrite));
                        break;
                    case ".json":
                        dynamicFileByteProvider = new JsonFileByteProvider(fi.FullName);
                        break;
                    default:
                        break;
                }
                hexBox.ByteProvider = dynamicFileByteProvider;

                // Display info for the file
                var hbIdx = int.Parse(hexBox.Name.Substring(hexBox.Name.Length - 1));
                var l = FindControls<Label>(Controls, $"lbl_hbfilename{hbIdx}").FirstOrDefault();
                if (l != null)
                {
                    l.Text = $"{fi.Name} ({fi.Length} bytes)";
                }

                // reset template dropdown.
                lockFlag = true;
                cb_templateA.SelectedIndex = 0;
                lockFlag = false;

                // run the comparison automatically
                PerformComparison();

                var msg = $"[!] Loaded '{fi.Name}' {Environment.NewLine}";
                txt_output.Text += msg;
            }
            catch (IOException ex) // write mode failed
            {
                // file cannot be opened
                var msg = $"[!] Failed to open file{Environment.NewLine}{ex.Message}{Environment.NewLine}";
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
                return res;
            }
            CleanUp(hexBox);
            return DialogResult.OK;
        }

        void CleanUp(HexBox b)
        {
            CleanUpHexBox(b);

            // Remove the file info
            var hbIdx = int.Parse(b.Name.Substring(b.Name.Length - 1));
            var l = FindControls<Label>(Controls, $"lbl_hbfilename{hbIdx}").FirstOrDefault();
            if (l != null)
            {
                txt_output.Text += $"[+] Closed file {l.Text}{Environment.NewLine}";
                l.Text = "N/A";
            }
        }

        void CleanUpHexBox(HexBox b)
        {
            if (b.ByteProvider == null) return;

            var byteProvider = b.ByteProvider as IDisposable;
            byteProvider?.Dispose();
            b.ByteProvider = null;
        }

        private void PerformComparison()
        {
            // Clear the last highlights
            hexBox1.ClearHighlights();
            hexBox2.ClearHighlights();

            if (hexBox1.ByteProvider != null && hexBox2.ByteProvider != null)
            {

                if (hexBox1.ByteProvider.Length == hexBox2.ByteProvider.Length)
                {
                    for (int i = 0; i < hexBox1.ByteProvider.Length; i++)
                    {
                        CompareByte(i);
                    }
                }
                else
                {
                    long min_common = 0;

                    if (hexBox1.ByteProvider.Length > hexBox2.ByteProvider.Length)
                    {
                        min_common = hexBox2.ByteProvider.Length;
                        hexBox1.AddHighlight(min_common, hexBox1.ByteProvider.Length - min_common, Color.Blue, Color.LightGreen);
                    }
                    else
                    {
                        min_common = hexBox1.ByteProvider.Length;
                        hexBox2.AddHighlight(min_common, hexBox2.ByteProvider.Length - min_common, Color.Red, Color.LightGreen);
                    }
                    // if different sized files
                    for (int i = 0; i < min_common; i++)
                    {
                        CompareByte(i);
                    }
                }
            }

            hexBox1.Invalidate();
            hexBox2.Invalidate();
        }

        private void CompareByte(int byteIndex)
        {
            if (hexBox1.ByteProvider.ReadByte(byteIndex) != hexBox2.ByteProvider.ReadByte(byteIndex))
            {
                //Console.WriteLine("Byte " + i + " is different.");
                hexBox1.AddHighlight(byteIndex, 1, Color.Blue, Color.LightGreen);
                hexBox2.AddHighlight(byteIndex, 1, Color.Red, Color.LightGreen);
            }
        }

        public static Control FindControlAtPoint(Control container, Point pos)
        {
            Control child;
            foreach (Control c in container.Controls)
            {
                if (c.Visible && c.Bounds.Contains(pos))
                {
                    child = FindControlAtPoint(c, new Point(pos.X - c.Left, pos.Y - c.Top));
                    return child ?? c;
                }
            }
            return null;
        }

        private static List<T> FindControls<T>(ICollection ctrls, string searchname) where T : Control
        {
            var list = new List<T>();

            // make sure we have controls to search for.
            if (ctrls == null || ctrls.Count == 0) return list;
            if (string.IsNullOrWhiteSpace(searchname)) return list;

            searchname = searchname.ToLower();

            foreach (Control cb in ctrls)
            {
                if (cb.HasChildren)
                {
                    list.AddRange(FindControls<T>(cb.Controls, searchname));
                }

                if (cb.Name.ToLower().StartsWith(searchname))
                    list.Add(cb as T);
            }

            return list;
        }

        public static void ApplyAll(Control c, Action<Control> action)
        {
            if (c == null) return;

            action(c);

            if (c.Controls.Count == 0) return;

            foreach (Control child in c.Controls)
                ApplyAll(child, action);
        }

        private void ConfigHMIForRevE()
        {
            var list = FindControls<ComboBox>(Controls, "cb_Rbutton");
            list.ForEach(a => ApplyAll(a, c =>
            {
                c.Visible = false;
            }));

            list = FindControls<ComboBox>(Controls, "cb_Lbutton");
            list.ForEach(a => ApplyAll(a, c =>
            {
                c.Visible = true;
                c.Width = REVEDefaultComboWidth;
            }));

            list = FindControls<ComboBox>(Controls, "cb_Rbuttonlong");
            list.ForEach(a => ApplyAll(a, c =>
            {
                c.Visible = false;
            }));

            list = FindControls<ComboBox>(Controls, "cb_Lbuttonlong");
            list.ForEach(a => ApplyAll(a, c =>
            {
                c.Visible = true;
                c.Width = REVEDefaultComboWidth;
            }));

            list = FindControls<ComboBox>(Controls, "cb_ledgreen");
            list.ForEach(a => ApplyAll(a, c =>
            {
                c.Visible = false;
                c.Enabled = false;
            }));

            list = FindControls<ComboBox>(Controls, "cb_ledred");
            list.ForEach(a => ApplyAll(a, c =>
            {
                c.Visible = false;
                c.Enabled = false;
            }));

            tbIdentify.Visible = false;
        }

        private void ConfigHMIForRevG()
        {

            var list = FindControls<ComboBox>(Controls, "cb_Rbutton");
            list.ForEach(a => ApplyAll(a, c =>
         {
             c.Visible = true;
             c.Width = REVGDefaultComboWidth;
         }));

            list = FindControls<ComboBox>(Controls, "cb_Lbutton");
            list.ForEach(a => ApplyAll(a, c =>
            {
                c.Visible = true;
                c.Width = REVGDefaultComboWidth;
            }));

            list = FindControls<ComboBox>(Controls, "cb_Rbuttonlong");
            list.ForEach(a => ApplyAll(a, c =>
            {
                c.Visible = true;
                c.Width = REVGDefaultComboWidth;
            }));

            list = FindControls<ComboBox>(Controls, "cb_Lbuttonlong");
            list.ForEach(a => ApplyAll(a, c =>
            {
                c.Visible = true;
                c.Width = REVGDefaultComboWidth;
            }));

            list = FindControls<ComboBox>(Controls, "cb_ledgreen");
            list.ForEach(a => ApplyAll(a, c =>
            {
                c.Visible = true;
                c.Enabled = true;
            }));

            list = FindControls<ComboBox>(Controls, "cb_ledred");
            list.ForEach(a => ApplyAll(a, c =>
            {
                c.Visible = true;
                c.Enabled = true;
            }));

            tbIdentify.Visible = true;
        }

        private void frm_main_Move(object sender, EventArgs e)
        {
            GroupBoxEnhanced.RedrawGroupBoxDisplay(tpOperation);
        }

		private void frm_main_Activated(object sender, EventArgs e)
        {
            GroupBoxEnhanced.RedrawGroupBoxDisplay(tpOperation);
        }

        private void frm_main_ResizeEnd(object sender, EventArgs e)
        {
            GroupBoxEnhanced.RedrawGroupBoxDisplay(tpOperation);
        }

        private void btn_Identify_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            var OriginalConfig = SendCommand($"CONFIG{ _cmdExtension}?");

            SendCommandWithoutResult($"CONFIG{_cmdExtension}=ISO14443A_READER");
            var Ident = SendCommandWithMultilineResponse($"IDENTIFY{_cmdExtension}");
            tbIdentify.Text = Ident.ToString();

            SendCommandWithoutResult($"CONFIG{ _cmdExtension}={OriginalConfig}");
            Cursor.Current = Cursors.Default;
        }
            #endregion
    }     
}
