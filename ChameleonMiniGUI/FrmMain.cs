using System;
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

namespace ChameleonMiniGUI
{
    public partial class frm_main : Form
    {
        private SerialPort _comport = null;
        private string[] modesArray = null;
        private string[] buttonModesArray = null;

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
            var applyButtonClicked = sender as Button;

            int tagslotIndex = int.Parse(applyButtonClicked.Name.Substring(applyButtonClicked.Name.Length - 1));
            if (tagslotIndex <= 0) return;

            //SETTINGMY=tagslotIndex-1
            SendCommandWithoutResult("SETTINGMY=" + (tagslotIndex - 1));

            //SETTINGMY? -> SHOULD BE "NO."+tagslotIndex
            string selectedSlot = SendCommand("SETTINGMY?");
            if ((selectedSlot != null) && (selectedSlot.Contains("NO." + (tagslotIndex - 1))))
            {
                // Set the mode of the selected slot
                ComboBox cb_mode = (ComboBox)(applyButtonClicked.Parent.Controls["cb_mode" + tagslotIndex]);

                if (cb_mode != null) {
                    //CONFIGMY=cb_mode.SelectedItem
                    SendCommandWithoutResult("CONFIGMY=" + cb_mode.SelectedItem);

                    // Set the UID
                    TextBox txtUid = (TextBox)(applyButtonClicked.Parent.Controls["txt_uid" + tagslotIndex]);
                    if (txtUid != null)
                    {
                        string uid = txtUid.Text;
                        if (!string.IsNullOrEmpty(uid) && IsUidValid(uid, (string)cb_mode.SelectedItem))
                        {
                            SendCommandWithoutResult("UIDMY=" + uid);
                        }
                    }
                }

                // Set the button mode of the selected slot
                ComboBox cb_buttonMode = (ComboBox)(applyButtonClicked.Parent.Controls["cb_button" + tagslotIndex]);

                if (cb_buttonMode != null)
                {
                    //BUTTONMY=cb_buttonMode.SelectedItem
                    SendCommandWithoutResult("BUTTONMY=" + cb_buttonMode.SelectedItem);
                }

                RefreshSlot(tagslotIndex - 1);
            }
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
                if (modesArray == null || buttonModesArray == null)
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
            SendCommandWithoutResult("UPGRADEMY");
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
            string dumpFilename = null;

            Button uploadButtonClicked = sender as Button;

            int tagslotIndex = int.Parse(uploadButtonClicked.Name.Substring(uploadButtonClicked.Name.Length - 1));
            if (tagslotIndex > 0)
            {
                // select the corresponding slot
                SendCommandWithoutResult("SETTINGMY=" + (tagslotIndex - 1));

                // Open dialog
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    dumpFilename = openFileDialog1.FileName;
                }

                if (dumpFilename != null)
                {
                    // Load the dump
                    LoadDump(dumpFilename);

                    // Refresh slot
                    RefreshSlot(tagslotIndex - 1);
                }
            }
        }

        private void btn_download_Click(object sender, EventArgs e)
        {
            string dumpFilename = null;

            Button downloadButtonClicked = sender as Button;

            int tagslotIndex = int.Parse(downloadButtonClicked.Name.Substring(downloadButtonClicked.Name.Length - 1));
            if (tagslotIndex > 0)
            {
                // select the corresponding slot
                SendCommandWithoutResult("SETTINGMY=" + (tagslotIndex - 1));

                // Save dialog
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    dumpFilename = saveFileDialog1.FileName;
                }

                if (dumpFilename != null)
                {
                    // Add extension if missing
                    dumpFilename = (!dumpFilename.ToLower().Contains(".dump") ? dumpFilename = dumpFilename + ".dump" : dumpFilename);

                    // Save the dump
                    SaveDump(dumpFilename);

                    // Refresh slot
                    RefreshSlot(tagslotIndex - 1);
                }
            }
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
                        string version = SendCommand("VERSIONMY?");
                        if (!string.IsNullOrEmpty(version) && version.Contains("Chameleon"))
                        {
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
            }

            try
            {
                _comport.Open();
            }
            catch (Exception) { }

            if (_comport == null)
            {
                throw new Exception("Unable to find ChameleonMini device.");
            }
        }

        private void SendCommandWithoutResult(String cmdText)
        {
            if (_comport == null || !_comport.IsOpen) return;

            // send command
            var tx_data = Encoding.UTF8.GetBytes(cmdText);
            _comport.Write(tx_data, 0, tx_data.Length);
            _comport.Write("\r\n");
        }

        private string SendCommand(string cmdText)
        {
            if (_comport == null || !_comport.IsOpen) return null;

            int read_count = 0;
            byte[] tx_data;
            byte[] rx_data = new byte[100];

            // send command
            tx_data = Encoding.UTF8.GetBytes(cmdText);
            _comport.Write(tx_data, 0, tx_data.Length);
            _comport.Write("\r\n");

            // wait to make sure data is transmitted
            Thread.Sleep(100);

            // read the result
            read_count = _comport.Read(rx_data, 0, rx_data.Length);

            if (read_count > 0)
            {
                var result = new string(Encoding.UTF8.GetChars(rx_data));
                if (!string.IsNullOrEmpty(result))
                {
                    result = result.Replace("101:OK WITH TEXT", "");
                    result = Regex.Replace(result, @"\p{C}+", string.Empty);
                    return result;
                }

            }

            return null;
        }

        private bool IsUidValid(string uid, string selectedMode)
        {
            if (string.IsNullOrEmpty(uid)) return false;

            if (!Regex.IsMatch(uid, @"\A\b[0-9a-fA-F]+\b\Z")) return false;

            // if mode is classic then UID must be 4 bytes (8 hex digits) long
            if (selectedMode == "MF_CLASSIC_1K" || selectedMode == "MF_CLASSIC_4K")
            {
                if (uid.Length == 8)
                {
                    return true;
                }
            }
                    
            // if mode is ul then UID must be 7 bytes (14 hex digits) long
            if (selectedMode == "MF_ULTRALIGHT")
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
            SendCommandWithoutResult("SETTINGMY=" + slotIndex);

            //SETTINGMY? -> SHOULD BE "NO."+i
            var selectedSlot = SendCommand("SETTINGMY?");
            if ((selectedSlot != null) && (selectedSlot.Contains("NO." + slotIndex)))
            {
                var gbTagSlot = (GroupBox)this.Controls["gb_tagslot" + (slotIndex + 1)];

                if (gbTagSlot == null) return;


                //CONFIGMY? -> RETURNS THE CONFIGURATION MODE
                var slotMode = SendCommand("CONFIGMY?");

                if (slotMode != null && IsModeValid(slotMode))
                {
                    // set the combobox value of the i+1 cb_mode
                    var cbMode = (ComboBox)gbTagSlot.Controls["cb_mode" + (slotIndex + 1)];
                    if (cbMode != null)
                    {
                        cbMode.SelectedItem = slotMode;
                    }
                }

                //UIDMY? -> RETURNS THE UID
                var slotUid = SendCommand("UIDMY?");
                if (slotUid != null)
                {
                    // set the textbox value of the i+1 txt_uid
                    var txtUid = (TextBox)gbTagSlot.Controls["txt_uid" + (slotIndex + 1)];
                    if (txtUid != null)
                    {
                        txtUid.Text = slotUid;
                    }
                }

                //BUTTONMY? -> RETURNS THE MODE OF THE BUTTON
                var slotButtonMode = SendCommand("BUTTONMY?");
                if (slotButtonMode != null && IsButtonModeValid(slotButtonMode))
                {
                    // set the combobox value of the i+1 cb_button
                    var cbButton = (ComboBox)gbTagSlot.Controls["cb_button" + (slotIndex + 1)];
                    if (cbButton != null)
                    {
                        cbButton.SelectedItem = slotButtonMode;
                    }
                }
            }
        }

        private bool IsButtonModeValid(string slotButtonMode)
        {
            if (buttonModesArray.Contains(slotButtonMode))
            {
                return true;
            }

            return false;
        }

        private bool IsModeValid(string slotMode)
        {
            if (modesArray.Contains(slotMode))
            {
                return true;
            }

            return false;
        }

        private void GetSupportedModes()
        {
            string resultModesStr = SendCommand("CONFIGMY");

            if (!String.IsNullOrEmpty(resultModesStr))
            {
                // split by comma
                modesArray = resultModesStr.Split(',');

                if (modesArray.Length > 0)
                {
                    // populate all dropdowns
                    this.cb_mode1.Items.Clear();
                    this.cb_mode1.Items.AddRange(modesArray);
                    this.cb_mode2.Items.Clear();
                    this.cb_mode2.Items.AddRange(modesArray);
                    this.cb_mode3.Items.Clear();
                    this.cb_mode3.Items.AddRange(modesArray);
                    this.cb_mode4.Items.Clear();
                    this.cb_mode4.Items.AddRange(modesArray);
                    this.cb_mode5.Items.Clear();
                    this.cb_mode5.Items.AddRange(modesArray);
                    this.cb_mode6.Items.Clear();
                    this.cb_mode6.Items.AddRange(modesArray);
                    this.cb_mode7.Items.Clear();
                    this.cb_mode7.Items.AddRange(modesArray);
                    this.cb_mode8.Items.Clear();
                    this.cb_mode8.Items.AddRange(modesArray);
                }
            }

            string resultButtonModesStr = SendCommand("BUTTONMY");

            if (!String.IsNullOrEmpty(resultButtonModesStr))
            {
                // split by comma
                buttonModesArray = resultButtonModesStr.Split(',');

                if (buttonModesArray.Length > 0)
                {
                    if (!buttonModesArray.Contains("SWITCHCARD"))
                    {
                        buttonModesArray = buttonModesArray.Concat(new string[] { "SWITCHCARD" }).ToArray();
                    }

                    // populate all dropdowns
                    this.cb_button1.Items.Clear();
                    this.cb_button1.Items.AddRange(buttonModesArray);
                    this.cb_button2.Items.Clear();
                    this.cb_button2.Items.AddRange(buttonModesArray);
                    this.cb_button3.Items.Clear();
                    this.cb_button3.Items.AddRange(buttonModesArray);
                    this.cb_button4.Items.Clear();
                    this.cb_button4.Items.AddRange(buttonModesArray);
                    this.cb_button5.Items.Clear();
                    this.cb_button5.Items.AddRange(buttonModesArray);
                    this.cb_button6.Items.Clear();
                    this.cb_button6.Items.AddRange(buttonModesArray);
                    this.cb_button7.Items.Clear();
                    this.cb_button7.Items.AddRange(buttonModesArray);
                    this.cb_button8.Items.Clear();
                    this.cb_button8.Items.AddRange(buttonModesArray);
                }
            }
        }

        private static byte[] ReadFileIntoByteArray(string filename)
        {
            MemoryStream outputStream;

            using (var inputStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                outputStream = new MemoryStream((int)inputStream.Length);
                inputStream.CopyTo(outputStream);
            }

            return outputStream.ToArray();
        }

        internal void LoadDump(string filename)
        {
            // Load the file into a memory block
            byte[] DataArray = ReadFileIntoByteArray(filename);

            // Set up an XMODEM object
            XMODEM Modem = new XMODEM(_comport, XMODEM.Variants.XModemChecksum);

            // First send the upload command
            SendCommandWithoutResult("UPLOADMY");
            _comport.ReadLine(); // For the "110:WAITING FOR XMODEM" text

            int numBytesSuccessfullySent = Modem.Send(DataArray);

            if (numBytesSuccessfullySent == DataArray.Length && Modem.TerminationReason == XMODEM.TerminationReasonEnum.EndOfFile)
                Console.WriteLine("FILE LOAD SUCCESSFUL!");
            else
                MessageBox.Show("Failed to upload file");

        }

        internal void SaveDump(string filename)
        {
            // Set up an XMODEM object
            XMODEM Modem = new XMODEM(_comport, XMODEM.Variants.XModemChecksum);

            // First send the download command
            SendCommandWithoutResult("DOWNLOADMY");
            
            _comport.ReadLine(); // For the "110:WAITING FOR XMODEM" text

            MemoryStream DataMemoryStream = new MemoryStream(); // Grows dynamically
            byte[] DataArray = new byte[0];

            XMODEM.TerminationReasonEnum terminationReason = Modem.Receive(DataMemoryStream);

            if (terminationReason == XMODEM.TerminationReasonEnum.EndOfFile)
            {
                Console.WriteLine("FILE RECEIVE SUCCESSFUL!");

                // Transfer successful, so convert MemoryStream to byte array
                DataArray = DataMemoryStream.ToArray();

                // Strip away the SUB (byte value 26) padding bytes
                DataArray = Modem.TrimPaddingBytesFromEnd(DataArray);

                // Write the actual file
                File.WriteAllBytes(filename, DataArray);
            }
            else
            {
                // Something went wrong during the transfer
                MessageBox.Show("Failed to save the dump");
            }
        }
        #endregion
    }
}
