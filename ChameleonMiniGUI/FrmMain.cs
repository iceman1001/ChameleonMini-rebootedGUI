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
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace ChameleonMiniGUI
{
    public partial class frm_main : Form
    {
        [DllImport("Crapto1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt64 mfkey(UInt32 uid, UInt32 nt, UInt32 nt1, UInt32 nr0_enc, UInt32 ar0_enc, UInt32 nr1_enc, UInt32 ar1_enc);

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
            string selectedMode = null;
            var applyButtonClicked = sender as Button;

            int tagslotIndex = int.Parse(applyButtonClicked.Name.Substring(applyButtonClicked.Name.Length - 1));
            if (tagslotIndex <= 0) return;

            //SETTINGMY=tagslotIndex-1
            SendCommandWithoutResult("SETTINGMY=" + (tagslotIndex - 1));

            //SETTINGMY? -> SHOULD BE "NO."+tagslotIndex
            string selectedSlot = SendCommand("SETTINGMY?");
            if ((selectedSlot != null) && (selectedSlot.Contains("" + (tagslotIndex - 1))))
            {
                // Set the mode of the selected slot
                ComboBox cb_mode = (ComboBox)(applyButtonClicked.Parent.Controls["cb_mode" + tagslotIndex]);

                if (cb_mode != null)
                {
                    //CONFIGMY=cb_mode.SelectedItem
                    SendCommandWithoutResult("CONFIGMY=" + cb_mode.SelectedItem);
                    selectedMode = (string)cb_mode.SelectedItem;
                }

                // Set the button mode of the selected slot
                ComboBox cb_buttonMode = (ComboBox)(applyButtonClicked.Parent.Controls["cb_button" + tagslotIndex]);

                if (cb_buttonMode != null)
                {
                    //BUTTONMY=cb_buttonMode.SelectedItem
                    SendCommandWithoutResult("BUTTONMY=" + cb_buttonMode.SelectedItem);
                }

                // Set the UID
                TextBox txtUid = (TextBox)(applyButtonClicked.Parent.Controls["txt_uid" + tagslotIndex]);
                if (txtUid != null)
                {
                    string uid = txtUid.Text;
                    if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(selectedMode) && IsUidValid(uid, selectedMode))
                    {
                        SendCommandWithoutResult("UIDMY=" + uid);
                    }
                    else
                    {
                        // set a random UID
                        SendCommandWithoutResult("UIDMY=?");
                    }
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

        private void btn_fastcalc_Click(object sender, EventArgs e)
        {
            var fastCalcButtonClicked = sender as Button;

            int tagslotIndex = int.Parse(fastCalcButtonClicked.Name.Substring(fastCalcButtonClicked.Name.Length - 1));
            if (tagslotIndex <= 0) return;

            //SETTINGMY=tagslotIndex-1
            SendCommandWithoutResult("SETTINGMY=" + (tagslotIndex - 1));

            string result = Key_Calculate(true);

            TextBox txtResult = (TextBox)(fastCalcButtonClicked.Parent.Controls["txt_result" + tagslotIndex]);
            if (txtResult != null)
            {
                txtResult.Text = result;
            }
        }

        private void btn_fullcalc_Click(object sender, EventArgs e)
        {
            var fullCalcButtonClicked = sender as Button;

            int tagslotIndex = int.Parse(fullCalcButtonClicked.Name.Substring(fullCalcButtonClicked.Name.Length - 1));
            if (tagslotIndex <= 0) return;

            //SETTINGMY=tagslotIndex-1
            SendCommandWithoutResult("SETTINGMY=" + (tagslotIndex - 1));

            string result = Key_Calculate(false);

            TextBox txtResult = (TextBox)(fullCalcButtonClicked.Parent.Controls["txt_result" + tagslotIndex]);
            if (txtResult != null)
            {
                txtResult.Text = result;
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            var clearButtonClicked = sender as Button;

            int tagslotIndex = int.Parse(clearButtonClicked.Name.Substring(clearButtonClicked.Name.Length - 1));
            if (tagslotIndex <= 0) return;

            //SETTINGMY=tagslotIndex-1
            SendCommandWithoutResult("SETTINGMY=" + (tagslotIndex - 1));

            // DETECTIONMY = CLOSED
            SendCommandWithoutResult("DETECTIONMY=CLOSED");

            TextBox txtResult = (TextBox)(clearButtonClicked.Parent.Controls["txt_result" + tagslotIndex]);
            if (txtResult != null)
            {
                txtResult.Clear();
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
            byte[] rx_data = new byte[200];

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
            SendCommandWithoutResult("SETTINGMY=" + slotIndex);

            //SETTINGMY? -> SHOULD BE "NO."+i
            var selectedSlot = SendCommand("SETTINGMY?");
            if ((selectedSlot != null) && (selectedSlot.Contains("" + slotIndex)))
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

                // TODO: Check if we should determine the size with MEMSIZEMY cmd

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

        string Key_Calculate(bool fast)
        {
            string show_all = "";

            // TODO: should be byte instead of char
            char[] data_receive = SendCommand("DETECTIONMY?").ToArray();
            if (data_receive != null && data_receive.Length > 0)
            {
                ComPass(data_receive, 123321, 208);
                if (ISO14443ACheckCRCA(data_receive, 208))
                {
                    //vector<uint64_t> key_sum;
                    List<MyKey> mykey = new List<MyKey>();

                    UInt32[] nt = { 0, 0, 0, 0, 0, 0 }, nr = { 0, 0, 0, 0, 0, 0 }, ar = { 0, 0, 0, 0, 0, 0 }, sector = { 0, 0, 0, 0, 0, 0 }, Key = { 0, 0, 0, 0, 0, 0 };
                    UInt32 UID = 0;

                    UID = (uint)(data_receive[0] << 24) + (uint)(data_receive[1] << 16) + (uint)(data_receive[2] << 8) + (data_receive[3]);

                    //KEYA data copy
                    for (byte i = 0; i < 6; i++)
                    {
                        Key[i] = data_receive[(i + 1) * 16];
                        sector[i] = data_receive[(i + 1) * 16 + 1];
                        nt[i] = (uint)(data_receive[(i + 1) * 16 + 4] << 24) + (uint)(data_receive[(i + 1) * 16 + 5] << 16) + (uint)(data_receive[(i + 1) * 16 + 6] << 8) + (data_receive[(i + 1) * 16 + 7]);
                        nr[i] = (uint)(data_receive[(i + 1) * 16 + 8] << 24) + (uint)(data_receive[(i + 1) * 16 + 9] << 16) + (uint)(data_receive[(i + 1) * 16 + 10] << 8) + (data_receive[(i + 1) * 16 + 11]);
                        ar[i] = (uint)(data_receive[(i + 1) * 16 + 12] << 24) + (uint)(data_receive[(i + 1) * 16 + 13] << 16) + (uint)(data_receive[(i + 1) * 16 + 14] << 8) + (data_receive[(i + 1) * 16 + 15]);
                    }

                    //KEYA into list
                    int cout = Calculation_times(fast);

                    //status
                    //byte k = 0;
                    //M_progress_speed.SetRange(0, (cout + 1) * cout / 2 * 2);
                    //M_progress_speed.SetPos(k);
                    // TODO: Set progress bar

                    for (byte i = 0; i < cout; i++)
                    {
                        for (byte j = i; j < cout; j++)
                        {
                            MyKey ktmp = new MyKey();
                            ktmp.key_sum = mfkey(UID, /* uid */
                                nt[i], /* nt0 */
                                nt[j + 1], /* nt1 */
                                nr[i], /* nr0 */
                                ar[i], /* ar0 */
                                nr[j + 1],  /* nr1 */
                                ar[j + 1]); /* ar1 */

                            ktmp.sector1 = sector[i];
                            ktmp.sector2 = sector[j + 1];

                            if (ktmp.key_sum != 0xffffffffffffffff)
                                mykey.Add(ktmp);

                            //refresh
                            //M_progress_speed.SetPos(++k);
                            // TODO: progress
                        }
                    }

                    //KEYA result
                    KeyComparer my_cmp = new KeyComparer();
                    mykey.Sort(my_cmp);

                    // TODO: Implement this
                    // mykey.erase(unique(mykey.begin(), mykey.end(), my_uiq), mykey.end());//remove repeat 

                    cout = mykey.Count;
                    if (cout > 0)
                    {
                        for (byte i = 0; i < cout; i++)
                        {
                            //temp_num.Format("KeyA%d:\r\n", i + 1);
                            // TODO: What to do with these?
                            string temp_num = "", temp_sector2 = "";

                            if (mykey[i].sector1 != 0xff)
                                show_all += (string.Format("%dsec%dblo A:\r\n", mykey[i].sector1 / 4, mykey[i].sector1) + temp_sector2 + temp_num + string.Format("%06X", mykey[i].key_sum / 16777216) + string.Format("%06X", mykey[i].key_sum % 16777216) + "\r\n");
                        }
                    }
                    else
                        show_all += "keyA error\r\n";

                    //clear keyb
                    mykey.Clear();

                    //KEYB data copy
                    for (byte i = 6; i < 12; i++)
                    {
                        Key[i - 6] = data_receive[(i + 1) * 16];
                        sector[i - 6] = data_receive[(i + 1) * 16 + 1];
                        nt[i - 6] = (uint)(data_receive[(i + 1) * 16 + 4] << 24) + (uint)(data_receive[(i + 1) * 16 + 5] << 16) + (uint)(data_receive[(i + 1) * 16 + 6] << 8) + (data_receive[(i + 1) * 16 + 7]);
                        nr[i - 6] = (uint)(data_receive[(i + 1) * 16 + 8] << 24) + (uint)(data_receive[(i + 1) * 16 + 9] << 16) + (uint)(data_receive[(i + 1) * 16 + 10] << 8) + (data_receive[(i + 1) * 16 + 11]);
                        ar[i - 6] = (uint)(data_receive[(i + 1) * 16 + 12] << 24) + (uint)(data_receive[(i + 1) * 16 + 13] << 16) + (uint)(data_receive[(i + 1) * 16 + 14] << 8) + (data_receive[(i + 1) * 16 + 15]);
                    }

                    //KEYB into list 
                    cout = Calculation_times(fast);

                    for (byte i = 0; i < cout; i++)
                    {
                        for (byte j = i; j < cout; j++)
                        {
                            MyKey ktmp = new MyKey();
                            ktmp.key_sum = mfkey(UID, /* uid */
                                nt[i], /* nt0 */
                                nt[j + 1], /* nt1 */
                                nr[i], /* nr0 */
                                ar[i], /* ar0 */
                                nr[j + 1],  /* nr1 */
                                ar[j + 1]); /* ar1 */
                            ktmp.sector1 = sector[i];
                            ktmp.sector2 = sector[j + 1];

                            if (ktmp.key_sum != 0xffffffffffffffff)
                                mykey.Add(ktmp);

                            //refresh
                            //M_progress_speed.SetPos(++k);
                            // TODO: progress
                        }
                    }

                    //KEYB get result
                    // sort(mykey.begin(), mykey.end(), my_cmp);
                    mykey.Sort(my_cmp);

                    // TODO: Implement this
                    //mykey.erase(unique(mykey.begin(), mykey.end(), my_uiq), mykey.end());//remove repeat

                    cout = mykey.Count;

                    if (cout > 0)
                    {
                        for (byte i = 0; i < cout; i++)
                        {
                            // TODO: What to do with these?
                            string temp_num = "", temp_sector2 = "";

                            if (mykey[i].sector1 != 0xff)
                                show_all += (string.Format("%dsec%dblo B:\r\n", mykey[i].sector1 / 4, mykey[i].sector1) + temp_sector2 + temp_num + string.Format("%06X", mykey[i].key_sum / 16777216) + string.Format("%06X", mykey[i].key_sum % 16777216) + "\r\n");
                        }
                    }
                    else
                        show_all += "keyB error\r\n";

                    //m_edit_carda_detection[num].SetWindowText(show_all);
                }
            }

            return show_all;

        }

        private int Calculation_times(bool fast)
        {
            // TODO: Implement this
            throw new NotImplementedException();
        }

        private int genFun(int size, int key, int i)
        {
            return size + key + i - size / key;
        }

        void ComPass(char[] toBeEncFileName, int key, int len)
        {
            uint[] newFileName = new uint[275];
            toBeEncFileName.CopyTo(newFileName, 0);
            int i, s, t, size = len;

            for (i = 0; i < size; i++)
            {
                s = (int)newFileName[i];
                t = genFun(size, key, i) ^ s;  // encryption
                toBeEncFileName[i] = (char)t;
            }
        }

        bool ISO14443ACheckCRCA(char[] Buffer, UInt16 ByteCount)
        {
            UInt16 Checksum = 0x6363;
            char[] DataPtr = Buffer;

            int i = 0;

            while (ByteCount > 0)
            {
                uint Byte = DataPtr[i++];

                Byte ^= (uint)(Checksum & 0x00FF);
                Byte ^= Byte << 4;

                Checksum = (ushort)((ushort)(Checksum >> 8) ^ (Checksum ^ (ushort)(Byte << 8)) ^ (ushort)(Checksum ^
                        ((ushort)Byte << 3) ^ (ushort)((ushort)Byte >> 4)));
                ByteCount--;
            }

            return (DataPtr[0] == ((Checksum >> 0) & 0xFF)) && (DataPtr[1] == ((Checksum >> 8) & 0xFF));
        }
        #endregion

    }

    #region MyKey
    public class MyKey
    {
        public UInt64 key_sum;
        public UInt32 sector1;
        public UInt32 sector2;
    }

    // TODO: Implement a real comparator
    public class KeyComparer : IComparer<MyKey>
    {
        public int Compare(MyKey x, MyKey y)
        {
            MyKey c1 = (MyKey)x;
            MyKey c2 = (MyKey)y;

            if (c1.key_sum > c2.key_sum)
                return 1;

            if (c1.key_sum < c2.key_sum)
                return -1;

            else
                return 0;
        }
    }
    #endregion
}
