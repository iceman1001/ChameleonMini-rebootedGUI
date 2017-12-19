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

namespace ChameleonMiniGUI
{
    public partial class frm_main : Form
    {
        private SerialPort _comport = null;

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
                var fp = ConfigurationManager.AppSettings["BOOTLOADER_PATH"];
                var fn = ConfigurationManager.AppSettings["BOOTLOADER_EXE"];
                var path = Path.Combine(fp, fn);

                if (File.Exists(path))
                {
                    Start(path);
                    failed = false;
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
            if (new[] { "SWITCHCARD", "RANDOM_UID", "CLOSED" }.Contains(slotButtonMode))
            {
                return true;
            }

            return false;
        }

        private bool IsModeValid(string slotMode)
        {
            if (new[] { "MF_CLASSIC_1K", "MF_CLASSIC_4K", "MF_ULTRALIGHT", "MF_DETECTION", "CLOSED" }.Contains(slotMode))
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
