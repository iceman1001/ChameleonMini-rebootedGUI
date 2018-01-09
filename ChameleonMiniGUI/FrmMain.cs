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

namespace ChameleonMiniGUI
{
    public partial class frm_main : Form
    {

        private SerialPort _comport = null;
        private string[] _modesArray = null;
        private string[] _buttonModesArray = null;
        private string _cmdExtension = "MY";
        public frm_main()
        {
            InitializeComponent();
        }

        #region Event Handlers
        private void frm_main_Load(object sender, EventArgs e)
        {
            // Find the COM port of the Chameleon (wait reply of VERSIONMY? from every available port)
            ConnectToChameleon();

            if (_comport != null && _comport.IsOpen)
            {
                // Get all available modes and populate the dropdowns
                GetSupportedModes();

                // Refresh all
                RefreshAllSlots();
            }
        }

        private void frm_main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_comport != null && _comport.IsOpen)
            {
                _comport.Close();
            }
        }

        private void btn_apply_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;

            var index = int.Parse(btn.Name.Substring(btn.Name.Length - 1));
            if (index <= 0) return;

            //SETTINGMY=tagslotIndex-1
            SendCommandWithoutResult($"SETTING{_cmdExtension}=" + (index - 1));

            //SETTINGMY? -> SHOULD BE "NO."+tagslotIndex
            var selectedSlot = SendCommand($"SETTING{_cmdExtension}?").ToString();
            if (!selectedSlot.Contains((index - 1).ToString())) return;


            var selectedMode = string.Empty;

            // Set the mode of the selected slot
            var cb_mode = FindControls<ComboBox>(Controls, $"cb_mode{index}").FirstOrDefault();
            if (cb_mode != null) 
            {
                //CONFIGMY=cb_mode.SelectedItem
                SendCommandWithoutResult($"CONFIG{_cmdExtension}={cb_mode.SelectedItem}");
                selectedMode = cb_mode.SelectedItem.ToString();
            }

            // Set the button mode of the selected slot
            var cb_button = FindControls<ComboBox>(Controls, $"cb_button{index}").FirstOrDefault();
            if (cb_button != null)
            {
                //BUTTONMY=cb_buttonMode.SelectedItem
                SendCommandWithoutResult($"BUTTON{_cmdExtension}={cb_button.SelectedItem}");
            }

            // Set the UID
            var txtUid = FindControls<TextBox>(Controls, $"txt_uid{index}").FirstOrDefault();
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

            RefreshSlot(index - 1);
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            if (_comport == null)
            {
                // Try to connect first
                ConnectToChameleon();
            }

            if (_comport != null && _comport.IsOpen)
            {
                if (_modesArray == null || _buttonModesArray == null)
                {
                    GetSupportedModes();
                }

                RefreshAllSlots();
            }
            else
            {
                MessageBox.Show("Problem communicating with Chameleon", "Error communicating", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_bootmode_Click(object sender, EventArgs e)
        {
            SendCommandWithoutResult("UPGRADE" + _cmdExtension);
            try
            {
                _comport.Close();
            }
            catch (Exception)
            {
                //
            }
            _comport = null;
            this.Text = "Device disconnected";
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
            var btn = sender as Button;
            if (btn == null) return;

            int tagslotIndex = int.Parse(btn.Name.Substring(btn.Name.Length - 1));
            if (tagslotIndex <= 0) return;

            // select the corresponding slot
            SendCommandWithoutResult($"SETTING{_cmdExtension}=" + (tagslotIndex - 1));

            // Open dialog
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var dumpFilename = openFileDialog1.FileName;

                // Load the dump
                LoadDump(dumpFilename);

                // Refresh slot
                RefreshSlot(tagslotIndex - 1);
            }
        }

        private void btn_download_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;

            int tagslotIndex = int.Parse(btn.Name.Substring(btn.Name.Length - 1));
            if (tagslotIndex <= 0) return;

            // select the corresponding slot
            SendCommandWithoutResult("SETTING" + _cmdExtension + "=" + (tagslotIndex - 1));

            // Save dialog
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var dumpFilename = saveFileDialog1.FileName;

                // Add extension if missing
                dumpFilename = !dumpFilename.ToLower().Contains(".bin") ? dumpFilename + ".bin" : dumpFilename;

                // Save the dump
                SaveDump(dumpFilename);

                // Refresh slot
                RefreshSlot(tagslotIndex - 1);
            }
        }

        private void btn_mfkey_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;

            int tagslotIndex = int.Parse(btn.Name.Substring(btn.Name.Length - 1));
            if (tagslotIndex <= 0) return;

            //SETTINGMY=tagslotIndex-1
            SendCommandWithoutResult($"SETTING{_cmdExtension}=" + (tagslotIndex - 1));

            var data = SendCommand($"DETECTION{_cmdExtension}?") as byte[];

            string result = MfKeyAttacks.Attack(data);

            txt_output.Text = result;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;

            int tagslotIndex = int.Parse(btn.Name.Substring(btn.Name.Length - 1));
            if (tagslotIndex <= 0) return;

            //SETTINGMY=tagslotIndex-1
            SendCommandWithoutResult($"SETTING{_cmdExtension}=" + (tagslotIndex - 1));

            // DETECTIONMY = CLOSED
            SendCommandWithoutResult($"DETECTION{_cmdExtension}=CLOSED");

            txt_output.Clear();
        }

        #endregion

        #region Helper methods
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

        // Alternative open serial port helper method
        private void OpenChameleonSerialPort()
        {
            var searcher = new ManagementObjectSearcher("select DeviceID from Win32_SerialPort where Description = \"ChameleonMini Virtual Serial Port\"");
            foreach (var obj in searcher.Get())
            {
                string comPortStr = (string)obj["DeviceID"];

                _comport = new SerialPort(comPortStr, 115200);
                _comport.ReadTimeout = 4000;
                _comport.WriteTimeout = 6000;
            }

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
                        return;
                    }

                    version = SendCommand("VERSIONMY?") as string;
                    if (!string.IsNullOrEmpty(version) && version.Contains("Chameleon"))
                    {
                        _cmdExtension = "MY";
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                
            }

            if (_comport == null)
            {
                throw new Exception("Unable to find ChameleonMini device.");
            }
        }

        private void SendCommandWithoutResult(string cmdText)
        {
            if (string.IsNullOrWhiteSpace(cmdText)) return;
            if (_comport == null || !_comport.IsOpen) return;

            // send command
            var tx_data = Encoding.ASCII.GetBytes(cmdText);
            _comport.Write(tx_data, 0, tx_data.Length);
            _comport.Write("\r\n");
        }

        private object SendCommand(string cmdText)
        {
            if (string.IsNullOrWhiteSpace(cmdText)) return string.Empty;
            if (_comport == null || !_comport.IsOpen) return string.Empty;

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

        private bool IsUidValid(string uid, string selectedMode)
        {
            if (!Regex.IsMatch(uid, @"\A\b[0-9a-fA-F]+\b\Z")) return false;

            // TODO: We could also find out the UID size with the UIDSIZEMY cmd

            // if mode is classic then UID must be 4 bytes (8 hex digits) long
            if (selectedMode == "MF_CLASSIC_1K" || selectedMode == "MF_CLASSIC_4K")
            {
                if (uid.Length == 8)
                {
                    return true;
                }
            }

            // if mode is ul then UID must be 7 bytes (14 hex digits) long
            if (selectedMode == "MF_ULTRALIGHT" || selectedMode == "MF_ULTRALIGHT_EV1_80B" || selectedMode == "MF_ULTRALIGHT_EV1_164B")
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
                    }
                }
            }

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

            // First send the download command
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

                // TODO: Check if we should determine the size with MEMSIZEMY cmd

                // Transfer successful, so convert MemoryStream to byte array
                var bytes = ms.ToArray();

                // Strip away the SUB (byte value 26) padding bytes
                bytes = xmodem.TrimPaddingBytesFromEnd(bytes);

                // Write the actual file
                File.WriteAllBytes(filename, bytes);

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

       #endregion
    }
}
