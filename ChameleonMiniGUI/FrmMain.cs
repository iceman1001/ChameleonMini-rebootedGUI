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
using System.Threading.Tasks;
using ChameleonMiniGUI.Dump;
using ChameleonMiniGUI.Json;
using static ChameleonMiniGUI.Properties.Settings;

namespace ChameleonMiniGUI
{
    enum DeviceType { Unknown , RevE, RevG };

    public partial class frm_main : Form
    {
        #region Fields
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
        private const int SerialRTimeoutMs = 4000;
        private const int SerialWTimeoutMs = 6000;
        private const int SerialRWTimeoutExtendedMs = 12000;

        private bool lockFlag = false;
        private DeviceType _CurrentDevType = DeviceType.RevG;
        private int _active_selected_slot;

        public string SoftwareVersion => $"Chameleon Mini GUI - {Default.version} - Iceman Edition 冰人";

        private List<string> AvailableCommands = new List<string>();
        private List<string> ErrorResponses = new List<string>(new string[] { "200:UNKNOWN COMMAND", "201:INVALID COMMAND USAGE", "202:INVALID PARAMETER", "203:TIMEOUT" });
        #endregion

        #region Properties
        private double ByteWidth
        {
            get
            {
                var width = (tbSerialOutput.Width / (tbSerialOutput.Font.SizeInPoints * 2.5));

                if (width > 32)
                {
                    width = 32;
                }
                else if (width > 16)
                {
                    width = 16;
                }
                else
                {
                    width = 8;
                }
                return width;
            }
        }

        public string FirmwareVersion
        {
            get { return _firmwareVersion; }
            set
            {
                _firmwareVersion = value;
                tb_firmware.Text = _firmwareVersion;
            }
        }

        // Attempt to reduce flickering of the controls
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        #endregion

        public frm_main()
        {
            InitializeComponent();

            this.Text = SoftwareVersion;
        }

        #region Event Handlers
        private void frm_main_Load(object sender, EventArgs e)
        {
            txt_output.SelectionStart = 0;

            OpenChameleonSerialPort();

            if (_comport != null && _comport.IsOpen)
            {
                DeviceConnected();
                InitHelp();
            }

            // Select no tag slot
            foreach (var cb in FindControls<CheckBox>(tpOperation.Controls, "checkBox"))
            {
                cb.Checked = false;
            }

            LoadSettings();
            InitTimer();

            SplashScreen.CloseForm();
        }

        private void frm_main_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1?.Stop();

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
                    Default.DownloadDumpPath = txt_defaultdownload.Text;
                    Default.Save();
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
                Default.Language = o.Value;
                Default.Save();
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
            this.Cursor = Cursors.WaitCursor;
            SaveActiveSlot();

            // Get all selected indices
            foreach (var cb in FindControls<CheckBox>(tpOperation.Controls, "checkBox"))
            {
                if (!cb.Checked) continue;

                var tagslotIndex = int.Parse(cb.Name.Substring(cb.Name.Length - 1));
                if (tagslotIndex <= 0) continue;

                SendCommandWithoutResult($"SETTING{_cmdExtension}=" + (tagslotIndex - _tagslotIndexOffset));

                string selectedMode = string.Empty;
                var mode = FindControls<ComboBox>(tpOperation.Controls, $"cb_mode{tagslotIndex}").FirstOrDefault();
                if (mode != null)
                {
                    string actualMode = SendCommand($"CONFIG{_cmdExtension}?").ToString();
                    selectedMode = mode.SelectedItem.ToString();
                    if (actualMode != selectedMode)
                    {
                        SendCommandWithoutResult($"CONFIG{_cmdExtension}={mode.SelectedItem}");
                    }
                    
                }

                switch (_CurrentDevType)
                {
                    case DeviceType.RevG:
                        {
                            FindControls<ComboBox>(tpOperation.Controls, $"cb_Lbutton{tagslotIndex}").ForEach(a => SendCommandWithoutResult($"LBUTTON{_cmdExtension}={a.SelectedItem}"));
                            FindControls<ComboBox>(tpOperation.Controls, $"cb_Lbuttonlong{tagslotIndex}").ForEach(a => SendCommandWithoutResult($"LBUTTON_LONG{_cmdExtension}={a.SelectedItem}"));
                            FindControls<ComboBox>(tpOperation.Controls, $"cb_Rbutton{tagslotIndex}").ForEach(a => SendCommandWithoutResult($"RBUTTON{_cmdExtension}={a.SelectedItem}"));
                            FindControls<ComboBox>(tpOperation.Controls, $"cb_Rbuttonlong{tagslotIndex}").ForEach(a => SendCommandWithoutResult($"RBUTTON_LONG{_cmdExtension}={a.SelectedItem}"));
                            FindControls<ComboBox>(tpOperation.Controls, $"cb_ledgreen{tagslotIndex}").ForEach(a => SendCommandWithoutResult($"LEDGREEN{_cmdExtension}={a.SelectedItem}"));
                            FindControls<ComboBox>(tpOperation.Controls, $"cb_ledred{tagslotIndex}").ForEach(a => SendCommandWithoutResult($"LEDRED{_cmdExtension}={a.SelectedItem}"));
                            break;
                        }
                    default:
                        {
                            FindControls<ComboBox>(tpOperation.Controls, $"cb_Lbutton{tagslotIndex}").ForEach(a => SendCommandWithoutResult($"BUTTON{_cmdExtension}={a.SelectedItem}"));
                            FindControls<ComboBox>(tpOperation.Controls, $"cb_Lbuttonlong{tagslotIndex}").ForEach(a => SendCommandWithoutResult($"BUTTON_LONG{_cmdExtension}={a.SelectedItem}"));
                            break;
                        }
                }

                // Set the UID
                var txtUid = FindControls<TextBox>(tpOperation.Controls, $"txt_uid{tagslotIndex}").FirstOrDefault();
                if (txtUid != null)
                {
                    string uid = txtUid.Text;
                    uint uidSize = uint.Parse(SendCommand($"UIDSIZE{_cmdExtension}?").ToString());
                    // Sets UID if valid only
                    if (IsUidValid(uid, uidSize, selectedMode))
                    {
                        SendCommandWithoutResult($"UID{_cmdExtension}={uid}");
                    }
                    txtUid.Text = SendCommand($"UID{_cmdExtension}?").ToString();
                }

                // Set MEMSIZE
                var slotMemSize = SendCommand($"MEMSIZE{_cmdExtension}?").ToString();
                if (!string.IsNullOrEmpty(slotMemSize) && !slotMemSize.StartsWith("202:"))
                {
                    FindControls<TextBox>(tpOperation.Controls, $"txt_size{tagslotIndex}").ForEach(a => a.Text = slotMemSize);
                }
            }
            RestoreActiveSlot();

            this.Cursor = Cursors.Default;
        }

        private void btn_bootmode_Click(object sender, EventArgs e)
        {
            SendCommandWithoutResult($"UPGRADE{_cmdExtension}");
            DisconnectDevice();
        }

        private void btn_exitboot_Click(object sender, EventArgs e)
        {
            bool success = false;
            bool found = false;

            ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity where DeviceID like '%VID_03EB&PID_2FDE%' or DeviceID like '%VID_03EB&PID_204A%' or DeviceID like '%VID_03EB&PID_2FE4%'");

            var queryCollection = searcher.Get();
            if (queryCollection != null && queryCollection.Count > 0)
            {
                var mo = queryCollection.OfType<ManagementObject>().FirstOrDefault();

                if (mo != null)
                {
                    var deviceName = mo["Name"].ToString().ToLower();
                    var deviceID = mo["DeviceID"].ToString().ToLower();
                    if (deviceName.Equals("atxmega128a4u") && (deviceID.Contains("vid_03eb&pid_2fde") || deviceID.Contains("vid_03eb&pid_204a")))
                    {
                        // RevG
                        found = true;
                        try
                        {
                            var extrasPath = ConfigurationManager.AppSettings["BOOTLOADER_PATH"];
                            var avrdudeFileName = ConfigurationManager.AppSettings["AVRDUDE_EXE"];
                            var flashFileName = ConfigurationManager.AppSettings["REVG_FLASH_HEX"];
                            var eepromFileName = ConfigurationManager.AppSettings["REVG_EEPROM_HEX"];

                            var fullpath = Path.Combine(Application.StartupPath, extrasPath);

                            var flasher = Path.Combine(fullpath, avrdudeFileName);
                            var firmware = Path.Combine(fullpath, flashFileName);
                            var eeprom = Path.Combine(fullpath, eepromFileName);

                            if (File.Exists(flasher) && File.Exists(firmware) && File.Exists(eeprom))
                            {
                                var ps = new ProcessStartInfo
                                {
                                    UseShellExecute = true,
                                    WorkingDirectory = fullpath,
                                    FileName = flasher,
                                    Arguments = "-c flip2 -p ATXMega128A4U -B 60 -P usb -U application:w:" + flashFileName + ":i -U eeprom:w:" + eepromFileName + ":i"
                                };
                                Start(ps);
                                success = true;
                            }
                            else
                            {
                                txt_output.AppendText("[!] Firmware flashing failed: Unable to find all the required files to flash the firmware");
                            }
                        }
                        catch (Exception ex)
                        {
                            //
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else if (deviceName.Equals("atxmega32a4u") && deviceID.Contains("vid_03eb&pid_2fe4"))
                    {
                        // RevE rebooted
                        found = true;
                        try
                        {
                            // Run the bootloader exe

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
                                success = true;
                            }
                            else
                            {
                                txt_output.AppendText("[!] Firmware flashing failed: Unable to find all the required files to flash the firmware");
                            }
                        }
                        catch (Exception ex)
                        {
                            //
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }

            if (success)
            {
                txt_output.AppendText("[+] Firmware flashed successfully");
            }
            else
            {
                if (!found)
                {
                    txt_output.AppendText("[!] Firmware flashing failed: Unable to find a ChameleonMini device. Maybe not in bootloader mode?");
                }
                MessageBox.Show("Unable to flash the firmware. Check the log output for more information.", "Firmware flashing failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            SaveActiveSlot();

            // Open dialog
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var dumpFilename = openFileDialog1.FileName;

                foreach (var cb in FindControls<CheckBox>(tpOperation.Controls, "checkBox"))
                {
                    if (!cb.Checked) continue;

                    var tagslotIndex = int.Parse(cb.Name.Substring(cb.Name.Length - 1));
                    if (tagslotIndex <= 0) continue;

                    // select the corresponding slot
                    SendCommandWithoutResult($"SETTING{_cmdExtension}={tagslotIndex - _tagslotIndexOffset}");

                    // Load the dump
                    UploadDump(dumpFilename);

                    // Refresh slot
                    RefreshSlot(tagslotIndex, false);
                }

            }

            RestoreActiveSlot();
            this.Cursor = Cursors.Default;
        }

        private void btn_download_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            SaveActiveSlot();

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
            foreach (var cb in FindControls<CheckBox>(tpOperation.Controls, "checkBox"))
            {
                if (!cb.Checked) continue;

                var tagslotIndex = int.Parse(cb.Name.Substring(cb.Name.Length - 1));
                if (tagslotIndex <= 0) continue;

                // select the corresponding slot
                SendCommandWithoutResult($"SETTING{_cmdExtension}={tagslotIndex - _tagslotIndexOffset}");

                if (btn_setactive.Enabled)
                {
                    // Only one tag slot is selected, show the save dialog

                    saveFileDialog1.InitialDirectory = downloadPath;

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        var dumpFilename = saveFileDialog1.FileName.Trim();

                        // Add extension if missing
                        switch (saveFileDialog1.FilterIndex)
                        {
                            case 1:
                                dumpFilename = !dumpFilename.ToLower().Contains(".bin") ? dumpFilename + ".bin" : dumpFilename;
                                break;
                            case 2:
                                dumpFilename = !dumpFilename.ToLower().EndsWith(".json") ? dumpFilename + ".json" : dumpFilename;
                                break;
                            case 3:
                                dumpFilename = !dumpFilename.ToLower().EndsWith(".eml") ? dumpFilename + ".eml" : dumpFilename;
                                break;
                            case 4:
                                dumpFilename = !dumpFilename.ToLower().EndsWith(".mct") ? dumpFilename + ".mct" : dumpFilename;
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
                    var uid = SendCommand($"UID{_cmdExtension}?").ToString();

                    if (!string.IsNullOrEmpty(uid))
                    {
                        var varFullDownloadPath = Path.Combine(downloadPath, $"{uid}.bin");
                        var dupeCount = 0;
                        while (File.Exists(varFullDownloadPath))
                            varFullDownloadPath = Path.Combine(downloadPath, $"{uid}_{++dupeCount}.bin");
                        DownloadAndSaveDump(varFullDownloadPath);
                    }
                }
            }

            RestoreActiveSlot();
            this.Cursor = Cursors.Default;
        }

        private void btn_mfkey_Click(object sender, EventArgs e)
        {
            /*
            this.Cursor = Cursors.WaitCursor;
            SaveActiveSlot();

            // Get all selected indices
            var results = FindControls<CheckBox>(tpOperation.Controls, "checkBox")
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

            RestoreActiveSlot();
            this.Cursor = Cursors.Default;
            */

            this.Cursor = Cursors.WaitCursor;
            SaveActiveSlot();

            if (_CurrentDevType == DeviceType.RevE)
            {
                // Get all selected indices
                var results = FindControls<CheckBox>(tpOperation.Controls, "checkBox")
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
            }
            else if (_CurrentDevType == DeviceType.RevG)
            {
                SendCommandWithoutResult($"DETECTION{_cmdExtension}?");

                var result = ReceiveXModemData();

                var result_size = result.Length;
                var idx = 0;

                if (result_size >= 2)
                {
                    // get the packet size
                    var packetSizeArray = new byte[] { result[idx++], result[idx++] };
                    int packetSize = (((int)packetSizeArray[1]) << 8) | ((int)packetSizeArray[0]);

                    var myKeys = new List<MyKey>();

                    while (idx <= packetSize)
                    {
                        if (idx + 4 > packetSize)
                            break;

                        var entry_type = result[idx++];
                        var data_length = result[idx++];
                        var timestampArray = new byte[] { result[idx++], result[idx++] };
                        int timestamp = (((int)timestampArray[0]) << 8) | ((int)timestampArray[1]);

                        if (entry_type == 0xC0 || entry_type == 0xA1)
                        {
                            if ((data_length > 0) && (idx + data_length <= packetSize))
                            {
                                byte[] data = new byte[data_length];
                                Array.Copy(result, idx, data, 0, data_length);

                                var mykey = new MyKey
                                {
                                    KeyType = data[0],
                                    Block = data[1],
                                    UID = MfKeyAttacks.ToUInt32(data, 2),
                                    nt0 = MfKeyAttacks.ToUInt32(data, 6),
                                    nr0 = MfKeyAttacks.ToUInt32(data, 10),
                                    ar0 = MfKeyAttacks.ToUInt32(data, 14)
                                };
                                mykey.Sector = MfKeyAttacks.ToSector(mykey.Block);

                                // skip sectors with 0xFF
                                if (mykey.Sector != 0xFF)
                                    myKeys.Add(mykey);
                            }
                        }

                        idx += data_length;
                    }

                    if (myKeys.Any() == false)
                    {
                        RestoreActiveSlot();
                        this.Cursor = Cursors.Default;
                        txt_output.AppendText($"{Environment.NewLine}Didn't find any nonces for mfkey32{Environment.NewLine}");
                        return;
                    }

                    txt_output.AppendText($"{Environment.NewLine}Starting mfkey32 with {myKeys.Count} nonces {Environment.NewLine}");

                    var my_cmp = new KeyComparer();
                    myKeys.Sort(my_cmp);

                    var show_all = MfKeyAttacks.KeyWorker(myKeys);

                    if (string.IsNullOrWhiteSpace(show_all))
                    {
                        txt_output.AppendText(string.Join(string.Empty,
                            $"mfkey32 attack failed, no keys found{Environment.NewLine}"));
                    }
                    else
                    {
                        txt_output.AppendText($"Success! recovered the following keys{Environment.NewLine}");
                        txt_output.AppendText(string.Join(string.Empty, $"{Environment.NewLine}", show_all));
                    }
                }
            }

            RestoreActiveSlot();
            this.Cursor = Cursors.Default;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            // Get all selected checkboxes
            List<CheckBox> selectedCheckBoxes = new List<CheckBox>();
            List<CheckBox> allSlotsCheckBoxes = FindControls<CheckBox>(tpOperation.Controls, "checkBox");
            foreach (var cb in allSlotsCheckBoxes)
            {
                if(cb.Checked)
                {
                    selectedCheckBoxes.Add(cb);
                }
            }

            // RevE-rebooted has ability to "CLEARALL" at once so if all CheckBoxes are selected and CLEARALL available, we do
            if ((_CurrentDevType == DeviceType.RevE) && (selectedCheckBoxes.Count == allSlotsCheckBoxes.Count) && AvailableCommands.Contains("CLEARALL"))
            {
                SendCommandWithoutResult($"CLEARALL");
                RefreshAllSlots();
            }
            // Else we clear all selected slots one by one
            else
            {
                SaveActiveSlot();
                foreach (var cb in selectedCheckBoxes)
                {
                    var tagslotIndex = int.Parse(cb.Name.Substring(cb.Name.Length - 1));
                    if (tagslotIndex <= 0) continue;

                    int slotIndex = tagslotIndex - _tagslotIndexOffset;
                    SendCommandWithoutResult($"SETTING{_cmdExtension}={slotIndex}");
                    SendCommandWithoutResult($"CLEAR{_cmdExtension}");

                    // The firmware of RevE-rebooted will deal with proper init to defaults after CLEAR/CLEARALL command
                    // For RevG, we manually set default values
                    if (_CurrentDevType == DeviceType.RevG)
                    {
                        FindControls<ComboBox>(tpOperation.Controls, $"cb_mode{tagslotIndex}").ForEach(a => SendCommandWithoutResult($"CONFIG{_cmdExtension}=NONE"));
                        FindControls<ComboBox>(tpOperation.Controls, $"cb_Lbutton{tagslotIndex}").ForEach(a => SendCommandWithoutResult($"LBUTTON{_cmdExtension}={a.Items[0]}"));
                        FindControls<ComboBox>(tpOperation.Controls, $"cb_Lbuttonlong{tagslotIndex}").ForEach(a => SendCommandWithoutResult($"LBUTTON_LONG{_cmdExtension}={a.Items[0]}"));
                        FindControls<ComboBox>(tpOperation.Controls, $"cb_Rbutton{tagslotIndex}").ForEach(a => SendCommandWithoutResult($"RBUTTON{_cmdExtension}={a.Items[0]}"));
                        FindControls<ComboBox>(tpOperation.Controls, $"cb_Rbuttonlong{tagslotIndex}").ForEach(a => SendCommandWithoutResult($"RBUTTON_LONG{_cmdExtension}={a.Items[0]}"));
                        FindControls<ComboBox>(tpOperation.Controls, $"cb_ledgreen{tagslotIndex}").ForEach(a => SendCommandWithoutResult($"LEDGREEN{_cmdExtension}={a.Items[0]}"));
                        FindControls<ComboBox>(tpOperation.Controls, $"cb_ledred{tagslotIndex}").ForEach(a => SendCommandWithoutResult($"LEDRED{_cmdExtension}={a.Items[0]}"));
                    }
                    RefreshSlot(tagslotIndex, false);
                }
                RestoreActiveSlot();
            }

            this.Cursor = Cursors.Default;
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            // Get all selected indices
            foreach (var cb in FindControls<CheckBox>(tpOperation.Controls, "checkBox"))
            {
                if (!cb.Checked) continue;

                var tagslotIndex = int.Parse(cb.Name.Substring(cb.Name.Length - 1));
                if (tagslotIndex <= 0) continue;

                RefreshSlot(tagslotIndex);
            }
            this.Cursor = Cursors.Default;
        }

        private void btn_setactive_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            foreach (var cb in FindControls<CheckBox>(tpOperation.Controls, "checkBox"))
            {
                if (!cb.Checked) continue;

                var tagslotIndex = int.Parse(cb.Name.Substring(cb.Name.Length - 1));
                if (tagslotIndex <= 0) continue;

                SendCommandWithoutResult($"SETTING{_cmdExtension}={tagslotIndex - _tagslotIndexOffset}");

                _active_selected_slot = tagslotIndex;

                break; // Only one can be set as active
            }

            HighlightActiveSlot();

            this.Cursor = Cursors.Default;
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
                    InitHelp();
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
                if (_CurrentDevType == DeviceType.RevG)
                    btn_identify.Enabled = true;
            }
            else if (checkCount > 1)
            {
                // Enable most buttons
                btn_apply.Enabled = true;
                btn_refresh.Enabled = true;
                btn_clear.Enabled = true;
                btn_setactive.Enabled = false;
                btn_keycalc.Enabled = true;
                btn_upload.Enabled = true;
                btn_download.Enabled = true;
                btn_identify.Enabled = false;
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
                btn_identify.Enabled = false;
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            // Send the RESETMY command
            SendCommandWithoutResult($"RESET{_cmdExtension}");
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
                    Default.DownloadDumpPath = selectedFolder;
                    Default.Save();
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
                Default.EnableKeepAlive = chk_keepalive.Checked;
                Default.KeepAliveInterval = keepAliveInterval;
                Default.Save();
            }
            else
            {
                MessageBox.Show("Only positive numbers are allowed for the interval of the keep-alive setting", "Interval not valid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            DisconnectDevice();
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

            disconnectPressed = false;
            DeviceConnected();
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

        private void byteWidthCheckBoxes_CheckedChanged(object sender, EventArgs e)
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
            var hb = (HexBox)sender;

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
            if (e.ClickedItem.Name == "tsmi_selectall")
            {
                tbSerialOutput.Focus();
                tbSerialOutput.SelectAll();
            }

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

        private void frm_main_Activated(object sender, EventArgs e)
        {
            GroupBoxEnhanced.RedrawGroupBoxDisplay(tpOperation);
        }

        private void frm_main_ResizeBegin(object sender, EventArgs e)
        {
        }

        private void frm_main_ResizeEnd(object sender, EventArgs e)
        {
            GroupBoxEnhanced.RedrawGroupBoxDisplay(tpOperation);
        }

        private void btn_identify_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            SaveActiveSlot();

            SendCommandWithoutResult($"CONFIG{_cmdExtension}=ISO14443A_READER");

            var s = SendCommandWithMultilineResponse($"IDENTIFY{_cmdExtension}").ToString();
            var lines = s.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach( var line in lines) {
                txt_output.Text += $"[+] {line}{Environment.NewLine}";
            }
            RestoreActiveSlot();
            this.Cursor = Cursors.Default;
        }

        private void tfSerialHelp_TextClick(object sender, EventArgs e)
        {
            var tbClicked = sender as TextBox;
            if (tbClicked == null) return;

            tbSerialCmd.Text = tbClicked.Text;
            tbSerialCmd.SelectionStart = tbSerialCmd.Text.Length;
            tbSerialCmd.SelectionLength = 0;
            this.ActiveControl = tbSerialCmd;
        }

        private void tableLayoutPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void tableLayoutPanel_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (s.Length > 0)
            {
                var dumpFileName = s[0];
                if (File.Exists(dumpFileName))
                {
                    // Get the tagslot index
                    var tagslotIndex = int.Parse(((TableLayoutPanel)sender).Name.Substring(((TableLayoutPanel)sender).Name.Length - 1));
                    if (tagslotIndex <= 0) return;

                    // Select the corresponding slot
                    SendCommandWithoutResult($"SETTING{_cmdExtension}={tagslotIndex - _tagslotIndexOffset}");

                    // Load the dump
                    UploadDump(dumpFileName);

                    // Refresh slot
                    RefreshSlot(tagslotIndex);
                }
            }
        }

         private void txt_output_VisibleChanged(object sender, EventArgs e)
        {
            var tb = (TextBox) sender;
            if (tb == null || !tb.Visible) return;
            
            tb.SelectionStart = tb.TextLength;
            tb.ScrollToCaret();
        }  
        #endregion

        #region Helper methods
        private void InitHelp()
        {
            tfSerialHelp.AvailableCommands = AvailableCommands;
            tfSerialHelp.SetList();
        }

        private void LoadSettings()
        {
            // Set the default download path if not empty and exists
            if (!string.IsNullOrEmpty(Default.DownloadDumpPath))
            {
                if (Directory.Exists(Default.DownloadDumpPath))
                {
                    txt_defaultdownload.Text = Default.DownloadDumpPath;
                } // else create folder?
            }

            // Set the keep alive options
            chk_keepalive.Checked = Default.EnableKeepAlive;
            if (Default.KeepAliveInterval > 0)
            {
                txt_interval.Text = Default.KeepAliveInterval.ToString();
            }
            else
            {
                // set the default value
                // should be a setting aswell
                txt_interval.Text = "10000";
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
            var lang = Default.Language.ToLowerInvariant();
            if (!string.IsNullOrWhiteSpace(lang))
            {
                ml.LoadLanguage(this.Controls, lang);

                // select lang in combobox
                lockFlag = true;
                foreach (KeyValuePair<string, string> i in cb_languages.Items)
                {
                    if (i.Key.ToLowerInvariant() == lang || i.Value.ToLowerInvariant() == lang)
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

        private async void Send(string cmd)
        {
            const string prompt = "--> ";
            var c = tbSerialOutput;

            c.Focus();
            c.SelectedText = string.Empty;
            c.SelectionStart = c.TextLength;
            c.SelectionColor = Color.Goldenrod;
            c.AppendText($"{Environment.NewLine}{prompt}{cmd}");
            c.ScrollToCaret();

            string res;
            if (cmd.ToLower().StartsWith("upload"))
            {
                res = "This command is not supported in this serial interface. Use Operations tag to upload.";
            }
            else if(cmd.ToLower().StartsWith("upgrade"))
            {
                res = "Putting Chameleon into upgrade mode";

                SendCommandWithoutResult($"UPGRADE{_cmdExtension}");

                DisconnectDevice();
            }
            else
            {
                var o = await SendCommand_ICE(cmd);
                res = o.ToString();
            }

            c.SelectedText = string.Empty;
            c.SelectionStart = c.TextLength;
            c.SelectionColor = c.ForeColor;
            c.AppendText($"{Environment.NewLine}{res}");
            c.ScrollToCaret();

            tbSerialCmd.Focus();
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
            this.Text = $"{SoftwareVersion} ---> Connected {_current_comport} {ident} - {Environment.OSVersion.VersionString}";
        }

        private void SetCheckBox(bool value)
        {
            foreach (var cb in FindControls<CheckBox>(tpOperation.Controls, "checkBox"))
            {
                cb.Checked = value;
            }
        }

        private int GetNumberOfChecked()
        {
            return FindControls<CheckBox>(tpOperation.Controls, "checkBox").Count(cb => cb.Checked);
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

            this.Text = $"{SoftwareVersion}";

            pb_device.Image = pb_device.InitialImage;
            FirmwareVersion = string.Empty;

            txt_constatus.Text = "NOT CONNECTED";
            txt_constatus.BackColor = Color.Red;
            txt_constatus.ForeColor = Color.White;
            txt_constatus.SelectionStart = 0;

            // Disable all tag slots and don't select any tag slot
            foreach (var cb in FindControls<CheckBox>(tpOperation.Controls, "checkBox"))
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
            btn_identify.Enabled = false;
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
            tfSerialHelp.AvailableCommands = null;
            tfSerialHelp.SetList();
        }

        private void DeviceConnected()
        {
            if (isConnected) return;

            this.Cursor = Cursors.WaitCursor;

            isConnected = true;

            DisplayText();

            SuspendUpdate.Suspend(tpOperation);

            // Enable all tag slots but don't select any tag slot
            foreach (var cb in FindControls<CheckBox>(tpOperation.Controls, "checkBox"))
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
            btn_identify.Enabled = false;
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

            SuspendUpdate.Resume(tpOperation);
            this.Cursor = Cursors.Default;
        }

        private void OpenChameleonSerialPort()
        {
            this.Cursor = Cursors.WaitCursor;
            pb_device.Image = pb_device.InitialImage;
            txt_output.AppendText($"{Environment.NewLine}"); // add empty line into log output

            var searcher = new ManagementObjectSearcher("select Name, DeviceID, PNPDeviceID from Win32_SerialPort where PNPDeviceID like '%VID_03EB&PID_2044%' or PNPDeviceID like '%VID_16D0&PID_04B2%'");

            foreach (var obj in searcher.Get())
            {
                var comPortStr = obj["DeviceID"].ToString();
                var pnpId = obj["PNPDeviceID"].ToString();

                _comport = new SerialPort(comPortStr, 115200)
                {
                    ReadTimeout = SerialRTimeoutMs,
                    WriteTimeout = SerialWTimeoutMs,
                    DtrEnable = true,
                    RtsEnable = true,
                };

                try
                {
                    var name = obj["Name"].ToString();
                    txt_output.AppendText($"[=] Connecting to {name} at {comPortStr}{Environment.NewLine}");
                    _comport.Open();
                }
                catch (Exception)
                {
                    txt_output.AppendText($"[!] Failed {comPortStr}{Environment.NewLine}");
                }

                if (_comport.IsOpen)
                {
                    tpOperation.SuspendLayout();
                    if (pnpId.Contains("VID_03EB&PID_2044"))
                    {
                        // revE
                        _deviceIdentification = "Firmware RevE rebooted";
                        pb_device.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("chamRevE");
                        _CurrentDevType = DeviceType.RevE;
                        _tagslotIndexOffset = 1;
                        tpOperation.Hide();
                        ConfigHMIForRevE();
                        tpOperation.Show();
                    }

                    else if (pnpId.Contains("VID_16D0&PID_04B2"))
                    {
                        // revG
                        _deviceIdentification = "Firmware RevG Official";
                        pb_device.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("chamRevG1");
                        _CurrentDevType = DeviceType.RevG;
                        _tagslotIndexOffset = 0;
                        tpOperation.Hide();
                        ConfigHMIForRevG();
                        tpOperation.Show();
                    }
                    tpOperation.ResumeLayout(true);

                    // try without the "MY" extension first
                    FirmwareVersion = SendCommand("VERSION?").ToString();
                    if (!string.IsNullOrEmpty(_firmwareVersion) && _firmwareVersion.Contains("Chameleon"))
                    {
                        _cmdExtension = string.Empty;
                        txt_output.AppendText($"[+] Success, found Chameleon Mini device on '{comPortStr}' with {_deviceIdentification} installed{Environment.NewLine}");
                        _current_comport = comPortStr;
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    FirmwareVersion = SendCommand("VERSIONMY?").ToString();
                    if (!string.IsNullOrEmpty(_firmwareVersion) && _firmwareVersion.Contains("Chameleon"))
                    {
                        _cmdExtension = "MY";
                        txt_output.AppendText($"[+] Success, found Chameleon Mini device on '{comPortStr}' with {_deviceIdentification} installed{Environment.NewLine}");
                        _current_comport = comPortStr;
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
            }

            // OK, no known USB HW-ID's found
            searcher = new ManagementObjectSearcher("select Name, DeviceID, PNPDeviceID from Win32_SerialPort");
            foreach (var obj in searcher.Get())
            {
                var comPortStr = obj["DeviceID"].ToString();

                _comport = new SerialPort(comPortStr, 115200)
                {
                    ReadTimeout = 4000,
                    WriteTimeout = 6000,
                    DtrEnable = true,
                    RtsEnable = true,
                };

                try
                {
                    var name = obj["Name"].ToString();
                    txt_output.AppendText($"[=] Connecting to {name} at {comPortStr}{Environment.NewLine}");
                    _comport.Open();
                }
                catch (Exception)
                {
                    txt_output.AppendText($"[!] Failed {comPortStr}{Environment.NewLine}");
                }

                if (!_comport.IsOpen) continue;


                _deviceIdentification = "Unknown Version";
                _CurrentDevType = DeviceType.Unknown;
                _tagslotIndexOffset = 1;

                FirmwareVersion = SendCommand("VERSION?").ToString();
                if (!string.IsNullOrEmpty(_firmwareVersion) && _firmwareVersion.Contains("Chameleon"))
                {
                    _cmdExtension = string.Empty;
                    pb_device.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("chamRevG1");
                    txt_output.AppendText($"[+] Success, found Chameleon Mini device on '{comPortStr}' with {_deviceIdentification} installed{Environment.NewLine}");
                    _current_comport = comPortStr;
                    _CurrentDevType = DeviceType.RevG;
                    ConfigHMIForRevG();
                    this.Cursor = Cursors.Default;
                    return;
                }

                FirmwareVersion = SendCommand("VERSIONMY?").ToString();
                if (!string.IsNullOrEmpty(_firmwareVersion) && _firmwareVersion.Contains("Chameleon"))
                {
                    _cmdExtension = "MY";
                    pb_device.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("chamRevE");
                    txt_output.AppendText($"[+] Success, found Chameleon Mini device on '{comPortStr}' with {_deviceIdentification} installed{Environment.NewLine}");
                    _current_comport = comPortStr;
                    _CurrentDevType = DeviceType.RevE;
                    ConfigHMIForRevE();
                    this.Cursor = Cursors.Default;
                    return;
                }

                pb_device.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("usbWarning");

                _comport.Close();

                txt_output.AppendText($"Didn't find a Chameleon on '{comPortStr}'{Environment.NewLine}");
            }
            _current_comport = string.Empty;
            this.Cursor = Cursors.Default;
            txt_output.AppendText($"Didn't find any Chameleon Mini device connected{Environment.NewLine}");
        }

        private bool SendCommandWithoutResult(string cmdText)
        {
            if (!SendCommandPossible(cmdText)) return false;
            bool isExtendedTimeout = false;
            try
            {
                if(cmdText.StartsWith("CLEAR"))
                {
                    isExtendedTimeout = true;
                    _comport.ReadTimeout = SerialRWTimeoutExtendedMs;
                    _comport.WriteTimeout = SerialRWTimeoutExtendedMs;
                }
                _comport.DiscardInBuffer();
                _comport.DiscardOutBuffer();
                var tx_data = Encoding.ASCII.GetBytes(cmdText);
                _comport.Write(tx_data, 0, tx_data.Length);
                _comport.Write("\r\n");
                if (!cmdText.StartsWith($"DETECTION{_cmdExtension}?"))
                {
                    string retCode = _comport.ReadLine();
                    // If we get an error
                    if (ErrorResponses.Any(s => retCode.Contains(s)))
                    {
                        throw new Exception(cmdText + "returned: " + retCode.Replace("\r", ""));
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = $"{Environment.NewLine}[!] {cmdText}: {ex.Message}{Environment.NewLine}";
                txt_output.AppendText(msg);
                return false;
            }
            finally
            {
                if (isExtendedTimeout)
                {
                    _comport.ReadTimeout = SerialRTimeoutMs;
                    _comport.WriteTimeout = SerialWTimeoutMs;
                }
            }
            return true;
        }

        private object SendCommand(string cmdText)
        {
            if (!SendCommandPossible(cmdText)) return string.Empty;

            try
            {
                // send command and catch return code
                if (!SendCommandWithoutResult(cmdText)) return string.Empty;

                if (cmdText.Contains($"DETECTION{_cmdExtension}?"))
                {
                    // wait to make sure data is transmitted
                    Thread.Sleep(100);

                    var rx_data = new byte[275];

                    // read the result
                    var read_count = _comport.Read(rx_data, 0, rx_data.Length);
                    if (read_count <= 0) return string.Empty;

                    var foo = new byte[read_count];
                    Array.Copy(rx_data, 0, foo, 0, read_count);
                    return foo;
                }
                else
                {
                    string read_response_line = string.Empty;

                    try
                    {
                        while (string.IsNullOrEmpty(read_response_line))
                        {
                            read_response_line = _comport.ReadLine();
                        }
                    }
                    catch(TimeoutException ex)
                    {
                        var msg = $"{Environment.NewLine}[!] {cmdText}: {ex.Message}{Environment.NewLine}";
                        txt_output.AppendText(msg);
                    }
                    
                    return read_response_line.Replace("\r", "");
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
            var full_response = string.Empty;
            try
            {
                // send command
                SendCommandWithoutResult(cmdText);
                var read_response = string.Empty;
                var receptionStarted = false;

                var start = DateTime.Now;
                var segment = DateTime.Now;

                // Read until no more data is received for 50ms
                while ((DateTime.Now.Subtract(start).TotalMilliseconds < 5000) || (receptionStarted && (DateTime.Now.Subtract(segment).TotalMilliseconds < 100)))
                {
                    read_response = _comport.ReadExisting();
                    if (!string.IsNullOrWhiteSpace(read_response))
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
            return !((_comport == null) || (!_comport.IsOpen) || string.IsNullOrWhiteSpace(cmdText));
        }

        private async Task<object> SendCommand_ICE(string cmdText)
        {
            if (string.IsNullOrWhiteSpace(cmdText)) return string.Empty;
            if (_comport == null || !_comport.IsOpen) return string.Empty;

            try
            {
                var tx_data = Encoding.ASCII.GetBytes(cmdText);
                _comport.Write(tx_data, 0, tx_data.Length);
                _comport.Write("\r\n");

                // wait to make sure data is transmitted
                Thread.Sleep(100);
                int blockLimit = 512;

                var cts = new CancellationTokenSource();
                var rx_data = new byte[blockLimit];

                var bytesread = await _comport.BaseStream.ReadAsync(rx_data, 0, blockLimit, cts.Token);

                if (bytesread <= 0) return string.Empty;

                var received = new byte[bytesread];
                Buffer.BlockCopy(rx_data, 0, received, 0, bytesread);

                if (_CurrentDevType == DeviceType.RevE && cmdText.Contains($"DETECTION{_cmdExtension}?"))
                {
                    var foo = new byte[bytesread];
                    Array.Copy(rx_data, 8, foo, 0, bytesread - 7);
                    var str = BitConverter.ToString(foo).Replace("-", " ");

                    return $"{Environment.NewLine}{str}{Environment.NewLine}";
                }

                // Check if chameleon wants to start XMODEM-Transfer
                var s = new string(Encoding.ASCII.GetChars(received));
                if (s.Contains("110:WAITING FOR XMODEM"))
                {
                    s = $"110:WAITING FOR XMODEM{Environment.NewLine}";
                    var result = ReceiveXModemData();
                    var hex = new StringBuilder(result.Length * 2);
                    hex.Append($"{Environment.NewLine}");
                    var counter = 0;

                    if (cmdText.Contains($"LOGDOWNLOAD{_cmdExtension}"))
                    {
                        return Log.LogEntryUtils.ParseDownloadedLog(result);
                    }

                    foreach (var b in result)
                    {
                        hex.AppendFormat("{0:x2} ", b);
                        counter++;

                        if (counter > this.ByteWidth - 1 )
                        {
                            counter = 0;
                            hex.Append(Environment.NewLine);
                        }
                    }

                    s += hex.ToString();
                }
                return s;
            }
            catch (Exception ex)
            {
                var msg = $"{Environment.NewLine}[!] {ex.Message}{Environment.NewLine}";
                txt_output.AppendText(msg);
                return string.Empty;
            }
        }

        private bool IsUidValid(string uid, uint uidSize, string selectedMode)
        {
            return (!string.IsNullOrEmpty(uid)) 
                && (!string.IsNullOrEmpty(selectedMode)) 
                && (Regex.IsMatch(uid, @"\A\b[0-9a-fA-F]+\b\Z"))
                && (uid.Length == uidSize*2);
        }

        private void RefreshAllSlots()
        {
            SaveActiveSlot();
            for (int i = 1; i < 9; i++)
            {
                RefreshSlot(i, false);
            }
            RestoreActiveSlot();
        }

        private void RefreshSlot(int slotIndex, bool doSaveActive = true)
        {
            if (doSaveActive) SaveActiveSlot();

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
                var tbs = FindControls<TextBox>(tpOperation.Controls, $"txt_uid{slotIndex}");
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
                var txtMemSize = FindControls<TextBox>(tpOperation.Controls, $"txt_size{slotIndex}");
                foreach (var box in txtMemSize)
                {
                    box.Text = slotMemSize;
                }
            }

            switch (_CurrentDevType)
            {
                case DeviceType.RevG:
                {
                    //CONFIGMY? -> RETURNS THE CONFIGURATION MODE
                    var slotMode = SendCommand($"CONFIG{_cmdExtension}?").ToString();
                    if (IsModeValid(slotMode))
                    {
                        FindControls<ComboBox>(tpOperation.Controls, $"cb_mode{slotIndex}").ForEach(box => box.SelectedItem = slotMode);
                    }

                    //BUTTONMY? -> RETURNS THE MODE OF THE BUTTON
                    var slotLButtonMode = SendCommand($"LBUTTON{_cmdExtension}?").ToString();
                    if (IsLButtonModeValid(slotLButtonMode))
                    {
                        FindControls<ComboBox>(tpOperation.Controls, $"cb_lbutton{slotIndex}").ForEach(box => box.SelectedItem = slotLButtonMode);
                    }

                    //BUTTON_LONGMY? -> RETURNS THE MODE OF THE BUTTON LONG
                    var slotLButtonLongMode = SendCommand($"LBUTTON_LONG{_cmdExtension}?").ToString();
                    if (IsLButtonLongModeValid(slotLButtonLongMode))
                    {
                        FindControls<ComboBox>(tpOperation.Controls, $"cb_lbuttonlong{slotIndex}").ForEach(box => box.SelectedItem = slotLButtonLongMode);
                    }

                    //BUTTONMY? -> RETURNS THE MODE OF THE BUTTON
                    var slotRButtonMode = SendCommand($"RBUTTON{_cmdExtension}?").ToString();
                    if (IsRButtonModeValid(slotRButtonMode))
                    {
                        FindControls<ComboBox>(tpOperation.Controls, $"cb_rbutton{slotIndex}").ForEach(box => box.SelectedItem = slotRButtonMode);
                    }

                    //BUTTON_LONGMY? -> RETURNS THE MODE OF THE BUTTON LONG
                    var slotRButtonLongMode = SendCommand($"RBUTTON_LONG{_cmdExtension}?").ToString();
                    if (IsRButtonLongModeValid(slotRButtonLongMode))
                    {
                        FindControls<ComboBox>(tpOperation.Controls, $"cb_rbuttonlong{slotIndex + _tagslotIndexOffset}").ForEach(box => box.SelectedItem = slotRButtonLongMode);
                    }

                    var slotGreen = SendCommand($"LEDGREEN{_cmdExtension}?").ToString();
                    if (IsLEDGreenModeValid(slotGreen))
                    {
                        FindControls<ComboBox>(tpOperation.Controls, $"cb_ledgreen{slotIndex}").ForEach(box => box.SelectedItem = slotGreen);
                    }

                    var slotRed = SendCommand($"LEDRED{_cmdExtension}?").ToString();
                    if (IsLEDRedModeValid(slotRed))
                    {
                        FindControls<ComboBox>(tpOperation.Controls, $"cb_ledred{slotIndex}").ForEach(box => box.SelectedItem = slotRed);
                    }
                    break;
                }
                default:
                {
                    //CONFIGMY? -> RETURNS THE CONFIGURATION MODE
                    var slotMode = SendCommand($"CONFIG{_cmdExtension}?").ToString();
                    if (IsModeValid(slotMode))
                    {
                        // set the combobox value of the i+1 cb_mode
                        var cbMode = FindControls<ComboBox>(tpOperation.Controls, $"cb_mode{slotIndex}");
                        foreach (var box in cbMode)
                        {
                            box.SelectedItem = slotMode;
                        }
                    }

                    //BUTTONMY? -> RETURNS THE MODE OF THE BUTTON
                    var slotLButtonMode = SendCommand($"BUTTON{_cmdExtension}?").ToString();
                    if (IsLButtonModeValid(slotLButtonMode))
                    {
                        FindControls<ComboBox>(tpOperation.Controls, $"cb_lbutton{slotIndex}").ForEach(box => box.SelectedItem = slotLButtonMode);
                    }

                    //BUTTON_LONGMY? -> RETURNS THE MODE OF THE BUTTON LONG
                    var slotLButtonLongMode = SendCommand($"BUTTON_LONG{_cmdExtension}?").ToString();
                    if (IsLButtonModeValid(slotLButtonLongMode))
                    {
                        FindControls<ComboBox>(tpOperation.Controls, $"cb_lbuttonlong{slotIndex}").ForEach(box => box.SelectedItem = slotLButtonLongMode);
                    }
                    break;
                }
            }
            if(doSaveActive) RestoreActiveSlot();
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
            var modesStr = SendCommand($"CONFIG{_cmdExtension}=?").ToString();

            if (!string.IsNullOrEmpty(modesStr))
            {
                _modesArray = modesStr.Split(',');
                if (_modesArray.Any())
                {
                    // populate all dropdowns
                    foreach (var cb in FindControls<ComboBox>(tpOperation.Controls, "cb_mode"))
                    {
                        cb.Items.Clear();
                        cb.Items.AddRange(_modesArray);
                    }
                }
            }
            // Get button modes
            var lbuttonModesStr = SendCommand($"LBUTTON{_cmdExtension}=?").ToString();
            if (string.IsNullOrEmpty(lbuttonModesStr)) return;

            _lbuttonModesArray = lbuttonModesStr.Split(',');
            if (!_lbuttonModesArray.Any()) return;

            // populate all dropdowns
            foreach (var cb in FindControls<ComboBox>(tpOperation.Controls, "cb_Lbutton"))
            {
                cb.Items.Clear();
                cb.Items.AddRange(_lbuttonModesArray);
            }

            // Get button modes
            var rbuttonModesStr = SendCommand($"RBUTTON{_cmdExtension}=?").ToString();
            if (string.IsNullOrEmpty(rbuttonModesStr)) return;

            _rbuttonModesArray = rbuttonModesStr.Split(',');
            if (!_rbuttonModesArray.Any()) return;

            // populate all dropdowns
            foreach (var cb in FindControls<ComboBox>(tpOperation.Controls, "cb_Rbutton"))
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
                foreach (var cb in FindControls<ComboBox>(tpOperation.Controls, "cb_Lbuttonlong"))
                {
                    cb.Items.Clear();
                    cb.Enabled = false;
                }
            }
            else
            {
                _lbuttonLongModesArray = lbuttonLongModesStr.Split(',');

                if (!_lbuttonLongModesArray.Any()) return;

                // populate all dropdowns
                foreach (var cb in FindControls<ComboBox>(tpOperation.Controls, "cb_lbuttonlong"))
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
                    foreach (var cb in FindControls<ComboBox>(tpOperation.Controls, "cb_rbuttonlong"))
                    {
                        cb.Items.Clear();
                        cb.Enabled = false;
                    }
                }
                else
                {
                    _rbuttonLongModesArray = rbuttonLongModesStr.Split(',');

                    if (!_rbuttonLongModesArray.Any()) return;

                    // populate all dropdowns
                    foreach (var cb in FindControls<ComboBox>(tpOperation.Controls, "cb_rbuttonlong"))
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

            _LEDGreenModesArray = LEDGreenModesStr.Split(',');
            if (!_LEDGreenModesArray.Any()) return;

            // populate all dropdowns
            foreach (var cb in FindControls<ComboBox>(tpOperation.Controls, "cb_ledgreen"))
            {
                cb.Items.Clear();
                cb.Items.AddRange(_LEDGreenModesArray);
            }

            // Get led modes
            var LEDRedModesStr = SendCommand($"LEDRED{_cmdExtension}=?").ToString();
            if (string.IsNullOrEmpty(LEDRedModesStr)) return;

            _LEDRedModesArray = LEDRedModesStr.Split(',');
            if (!_LEDRedModesArray.Any()) return;

            // populate all dropdowns
            foreach (var cb in FindControls<ComboBox>(tpOperation.Controls, "cb_ledred"))
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
                _modesArray = modesStr.Split(',');
                if (_modesArray.Any())
                {
                    // populate all dropdowns
                    foreach (var cb in FindControls<ComboBox>(tpOperation.Controls, "cb_mode"))
                    {
                        cb.Items.Clear();
                        cb.Items.AddRange(_modesArray);
                    }
                }
            }

            // Get button modes
            var lbuttonModesStr = SendCommand($"BUTTON{_cmdExtension}").ToString();
            if (string.IsNullOrEmpty(lbuttonModesStr)) return;

            _lbuttonModesArray = lbuttonModesStr.Split(',');
            if (!_lbuttonModesArray.Any()) return;

            // populate all dropdowns
            foreach (var cb in FindControls<ComboBox>(tpOperation.Controls, "cb_Lbutton"))
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
                foreach (var cb in FindControls<ComboBox>(tpOperation.Controls, "cb_Lbuttonlong"))
                {
                    cb.Items.Clear();
                    cb.Enabled = false;
                }
            }
            else
            {
                _lbuttonLongModesArray = lbuttonLongModesStr.Split(',');

                if (!_lbuttonLongModesArray.Any()) return;

                // populate all dropdowns
                foreach (var cb in FindControls<ComboBox>(tpOperation.Controls, "cb_lbuttonlong"))
                {
                    cb.Enabled = true;
                    cb.Items.Clear();
                    cb.Items.AddRange(_lbuttonLongModesArray);
                }
            }
        }

        private void GetSupportedModes()
        {
            SuspendUpdate.Suspend(tpOperation);
            switch (_CurrentDevType)
            {
                case DeviceType.RevG:
                    SetRevGButtons();
                    break;
                case DeviceType.RevE:
                    SetRevEButtons();
                    break;
            }
            SuspendUpdate.Resume(tpOperation);
        }

        private void GetAvailableCommands()
        {
            var cmd = $"HELP{_cmdExtension}";

            var result = SendCommand(cmd).ToString();
            if (string.IsNullOrEmpty(result)) return;

            var helpArray = result.Split(',');
            if (!helpArray.Any()) return;

            Array.Sort(helpArray);

            AvailableCommands.Clear();
            AvailableCommands.AddRange(helpArray);
        }

        private class DumpData
        {
            public byte[] Data;
            public byte[] Extra;
        }

        private static DumpData ReadFileIntoByteArray(string filename)
        {
            var fi = new FileInfo(filename);
            if (!fi.Exists) return null;

            var dumpStrategy = DumpStrategyFactory.Create(fi.FullName);
            var data = dumpStrategy.Read();

            // most likely Mifare Classic card.
            if (data.Length >= 1024 || data.Length == 320)
                return new DumpData() { Data = data, Extra = new byte[] { } };

            // new format of ev1/ntag dump header
            if (MifareUltralightModel.HasUltralightNewHeader(data))
            {
                // extract header data into pages out of dump
                var getVersion = data.Skip(0).Take(8);
                var signature = data.Skip(12).Take(32);
                var counters = data.Skip(44).Take(12);
                return
                    new DumpData()
                    {
                        Data = data.Skip(MifareUltralightCardInfo.NewPrefixLength).ToArray(),
                        Extra = counters.Concat(getVersion).Concat(signature).ToArray()
                    };
            }
            // ultralight/ntag based dump might have a header
            if (MifareUltralightModel.HasUltralightHeader(data) )
            {
                // extract header data into pages out of dump
                var getVersion = data.Skip(0).Take(8);
                var counters = new byte[] {0, 0, 0}.Concat(data.Skip(10).Take(1)).
                    Concat(new byte[] { 0, 0, 0 }).Concat(data.Skip(11).Take(1)).
                    Concat(new byte[] { 0, 0, 0 }).Concat(data.Skip(12).Take(1));
                var signature = data.Skip(16).Take(32);
                return
                    new DumpData()
                    {
                        Data = data.Skip(MifareUltralightCardInfo.PrefixLength).ToArray(),
                        Extra = counters.Concat(getVersion).Concat(signature).ToArray()
                    };
            }
            // for general purpose return zero-filled extra datas
            return new DumpData() { Data = data, Extra = Enumerable.Repeat((byte)0, 52).ToArray() };
        }

        internal void UploadDump(string filename)
        {
            var dump = ReadFileIntoByteArray(filename);

            // Try to identify the dump type
            IndentifyDumpTypeBySize(dump);

            var xmodem = new XMODEM(_comport, XMODEM.Variants.XModemChecksum);

            SendCommandWithoutResult($"UPLOAD{_cmdExtension}");

            var bytes = dump.Data.Concat(dump.Extra).ToArray();
            
            int numBytesSuccessfullySent = xmodem.Send(bytes);

            if (numBytesSuccessfullySent == bytes.Length && xmodem.TerminationReason == XMODEM.TerminationReasonEnum.EndOfFile)
            {
                var msg = $"[+] File upload ok{Environment.NewLine}";
                Console.WriteLine(msg);
                txt_output.AppendText(msg);
            }
            else
            {
                var msg = $"[!] Failed to upload file{Environment.NewLine}";
                MessageBox.Show(msg);
                txt_output.AppendText(msg);
            }
        }

        private int IdentifyUIDSize(byte[] bytes)
        {
            // we accept more 16 bytes dump
            // try to check single UID size
            byte bcc = (byte) (bytes[0] ^ bytes[1] ^ bytes[2] ^ bytes[3]);
            if (bcc == bytes[4] && (bytes[6] & 0xc0) == 0)
                return 4;
            // try to check double UID size
            if ((bytes[8] & 0xc0) == 0x40)
                return 7;
            // by default single UID size
            return 4;
        }

        private void IndentifyDumpTypeBySize(DumpData dump)
        {
            switch (dump.Data.Length)
            {
                case 4096:
                    SendCommandWithoutResult(IdentifyUIDSize(dump.Data) == 7 ? $"CONFIG{_cmdExtension}=MF_CLASSIC_4K_7B" : $"CONFIG{_cmdExtension}=MF_CLASSIC_4K");
                    break;
                case 64:
                    SendCommandWithoutResult($"CONFIG{_cmdExtension}=MF_ULTRALIGHT");
                    break;
                case 80:
                    SendCommandWithoutResult($"CONFIG{_cmdExtension}=MF_ULTRALIGHT_EV1_80B");
                    break;
                case 164:
                    SendCommandWithoutResult($"CONFIG{_cmdExtension}=MF_ULTRALIGHT_EV1_164B");
                    break;
                // case for NTAG21X
                case 180:
                    SendCommandWithoutResult($"CONFIG{_cmdExtension}=NTAG213");
                    break;
                case 540:
                    SendCommandWithoutResult($"CONFIG{_cmdExtension}=NTAG215");
                    break;
                case 924:
                    SendCommandWithoutResult($"CONFIG{_cmdExtension}=NTAG216");
                    break;
                // default to m1
                default:
                    SendCommandWithoutResult(IdentifyUIDSize(dump.Data) == 7 ? $"CONFIG{_cmdExtension}=MF_CLASSIC_1K_7B" : $"CONFIG{_cmdExtension}=MF_CLASSIC_1K");
                    break;
            }
        }

        internal void DownloadAndSaveDump(string filename)
        {
            // First get the current memory size of the slot
            var memsizeStr = SendCommand($"MEMSIZE{_cmdExtension}?").ToString();

            // Default value
            int memsize = 4096;
            int extrasize = 0;

            if (!string.IsNullOrEmpty(memsizeStr))
            {
                int.TryParse(memsizeStr, out memsize);
            }

            // Also check if the tag is UL to save the counters too
            var configStr = SendCommand($"CONFIG{_cmdExtension}?").ToString();
            if (!string.IsNullOrWhiteSpace(configStr) && (configStr.Contains("ULTRALIGHT_EV1")))
            {
                if (memsize < 4069)
                {
                    // 52 bytes extra data = 3 * 4 (counters, tearing) + 8 (version) + 32 (signature)
                    extrasize = 52;
                }
            }

            // Then send the download command
            SendCommandWithoutResult($"DOWNLOAD{_cmdExtension}");

            var bytes = ReceiveXModemData();
            if (bytes != null)
            {
                var msg = $"[+] File download from device ok{Environment.NewLine}";
                Console.WriteLine(msg);
                txt_output.AppendText(msg);

                byte[] neededBytes = bytes;
                byte[] extraBytes = new byte[] { };

                if (bytes.Length > memsize)
                {
                    // Create a new array same size as memsize
                    neededBytes = new byte[memsize];
                    Array.Copy(bytes, neededBytes, neededBytes.Length);
                    // Create extra data
                    if (bytes.Length >= memsize + extrasize)
                    {
                        extraBytes = new byte[extrasize];
                        Array.Copy(bytes.Skip(memsize).ToArray(), extraBytes, extrasize);
                    }
                }

                if (neededBytes.Length < 1024 && neededBytes.Length != 320 && neededBytes.Length % 4 == 0 && extraBytes.Length > 0)
                {
                    // Construct dump with new heder format
                    var counters = extraBytes.Skip(0).Take(12);
                    var version = extraBytes.Skip(12).Take(8);
                    var signature = extraBytes.Skip(20).Take(32);
                    byte[] pagecount = new byte[] { (byte)(neededBytes.Length / 4 - 1)};
                    neededBytes =  version
                        .Concat(Enumerable.Repeat((byte)0, 3)).Concat(pagecount)
                        .Concat(signature).Concat(counters)
                        .Concat(neededBytes)
                        .ToArray();
                }
                // Write the actual file
                var dumpStrategy = DumpStrategyFactory.Create(filename);
                dumpStrategy?.Save(neededBytes);

                msg = $"[+] File saved to {filename}{Environment.NewLine}";
                Console.WriteLine(msg);
                txt_output.AppendText(msg);
            }
            else
            {
                // Something went wrong during the transfer
                var msg = $"[!] Failed to save dump{Environment.NewLine}";
                MessageBox.Show(msg);
                txt_output.AppendText(msg);
            }
        }

        private byte[] ReceiveXModemData()
        {
            byte[] response;

            var xmodem = new XMODEM(_comport, XMODEM.Variants.XModemChecksum);
            var ms = new MemoryStream();
            var reason = xmodem.Receive(ms);

            if (reason == XMODEM.TerminationReasonEnum.EndOfFile)
            {
                var bytes = ms.ToArray();

                // Strip away the SUB (byte value 26) padding bytes
                bytes = xmodem.TrimPaddingBytesFromEnd(bytes);

                response = new byte[bytes.Length];

                Array.Copy(bytes, response, response.Length);
            }
            else
            {
                // Something went wrong during the transfer
                response = null;
            }
            return response;
        }

        private void InitTimer()
        {
            int tickInterval = 0;
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += timer1_Tick;

            if (!int.TryParse(txt_interval.Text, out tickInterval)) return;

            timer1.Interval = tickInterval > 0 ? tickInterval : Default.KeepAliveInterval;

            timer1.Start();
        }

        void SaveFile(HexBox hexBox)
        {
            if (hexBox.ByteProvider == null) return;

            int idx;
            if (!int.TryParse(hexBox.Name.Substring(hexBox.Name.Length - 1), out idx)) return;

            var l = FindControls<Label>(Controls, $"lbl_hbfilename{idx}").FirstOrDefault();

            try
            {
                var dynamicFileByteProvider = hexBox.ByteProvider;
                dynamicFileByteProvider?.ApplyChanges();

                txt_output.AppendText($"[+] Saved file {l?.Text}{Environment.NewLine}");
            }
            catch (Exception)
            {
                var msg = $"[!] Failed to save file {l?.Text}{Environment.NewLine}";
                MessageBox.Show(msg);
                txt_output.AppendText(msg);
            }
        }

        public void OpenFile(string fileName, HexBox hexBox)
        {
            if (!File.Exists(fileName))
            {
                var msg = $"[!] Failed to open - File does not exist{Environment.NewLine}";
                MessageBox.Show(msg);
                txt_output.AppendText(msg);
                return;
            }

            if (CloseFile(hexBox) == DialogResult.Cancel)
                return;

            var fi = new FileInfo(fileName);


            var detect_width = false;

            rbtn_bytewidth04.Checked = false;
            rbtn_bytewidth08.Checked = false;
            rbtn_bytewidth16.Checked = false;

            // iclass dumps should be 8bytes width
            if (fileName.ToLower().Contains("iclass"))
            {
                rbtn_bytewidth08.Checked = true;
                detect_width = true;
            }
            else if (fileName.ToLower().Contains("hf-mf"))
            {
                rbtn_bytewidth16.Checked = true;
                detect_width = true;
            }
            else if (fileName.ToLower().Contains("hf-mfu"))
            {
                rbtn_bytewidth04.Checked = true;
                detect_width = true;
            }

            try
            {
                CleanUpHexBox(hexBox);

                // try to open in write mode
                hexBox.ByteProvider = new DumpFileByteProvider(fi.FullName);

                if (!detect_width)
                {
                    switch (hexBox.ByteProvider.Length)
                    {
                        case 320:
                        case 1024:
                        case 4096:
                            rbtn_bytewidth16.Checked = true;
                            break;
                        default:
                            rbtn_bytewidth04.Checked = true;
                            break;
                    }
                }

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
                txt_output.AppendText(msg);
            }
            catch (IOException ex) // write mode failed
            {
                // file cannot be opened
                var msg = $"[!] Failed to open file{Environment.NewLine}{ex.Message}{Environment.NewLine}";
                MessageBox.Show(msg);
                txt_output.AppendText(msg);
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
                txt_output.AppendText($"[+] Closed file {l.Text}{Environment.NewLine}");
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
            var list = FindControls<ComboBox>(tpOperation.Controls, "cb_Rbutton");
            list.ForEach(a => ApplyAll(a, c => { c.Visible = false; }));

            list = FindControls<ComboBox>(tpOperation.Controls, "cb_Lbutton");
            list.ForEach(a => ApplyAll(a, c =>
            {
                c.Visible = true;
                c.Width = REVEDefaultComboWidth;
            }));

            list = FindControls<ComboBox>(tpOperation.Controls, "cb_Rbuttonlong");
            list.ForEach(a => ApplyAll(a, c => { c.Visible = false; }));

            list = FindControls<ComboBox>(tpOperation.Controls, "cb_Lbuttonlong");
            list.ForEach(a => ApplyAll(a, c =>
            {
                c.Visible = true;
                c.Width = REVEDefaultComboWidth;
            }));

            list = FindControls<ComboBox>(tpOperation.Controls, "cb_ledgreen");
            list.ForEach(a => ApplyAll(a, c =>
            {
                c.Visible = false;
                c.Enabled = false;
            }));

            list = FindControls<ComboBox>(tpOperation.Controls, "cb_ledred");
            list.ForEach(a => ApplyAll(a, c =>
            {
                c.Visible = false;
                c.Enabled = false;
            }));

            btn_identify.Visible = false;
            btn_keycalc.Visible = true;

            for (int cidx = 1; cidx < 9; cidx++)
            {
                var gpbx = (GroupBox) this.tpOperation.Controls.Find($"gb_tagslot{cidx}", true).First();
                var pnl = (TableLayoutPanel) gpbx.Controls[$"tableLayoutPanel{cidx}"];
                pnl.SetColumnSpan(pnl.Controls[$"cb_Lbutton{cidx}"], 2);
                pnl.SetColumnSpan(pnl.Controls[$"cb_Lbuttonlong{cidx}"], 2);
                pnl.RowStyles[4].Height = 0;
                pnl.RowStyles[5].Height = 0;
                gpbx.Size = new Size(278, 181);
            }
        }

        private void ConfigHMIForRevG()
        {
            var list = FindControls<ComboBox>(tpOperation.Controls, "cb_Rbutton");
            list.ForEach(a => ApplyAll(a, c =>
            {
                c.Visible = true;
                c.Width = REVGDefaultComboWidth;
            }));

            list = FindControls<ComboBox>(tpOperation.Controls, "cb_Lbutton");
            list.ForEach(a => ApplyAll(a, c =>
            {
                c.Visible = true;
                c.Width = REVGDefaultComboWidth;
            }));

            list = FindControls<ComboBox>(tpOperation.Controls, "cb_Rbuttonlong");
            list.ForEach(a => ApplyAll(a, c =>
            {
                c.Visible = true;
                c.Width = REVGDefaultComboWidth;
            }));

            list = FindControls<ComboBox>(tpOperation.Controls, "cb_Lbuttonlong");
            list.ForEach(a => ApplyAll(a, c =>
            {
                c.Visible = true;
                c.Width = REVGDefaultComboWidth;
            }));

            list = FindControls<ComboBox>(tpOperation.Controls, "cb_ledgreen");
            list.ForEach(a => ApplyAll(a, c =>
            {
                c.Visible = true;
                c.Enabled = true;
            }));

            list = FindControls<ComboBox>(tpOperation.Controls, "cb_ledred");
            list.ForEach(a => ApplyAll(a, c =>
            {
                c.Visible = true;
                c.Enabled = true;
            }));

            btn_identify.Visible = true;

            // is MFKEY32 button visible / enabled?
            // find if MF_DETECTED is supported by running CONFIG? 
            // 
            var detection_str = SendCommand($"HELP{_cmdExtension}").ToString().ToLower();
            var support_detection = detection_str.Contains("detection");

            btn_keycalc.Visible = support_detection;

            if (support_detection == false)
            {
                txt_output.Text += $"[!] Firmware doesn't support DETECTION cmd -- mfkey32 key recovery is not possible{ Environment.NewLine}";
            }

            for (int cidx = 1; cidx < 9; cidx++)
            {
                var gpbx = (GroupBox) this.tpOperation.Controls.Find($"gb_tagslot{cidx}", true).First();
                var pnl = (TableLayoutPanel) gpbx.Controls[$"tableLayoutPanel{cidx}"];
                pnl.SetColumnSpan(pnl.Controls[$"cb_Lbutton{cidx}"], 1);
                pnl.SetColumnSpan(pnl.Controls[$"cb_Lbuttonlong{cidx}"], 1);
                pnl.RowStyles[4].Height = 30;
                pnl.RowStyles[5].Height = 30;
                gpbx.Size = new Size(278, 242);
            }
        }

        private void HighlightActiveSlot()
        {
            foreach (var gb in FindControls<GroupBoxEnhanced>(tpOperation.Controls, "gb_tagslot"))
            {
                var tagslotIndex = int.Parse(gb.Name.Substring(gb.Name.Length - 1));
                if (tagslotIndex <= 0) continue;

                if (gb.Name.ToLower().StartsWith($"gb_tagslot{_active_selected_slot}"))
                {
                    gb.BorderColor = Color.Green;
                    gb.BorderColorLight = Color.AntiqueWhite;
                }
                else
                {
                    gb.BorderColor = SystemColors.ControlLight;
                    gb.BorderColorLight = SystemColors.ControlLightLight;
                }
            }
            GroupBoxEnhanced.RedrawGroupBoxDisplay(tpOperation);
        }

        /// <summary>
        /// save the active slot.  Use before all
        /// </summary>
        private bool SaveActiveSlot()
        {
            // Determine which slot is active
            var actSetting = SendCommand($"SETTING{_cmdExtension}?").ToString();

            // return false if empty answer or set default value (ex. 0)
            if (actSetting.Length <= 0) return false;// or actSetting = "NO.0";

            int slotindex;
            if (int.TryParse(actSetting.Substring(actSetting.Length - 1), out slotindex))
            {
                _active_selected_slot = slotindex + _tagslotIndexOffset;
                return true;
            }
            return false;
        }

        private void RestoreActiveSlot()
        {
            // Check if saved active slot is within the limit (0...7)
            if (_active_selected_slot < _tagslotIndexOffset) _active_selected_slot = _tagslotIndexOffset;
            if (_active_selected_slot > _tagslotIndexOffset+7) _active_selected_slot = _tagslotIndexOffset+7;

            SendCommandWithoutResult($"SETTING{_cmdExtension}={_active_selected_slot - _tagslotIndexOffset}");
            HighlightActiveSlot();
        }

        // Disconnect device clean
        private void DisconnectDevice()
        {
            if (!isConnected) return;

            if (_comport != null && _comport.IsOpen)
            {
                try
                {
                    _comport.Close();
                }
                catch
                {
                }

                _comport = null;
                disconnectPressed = true;
            }
            DeviceDisconnected();
        }
        #endregion

    }
}
