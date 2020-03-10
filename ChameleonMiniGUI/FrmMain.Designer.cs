namespace ChameleonMiniGUI
{
    partial class frm_main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_main));
            this.txt_output = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.gb_output = new System.Windows.Forms.GroupBox();
            this.cb_languages = new System.Windows.Forms.ComboBox();
            this.bsLanguages = new System.Windows.Forms.BindingSource(this.components);
            this.tpSettings = new System.Windows.Forms.TabPage();
            this.gb_language = new System.Windows.Forms.GroupBox();
            this.btnStartlocation = new System.Windows.Forms.Button();
            this.gb_keepalive = new System.Windows.Forms.GroupBox();
            this.btn_setInterval = new System.Windows.Forms.Button();
            this.txt_interval = new System.Windows.Forms.TextBox();
            this.lbl_interval = new System.Windows.Forms.Label();
            this.chk_keepalive = new System.Windows.Forms.CheckBox();
            this.gb_connectionSettings = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pb_device = new System.Windows.Forms.PictureBox();
            this.tb_firmware = new System.Windows.Forms.TextBox();
            this.txt_constatus = new System.Windows.Forms.TextBox();
            this.btn_disconnect = new System.Windows.Forms.Button();
            this.btn_connect = new System.Windows.Forms.Button();
            this.gb_bootloader = new System.Windows.Forms.GroupBox();
            this.lbl_defaults = new System.Windows.Forms.Label();
            this.lbl_reset = new System.Windows.Forms.Label();
            this.btn_reset = new System.Windows.Forms.Button();
            this.lbl_upgrade = new System.Windows.Forms.Label();
            this.btn_bootmode = new System.Windows.Forms.Button();
            this.btn_exitboot = new System.Windows.Forms.Button();
            this.gb_defaultdownload = new System.Windows.Forms.GroupBox();
            this.lbl_defaultdownload = new System.Windows.Forms.Label();
            this.txt_defaultdownload = new System.Windows.Forms.TextBox();
            this.btn_browsedownloads = new System.Windows.Forms.Button();
            this.gb_rssi = new System.Windows.Forms.GroupBox();
            this.btn_rssirefresh = new System.Windows.Forms.Button();
            this.txt_rssi = new System.Windows.Forms.TextBox();
            this.lbl_rssi = new System.Windows.Forms.Label();
            this.tpOperation = new System.Windows.Forms.TabPage();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.gb_actions = new System.Windows.Forms.GroupBox();
            this.btn_identify = new System.Windows.Forms.Button();
            this.btn_setactive = new System.Windows.Forms.Button();
            this.btn_selectnone = new System.Windows.Forms.Button();
            this.btn_selectall = new System.Windows.Forms.Button();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_keycalc = new System.Windows.Forms.Button();
            this.btn_apply = new System.Windows.Forms.Button();
            this.btn_download = new System.Windows.Forms.Button();
            this.btn_upload = new System.Windows.Forms.Button();
            this.gb_tagslot6 = new ChameleonMiniGUI.GroupBoxEnhanced();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_mode6 = new System.Windows.Forms.Label();
            this.cb_Rbuttonlong6 = new System.Windows.Forms.ComboBox();
            this.cb_ledred6 = new System.Windows.Forms.ComboBox();
            this.cb_Rbutton6 = new System.Windows.Forms.ComboBox();
            this.cb_mode6 = new System.Windows.Forms.ComboBox();
            this.txt_size6 = new System.Windows.Forms.TextBox();
            this.cb_ledgreen6 = new System.Windows.Forms.ComboBox();
            this.lbl_uid6 = new System.Windows.Forms.Label();
            this.lb_ledledred6 = new System.Windows.Forms.Label();
            this.lbl_button6 = new System.Windows.Forms.Label();
            this.cb_Lbuttonlong6 = new System.Windows.Forms.ComboBox();
            this.lb_ledledgreen6 = new System.Windows.Forms.Label();
            this.lbl_buttonlong6 = new System.Windows.Forms.Label();
            this.cb_Lbutton6 = new System.Windows.Forms.ComboBox();
            this.lbl_size6 = new System.Windows.Forms.Label();
            this.txt_uid6 = new System.Windows.Forms.TextBox();
            this.gb_tagslot1 = new ChameleonMiniGUI.GroupBoxEnhanced();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_uid1 = new System.Windows.Forms.Label();
            this.txt_size1 = new System.Windows.Forms.TextBox();
            this.cb_ledred1 = new System.Windows.Forms.ComboBox();
            this.lbl_button1 = new System.Windows.Forms.Label();
            this.cb_ledgreen1 = new System.Windows.Forms.ComboBox();
            this.cb_Rbuttonlong1 = new System.Windows.Forms.ComboBox();
            this.cb_Lbuttonlong1 = new System.Windows.Forms.ComboBox();
            this.lb_ledledred1 = new System.Windows.Forms.Label();
            this.cb_Rbutton1 = new System.Windows.Forms.ComboBox();
            this.cb_Lbutton1 = new System.Windows.Forms.ComboBox();
            this.lbl_buttonlong1 = new System.Windows.Forms.Label();
            this.lb_ledledgreen1 = new System.Windows.Forms.Label();
            this.lbl_size1 = new System.Windows.Forms.Label();
            this.cb_mode1 = new System.Windows.Forms.ComboBox();
            this.lbl_mode1 = new System.Windows.Forms.Label();
            this.txt_uid1 = new System.Windows.Forms.TextBox();
            this.gb_tagslot2 = new ChameleonMiniGUI.GroupBoxEnhanced();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_mode2 = new System.Windows.Forms.Label();
            this.cb_Rbuttonlong2 = new System.Windows.Forms.ComboBox();
            this.cb_ledgreen2 = new System.Windows.Forms.ComboBox();
            this.cb_Rbutton2 = new System.Windows.Forms.ComboBox();
            this.cb_ledred2 = new System.Windows.Forms.ComboBox();
            this.cb_Lbutton2 = new System.Windows.Forms.ComboBox();
            this.cb_Lbuttonlong2 = new System.Windows.Forms.ComboBox();
            this.cb_mode2 = new System.Windows.Forms.ComboBox();
            this.lbl_uid2 = new System.Windows.Forms.Label();
            this.lb_ledledred2 = new System.Windows.Forms.Label();
            this.txt_uid2 = new System.Windows.Forms.TextBox();
            this.lb_ledledgreen2 = new System.Windows.Forms.Label();
            this.txt_size2 = new System.Windows.Forms.TextBox();
            this.lbl_button2 = new System.Windows.Forms.Label();
            this.lbl_size2 = new System.Windows.Forms.Label();
            this.lbl_buttonlong2 = new System.Windows.Forms.Label();
            this.gb_tagslot4 = new ChameleonMiniGUI.GroupBoxEnhanced();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_mode4 = new System.Windows.Forms.Label();
            this.cb_Rbuttonlong4 = new System.Windows.Forms.ComboBox();
            this.cb_ledred4 = new System.Windows.Forms.ComboBox();
            this.cb_Rbutton4 = new System.Windows.Forms.ComboBox();
            this.lbl_uid4 = new System.Windows.Forms.Label();
            this.txt_size4 = new System.Windows.Forms.TextBox();
            this.cb_ledgreen4 = new System.Windows.Forms.ComboBox();
            this.lbl_button4 = new System.Windows.Forms.Label();
            this.lb_ledledred4 = new System.Windows.Forms.Label();
            this.lbl_buttonlong4 = new System.Windows.Forms.Label();
            this.cb_Lbuttonlong4 = new System.Windows.Forms.ComboBox();
            this.lb_ledledgreen4 = new System.Windows.Forms.Label();
            this.lbl_size4 = new System.Windows.Forms.Label();
            this.cb_Lbutton4 = new System.Windows.Forms.ComboBox();
            this.cb_mode4 = new System.Windows.Forms.ComboBox();
            this.txt_uid4 = new System.Windows.Forms.TextBox();
            this.gb_tagslot3 = new ChameleonMiniGUI.GroupBoxEnhanced();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_mode3 = new System.Windows.Forms.Label();
            this.cb_Rbuttonlong3 = new System.Windows.Forms.ComboBox();
            this.cb_ledred3 = new System.Windows.Forms.ComboBox();
            this.cb_Rbutton3 = new System.Windows.Forms.ComboBox();
            this.lbl_uid3 = new System.Windows.Forms.Label();
            this.txt_size3 = new System.Windows.Forms.TextBox();
            this.cb_ledgreen3 = new System.Windows.Forms.ComboBox();
            this.lbl_button3 = new System.Windows.Forms.Label();
            this.lb_ledledred3 = new System.Windows.Forms.Label();
            this.lbl_buttonlong3 = new System.Windows.Forms.Label();
            this.cb_Lbuttonlong3 = new System.Windows.Forms.ComboBox();
            this.lb_ledledgreen3 = new System.Windows.Forms.Label();
            this.lbl_size3 = new System.Windows.Forms.Label();
            this.cb_Lbutton3 = new System.Windows.Forms.ComboBox();
            this.cb_mode3 = new System.Windows.Forms.ComboBox();
            this.txt_uid3 = new System.Windows.Forms.TextBox();
            this.gb_tagslot5 = new ChameleonMiniGUI.GroupBoxEnhanced();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_mode5 = new System.Windows.Forms.Label();
            this.cb_Rbuttonlong5 = new System.Windows.Forms.ComboBox();
            this.cb_ledred5 = new System.Windows.Forms.ComboBox();
            this.cb_Rbutton5 = new System.Windows.Forms.ComboBox();
            this.lbl_uid5 = new System.Windows.Forms.Label();
            this.txt_size5 = new System.Windows.Forms.TextBox();
            this.cb_ledgreen5 = new System.Windows.Forms.ComboBox();
            this.lbl_button5 = new System.Windows.Forms.Label();
            this.lb_ledledred5 = new System.Windows.Forms.Label();
            this.lbl_buttonlong5 = new System.Windows.Forms.Label();
            this.cb_Lbuttonlong5 = new System.Windows.Forms.ComboBox();
            this.lb_ledledgreen5 = new System.Windows.Forms.Label();
            this.lbl_size5 = new System.Windows.Forms.Label();
            this.cb_Lbutton5 = new System.Windows.Forms.ComboBox();
            this.cb_mode5 = new System.Windows.Forms.ComboBox();
            this.txt_uid5 = new System.Windows.Forms.TextBox();
            this.gb_tagslot7 = new ChameleonMiniGUI.GroupBoxEnhanced();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_mode7 = new System.Windows.Forms.Label();
            this.cb_Rbuttonlong7 = new System.Windows.Forms.ComboBox();
            this.cb_ledred7 = new System.Windows.Forms.ComboBox();
            this.cb_Rbutton7 = new System.Windows.Forms.ComboBox();
            this.lbl_uid7 = new System.Windows.Forms.Label();
            this.txt_size7 = new System.Windows.Forms.TextBox();
            this.cb_ledgreen7 = new System.Windows.Forms.ComboBox();
            this.lbl_button7 = new System.Windows.Forms.Label();
            this.lb_ledledred7 = new System.Windows.Forms.Label();
            this.lbl_buttonlong7 = new System.Windows.Forms.Label();
            this.cb_Lbuttonlong7 = new System.Windows.Forms.ComboBox();
            this.lb_ledledgreen7 = new System.Windows.Forms.Label();
            this.lbl_size7 = new System.Windows.Forms.Label();
            this.cb_Lbutton7 = new System.Windows.Forms.ComboBox();
            this.cb_mode7 = new System.Windows.Forms.ComboBox();
            this.txt_uid7 = new System.Windows.Forms.TextBox();
            this.gb_tagslot8 = new ChameleonMiniGUI.GroupBoxEnhanced();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_mode8 = new System.Windows.Forms.Label();
            this.cb_Rbuttonlong8 = new System.Windows.Forms.ComboBox();
            this.cb_ledred8 = new System.Windows.Forms.ComboBox();
            this.cb_Rbutton8 = new System.Windows.Forms.ComboBox();
            this.lbl_uid8 = new System.Windows.Forms.Label();
            this.txt_size8 = new System.Windows.Forms.TextBox();
            this.cb_ledgreen8 = new System.Windows.Forms.ComboBox();
            this.lbl_button8 = new System.Windows.Forms.Label();
            this.lb_ledledred8 = new System.Windows.Forms.Label();
            this.lbl_buttonlong8 = new System.Windows.Forms.Label();
            this.cb_Lbuttonlong8 = new System.Windows.Forms.ComboBox();
            this.lb_ledledgreen8 = new System.Windows.Forms.Label();
            this.lbl_size8 = new System.Windows.Forms.Label();
            this.cb_Lbutton8 = new System.Windows.Forms.ComboBox();
            this.cb_mode8 = new System.Windows.Forms.ComboBox();
            this.txt_uid8 = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpDump = new System.Windows.Forms.TabPage();
            this.menuScroll = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ucLegend1 = new ChameleonMiniGUI.UcLegend();
            this.btn_close2 = new System.Windows.Forms.Button();
            this.btn_close1 = new System.Windows.Forms.Button();
            this.lbl_template = new System.Windows.Forms.Label();
            this.cb_templateA = new System.Windows.Forms.ComboBox();
            this.bsTemplates = new System.Windows.Forms.BindingSource(this.components);
            this.chkSyncScroll = new System.Windows.Forms.CheckBox();
            this.lbl_hbfilename2 = new System.Windows.Forms.Label();
            this.lbl_hbfilename1 = new System.Windows.Forms.Label();
            this.rbtn_bytewidth16 = new System.Windows.Forms.RadioButton();
            this.rbtn_bytewidth08 = new System.Windows.Forms.RadioButton();
            this.rbtn_bytewidth04 = new System.Windows.Forms.RadioButton();
            this.lbl_bytewidth = new System.Windows.Forms.Label();
            this.btn_save2 = new System.Windows.Forms.Button();
            this.btn_open2 = new System.Windows.Forms.Button();
            this.btn_save1 = new System.Windows.Forms.Button();
            this.btn_open1 = new System.Windows.Forms.Button();
            this.hexBox2 = new Be.Windows.Forms.HexBox();
            this.hexBox1 = new Be.Windows.Forms.HexBox();
            this.tpUtils = new System.Windows.Forms.TabPage();
            this.ucExplorer1 = new ChameleonMiniGUI.UcExplorer();
            this.tpSerial = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbAvailableCmds = new System.Windows.Forms.GroupBox();
            this.tfSerialHelp = new ChameleonMiniGUI.UcTextFlow();
            this.linkRevG = new System.Windows.Forms.LinkLabel();
            this.linkRevE = new System.Windows.Forms.LinkLabel();
            this.gbSerial_interface = new System.Windows.Forms.GroupBox();
            this.btnSerialSend = new System.Windows.Forms.Button();
            this.tbSerialCmd = new System.Windows.Forms.TextBox();
            this.btnClearCmd = new System.Windows.Forms.Button();
            this.tbSerialOutput = new System.Windows.Forms.RichTextBox();
            this.menuClear = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_selectall = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_clear = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_copy = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gb_output.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsLanguages)).BeginInit();
            this.tpSettings.SuspendLayout();
            this.gb_language.SuspendLayout();
            this.gb_keepalive.SuspendLayout();
            this.gb_connectionSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_device)).BeginInit();
            this.gb_bootloader.SuspendLayout();
            this.gb_defaultdownload.SuspendLayout();
            this.gb_rssi.SuspendLayout();
            this.tpOperation.SuspendLayout();
            this.gb_actions.SuspendLayout();
            this.gb_tagslot6.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.gb_tagslot1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.gb_tagslot2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.gb_tagslot4.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.gb_tagslot3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.gb_tagslot5.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.gb_tagslot7.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.gb_tagslot8.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpDump.SuspendLayout();
            this.menuScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsTemplates)).BeginInit();
            this.tpUtils.SuspendLayout();
            this.tpSerial.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.gbAvailableCmds.SuspendLayout();
            this.gbSerial_interface.SuspendLayout();
            this.menuClear.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_output
            // 
            this.txt_output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_output.Location = new System.Drawing.Point(3, 16);
            this.txt_output.Multiline = true;
            this.txt_output.Name = "txt_output";
            this.txt_output.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_output.Size = new System.Drawing.Size(1277, 150);
            this.txt_output.TabIndex = 6;
            this.txt_output.VisibleChanged += new System.EventHandler(this.txt_output_VisibleChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Dump files| *.bin; *.dump; *.mfd; *.hex; *.json; *.eml; *.mct|All files| *.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Dump files| *.bin; *.dump; *.mfd; *.hex|JSON file|*.json|EML file|*.eml|MCT file|" +
    "*.mct";
            // 
            // gb_output
            // 
            this.gb_output.Controls.Add(this.txt_output);
            this.gb_output.Location = new System.Drawing.Point(12, 709);
            this.gb_output.Name = "gb_output";
            this.gb_output.Size = new System.Drawing.Size(1283, 169);
            this.gb_output.TabIndex = 7;
            this.gb_output.TabStop = false;
            this.gb_output.Text = "Output";
            // 
            // cb_languages
            // 
            this.cb_languages.DataSource = this.bsLanguages;
            this.cb_languages.FormattingEnabled = true;
            this.cb_languages.Location = new System.Drawing.Point(27, 40);
            this.cb_languages.Name = "cb_languages";
            this.cb_languages.Size = new System.Drawing.Size(121, 21);
            this.cb_languages.TabIndex = 7;
            this.cb_languages.SelectedIndexChanged += new System.EventHandler(this.cb_languages_SelectedIndexChanged);
            // 
            // tpSettings
            // 
            this.tpSettings.Controls.Add(this.gb_language);
            this.tpSettings.Controls.Add(this.gb_keepalive);
            this.tpSettings.Controls.Add(this.gb_connectionSettings);
            this.tpSettings.Controls.Add(this.gb_bootloader);
            this.tpSettings.Controls.Add(this.gb_defaultdownload);
            this.tpSettings.Controls.Add(this.gb_rssi);
            this.tpSettings.Location = new System.Drawing.Point(4, 22);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Size = new System.Drawing.Size(1279, 665);
            this.tpSettings.TabIndex = 3;
            this.tpSettings.Text = "Settings";
            this.tpSettings.UseVisualStyleBackColor = true;
            // 
            // gb_language
            // 
            this.gb_language.Controls.Add(this.btnStartlocation);
            this.gb_language.Controls.Add(this.cb_languages);
            this.gb_language.Location = new System.Drawing.Point(919, 200);
            this.gb_language.Name = "gb_language";
            this.gb_language.Size = new System.Drawing.Size(179, 120);
            this.gb_language.TabIndex = 8;
            this.gb_language.TabStop = false;
            this.gb_language.Text = "Languages && Templates";
            // 
            // btnStartlocation
            // 
            this.btnStartlocation.Location = new System.Drawing.Point(27, 67);
            this.btnStartlocation.Name = "btnStartlocation";
            this.btnStartlocation.Size = new System.Drawing.Size(121, 23);
            this.btnStartlocation.TabIndex = 8;
            this.btnStartlocation.Text = "Open Location";
            this.btnStartlocation.UseVisualStyleBackColor = true;
            this.btnStartlocation.Click += new System.EventHandler(this.btnStartlocation_Click);
            // 
            // gb_keepalive
            // 
            this.gb_keepalive.Controls.Add(this.btn_setInterval);
            this.gb_keepalive.Controls.Add(this.txt_interval);
            this.gb_keepalive.Controls.Add(this.lbl_interval);
            this.gb_keepalive.Controls.Add(this.chk_keepalive);
            this.gb_keepalive.Location = new System.Drawing.Point(674, 18);
            this.gb_keepalive.Name = "gb_keepalive";
            this.gb_keepalive.Size = new System.Drawing.Size(424, 166);
            this.gb_keepalive.TabIndex = 4;
            this.gb_keepalive.TabStop = false;
            this.gb_keepalive.Text = "Keep Alive";
            // 
            // btn_setInterval
            // 
            this.btn_setInterval.Enabled = false;
            this.btn_setInterval.Location = new System.Drawing.Point(260, 88);
            this.btn_setInterval.Name = "btn_setInterval";
            this.btn_setInterval.Size = new System.Drawing.Size(97, 30);
            this.btn_setInterval.TabIndex = 3;
            this.btn_setInterval.Text = "Set";
            this.btn_setInterval.UseVisualStyleBackColor = true;
            this.btn_setInterval.Click += new System.EventHandler(this.btn_setInterval_Click);
            // 
            // txt_interval
            // 
            this.txt_interval.Location = new System.Drawing.Point(25, 94);
            this.txt_interval.Name = "txt_interval";
            this.txt_interval.Size = new System.Drawing.Size(69, 20);
            this.txt_interval.TabIndex = 2;
            // 
            // lbl_interval
            // 
            this.lbl_interval.AutoSize = true;
            this.lbl_interval.Location = new System.Drawing.Point(100, 97);
            this.lbl_interval.Name = "lbl_interval";
            this.lbl_interval.Size = new System.Drawing.Size(64, 13);
            this.lbl_interval.TabIndex = 1;
            this.lbl_interval.Text = "Interval (ms)";
            // 
            // chk_keepalive
            // 
            this.chk_keepalive.AutoSize = true;
            this.chk_keepalive.Location = new System.Drawing.Point(25, 42);
            this.chk_keepalive.Name = "chk_keepalive";
            this.chk_keepalive.Size = new System.Drawing.Size(209, 17);
            this.chk_keepalive.TabIndex = 0;
            this.chk_keepalive.Text = "Send keep-alive (VERSION command)";
            this.chk_keepalive.UseVisualStyleBackColor = true;
            // 
            // gb_connectionSettings
            // 
            this.gb_connectionSettings.Controls.Add(this.label1);
            this.gb_connectionSettings.Controls.Add(this.pb_device);
            this.gb_connectionSettings.Controls.Add(this.tb_firmware);
            this.gb_connectionSettings.Controls.Add(this.txt_constatus);
            this.gb_connectionSettings.Controls.Add(this.btn_disconnect);
            this.gb_connectionSettings.Controls.Add(this.btn_connect);
            this.gb_connectionSettings.Location = new System.Drawing.Point(167, 18);
            this.gb_connectionSettings.Name = "gb_connectionSettings";
            this.gb_connectionSettings.Size = new System.Drawing.Size(481, 166);
            this.gb_connectionSettings.TabIndex = 3;
            this.gb_connectionSettings.TabStop = false;
            this.gb_connectionSettings.Text = "Connection status";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(175, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Firmware version";
            // 
            // pb_device
            // 
            this.pb_device.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pb_device.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_device.Image = global::ChameleonMiniGUI.Properties.Resources.usbWarning;
            this.pb_device.InitialImage = global::ChameleonMiniGUI.Properties.Resources.usbWarning;
            this.pb_device.Location = new System.Drawing.Point(25, 27);
            this.pb_device.Name = "pb_device";
            this.pb_device.Size = new System.Drawing.Size(128, 128);
            this.pb_device.TabIndex = 5;
            this.pb_device.TabStop = false;
            // 
            // tb_firmware
            // 
            this.tb_firmware.Enabled = false;
            this.tb_firmware.Location = new System.Drawing.Point(178, 134);
            this.tb_firmware.Name = "tb_firmware";
            this.tb_firmware.ReadOnly = true;
            this.tb_firmware.Size = new System.Drawing.Size(256, 20);
            this.tb_firmware.TabIndex = 6;
            // 
            // txt_constatus
            // 
            this.txt_constatus.Location = new System.Drawing.Point(178, 70);
            this.txt_constatus.Margin = new System.Windows.Forms.Padding(8);
            this.txt_constatus.Name = "txt_constatus";
            this.txt_constatus.ReadOnly = true;
            this.txt_constatus.Size = new System.Drawing.Size(256, 20);
            this.txt_constatus.TabIndex = 5;
            this.txt_constatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_disconnect
            // 
            this.btn_disconnect.Enabled = false;
            this.btn_disconnect.Location = new System.Drawing.Point(349, 29);
            this.btn_disconnect.Name = "btn_disconnect";
            this.btn_disconnect.Size = new System.Drawing.Size(85, 30);
            this.btn_disconnect.TabIndex = 1;
            this.btn_disconnect.Text = "Disconnect";
            this.btn_disconnect.UseVisualStyleBackColor = true;
            this.btn_disconnect.Click += new System.EventHandler(this.btn_disconnect_Click);
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(178, 29);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(85, 30);
            this.btn_connect.TabIndex = 0;
            this.btn_connect.Text = "Connect";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // gb_bootloader
            // 
            this.gb_bootloader.Controls.Add(this.lbl_defaults);
            this.gb_bootloader.Controls.Add(this.lbl_reset);
            this.gb_bootloader.Controls.Add(this.btn_reset);
            this.gb_bootloader.Controls.Add(this.lbl_upgrade);
            this.gb_bootloader.Controls.Add(this.btn_bootmode);
            this.gb_bootloader.Controls.Add(this.btn_exitboot);
            this.gb_bootloader.Location = new System.Drawing.Point(167, 335);
            this.gb_bootloader.Name = "gb_bootloader";
            this.gb_bootloader.Size = new System.Drawing.Size(931, 136);
            this.gb_bootloader.TabIndex = 2;
            this.gb_bootloader.TabStop = false;
            this.gb_bootloader.Text = "Reset && Bootloader options";
            // 
            // lbl_defaults
            // 
            this.lbl_defaults.AutoSize = true;
            this.lbl_defaults.Location = new System.Drawing.Point(245, 95);
            this.lbl_defaults.Name = "lbl_defaults";
            this.lbl_defaults.Size = new System.Drawing.Size(397, 13);
            this.lbl_defaults.TabIndex = 10;
            this.lbl_defaults.Text = "Flashes the stock firmware of the ChameleonMini. Needs to be in bootloader mode.";
            // 
            // lbl_reset
            // 
            this.lbl_reset.AutoSize = true;
            this.lbl_reset.Location = new System.Drawing.Point(245, 37);
            this.lbl_reset.Name = "lbl_reset";
            this.lbl_reset.Size = new System.Drawing.Size(350, 13);
            this.lbl_reset.TabIndex = 9;
            this.lbl_reset.Text = "Reboots the ChameleonMini, i.e., power down and subsequent power-up";
            // 
            // btn_reset
            // 
            this.btn_reset.Enabled = false;
            this.btn_reset.Location = new System.Drawing.Point(75, 32);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(135, 23);
            this.btn_reset.TabIndex = 8;
            this.btn_reset.Text = "Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // lbl_upgrade
            // 
            this.lbl_upgrade.AutoSize = true;
            this.lbl_upgrade.Location = new System.Drawing.Point(245, 66);
            this.lbl_upgrade.Name = "lbl_upgrade";
            this.lbl_upgrade.Size = new System.Drawing.Size(285, 13);
            this.lbl_upgrade.TabIndex = 7;
            this.lbl_upgrade.Text = "Sets the ChameleonMini into firmware upgrade mode (DFU)";
            // 
            // btn_bootmode
            // 
            this.btn_bootmode.Enabled = false;
            this.btn_bootmode.Location = new System.Drawing.Point(75, 61);
            this.btn_bootmode.Name = "btn_bootmode";
            this.btn_bootmode.Size = new System.Drawing.Size(135, 23);
            this.btn_bootmode.TabIndex = 6;
            this.btn_bootmode.Text = "Upgrade";
            this.btn_bootmode.UseVisualStyleBackColor = true;
            this.btn_bootmode.Click += new System.EventHandler(this.btn_bootmode_Click);
            // 
            // btn_exitboot
            // 
            this.btn_exitboot.Location = new System.Drawing.Point(75, 90);
            this.btn_exitboot.Name = "btn_exitboot";
            this.btn_exitboot.Size = new System.Drawing.Size(135, 23);
            this.btn_exitboot.TabIndex = 5;
            this.btn_exitboot.Text = "Load Defaults";
            this.btn_exitboot.UseVisualStyleBackColor = true;
            this.btn_exitboot.Click += new System.EventHandler(this.btn_exitboot_Click);
            // 
            // gb_defaultdownload
            // 
            this.gb_defaultdownload.Controls.Add(this.lbl_defaultdownload);
            this.gb_defaultdownload.Controls.Add(this.txt_defaultdownload);
            this.gb_defaultdownload.Controls.Add(this.btn_browsedownloads);
            this.gb_defaultdownload.Location = new System.Drawing.Point(167, 200);
            this.gb_defaultdownload.Name = "gb_defaultdownload";
            this.gb_defaultdownload.Size = new System.Drawing.Size(434, 120);
            this.gb_defaultdownload.TabIndex = 1;
            this.gb_defaultdownload.TabStop = false;
            this.gb_defaultdownload.Text = "Download Location";
            // 
            // lbl_defaultdownload
            // 
            this.lbl_defaultdownload.AutoSize = true;
            this.lbl_defaultdownload.Location = new System.Drawing.Point(15, 33);
            this.lbl_defaultdownload.Name = "lbl_defaultdownload";
            this.lbl_defaultdownload.Size = new System.Drawing.Size(267, 13);
            this.lbl_defaultdownload.TabIndex = 2;
            this.lbl_defaultdownload.Text = "Choose the default directory for the downloaded dumps";
            // 
            // txt_defaultdownload
            // 
            this.txt_defaultdownload.Location = new System.Drawing.Point(18, 65);
            this.txt_defaultdownload.Name = "txt_defaultdownload";
            this.txt_defaultdownload.Size = new System.Drawing.Size(337, 20);
            this.txt_defaultdownload.TabIndex = 1;
            // 
            // btn_browsedownloads
            // 
            this.btn_browsedownloads.Location = new System.Drawing.Point(375, 63);
            this.btn_browsedownloads.Name = "btn_browsedownloads";
            this.btn_browsedownloads.Size = new System.Drawing.Size(33, 23);
            this.btn_browsedownloads.TabIndex = 0;
            this.btn_browsedownloads.Text = "...";
            this.btn_browsedownloads.UseVisualStyleBackColor = true;
            this.btn_browsedownloads.Click += new System.EventHandler(this.btn_browsedownloads_Click);
            // 
            // gb_rssi
            // 
            this.gb_rssi.Controls.Add(this.btn_rssirefresh);
            this.gb_rssi.Controls.Add(this.txt_rssi);
            this.gb_rssi.Controls.Add(this.lbl_rssi);
            this.gb_rssi.Location = new System.Drawing.Point(621, 200);
            this.gb_rssi.Name = "gb_rssi";
            this.gb_rssi.Size = new System.Drawing.Size(278, 120);
            this.gb_rssi.TabIndex = 0;
            this.gb_rssi.TabStop = false;
            this.gb_rssi.Text = "RSSI Voltage";
            // 
            // btn_rssirefresh
            // 
            this.btn_rssirefresh.Enabled = false;
            this.btn_rssirefresh.Location = new System.Drawing.Point(167, 51);
            this.btn_rssirefresh.Name = "btn_rssirefresh";
            this.btn_rssirefresh.Size = new System.Drawing.Size(83, 30);
            this.btn_rssirefresh.TabIndex = 2;
            this.btn_rssirefresh.Text = "Refresh";
            this.btn_rssirefresh.UseVisualStyleBackColor = true;
            this.btn_rssirefresh.Click += new System.EventHandler(this.btn_rssirefresh_Click);
            // 
            // txt_rssi
            // 
            this.txt_rssi.Location = new System.Drawing.Point(21, 57);
            this.txt_rssi.Name = "txt_rssi";
            this.txt_rssi.Size = new System.Drawing.Size(114, 20);
            this.txt_rssi.TabIndex = 1;
            // 
            // lbl_rssi
            // 
            this.lbl_rssi.AutoSize = true;
            this.lbl_rssi.Location = new System.Drawing.Point(18, 35);
            this.lbl_rssi.Name = "lbl_rssi";
            this.lbl_rssi.Size = new System.Drawing.Size(69, 13);
            this.lbl_rssi.TabIndex = 0;
            this.lbl_rssi.Text = "Current RSSI";
            // 
            // tpOperation
            // 
            this.tpOperation.Controls.Add(this.checkBox8);
            this.tpOperation.Controls.Add(this.checkBox7);
            this.tpOperation.Controls.Add(this.checkBox6);
            this.tpOperation.Controls.Add(this.checkBox5);
            this.tpOperation.Controls.Add(this.checkBox4);
            this.tpOperation.Controls.Add(this.checkBox3);
            this.tpOperation.Controls.Add(this.checkBox2);
            this.tpOperation.Controls.Add(this.checkBox1);
            this.tpOperation.Controls.Add(this.gb_actions);
            this.tpOperation.Controls.Add(this.gb_tagslot6);
            this.tpOperation.Controls.Add(this.gb_tagslot1);
            this.tpOperation.Controls.Add(this.gb_tagslot2);
            this.tpOperation.Controls.Add(this.gb_tagslot4);
            this.tpOperation.Controls.Add(this.gb_tagslot3);
            this.tpOperation.Controls.Add(this.gb_tagslot5);
            this.tpOperation.Controls.Add(this.gb_tagslot7);
            this.tpOperation.Controls.Add(this.gb_tagslot8);
            this.tpOperation.Location = new System.Drawing.Point(4, 22);
            this.tpOperation.Name = "tpOperation";
            this.tpOperation.Padding = new System.Windows.Forms.Padding(3);
            this.tpOperation.Size = new System.Drawing.Size(1279, 665);
            this.tpOperation.TabIndex = 0;
            this.tpOperation.Text = "Operation";
            this.tpOperation.UseVisualStyleBackColor = true;
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Enabled = false;
            this.checkBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox8.Location = new System.Drawing.Point(943, 278);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(59, 17);
            this.checkBox8.TabIndex = 8;
            this.checkBox8.Text = "Slot 8";
            this.checkBox8.UseVisualStyleBackColor = true;
            this.checkBox8.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Enabled = false;
            this.checkBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox7.Location = new System.Drawing.Point(652, 278);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(59, 17);
            this.checkBox7.TabIndex = 7;
            this.checkBox7.Text = "Slot 7";
            this.checkBox7.UseVisualStyleBackColor = true;
            this.checkBox7.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Enabled = false;
            this.checkBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox6.Location = new System.Drawing.Point(361, 278);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(59, 17);
            this.checkBox6.TabIndex = 6;
            this.checkBox6.Text = "Slot 6";
            this.checkBox6.UseVisualStyleBackColor = true;
            this.checkBox6.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Enabled = false;
            this.checkBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox5.Location = new System.Drawing.Point(70, 278);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(59, 17);
            this.checkBox5.TabIndex = 5;
            this.checkBox5.Text = "Slot 5";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Enabled = false;
            this.checkBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox4.Location = new System.Drawing.Point(943, 11);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(59, 17);
            this.checkBox4.TabIndex = 4;
            this.checkBox4.Text = "Slot 4";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Enabled = false;
            this.checkBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox3.Location = new System.Drawing.Point(652, 11);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(59, 17);
            this.checkBox3.TabIndex = 3;
            this.checkBox3.Text = "Slot 3";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Enabled = false;
            this.checkBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox2.Location = new System.Drawing.Point(361, 11);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(59, 17);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.Text = "Slot 2";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Enabled = false;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(70, 11);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(59, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Slot 1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // gb_actions
            // 
            this.gb_actions.Controls.Add(this.btn_identify);
            this.gb_actions.Controls.Add(this.btn_setactive);
            this.gb_actions.Controls.Add(this.btn_selectnone);
            this.gb_actions.Controls.Add(this.btn_selectall);
            this.gb_actions.Controls.Add(this.btn_refresh);
            this.gb_actions.Controls.Add(this.btn_clear);
            this.gb_actions.Controls.Add(this.btn_keycalc);
            this.gb_actions.Controls.Add(this.btn_apply);
            this.gb_actions.Controls.Add(this.btn_download);
            this.gb_actions.Controls.Add(this.btn_upload);
            this.gb_actions.Location = new System.Drawing.Point(61, 580);
            this.gb_actions.Name = "gb_actions";
            this.gb_actions.Size = new System.Drawing.Size(1151, 61);
            this.gb_actions.TabIndex = 36;
            this.gb_actions.TabStop = false;
            this.gb_actions.Text = "Available Actions";
            // 
            // btn_identify
            // 
            this.btn_identify.Location = new System.Drawing.Point(758, 24);
            this.btn_identify.Name = "btn_identify";
            this.btn_identify.Size = new System.Drawing.Size(75, 23);
            this.btn_identify.TabIndex = 18;
            this.btn_identify.Text = "Identify";
            this.toolTip1.SetToolTip(this.btn_identify, "Identify Card at Reader");
            this.btn_identify.UseVisualStyleBackColor = true;
            this.btn_identify.Click += new System.EventHandler(this.btn_identify_Click);
            // 
            // btn_setactive
            // 
            this.btn_setactive.Location = new System.Drawing.Point(574, 24);
            this.btn_setactive.Name = "btn_setactive";
            this.btn_setactive.Size = new System.Drawing.Size(86, 23);
            this.btn_setactive.TabIndex = 14;
            this.btn_setactive.Text = "Set Active";
            this.btn_setactive.UseVisualStyleBackColor = true;
            this.btn_setactive.Click += new System.EventHandler(this.btn_setactive_Click);
            // 
            // btn_selectnone
            // 
            this.btn_selectnone.AutoSize = true;
            this.btn_selectnone.Enabled = false;
            this.btn_selectnone.Location = new System.Drawing.Point(110, 20);
            this.btn_selectnone.Name = "btn_selectnone";
            this.btn_selectnone.Size = new System.Drawing.Size(106, 30);
            this.btn_selectnone.TabIndex = 10;
            this.btn_selectnone.Text = "Select None";
            this.btn_selectnone.UseVisualStyleBackColor = true;
            this.btn_selectnone.Click += new System.EventHandler(this.btn_selectnone_Click);
            // 
            // btn_selectall
            // 
            this.btn_selectall.AutoSize = true;
            this.btn_selectall.Enabled = false;
            this.btn_selectall.Location = new System.Drawing.Point(9, 20);
            this.btn_selectall.Name = "btn_selectall";
            this.btn_selectall.Size = new System.Drawing.Size(95, 30);
            this.btn_selectall.TabIndex = 9;
            this.btn_selectall.Text = "Select All";
            this.btn_selectall.UseVisualStyleBackColor = true;
            this.btn_selectall.Click += new System.EventHandler(this.btn_selectall_Click);
            // 
            // btn_refresh
            // 
            this.btn_refresh.Location = new System.Drawing.Point(482, 24);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(86, 23);
            this.btn_refresh.TabIndex = 13;
            this.btn_refresh.Text = "Refresh";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(390, 24);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(86, 23);
            this.btn_clear.TabIndex = 12;
            this.btn_clear.Text = "Clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_keycalc
            // 
            this.btn_keycalc.Location = new System.Drawing.Point(666, 24);
            this.btn_keycalc.Name = "btn_keycalc";
            this.btn_keycalc.Size = new System.Drawing.Size(86, 23);
            this.btn_keycalc.TabIndex = 15;
            this.btn_keycalc.Text = "mfkey32";
            this.btn_keycalc.UseVisualStyleBackColor = true;
            this.btn_keycalc.Click += new System.EventHandler(this.btn_mfkey_Click);
            // 
            // btn_apply
            // 
            this.btn_apply.Location = new System.Drawing.Point(298, 24);
            this.btn_apply.Name = "btn_apply";
            this.btn_apply.Size = new System.Drawing.Size(86, 23);
            this.btn_apply.TabIndex = 11;
            this.btn_apply.Text = "Apply";
            this.btn_apply.UseVisualStyleBackColor = true;
            this.btn_apply.Click += new System.EventHandler(this.btn_apply_Click);
            // 
            // btn_download
            // 
            this.btn_download.Location = new System.Drawing.Point(1042, 24);
            this.btn_download.Name = "btn_download";
            this.btn_download.Size = new System.Drawing.Size(103, 23);
            this.btn_download.TabIndex = 17;
            this.btn_download.Text = "Download Dump";
            this.btn_download.UseVisualStyleBackColor = true;
            this.btn_download.Click += new System.EventHandler(this.btn_download_Click);
            // 
            // btn_upload
            // 
            this.btn_upload.Location = new System.Drawing.Point(933, 24);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.Size = new System.Drawing.Size(103, 23);
            this.btn_upload.TabIndex = 16;
            this.btn_upload.Text = "Upload Dump";
            this.btn_upload.UseVisualStyleBackColor = true;
            this.btn_upload.Click += new System.EventHandler(this.btn_upload_Click);
            // 
            // gb_tagslot6
            // 
            this.gb_tagslot6.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.gb_tagslot6.BorderColorLight = System.Drawing.SystemColors.ControlLightLight;
            this.gb_tagslot6.BorderRadius = 5;
            this.gb_tagslot6.BorderWidth = 1F;
            this.gb_tagslot6.Controls.Add(this.tableLayoutPanel6);
            this.gb_tagslot6.Location = new System.Drawing.Point(352, 295);
            this.gb_tagslot6.Name = "gb_tagslot6";
            this.gb_tagslot6.Size = new System.Drawing.Size(278, 242);
            this.gb_tagslot6.TabIndex = 26;
            this.gb_tagslot6.TabStop = false;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AllowDrop = true;
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel6.Controls.Add(this.lbl_mode6, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.cb_Rbuttonlong6, 2, 3);
            this.tableLayoutPanel6.Controls.Add(this.cb_ledred6, 1, 5);
            this.tableLayoutPanel6.Controls.Add(this.cb_Rbutton6, 2, 2);
            this.tableLayoutPanel6.Controls.Add(this.cb_mode6, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.txt_size6, 1, 6);
            this.tableLayoutPanel6.Controls.Add(this.cb_ledgreen6, 1, 4);
            this.tableLayoutPanel6.Controls.Add(this.lbl_uid6, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.lb_ledledred6, 0, 5);
            this.tableLayoutPanel6.Controls.Add(this.lbl_button6, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.cb_Lbuttonlong6, 1, 3);
            this.tableLayoutPanel6.Controls.Add(this.lb_ledledgreen6, 0, 4);
            this.tableLayoutPanel6.Controls.Add(this.lbl_buttonlong6, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.cb_Lbutton6, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.lbl_size6, 0, 6);
            this.tableLayoutPanel6.Controls.Add(this.txt_uid6, 1, 1);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(9, 17);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 7;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(254, 213);
            this.tableLayoutPanel6.TabIndex = 26;
            this.tableLayoutPanel6.DragDrop += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel_DragDrop);
            this.tableLayoutPanel6.DragEnter += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel_DragEnter);
            // 
            // lbl_mode6
            // 
            this.lbl_mode6.AutoSize = true;
            this.lbl_mode6.Location = new System.Drawing.Point(3, 0);
            this.lbl_mode6.Name = "lbl_mode6";
            this.lbl_mode6.Size = new System.Drawing.Size(34, 13);
            this.lbl_mode6.TabIndex = 1;
            this.lbl_mode6.Text = "Mode";
            // 
            // cb_Rbuttonlong6
            // 
            this.cb_Rbuttonlong6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Rbuttonlong6.DropDownWidth = 160;
            this.cb_Rbuttonlong6.FormattingEnabled = true;
            this.cb_Rbuttonlong6.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Rbuttonlong6.Location = new System.Drawing.Point(167, 93);
            this.cb_Rbuttonlong6.Name = "cb_Rbuttonlong6";
            this.cb_Rbuttonlong6.Size = new System.Drawing.Size(80, 21);
            this.cb_Rbuttonlong6.TabIndex = 21;
            // 
            // cb_ledred6
            // 
            this.tableLayoutPanel6.SetColumnSpan(this.cb_ledred6, 2);
            this.cb_ledred6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ledred6.FormattingEnabled = true;
            this.cb_ledred6.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER",
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_ledred6.Location = new System.Drawing.Point(79, 153);
            this.cb_ledred6.Name = "cb_ledred6";
            this.cb_ledred6.Size = new System.Drawing.Size(172, 21);
            this.cb_ledred6.TabIndex = 25;
            // 
            // cb_Rbutton6
            // 
            this.cb_Rbutton6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Rbutton6.DropDownWidth = 160;
            this.cb_Rbutton6.FormattingEnabled = true;
            this.cb_Rbutton6.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Rbutton6.Location = new System.Drawing.Point(167, 63);
            this.cb_Rbutton6.Name = "cb_Rbutton6";
            this.cb_Rbutton6.Size = new System.Drawing.Size(80, 21);
            this.cb_Rbutton6.TabIndex = 20;
            // 
            // cb_mode6
            // 
            this.tableLayoutPanel6.SetColumnSpan(this.cb_mode6, 2);
            this.cb_mode6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_mode6.FormattingEnabled = true;
            this.cb_mode6.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_mode6.Location = new System.Drawing.Point(79, 3);
            this.cb_mode6.Name = "cb_mode6";
            this.cb_mode6.Size = new System.Drawing.Size(172, 21);
            this.cb_mode6.TabIndex = 0;
            // 
            // txt_size6
            // 
            this.tableLayoutPanel6.SetColumnSpan(this.txt_size6, 2);
            this.txt_size6.Location = new System.Drawing.Point(79, 183);
            this.txt_size6.Name = "txt_size6";
            this.txt_size6.ReadOnly = true;
            this.txt_size6.Size = new System.Drawing.Size(172, 20);
            this.txt_size6.TabIndex = 15;
            // 
            // cb_ledgreen6
            // 
            this.tableLayoutPanel6.SetColumnSpan(this.cb_ledgreen6, 2);
            this.cb_ledgreen6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ledgreen6.FormattingEnabled = true;
            this.cb_ledgreen6.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER",
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_ledgreen6.Location = new System.Drawing.Point(79, 123);
            this.cb_ledgreen6.Name = "cb_ledgreen6";
            this.cb_ledgreen6.Size = new System.Drawing.Size(172, 21);
            this.cb_ledgreen6.TabIndex = 24;
            // 
            // lbl_uid6
            // 
            this.lbl_uid6.AutoSize = true;
            this.lbl_uid6.Location = new System.Drawing.Point(3, 30);
            this.lbl_uid6.Name = "lbl_uid6";
            this.lbl_uid6.Size = new System.Drawing.Size(26, 13);
            this.lbl_uid6.TabIndex = 2;
            this.lbl_uid6.Text = "UID";
            // 
            // lb_ledledred6
            // 
            this.lb_ledledred6.AutoSize = true;
            this.lb_ledledred6.Location = new System.Drawing.Point(3, 150);
            this.lb_ledledred6.Name = "lb_ledledred6";
            this.lb_ledledred6.Size = new System.Drawing.Size(48, 13);
            this.lb_ledledred6.TabIndex = 23;
            this.lb_ledledred6.Text = "LEDRed";
            // 
            // lbl_button6
            // 
            this.lbl_button6.AutoSize = true;
            this.lbl_button6.Location = new System.Drawing.Point(3, 60);
            this.lbl_button6.Name = "lbl_button6";
            this.lbl_button6.Size = new System.Drawing.Size(38, 13);
            this.lbl_button6.TabIndex = 4;
            this.lbl_button6.Text = "Button";
            // 
            // cb_Lbuttonlong6
            // 
            this.cb_Lbuttonlong6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Lbuttonlong6.DropDownWidth = 160;
            this.cb_Lbuttonlong6.FormattingEnabled = true;
            this.cb_Lbuttonlong6.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Lbuttonlong6.Location = new System.Drawing.Point(79, 93);
            this.cb_Lbuttonlong6.Name = "cb_Lbuttonlong6";
            this.cb_Lbuttonlong6.Size = new System.Drawing.Size(80, 21);
            this.cb_Lbuttonlong6.TabIndex = 19;
            // 
            // lb_ledledgreen6
            // 
            this.lb_ledledgreen6.AutoSize = true;
            this.lb_ledledgreen6.Location = new System.Drawing.Point(3, 120);
            this.lb_ledledgreen6.Name = "lb_ledledgreen6";
            this.lb_ledledgreen6.Size = new System.Drawing.Size(57, 13);
            this.lb_ledledgreen6.TabIndex = 22;
            this.lb_ledledgreen6.Text = "LEDGreen";
            // 
            // lbl_buttonlong6
            // 
            this.lbl_buttonlong6.AutoSize = true;
            this.lbl_buttonlong6.Location = new System.Drawing.Point(3, 90);
            this.lbl_buttonlong6.Name = "lbl_buttonlong6";
            this.lbl_buttonlong6.Size = new System.Drawing.Size(47, 13);
            this.lbl_buttonlong6.TabIndex = 18;
            this.lbl_buttonlong6.Text = "BtnLong";
            // 
            // cb_Lbutton6
            // 
            this.cb_Lbutton6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Lbutton6.DropDownWidth = 160;
            this.cb_Lbutton6.FormattingEnabled = true;
            this.cb_Lbutton6.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Lbutton6.Location = new System.Drawing.Point(79, 63);
            this.cb_Lbutton6.Name = "cb_Lbutton6";
            this.cb_Lbutton6.Size = new System.Drawing.Size(80, 21);
            this.cb_Lbutton6.TabIndex = 5;
            // 
            // lbl_size6
            // 
            this.lbl_size6.AutoSize = true;
            this.lbl_size6.Location = new System.Drawing.Point(3, 180);
            this.lbl_size6.Name = "lbl_size6";
            this.lbl_size6.Size = new System.Drawing.Size(27, 13);
            this.lbl_size6.TabIndex = 14;
            this.lbl_size6.Text = "Size";
            // 
            // txt_uid6
            // 
            this.tableLayoutPanel6.SetColumnSpan(this.txt_uid6, 2);
            this.txt_uid6.Location = new System.Drawing.Point(79, 33);
            this.txt_uid6.Name = "txt_uid6";
            this.txt_uid6.Size = new System.Drawing.Size(172, 20);
            this.txt_uid6.TabIndex = 3;
            // 
            // gb_tagslot1
            // 
            this.gb_tagslot1.BackColor = System.Drawing.Color.Transparent;
            this.gb_tagslot1.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.gb_tagslot1.BorderColorLight = System.Drawing.SystemColors.ControlLightLight;
            this.gb_tagslot1.BorderRadius = 5;
            this.gb_tagslot1.BorderWidth = 1F;
            this.gb_tagslot1.Controls.Add(this.tableLayoutPanel1);
            this.gb_tagslot1.Location = new System.Drawing.Point(61, 30);
            this.gb_tagslot1.Name = "gb_tagslot1";
            this.gb_tagslot1.Size = new System.Drawing.Size(278, 242);
            this.gb_tagslot1.TabIndex = 25;
            this.gb_tagslot1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AllowDrop = true;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_uid1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txt_size1, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.cb_ledred1, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lbl_button1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cb_ledgreen1, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.cb_Rbuttonlong1, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.cb_Lbuttonlong1, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lb_ledledred1, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.cb_Rbutton1, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.cb_Lbutton1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbl_buttonlong1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lb_ledledgreen1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbl_size1, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.cb_mode1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_mode1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txt_uid1, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 18);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(254, 213);
            this.tableLayoutPanel1.TabIndex = 37;
            this.tableLayoutPanel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel_DragDrop);
            this.tableLayoutPanel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel_DragEnter);
            // 
            // lbl_uid1
            // 
            this.lbl_uid1.AutoSize = true;
            this.lbl_uid1.Location = new System.Drawing.Point(3, 30);
            this.lbl_uid1.Name = "lbl_uid1";
            this.lbl_uid1.Size = new System.Drawing.Size(26, 13);
            this.lbl_uid1.TabIndex = 2;
            this.lbl_uid1.Text = "UID";
            // 
            // txt_size1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txt_size1, 2);
            this.txt_size1.Location = new System.Drawing.Point(79, 183);
            this.txt_size1.Name = "txt_size1";
            this.txt_size1.ReadOnly = true;
            this.txt_size1.Size = new System.Drawing.Size(172, 20);
            this.txt_size1.TabIndex = 9;
            // 
            // cb_ledred1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.cb_ledred1, 2);
            this.cb_ledred1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ledred1.FormattingEnabled = true;
            this.cb_ledred1.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER",
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_ledred1.Location = new System.Drawing.Point(79, 153);
            this.cb_ledred1.Name = "cb_ledred1";
            this.cb_ledred1.Size = new System.Drawing.Size(172, 21);
            this.cb_ledred1.TabIndex = 17;
            // 
            // lbl_button1
            // 
            this.lbl_button1.AutoSize = true;
            this.lbl_button1.Location = new System.Drawing.Point(3, 60);
            this.lbl_button1.Name = "lbl_button1";
            this.lbl_button1.Size = new System.Drawing.Size(38, 13);
            this.lbl_button1.TabIndex = 4;
            this.lbl_button1.Text = "Button";
            // 
            // cb_ledgreen1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.cb_ledgreen1, 2);
            this.cb_ledgreen1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ledgreen1.FormattingEnabled = true;
            this.cb_ledgreen1.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER",
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_ledgreen1.Location = new System.Drawing.Point(79, 123);
            this.cb_ledgreen1.Name = "cb_ledgreen1";
            this.cb_ledgreen1.Size = new System.Drawing.Size(172, 21);
            this.cb_ledgreen1.TabIndex = 16;
            // 
            // cb_Rbuttonlong1
            // 
            this.cb_Rbuttonlong1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Rbuttonlong1.DropDownWidth = 160;
            this.cb_Rbuttonlong1.FormattingEnabled = true;
            this.cb_Rbuttonlong1.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Rbuttonlong1.Location = new System.Drawing.Point(167, 93);
            this.cb_Rbuttonlong1.Name = "cb_Rbuttonlong1";
            this.cb_Rbuttonlong1.Size = new System.Drawing.Size(84, 21);
            this.cb_Rbuttonlong1.TabIndex = 13;
            // 
            // cb_Lbuttonlong1
            // 
            this.cb_Lbuttonlong1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Lbuttonlong1.DropDownWidth = 160;
            this.cb_Lbuttonlong1.FormattingEnabled = true;
            this.cb_Lbuttonlong1.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Lbuttonlong1.Location = new System.Drawing.Point(79, 93);
            this.cb_Lbuttonlong1.Name = "cb_Lbuttonlong1";
            this.cb_Lbuttonlong1.Size = new System.Drawing.Size(82, 21);
            this.cb_Lbuttonlong1.TabIndex = 11;
            // 
            // lb_ledledred1
            // 
            this.lb_ledledred1.AutoSize = true;
            this.lb_ledledred1.Location = new System.Drawing.Point(3, 150);
            this.lb_ledledred1.Name = "lb_ledledred1";
            this.lb_ledledred1.Size = new System.Drawing.Size(48, 13);
            this.lb_ledledred1.TabIndex = 15;
            this.lb_ledledred1.Text = "LEDRed";
            // 
            // cb_Rbutton1
            // 
            this.cb_Rbutton1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Rbutton1.DropDownWidth = 160;
            this.cb_Rbutton1.FormattingEnabled = true;
            this.cb_Rbutton1.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Rbutton1.Location = new System.Drawing.Point(167, 63);
            this.cb_Rbutton1.Name = "cb_Rbutton1";
            this.cb_Rbutton1.Size = new System.Drawing.Size(84, 21);
            this.cb_Rbutton1.TabIndex = 12;
            // 
            // cb_Lbutton1
            // 
            this.cb_Lbutton1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Lbutton1.DropDownWidth = 160;
            this.cb_Lbutton1.FormattingEnabled = true;
            this.cb_Lbutton1.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Lbutton1.Location = new System.Drawing.Point(79, 63);
            this.cb_Lbutton1.Name = "cb_Lbutton1";
            this.cb_Lbutton1.Size = new System.Drawing.Size(82, 21);
            this.cb_Lbutton1.TabIndex = 5;
            // 
            // lbl_buttonlong1
            // 
            this.lbl_buttonlong1.AutoSize = true;
            this.lbl_buttonlong1.Location = new System.Drawing.Point(3, 90);
            this.lbl_buttonlong1.Name = "lbl_buttonlong1";
            this.lbl_buttonlong1.Size = new System.Drawing.Size(47, 13);
            this.lbl_buttonlong1.TabIndex = 10;
            this.lbl_buttonlong1.Text = "BtnLong";
            // 
            // lb_ledledgreen1
            // 
            this.lb_ledledgreen1.AutoSize = true;
            this.lb_ledledgreen1.Location = new System.Drawing.Point(3, 120);
            this.lb_ledledgreen1.Name = "lb_ledledgreen1";
            this.lb_ledledgreen1.Size = new System.Drawing.Size(57, 13);
            this.lb_ledledgreen1.TabIndex = 14;
            this.lb_ledledgreen1.Text = "LEDGreen";
            // 
            // lbl_size1
            // 
            this.lbl_size1.AutoSize = true;
            this.lbl_size1.Location = new System.Drawing.Point(3, 180);
            this.lbl_size1.Name = "lbl_size1";
            this.lbl_size1.Size = new System.Drawing.Size(27, 13);
            this.lbl_size1.TabIndex = 8;
            this.lbl_size1.Text = "Size";
            // 
            // cb_mode1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.cb_mode1, 2);
            this.cb_mode1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_mode1.FormattingEnabled = true;
            this.cb_mode1.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_mode1.Location = new System.Drawing.Point(79, 3);
            this.cb_mode1.Name = "cb_mode1";
            this.cb_mode1.Size = new System.Drawing.Size(172, 21);
            this.cb_mode1.TabIndex = 0;
            // 
            // lbl_mode1
            // 
            this.lbl_mode1.AutoSize = true;
            this.lbl_mode1.Location = new System.Drawing.Point(3, 0);
            this.lbl_mode1.Name = "lbl_mode1";
            this.lbl_mode1.Size = new System.Drawing.Size(34, 13);
            this.lbl_mode1.TabIndex = 1;
            this.lbl_mode1.Text = "Mode";
            // 
            // txt_uid1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txt_uid1, 2);
            this.txt_uid1.Location = new System.Drawing.Point(79, 33);
            this.txt_uid1.Name = "txt_uid1";
            this.txt_uid1.Size = new System.Drawing.Size(172, 20);
            this.txt_uid1.TabIndex = 3;
            // 
            // gb_tagslot2
            // 
            this.gb_tagslot2.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.gb_tagslot2.BorderColorLight = System.Drawing.SystemColors.ControlLightLight;
            this.gb_tagslot2.BorderRadius = 5;
            this.gb_tagslot2.BorderWidth = 1F;
            this.gb_tagslot2.Controls.Add(this.tableLayoutPanel2);
            this.gb_tagslot2.Location = new System.Drawing.Point(352, 30);
            this.gb_tagslot2.Name = "gb_tagslot2";
            this.gb_tagslot2.Size = new System.Drawing.Size(278, 242);
            this.gb_tagslot2.TabIndex = 24;
            this.gb_tagslot2.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AllowDrop = true;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel2.Controls.Add(this.lbl_mode2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cb_Rbuttonlong2, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.cb_ledgreen2, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.cb_Rbutton2, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.cb_ledred2, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.cb_Lbutton2, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.cb_Lbuttonlong2, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.cb_mode2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl_uid2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lb_ledledred2, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.txt_uid2, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lb_ledledgreen2, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.txt_size2, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.lbl_button2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lbl_size2, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.lbl_buttonlong2, 0, 3);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(9, 18);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 7;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(254, 213);
            this.tableLayoutPanel2.TabIndex = 22;
            this.tableLayoutPanel2.DragDrop += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel_DragDrop);
            this.tableLayoutPanel2.DragEnter += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel_DragEnter);
            // 
            // lbl_mode2
            // 
            this.lbl_mode2.AutoSize = true;
            this.lbl_mode2.Location = new System.Drawing.Point(3, 0);
            this.lbl_mode2.Name = "lbl_mode2";
            this.lbl_mode2.Size = new System.Drawing.Size(34, 13);
            this.lbl_mode2.TabIndex = 1;
            this.lbl_mode2.Text = "Mode";
            // 
            // cb_Rbuttonlong2
            // 
            this.cb_Rbuttonlong2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Rbuttonlong2.DropDownWidth = 160;
            this.cb_Rbuttonlong2.FormattingEnabled = true;
            this.cb_Rbuttonlong2.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Rbuttonlong2.Location = new System.Drawing.Point(167, 93);
            this.cb_Rbuttonlong2.Name = "cb_Rbuttonlong2";
            this.cb_Rbuttonlong2.Size = new System.Drawing.Size(80, 21);
            this.cb_Rbuttonlong2.TabIndex = 15;
            // 
            // cb_ledgreen2
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.cb_ledgreen2, 2);
            this.cb_ledgreen2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ledgreen2.FormattingEnabled = true;
            this.cb_ledgreen2.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_ledgreen2.Location = new System.Drawing.Point(79, 123);
            this.cb_ledgreen2.Name = "cb_ledgreen2";
            this.cb_ledgreen2.Size = new System.Drawing.Size(172, 21);
            this.cb_ledgreen2.TabIndex = 20;
            // 
            // cb_Rbutton2
            // 
            this.cb_Rbutton2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Rbutton2.DropDownWidth = 160;
            this.cb_Rbutton2.FormattingEnabled = true;
            this.cb_Rbutton2.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Rbutton2.Location = new System.Drawing.Point(167, 63);
            this.cb_Rbutton2.Name = "cb_Rbutton2";
            this.cb_Rbutton2.Size = new System.Drawing.Size(80, 21);
            this.cb_Rbutton2.TabIndex = 14;
            // 
            // cb_ledred2
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.cb_ledred2, 2);
            this.cb_ledred2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ledred2.FormattingEnabled = true;
            this.cb_ledred2.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_ledred2.Location = new System.Drawing.Point(79, 153);
            this.cb_ledred2.Name = "cb_ledred2";
            this.cb_ledred2.Size = new System.Drawing.Size(172, 21);
            this.cb_ledred2.TabIndex = 21;
            // 
            // cb_Lbutton2
            // 
            this.cb_Lbutton2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Lbutton2.DropDownWidth = 160;
            this.cb_Lbutton2.FormattingEnabled = true;
            this.cb_Lbutton2.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Lbutton2.Location = new System.Drawing.Point(79, 63);
            this.cb_Lbutton2.Name = "cb_Lbutton2";
            this.cb_Lbutton2.Size = new System.Drawing.Size(80, 21);
            this.cb_Lbutton2.TabIndex = 5;
            // 
            // cb_Lbuttonlong2
            // 
            this.cb_Lbuttonlong2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Lbuttonlong2.DropDownWidth = 160;
            this.cb_Lbuttonlong2.FormattingEnabled = true;
            this.cb_Lbuttonlong2.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Lbuttonlong2.Location = new System.Drawing.Point(79, 93);
            this.cb_Lbuttonlong2.Name = "cb_Lbuttonlong2";
            this.cb_Lbuttonlong2.Size = new System.Drawing.Size(80, 21);
            this.cb_Lbuttonlong2.TabIndex = 13;
            // 
            // cb_mode2
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.cb_mode2, 2);
            this.cb_mode2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_mode2.FormattingEnabled = true;
            this.cb_mode2.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_mode2.Location = new System.Drawing.Point(79, 3);
            this.cb_mode2.Name = "cb_mode2";
            this.cb_mode2.Size = new System.Drawing.Size(172, 21);
            this.cb_mode2.TabIndex = 0;
            // 
            // lbl_uid2
            // 
            this.lbl_uid2.AutoSize = true;
            this.lbl_uid2.Location = new System.Drawing.Point(3, 30);
            this.lbl_uid2.Name = "lbl_uid2";
            this.lbl_uid2.Size = new System.Drawing.Size(26, 13);
            this.lbl_uid2.TabIndex = 2;
            this.lbl_uid2.Text = "UID";
            // 
            // lb_ledledred2
            // 
            this.lb_ledledred2.AutoSize = true;
            this.lb_ledledred2.Location = new System.Drawing.Point(3, 150);
            this.lb_ledledred2.Name = "lb_ledledred2";
            this.lb_ledledred2.Size = new System.Drawing.Size(48, 13);
            this.lb_ledledred2.TabIndex = 19;
            this.lb_ledledred2.Text = "LEDRed";
            // 
            // txt_uid2
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.txt_uid2, 2);
            this.txt_uid2.Location = new System.Drawing.Point(79, 33);
            this.txt_uid2.Name = "txt_uid2";
            this.txt_uid2.Size = new System.Drawing.Size(172, 20);
            this.txt_uid2.TabIndex = 3;
            // 
            // lb_ledledgreen2
            // 
            this.lb_ledledgreen2.AutoSize = true;
            this.lb_ledledgreen2.Location = new System.Drawing.Point(3, 120);
            this.lb_ledledgreen2.Name = "lb_ledledgreen2";
            this.lb_ledledgreen2.Size = new System.Drawing.Size(57, 13);
            this.lb_ledledgreen2.TabIndex = 18;
            this.lb_ledledgreen2.Text = "LEDGreen";
            // 
            // txt_size2
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.txt_size2, 2);
            this.txt_size2.Location = new System.Drawing.Point(79, 183);
            this.txt_size2.Name = "txt_size2";
            this.txt_size2.ReadOnly = true;
            this.txt_size2.Size = new System.Drawing.Size(172, 20);
            this.txt_size2.TabIndex = 7;
            // 
            // lbl_button2
            // 
            this.lbl_button2.AutoSize = true;
            this.lbl_button2.Location = new System.Drawing.Point(3, 60);
            this.lbl_button2.Name = "lbl_button2";
            this.lbl_button2.Size = new System.Drawing.Size(38, 13);
            this.lbl_button2.TabIndex = 4;
            this.lbl_button2.Text = "Button";
            // 
            // lbl_size2
            // 
            this.lbl_size2.AutoSize = true;
            this.lbl_size2.Location = new System.Drawing.Point(3, 180);
            this.lbl_size2.Name = "lbl_size2";
            this.lbl_size2.Size = new System.Drawing.Size(27, 13);
            this.lbl_size2.TabIndex = 6;
            this.lbl_size2.Text = "Size";
            // 
            // lbl_buttonlong2
            // 
            this.lbl_buttonlong2.AutoSize = true;
            this.lbl_buttonlong2.Location = new System.Drawing.Point(3, 90);
            this.lbl_buttonlong2.Name = "lbl_buttonlong2";
            this.lbl_buttonlong2.Size = new System.Drawing.Size(47, 13);
            this.lbl_buttonlong2.TabIndex = 12;
            this.lbl_buttonlong2.Text = "BtnLong";
            // 
            // gb_tagslot4
            // 
            this.gb_tagslot4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.gb_tagslot4.BorderColorLight = System.Drawing.SystemColors.ControlLightLight;
            this.gb_tagslot4.BorderRadius = 5;
            this.gb_tagslot4.BorderWidth = 1F;
            this.gb_tagslot4.Controls.Add(this.tableLayoutPanel4);
            this.gb_tagslot4.Location = new System.Drawing.Point(934, 30);
            this.gb_tagslot4.Name = "gb_tagslot4";
            this.gb_tagslot4.Size = new System.Drawing.Size(278, 242);
            this.gb_tagslot4.TabIndex = 23;
            this.gb_tagslot4.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AllowDrop = true;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel4.Controls.Add(this.lbl_mode4, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.cb_Rbuttonlong4, 2, 3);
            this.tableLayoutPanel4.Controls.Add(this.cb_ledred4, 1, 5);
            this.tableLayoutPanel4.Controls.Add(this.cb_Rbutton4, 2, 2);
            this.tableLayoutPanel4.Controls.Add(this.lbl_uid4, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.txt_size4, 1, 6);
            this.tableLayoutPanel4.Controls.Add(this.cb_ledgreen4, 1, 4);
            this.tableLayoutPanel4.Controls.Add(this.lbl_button4, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.lb_ledledred4, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this.lbl_buttonlong4, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.cb_Lbuttonlong4, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.lb_ledledgreen4, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.lbl_size4, 0, 6);
            this.tableLayoutPanel4.Controls.Add(this.cb_Lbutton4, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.cb_mode4, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.txt_uid4, 1, 1);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(9, 18);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 7;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(254, 213);
            this.tableLayoutPanel4.TabIndex = 22;
            this.tableLayoutPanel4.DragDrop += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel_DragDrop);
            this.tableLayoutPanel4.DragEnter += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel_DragEnter);
            // 
            // lbl_mode4
            // 
            this.lbl_mode4.AutoSize = true;
            this.lbl_mode4.Location = new System.Drawing.Point(3, 0);
            this.lbl_mode4.Name = "lbl_mode4";
            this.lbl_mode4.Size = new System.Drawing.Size(34, 13);
            this.lbl_mode4.TabIndex = 1;
            this.lbl_mode4.Text = "Mode";
            // 
            // cb_Rbuttonlong4
            // 
            this.cb_Rbuttonlong4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Rbuttonlong4.DropDownWidth = 160;
            this.cb_Rbuttonlong4.FormattingEnabled = true;
            this.cb_Rbuttonlong4.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Rbuttonlong4.Location = new System.Drawing.Point(167, 93);
            this.cb_Rbuttonlong4.Name = "cb_Rbuttonlong4";
            this.cb_Rbuttonlong4.Size = new System.Drawing.Size(80, 21);
            this.cb_Rbuttonlong4.TabIndex = 17;
            // 
            // cb_ledred4
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.cb_ledred4, 2);
            this.cb_ledred4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ledred4.FormattingEnabled = true;
            this.cb_ledred4.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_ledred4.Location = new System.Drawing.Point(79, 153);
            this.cb_ledred4.Name = "cb_ledred4";
            this.cb_ledred4.Size = new System.Drawing.Size(172, 21);
            this.cb_ledred4.TabIndex = 21;
            // 
            // cb_Rbutton4
            // 
            this.cb_Rbutton4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Rbutton4.DropDownWidth = 160;
            this.cb_Rbutton4.FormattingEnabled = true;
            this.cb_Rbutton4.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Rbutton4.Location = new System.Drawing.Point(167, 63);
            this.cb_Rbutton4.Name = "cb_Rbutton4";
            this.cb_Rbutton4.Size = new System.Drawing.Size(80, 21);
            this.cb_Rbutton4.TabIndex = 16;
            // 
            // lbl_uid4
            // 
            this.lbl_uid4.AutoSize = true;
            this.lbl_uid4.Location = new System.Drawing.Point(3, 30);
            this.lbl_uid4.Name = "lbl_uid4";
            this.lbl_uid4.Size = new System.Drawing.Size(26, 13);
            this.lbl_uid4.TabIndex = 2;
            this.lbl_uid4.Text = "UID";
            // 
            // txt_size4
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.txt_size4, 2);
            this.txt_size4.Location = new System.Drawing.Point(79, 183);
            this.txt_size4.Name = "txt_size4";
            this.txt_size4.ReadOnly = true;
            this.txt_size4.Size = new System.Drawing.Size(172, 20);
            this.txt_size4.TabIndex = 13;
            // 
            // cb_ledgreen4
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.cb_ledgreen4, 2);
            this.cb_ledgreen4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ledgreen4.FormattingEnabled = true;
            this.cb_ledgreen4.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_ledgreen4.Location = new System.Drawing.Point(79, 123);
            this.cb_ledgreen4.Name = "cb_ledgreen4";
            this.cb_ledgreen4.Size = new System.Drawing.Size(172, 21);
            this.cb_ledgreen4.TabIndex = 20;
            // 
            // lbl_button4
            // 
            this.lbl_button4.AutoSize = true;
            this.lbl_button4.Location = new System.Drawing.Point(3, 60);
            this.lbl_button4.Name = "lbl_button4";
            this.lbl_button4.Size = new System.Drawing.Size(38, 13);
            this.lbl_button4.TabIndex = 4;
            this.lbl_button4.Text = "Button";
            // 
            // lb_ledledred4
            // 
            this.lb_ledledred4.AutoSize = true;
            this.lb_ledledred4.Location = new System.Drawing.Point(3, 150);
            this.lb_ledledred4.Name = "lb_ledledred4";
            this.lb_ledledred4.Size = new System.Drawing.Size(48, 13);
            this.lb_ledledred4.TabIndex = 19;
            this.lb_ledledred4.Text = "LEDRed";
            // 
            // lbl_buttonlong4
            // 
            this.lbl_buttonlong4.AutoSize = true;
            this.lbl_buttonlong4.Location = new System.Drawing.Point(3, 90);
            this.lbl_buttonlong4.Name = "lbl_buttonlong4";
            this.lbl_buttonlong4.Size = new System.Drawing.Size(47, 13);
            this.lbl_buttonlong4.TabIndex = 14;
            this.lbl_buttonlong4.Text = "BtnLong";
            // 
            // cb_Lbuttonlong4
            // 
            this.cb_Lbuttonlong4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Lbuttonlong4.DropDownWidth = 160;
            this.cb_Lbuttonlong4.FormattingEnabled = true;
            this.cb_Lbuttonlong4.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Lbuttonlong4.Location = new System.Drawing.Point(79, 93);
            this.cb_Lbuttonlong4.Name = "cb_Lbuttonlong4";
            this.cb_Lbuttonlong4.Size = new System.Drawing.Size(80, 21);
            this.cb_Lbuttonlong4.TabIndex = 15;
            // 
            // lb_ledledgreen4
            // 
            this.lb_ledledgreen4.AutoSize = true;
            this.lb_ledledgreen4.Location = new System.Drawing.Point(3, 120);
            this.lb_ledledgreen4.Name = "lb_ledledgreen4";
            this.lb_ledledgreen4.Size = new System.Drawing.Size(57, 13);
            this.lb_ledledgreen4.TabIndex = 18;
            this.lb_ledledgreen4.Text = "LEDGreen";
            // 
            // lbl_size4
            // 
            this.lbl_size4.AutoSize = true;
            this.lbl_size4.Location = new System.Drawing.Point(3, 180);
            this.lbl_size4.Name = "lbl_size4";
            this.lbl_size4.Size = new System.Drawing.Size(27, 13);
            this.lbl_size4.TabIndex = 12;
            this.lbl_size4.Text = "Size";
            // 
            // cb_Lbutton4
            // 
            this.cb_Lbutton4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Lbutton4.DropDownWidth = 160;
            this.cb_Lbutton4.FormattingEnabled = true;
            this.cb_Lbutton4.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Lbutton4.Location = new System.Drawing.Point(79, 63);
            this.cb_Lbutton4.Name = "cb_Lbutton4";
            this.cb_Lbutton4.Size = new System.Drawing.Size(80, 21);
            this.cb_Lbutton4.TabIndex = 5;
            // 
            // cb_mode4
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.cb_mode4, 2);
            this.cb_mode4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_mode4.FormattingEnabled = true;
            this.cb_mode4.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_mode4.Location = new System.Drawing.Point(79, 3);
            this.cb_mode4.Name = "cb_mode4";
            this.cb_mode4.Size = new System.Drawing.Size(172, 21);
            this.cb_mode4.TabIndex = 0;
            // 
            // txt_uid4
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.txt_uid4, 2);
            this.txt_uid4.Location = new System.Drawing.Point(79, 33);
            this.txt_uid4.Name = "txt_uid4";
            this.txt_uid4.Size = new System.Drawing.Size(172, 20);
            this.txt_uid4.TabIndex = 3;
            // 
            // gb_tagslot3
            // 
            this.gb_tagslot3.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.gb_tagslot3.BorderColorLight = System.Drawing.SystemColors.ControlLightLight;
            this.gb_tagslot3.BorderRadius = 5;
            this.gb_tagslot3.BorderWidth = 1F;
            this.gb_tagslot3.Controls.Add(this.tableLayoutPanel3);
            this.gb_tagslot3.Location = new System.Drawing.Point(643, 30);
            this.gb_tagslot3.Name = "gb_tagslot3";
            this.gb_tagslot3.Size = new System.Drawing.Size(278, 242);
            this.gb_tagslot3.TabIndex = 22;
            this.gb_tagslot3.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AllowDrop = true;
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel3.Controls.Add(this.lbl_mode3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.cb_Rbuttonlong3, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.cb_ledred3, 1, 5);
            this.tableLayoutPanel3.Controls.Add(this.cb_Rbutton3, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.lbl_uid3, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.txt_size3, 1, 6);
            this.tableLayoutPanel3.Controls.Add(this.cb_ledgreen3, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.lbl_button3, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.lb_ledledred3, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.lbl_buttonlong3, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.cb_Lbuttonlong3, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.lb_ledledgreen3, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.lbl_size3, 0, 6);
            this.tableLayoutPanel3.Controls.Add(this.cb_Lbutton3, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.cb_mode3, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.txt_uid3, 1, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(9, 18);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 7;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(254, 213);
            this.tableLayoutPanel3.TabIndex = 22;
            this.tableLayoutPanel3.DragDrop += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel_DragDrop);
            this.tableLayoutPanel3.DragEnter += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel_DragEnter);
            // 
            // lbl_mode3
            // 
            this.lbl_mode3.AutoSize = true;
            this.lbl_mode3.Location = new System.Drawing.Point(3, 0);
            this.lbl_mode3.Name = "lbl_mode3";
            this.lbl_mode3.Size = new System.Drawing.Size(34, 13);
            this.lbl_mode3.TabIndex = 1;
            this.lbl_mode3.Text = "Mode";
            // 
            // cb_Rbuttonlong3
            // 
            this.cb_Rbuttonlong3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Rbuttonlong3.DropDownWidth = 160;
            this.cb_Rbuttonlong3.FormattingEnabled = true;
            this.cb_Rbuttonlong3.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Rbuttonlong3.Location = new System.Drawing.Point(167, 93);
            this.cb_Rbuttonlong3.Name = "cb_Rbuttonlong3";
            this.cb_Rbuttonlong3.Size = new System.Drawing.Size(80, 21);
            this.cb_Rbuttonlong3.TabIndex = 17;
            // 
            // cb_ledred3
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.cb_ledred3, 2);
            this.cb_ledred3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ledred3.FormattingEnabled = true;
            this.cb_ledred3.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_ledred3.Location = new System.Drawing.Point(79, 153);
            this.cb_ledred3.Name = "cb_ledred3";
            this.cb_ledred3.Size = new System.Drawing.Size(172, 21);
            this.cb_ledred3.TabIndex = 21;
            // 
            // cb_Rbutton3
            // 
            this.cb_Rbutton3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Rbutton3.DropDownWidth = 160;
            this.cb_Rbutton3.FormattingEnabled = true;
            this.cb_Rbutton3.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Rbutton3.Location = new System.Drawing.Point(167, 63);
            this.cb_Rbutton3.Name = "cb_Rbutton3";
            this.cb_Rbutton3.Size = new System.Drawing.Size(80, 21);
            this.cb_Rbutton3.TabIndex = 16;
            // 
            // lbl_uid3
            // 
            this.lbl_uid3.AutoSize = true;
            this.lbl_uid3.Location = new System.Drawing.Point(3, 30);
            this.lbl_uid3.Name = "lbl_uid3";
            this.lbl_uid3.Size = new System.Drawing.Size(26, 13);
            this.lbl_uid3.TabIndex = 2;
            this.lbl_uid3.Text = "UID";
            // 
            // txt_size3
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.txt_size3, 2);
            this.txt_size3.Location = new System.Drawing.Point(79, 183);
            this.txt_size3.Name = "txt_size3";
            this.txt_size3.ReadOnly = true;
            this.txt_size3.Size = new System.Drawing.Size(172, 20);
            this.txt_size3.TabIndex = 11;
            // 
            // cb_ledgreen3
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.cb_ledgreen3, 2);
            this.cb_ledgreen3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ledgreen3.FormattingEnabled = true;
            this.cb_ledgreen3.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_ledgreen3.Location = new System.Drawing.Point(79, 123);
            this.cb_ledgreen3.Name = "cb_ledgreen3";
            this.cb_ledgreen3.Size = new System.Drawing.Size(172, 21);
            this.cb_ledgreen3.TabIndex = 20;
            // 
            // lbl_button3
            // 
            this.lbl_button3.AutoSize = true;
            this.lbl_button3.Location = new System.Drawing.Point(3, 60);
            this.lbl_button3.Name = "lbl_button3";
            this.lbl_button3.Size = new System.Drawing.Size(38, 13);
            this.lbl_button3.TabIndex = 4;
            this.lbl_button3.Text = "Button";
            // 
            // lb_ledledred3
            // 
            this.lb_ledledred3.AutoSize = true;
            this.lb_ledledred3.Location = new System.Drawing.Point(3, 150);
            this.lb_ledledred3.Name = "lb_ledledred3";
            this.lb_ledledred3.Size = new System.Drawing.Size(48, 13);
            this.lb_ledledred3.TabIndex = 19;
            this.lb_ledledred3.Text = "LEDRed";
            // 
            // lbl_buttonlong3
            // 
            this.lbl_buttonlong3.AutoSize = true;
            this.lbl_buttonlong3.Location = new System.Drawing.Point(3, 90);
            this.lbl_buttonlong3.Name = "lbl_buttonlong3";
            this.lbl_buttonlong3.Size = new System.Drawing.Size(47, 13);
            this.lbl_buttonlong3.TabIndex = 14;
            this.lbl_buttonlong3.Text = "BtnLong";
            // 
            // cb_Lbuttonlong3
            // 
            this.cb_Lbuttonlong3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Lbuttonlong3.DropDownWidth = 160;
            this.cb_Lbuttonlong3.FormattingEnabled = true;
            this.cb_Lbuttonlong3.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Lbuttonlong3.Location = new System.Drawing.Point(79, 93);
            this.cb_Lbuttonlong3.Name = "cb_Lbuttonlong3";
            this.cb_Lbuttonlong3.Size = new System.Drawing.Size(80, 21);
            this.cb_Lbuttonlong3.TabIndex = 15;
            // 
            // lb_ledledgreen3
            // 
            this.lb_ledledgreen3.AutoSize = true;
            this.lb_ledledgreen3.Location = new System.Drawing.Point(3, 120);
            this.lb_ledledgreen3.Name = "lb_ledledgreen3";
            this.lb_ledledgreen3.Size = new System.Drawing.Size(57, 13);
            this.lb_ledledgreen3.TabIndex = 18;
            this.lb_ledledgreen3.Text = "LEDGreen";
            // 
            // lbl_size3
            // 
            this.lbl_size3.AutoSize = true;
            this.lbl_size3.Location = new System.Drawing.Point(3, 180);
            this.lbl_size3.Name = "lbl_size3";
            this.lbl_size3.Size = new System.Drawing.Size(27, 13);
            this.lbl_size3.TabIndex = 10;
            this.lbl_size3.Text = "Size";
            // 
            // cb_Lbutton3
            // 
            this.cb_Lbutton3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Lbutton3.DropDownWidth = 160;
            this.cb_Lbutton3.FormattingEnabled = true;
            this.cb_Lbutton3.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Lbutton3.Location = new System.Drawing.Point(79, 63);
            this.cb_Lbutton3.Name = "cb_Lbutton3";
            this.cb_Lbutton3.Size = new System.Drawing.Size(80, 21);
            this.cb_Lbutton3.TabIndex = 5;
            // 
            // cb_mode3
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.cb_mode3, 2);
            this.cb_mode3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_mode3.FormattingEnabled = true;
            this.cb_mode3.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_mode3.Location = new System.Drawing.Point(79, 3);
            this.cb_mode3.Name = "cb_mode3";
            this.cb_mode3.Size = new System.Drawing.Size(172, 21);
            this.cb_mode3.TabIndex = 0;
            // 
            // txt_uid3
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.txt_uid3, 2);
            this.txt_uid3.Location = new System.Drawing.Point(79, 33);
            this.txt_uid3.Name = "txt_uid3";
            this.txt_uid3.Size = new System.Drawing.Size(172, 20);
            this.txt_uid3.TabIndex = 3;
            // 
            // gb_tagslot5
            // 
            this.gb_tagslot5.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.gb_tagslot5.BorderColorLight = System.Drawing.SystemColors.ControlLightLight;
            this.gb_tagslot5.BorderRadius = 5;
            this.gb_tagslot5.BorderWidth = 1F;
            this.gb_tagslot5.Controls.Add(this.tableLayoutPanel5);
            this.gb_tagslot5.Location = new System.Drawing.Point(61, 295);
            this.gb_tagslot5.Name = "gb_tagslot5";
            this.gb_tagslot5.Size = new System.Drawing.Size(278, 242);
            this.gb_tagslot5.TabIndex = 21;
            this.gb_tagslot5.TabStop = false;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AllowDrop = true;
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel5.Controls.Add(this.lbl_mode5, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.cb_Rbuttonlong5, 2, 3);
            this.tableLayoutPanel5.Controls.Add(this.cb_ledred5, 1, 5);
            this.tableLayoutPanel5.Controls.Add(this.cb_Rbutton5, 2, 2);
            this.tableLayoutPanel5.Controls.Add(this.lbl_uid5, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.txt_size5, 1, 6);
            this.tableLayoutPanel5.Controls.Add(this.cb_ledgreen5, 1, 4);
            this.tableLayoutPanel5.Controls.Add(this.lbl_button5, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.lb_ledledred5, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.lbl_buttonlong5, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.cb_Lbuttonlong5, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.lb_ledledgreen5, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.lbl_size5, 0, 6);
            this.tableLayoutPanel5.Controls.Add(this.cb_Lbutton5, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.cb_mode5, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.txt_uid5, 1, 1);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(9, 17);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 7;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(254, 213);
            this.tableLayoutPanel5.TabIndex = 24;
            this.tableLayoutPanel5.DragDrop += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel_DragDrop);
            this.tableLayoutPanel5.DragEnter += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel_DragEnter);
            // 
            // lbl_mode5
            // 
            this.lbl_mode5.AutoSize = true;
            this.lbl_mode5.Location = new System.Drawing.Point(3, 0);
            this.lbl_mode5.Name = "lbl_mode5";
            this.lbl_mode5.Size = new System.Drawing.Size(34, 13);
            this.lbl_mode5.TabIndex = 1;
            this.lbl_mode5.Text = "Mode";
            // 
            // cb_Rbuttonlong5
            // 
            this.cb_Rbuttonlong5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Rbuttonlong5.DropDownWidth = 160;
            this.cb_Rbuttonlong5.FormattingEnabled = true;
            this.cb_Rbuttonlong5.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Rbuttonlong5.Location = new System.Drawing.Point(167, 93);
            this.cb_Rbuttonlong5.Name = "cb_Rbuttonlong5";
            this.cb_Rbuttonlong5.Size = new System.Drawing.Size(80, 21);
            this.cb_Rbuttonlong5.TabIndex = 19;
            // 
            // cb_ledred5
            // 
            this.tableLayoutPanel5.SetColumnSpan(this.cb_ledred5, 2);
            this.cb_ledred5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ledred5.FormattingEnabled = true;
            this.cb_ledred5.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_ledred5.Location = new System.Drawing.Point(79, 153);
            this.cb_ledred5.Name = "cb_ledred5";
            this.cb_ledred5.Size = new System.Drawing.Size(172, 21);
            this.cb_ledred5.TabIndex = 23;
            // 
            // cb_Rbutton5
            // 
            this.cb_Rbutton5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Rbutton5.DropDownWidth = 160;
            this.cb_Rbutton5.FormattingEnabled = true;
            this.cb_Rbutton5.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Rbutton5.Location = new System.Drawing.Point(167, 63);
            this.cb_Rbutton5.Name = "cb_Rbutton5";
            this.cb_Rbutton5.Size = new System.Drawing.Size(80, 21);
            this.cb_Rbutton5.TabIndex = 18;
            // 
            // lbl_uid5
            // 
            this.lbl_uid5.AutoSize = true;
            this.lbl_uid5.Location = new System.Drawing.Point(3, 30);
            this.lbl_uid5.Name = "lbl_uid5";
            this.lbl_uid5.Size = new System.Drawing.Size(26, 13);
            this.lbl_uid5.TabIndex = 2;
            this.lbl_uid5.Text = "UID";
            // 
            // txt_size5
            // 
            this.tableLayoutPanel5.SetColumnSpan(this.txt_size5, 2);
            this.txt_size5.Location = new System.Drawing.Point(79, 183);
            this.txt_size5.Name = "txt_size5";
            this.txt_size5.ReadOnly = true;
            this.txt_size5.Size = new System.Drawing.Size(172, 20);
            this.txt_size5.TabIndex = 15;
            // 
            // cb_ledgreen5
            // 
            this.tableLayoutPanel5.SetColumnSpan(this.cb_ledgreen5, 2);
            this.cb_ledgreen5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ledgreen5.FormattingEnabled = true;
            this.cb_ledgreen5.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_ledgreen5.Location = new System.Drawing.Point(79, 123);
            this.cb_ledgreen5.Name = "cb_ledgreen5";
            this.cb_ledgreen5.Size = new System.Drawing.Size(172, 21);
            this.cb_ledgreen5.TabIndex = 22;
            // 
            // lbl_button5
            // 
            this.lbl_button5.AutoSize = true;
            this.lbl_button5.Location = new System.Drawing.Point(3, 60);
            this.lbl_button5.Name = "lbl_button5";
            this.lbl_button5.Size = new System.Drawing.Size(38, 13);
            this.lbl_button5.TabIndex = 4;
            this.lbl_button5.Text = "Button";
            // 
            // lb_ledledred5
            // 
            this.lb_ledledred5.AutoSize = true;
            this.lb_ledledred5.Location = new System.Drawing.Point(3, 150);
            this.lb_ledledred5.Name = "lb_ledledred5";
            this.lb_ledledred5.Size = new System.Drawing.Size(48, 13);
            this.lb_ledledred5.TabIndex = 21;
            this.lb_ledledred5.Text = "LEDRed";
            // 
            // lbl_buttonlong5
            // 
            this.lbl_buttonlong5.AutoSize = true;
            this.lbl_buttonlong5.Location = new System.Drawing.Point(3, 90);
            this.lbl_buttonlong5.Name = "lbl_buttonlong5";
            this.lbl_buttonlong5.Size = new System.Drawing.Size(47, 13);
            this.lbl_buttonlong5.TabIndex = 16;
            this.lbl_buttonlong5.Text = "BtnLong";
            // 
            // cb_Lbuttonlong5
            // 
            this.cb_Lbuttonlong5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Lbuttonlong5.DropDownWidth = 160;
            this.cb_Lbuttonlong5.FormattingEnabled = true;
            this.cb_Lbuttonlong5.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Lbuttonlong5.Location = new System.Drawing.Point(79, 93);
            this.cb_Lbuttonlong5.Name = "cb_Lbuttonlong5";
            this.cb_Lbuttonlong5.Size = new System.Drawing.Size(80, 21);
            this.cb_Lbuttonlong5.TabIndex = 17;
            // 
            // lb_ledledgreen5
            // 
            this.lb_ledledgreen5.AutoSize = true;
            this.lb_ledledgreen5.Location = new System.Drawing.Point(3, 120);
            this.lb_ledledgreen5.Name = "lb_ledledgreen5";
            this.lb_ledledgreen5.Size = new System.Drawing.Size(57, 13);
            this.lb_ledledgreen5.TabIndex = 20;
            this.lb_ledledgreen5.Text = "LEDGreen";
            // 
            // lbl_size5
            // 
            this.lbl_size5.AutoSize = true;
            this.lbl_size5.Location = new System.Drawing.Point(3, 180);
            this.lbl_size5.Name = "lbl_size5";
            this.lbl_size5.Size = new System.Drawing.Size(27, 13);
            this.lbl_size5.TabIndex = 14;
            this.lbl_size5.Text = "Size";
            // 
            // cb_Lbutton5
            // 
            this.cb_Lbutton5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Lbutton5.DropDownWidth = 160;
            this.cb_Lbutton5.FormattingEnabled = true;
            this.cb_Lbutton5.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Lbutton5.Location = new System.Drawing.Point(79, 63);
            this.cb_Lbutton5.Name = "cb_Lbutton5";
            this.cb_Lbutton5.Size = new System.Drawing.Size(80, 21);
            this.cb_Lbutton5.TabIndex = 5;
            // 
            // cb_mode5
            // 
            this.tableLayoutPanel5.SetColumnSpan(this.cb_mode5, 2);
            this.cb_mode5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_mode5.FormattingEnabled = true;
            this.cb_mode5.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_mode5.Location = new System.Drawing.Point(79, 3);
            this.cb_mode5.Name = "cb_mode5";
            this.cb_mode5.Size = new System.Drawing.Size(172, 21);
            this.cb_mode5.TabIndex = 0;
            // 
            // txt_uid5
            // 
            this.tableLayoutPanel5.SetColumnSpan(this.txt_uid5, 2);
            this.txt_uid5.Location = new System.Drawing.Point(79, 33);
            this.txt_uid5.Name = "txt_uid5";
            this.txt_uid5.Size = new System.Drawing.Size(172, 20);
            this.txt_uid5.TabIndex = 3;
            // 
            // gb_tagslot7
            // 
            this.gb_tagslot7.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.gb_tagslot7.BorderColorLight = System.Drawing.SystemColors.ControlLightLight;
            this.gb_tagslot7.BorderRadius = 5;
            this.gb_tagslot7.BorderWidth = 1F;
            this.gb_tagslot7.Controls.Add(this.tableLayoutPanel7);
            this.gb_tagslot7.Location = new System.Drawing.Point(643, 295);
            this.gb_tagslot7.Name = "gb_tagslot7";
            this.gb_tagslot7.Size = new System.Drawing.Size(278, 242);
            this.gb_tagslot7.TabIndex = 27;
            this.gb_tagslot7.TabStop = false;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.AllowDrop = true;
            this.tableLayoutPanel7.ColumnCount = 3;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel7.Controls.Add(this.lbl_mode7, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.cb_Rbuttonlong7, 2, 3);
            this.tableLayoutPanel7.Controls.Add(this.cb_ledred7, 1, 5);
            this.tableLayoutPanel7.Controls.Add(this.cb_Rbutton7, 2, 2);
            this.tableLayoutPanel7.Controls.Add(this.lbl_uid7, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.txt_size7, 1, 6);
            this.tableLayoutPanel7.Controls.Add(this.cb_ledgreen7, 1, 4);
            this.tableLayoutPanel7.Controls.Add(this.lbl_button7, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.lb_ledledred7, 0, 5);
            this.tableLayoutPanel7.Controls.Add(this.lbl_buttonlong7, 0, 3);
            this.tableLayoutPanel7.Controls.Add(this.cb_Lbuttonlong7, 1, 3);
            this.tableLayoutPanel7.Controls.Add(this.lb_ledledgreen7, 0, 4);
            this.tableLayoutPanel7.Controls.Add(this.lbl_size7, 0, 6);
            this.tableLayoutPanel7.Controls.Add(this.cb_Lbutton7, 1, 2);
            this.tableLayoutPanel7.Controls.Add(this.cb_mode7, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.txt_uid7, 1, 1);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(9, 17);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 7;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(254, 213);
            this.tableLayoutPanel7.TabIndex = 26;
            this.tableLayoutPanel7.DragDrop += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel_DragDrop);
            this.tableLayoutPanel7.DragEnter += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel_DragEnter);
            // 
            // lbl_mode7
            // 
            this.lbl_mode7.AutoSize = true;
            this.lbl_mode7.Location = new System.Drawing.Point(3, 0);
            this.lbl_mode7.Name = "lbl_mode7";
            this.lbl_mode7.Size = new System.Drawing.Size(34, 13);
            this.lbl_mode7.TabIndex = 1;
            this.lbl_mode7.Text = "Mode";
            // 
            // cb_Rbuttonlong7
            // 
            this.cb_Rbuttonlong7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Rbuttonlong7.DropDownWidth = 160;
            this.cb_Rbuttonlong7.FormattingEnabled = true;
            this.cb_Rbuttonlong7.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Rbuttonlong7.Location = new System.Drawing.Point(167, 93);
            this.cb_Rbuttonlong7.Name = "cb_Rbuttonlong7";
            this.cb_Rbuttonlong7.Size = new System.Drawing.Size(80, 21);
            this.cb_Rbuttonlong7.TabIndex = 21;
            // 
            // cb_ledred7
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.cb_ledred7, 2);
            this.cb_ledred7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ledred7.FormattingEnabled = true;
            this.cb_ledred7.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_ledred7.Location = new System.Drawing.Point(79, 153);
            this.cb_ledred7.Name = "cb_ledred7";
            this.cb_ledred7.Size = new System.Drawing.Size(172, 21);
            this.cb_ledred7.TabIndex = 25;
            // 
            // cb_Rbutton7
            // 
            this.cb_Rbutton7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Rbutton7.DropDownWidth = 160;
            this.cb_Rbutton7.FormattingEnabled = true;
            this.cb_Rbutton7.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Rbutton7.Location = new System.Drawing.Point(167, 63);
            this.cb_Rbutton7.Name = "cb_Rbutton7";
            this.cb_Rbutton7.Size = new System.Drawing.Size(80, 21);
            this.cb_Rbutton7.TabIndex = 20;
            // 
            // lbl_uid7
            // 
            this.lbl_uid7.AutoSize = true;
            this.lbl_uid7.Location = new System.Drawing.Point(3, 30);
            this.lbl_uid7.Name = "lbl_uid7";
            this.lbl_uid7.Size = new System.Drawing.Size(26, 13);
            this.lbl_uid7.TabIndex = 2;
            this.lbl_uid7.Text = "UID";
            // 
            // txt_size7
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.txt_size7, 2);
            this.txt_size7.Location = new System.Drawing.Point(79, 183);
            this.txt_size7.Name = "txt_size7";
            this.txt_size7.ReadOnly = true;
            this.txt_size7.Size = new System.Drawing.Size(172, 20);
            this.txt_size7.TabIndex = 15;
            // 
            // cb_ledgreen7
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.cb_ledgreen7, 2);
            this.cb_ledgreen7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ledgreen7.FormattingEnabled = true;
            this.cb_ledgreen7.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_ledgreen7.Location = new System.Drawing.Point(79, 123);
            this.cb_ledgreen7.Name = "cb_ledgreen7";
            this.cb_ledgreen7.Size = new System.Drawing.Size(172, 21);
            this.cb_ledgreen7.TabIndex = 24;
            // 
            // lbl_button7
            // 
            this.lbl_button7.AutoSize = true;
            this.lbl_button7.Location = new System.Drawing.Point(3, 60);
            this.lbl_button7.Name = "lbl_button7";
            this.lbl_button7.Size = new System.Drawing.Size(38, 13);
            this.lbl_button7.TabIndex = 4;
            this.lbl_button7.Text = "Button";
            // 
            // lb_ledledred7
            // 
            this.lb_ledledred7.AutoSize = true;
            this.lb_ledledred7.Location = new System.Drawing.Point(3, 150);
            this.lb_ledledred7.Name = "lb_ledledred7";
            this.lb_ledledred7.Size = new System.Drawing.Size(48, 13);
            this.lb_ledledred7.TabIndex = 23;
            this.lb_ledledred7.Text = "LEDRed";
            // 
            // lbl_buttonlong7
            // 
            this.lbl_buttonlong7.AutoSize = true;
            this.lbl_buttonlong7.Location = new System.Drawing.Point(3, 90);
            this.lbl_buttonlong7.Name = "lbl_buttonlong7";
            this.lbl_buttonlong7.Size = new System.Drawing.Size(47, 13);
            this.lbl_buttonlong7.TabIndex = 18;
            this.lbl_buttonlong7.Text = "BtnLong";
            // 
            // cb_Lbuttonlong7
            // 
            this.cb_Lbuttonlong7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Lbuttonlong7.DropDownWidth = 160;
            this.cb_Lbuttonlong7.FormattingEnabled = true;
            this.cb_Lbuttonlong7.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Lbuttonlong7.Location = new System.Drawing.Point(79, 93);
            this.cb_Lbuttonlong7.Name = "cb_Lbuttonlong7";
            this.cb_Lbuttonlong7.Size = new System.Drawing.Size(80, 21);
            this.cb_Lbuttonlong7.TabIndex = 19;
            // 
            // lb_ledledgreen7
            // 
            this.lb_ledledgreen7.AutoSize = true;
            this.lb_ledledgreen7.Location = new System.Drawing.Point(3, 120);
            this.lb_ledledgreen7.Name = "lb_ledledgreen7";
            this.lb_ledledgreen7.Size = new System.Drawing.Size(57, 13);
            this.lb_ledledgreen7.TabIndex = 22;
            this.lb_ledledgreen7.Text = "LEDGreen";
            // 
            // lbl_size7
            // 
            this.lbl_size7.AutoSize = true;
            this.lbl_size7.Location = new System.Drawing.Point(3, 180);
            this.lbl_size7.Name = "lbl_size7";
            this.lbl_size7.Size = new System.Drawing.Size(27, 13);
            this.lbl_size7.TabIndex = 14;
            this.lbl_size7.Text = "Size";
            // 
            // cb_Lbutton7
            // 
            this.cb_Lbutton7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Lbutton7.DropDownWidth = 160;
            this.cb_Lbutton7.FormattingEnabled = true;
            this.cb_Lbutton7.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Lbutton7.Location = new System.Drawing.Point(79, 63);
            this.cb_Lbutton7.Name = "cb_Lbutton7";
            this.cb_Lbutton7.Size = new System.Drawing.Size(80, 21);
            this.cb_Lbutton7.TabIndex = 5;
            // 
            // cb_mode7
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.cb_mode7, 2);
            this.cb_mode7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_mode7.FormattingEnabled = true;
            this.cb_mode7.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_mode7.Location = new System.Drawing.Point(79, 3);
            this.cb_mode7.Name = "cb_mode7";
            this.cb_mode7.Size = new System.Drawing.Size(172, 21);
            this.cb_mode7.TabIndex = 0;
            // 
            // txt_uid7
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.txt_uid7, 2);
            this.txt_uid7.Location = new System.Drawing.Point(79, 33);
            this.txt_uid7.Name = "txt_uid7";
            this.txt_uid7.Size = new System.Drawing.Size(172, 20);
            this.txt_uid7.TabIndex = 3;
            // 
            // gb_tagslot8
            // 
            this.gb_tagslot8.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.gb_tagslot8.BorderColorLight = System.Drawing.SystemColors.ControlLightLight;
            this.gb_tagslot8.BorderRadius = 5;
            this.gb_tagslot8.BorderWidth = 1F;
            this.gb_tagslot8.Controls.Add(this.tableLayoutPanel8);
            this.gb_tagslot8.Location = new System.Drawing.Point(934, 295);
            this.gb_tagslot8.Name = "gb_tagslot8";
            this.gb_tagslot8.Size = new System.Drawing.Size(278, 242);
            this.gb_tagslot8.TabIndex = 20;
            this.gb_tagslot8.TabStop = false;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.AllowDrop = true;
            this.tableLayoutPanel8.ColumnCount = 3;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel8.Controls.Add(this.lbl_mode8, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.cb_Rbuttonlong8, 2, 3);
            this.tableLayoutPanel8.Controls.Add(this.cb_ledred8, 1, 5);
            this.tableLayoutPanel8.Controls.Add(this.cb_Rbutton8, 2, 2);
            this.tableLayoutPanel8.Controls.Add(this.lbl_uid8, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.txt_size8, 1, 6);
            this.tableLayoutPanel8.Controls.Add(this.cb_ledgreen8, 1, 4);
            this.tableLayoutPanel8.Controls.Add(this.lbl_button8, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.lb_ledledred8, 0, 5);
            this.tableLayoutPanel8.Controls.Add(this.lbl_buttonlong8, 0, 3);
            this.tableLayoutPanel8.Controls.Add(this.cb_Lbuttonlong8, 1, 3);
            this.tableLayoutPanel8.Controls.Add(this.lb_ledledgreen8, 0, 4);
            this.tableLayoutPanel8.Controls.Add(this.lbl_size8, 0, 6);
            this.tableLayoutPanel8.Controls.Add(this.cb_Lbutton8, 1, 2);
            this.tableLayoutPanel8.Controls.Add(this.cb_mode8, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.txt_uid8, 1, 1);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(9, 17);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 7;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(254, 213);
            this.tableLayoutPanel8.TabIndex = 26;
            this.tableLayoutPanel8.DragDrop += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel_DragDrop);
            this.tableLayoutPanel8.DragEnter += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel_DragEnter);
            // 
            // lbl_mode8
            // 
            this.lbl_mode8.AutoSize = true;
            this.lbl_mode8.Location = new System.Drawing.Point(3, 0);
            this.lbl_mode8.Name = "lbl_mode8";
            this.lbl_mode8.Size = new System.Drawing.Size(34, 13);
            this.lbl_mode8.TabIndex = 1;
            this.lbl_mode8.Text = "Mode";
            // 
            // cb_Rbuttonlong8
            // 
            this.cb_Rbuttonlong8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Rbuttonlong8.DropDownWidth = 160;
            this.cb_Rbuttonlong8.FormattingEnabled = true;
            this.cb_Rbuttonlong8.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Rbuttonlong8.Location = new System.Drawing.Point(167, 93);
            this.cb_Rbuttonlong8.Name = "cb_Rbuttonlong8";
            this.cb_Rbuttonlong8.Size = new System.Drawing.Size(80, 21);
            this.cb_Rbuttonlong8.TabIndex = 21;
            // 
            // cb_ledred8
            // 
            this.tableLayoutPanel8.SetColumnSpan(this.cb_ledred8, 2);
            this.cb_ledred8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ledred8.FormattingEnabled = true;
            this.cb_ledred8.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_ledred8.Location = new System.Drawing.Point(79, 153);
            this.cb_ledred8.Name = "cb_ledred8";
            this.cb_ledred8.Size = new System.Drawing.Size(172, 21);
            this.cb_ledred8.TabIndex = 25;
            // 
            // cb_Rbutton8
            // 
            this.cb_Rbutton8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Rbutton8.DropDownWidth = 160;
            this.cb_Rbutton8.FormattingEnabled = true;
            this.cb_Rbutton8.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Rbutton8.Location = new System.Drawing.Point(167, 63);
            this.cb_Rbutton8.Name = "cb_Rbutton8";
            this.cb_Rbutton8.Size = new System.Drawing.Size(80, 21);
            this.cb_Rbutton8.TabIndex = 20;
            // 
            // lbl_uid8
            // 
            this.lbl_uid8.AutoSize = true;
            this.lbl_uid8.Location = new System.Drawing.Point(3, 30);
            this.lbl_uid8.Name = "lbl_uid8";
            this.lbl_uid8.Size = new System.Drawing.Size(26, 13);
            this.lbl_uid8.TabIndex = 2;
            this.lbl_uid8.Text = "UID";
            // 
            // txt_size8
            // 
            this.tableLayoutPanel8.SetColumnSpan(this.txt_size8, 2);
            this.txt_size8.Location = new System.Drawing.Point(79, 183);
            this.txt_size8.Name = "txt_size8";
            this.txt_size8.ReadOnly = true;
            this.txt_size8.Size = new System.Drawing.Size(172, 20);
            this.txt_size8.TabIndex = 17;
            // 
            // cb_ledgreen8
            // 
            this.tableLayoutPanel8.SetColumnSpan(this.cb_ledgreen8, 2);
            this.cb_ledgreen8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ledgreen8.FormattingEnabled = true;
            this.cb_ledgreen8.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_ledgreen8.Location = new System.Drawing.Point(79, 123);
            this.cb_ledgreen8.Name = "cb_ledgreen8";
            this.cb_ledgreen8.Size = new System.Drawing.Size(172, 21);
            this.cb_ledgreen8.TabIndex = 24;
            // 
            // lbl_button8
            // 
            this.lbl_button8.AutoSize = true;
            this.lbl_button8.Location = new System.Drawing.Point(3, 60);
            this.lbl_button8.Name = "lbl_button8";
            this.lbl_button8.Size = new System.Drawing.Size(38, 13);
            this.lbl_button8.TabIndex = 4;
            this.lbl_button8.Text = "Button";
            // 
            // lb_ledledred8
            // 
            this.lb_ledledred8.AutoSize = true;
            this.lb_ledledred8.Location = new System.Drawing.Point(3, 150);
            this.lb_ledledred8.Name = "lb_ledledred8";
            this.lb_ledledred8.Size = new System.Drawing.Size(48, 13);
            this.lb_ledledred8.TabIndex = 23;
            this.lb_ledledred8.Text = "LEDRed";
            // 
            // lbl_buttonlong8
            // 
            this.lbl_buttonlong8.AutoSize = true;
            this.lbl_buttonlong8.Location = new System.Drawing.Point(3, 90);
            this.lbl_buttonlong8.Name = "lbl_buttonlong8";
            this.lbl_buttonlong8.Size = new System.Drawing.Size(47, 13);
            this.lbl_buttonlong8.TabIndex = 18;
            this.lbl_buttonlong8.Text = "BtnLong";
            // 
            // cb_Lbuttonlong8
            // 
            this.cb_Lbuttonlong8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Lbuttonlong8.DropDownWidth = 160;
            this.cb_Lbuttonlong8.FormattingEnabled = true;
            this.cb_Lbuttonlong8.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Lbuttonlong8.Location = new System.Drawing.Point(79, 93);
            this.cb_Lbuttonlong8.Name = "cb_Lbuttonlong8";
            this.cb_Lbuttonlong8.Size = new System.Drawing.Size(80, 21);
            this.cb_Lbuttonlong8.TabIndex = 19;
            // 
            // lb_ledledgreen8
            // 
            this.lb_ledledgreen8.AutoSize = true;
            this.lb_ledledgreen8.Location = new System.Drawing.Point(3, 120);
            this.lb_ledledgreen8.Name = "lb_ledledgreen8";
            this.lb_ledledgreen8.Size = new System.Drawing.Size(57, 13);
            this.lb_ledledgreen8.TabIndex = 22;
            this.lb_ledledgreen8.Text = "LEDGreen";
            // 
            // lbl_size8
            // 
            this.lbl_size8.AutoSize = true;
            this.lbl_size8.Location = new System.Drawing.Point(3, 180);
            this.lbl_size8.Name = "lbl_size8";
            this.lbl_size8.Size = new System.Drawing.Size(27, 13);
            this.lbl_size8.TabIndex = 16;
            this.lbl_size8.Text = "Size";
            // 
            // cb_Lbutton8
            // 
            this.cb_Lbutton8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Lbutton8.DropDownWidth = 160;
            this.cb_Lbutton8.FormattingEnabled = true;
            this.cb_Lbutton8.Items.AddRange(new object[] {
            "NONE",
            "UID_RANDOM",
            "UID_LEFT_INCREMENT",
            "UID_RIGHT_INCREMENT",
            "UID_LEFT_DECREMENT",
            "UID_RIGHT_DECREMENT",
            "CYCLE_SETTINGS",
            "STORE_MEM",
            "RECALL_MEM",
            "TOGGLE_FIELD",
            "STORE_LOG",
            "CLONE"});
            this.cb_Lbutton8.Location = new System.Drawing.Point(79, 63);
            this.cb_Lbutton8.Name = "cb_Lbutton8";
            this.cb_Lbutton8.Size = new System.Drawing.Size(80, 21);
            this.cb_Lbutton8.TabIndex = 5;
            // 
            // cb_mode8
            // 
            this.tableLayoutPanel8.SetColumnSpan(this.cb_mode8, 2);
            this.cb_mode8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_mode8.FormattingEnabled = true;
            this.cb_mode8.Items.AddRange(new object[] {
            "NONE",
            "MF_CLASSIC_1K",
            "MF_CLASSIC_1K_7B",
            "MF_CLASSIC_4K",
            "MF_CLASSIC_4K_7B",
            "MF_ULTRALIGHT",
            "ISO14443A_SNIFF",
            "ISO14443A_READER"});
            this.cb_mode8.Location = new System.Drawing.Point(79, 3);
            this.cb_mode8.Name = "cb_mode8";
            this.cb_mode8.Size = new System.Drawing.Size(172, 21);
            this.cb_mode8.TabIndex = 0;
            // 
            // txt_uid8
            // 
            this.tableLayoutPanel8.SetColumnSpan(this.txt_uid8, 2);
            this.txt_uid8.Location = new System.Drawing.Point(79, 33);
            this.txt_uid8.Name = "txt_uid8";
            this.txt_uid8.Size = new System.Drawing.Size(172, 20);
            this.txt_uid8.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpOperation);
            this.tabControl1.Controls.Add(this.tpDump);
            this.tabControl1.Controls.Add(this.tpUtils);
            this.tabControl1.Controls.Add(this.tpSerial);
            this.tabControl1.Controls.Add(this.tpSettings);
            this.tabControl1.Location = new System.Drawing.Point(8, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1287, 691);
            this.tabControl1.TabIndex = 8;
            // 
            // tpDump
            // 
            this.tpDump.AllowDrop = true;
            this.tpDump.ContextMenuStrip = this.menuScroll;
            this.tpDump.Controls.Add(this.ucLegend1);
            this.tpDump.Controls.Add(this.btn_close2);
            this.tpDump.Controls.Add(this.btn_close1);
            this.tpDump.Controls.Add(this.lbl_template);
            this.tpDump.Controls.Add(this.cb_templateA);
            this.tpDump.Controls.Add(this.chkSyncScroll);
            this.tpDump.Controls.Add(this.lbl_hbfilename2);
            this.tpDump.Controls.Add(this.lbl_hbfilename1);
            this.tpDump.Controls.Add(this.rbtn_bytewidth16);
            this.tpDump.Controls.Add(this.rbtn_bytewidth08);
            this.tpDump.Controls.Add(this.rbtn_bytewidth04);
            this.tpDump.Controls.Add(this.lbl_bytewidth);
            this.tpDump.Controls.Add(this.btn_save2);
            this.tpDump.Controls.Add(this.btn_open2);
            this.tpDump.Controls.Add(this.btn_save1);
            this.tpDump.Controls.Add(this.btn_open1);
            this.tpDump.Controls.Add(this.hexBox2);
            this.tpDump.Controls.Add(this.hexBox1);
            this.tpDump.Location = new System.Drawing.Point(4, 22);
            this.tpDump.Name = "tpDump";
            this.tpDump.Size = new System.Drawing.Size(1279, 665);
            this.tpDump.TabIndex = 4;
            this.tpDump.Text = "Dump Management";
            this.tpDump.UseVisualStyleBackColor = true;
            this.tpDump.DragDrop += new System.Windows.Forms.DragEventHandler(this.tabPage3_DragDrop);
            this.tpDump.DragEnter += new System.Windows.Forms.DragEventHandler(this.tabPage3_DragEnter);
            this.tpDump.MouseEnter += new System.EventHandler(this.tabPage3_MouseEnter);
            // 
            // menuScroll
            // 
            this.menuScroll.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuScroll.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.menuScroll.Name = "menuScroll";
            this.menuScroll.Size = new System.Drawing.Size(172, 48);
            this.menuScroll.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuScroll_ItemClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(171, 22);
            this.toolStripMenuItem1.Text = "Toggle Sync Scroll";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(171, 22);
            this.toolStripMenuItem2.Text = "Close Files";
            // 
            // ucLegend1
            // 
            this.ucLegend1.AutoSize = true;
            this.ucLegend1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucLegend1.Expanded = false;
            this.ucLegend1.Items = null;
            this.ucLegend1.Location = new System.Drawing.Point(978, 3);
            this.ucLegend1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucLegend1.MaximumSize = new System.Drawing.Size(180, 300);
            this.ucLegend1.Name = "ucLegend1";
            this.ucLegend1.Size = new System.Drawing.Size(180, 42);
            this.ucLegend1.TabIndex = 22;
            this.ucLegend1.Title = "Legend";
            // 
            // btn_close2
            // 
            this.btn_close2.Location = new System.Drawing.Point(837, 41);
            this.btn_close2.Name = "btn_close2";
            this.btn_close2.Size = new System.Drawing.Size(80, 23);
            this.btn_close2.TabIndex = 24;
            this.btn_close2.Text = "Close";
            this.btn_close2.UseVisualStyleBackColor = true;
            this.btn_close2.Click += new System.EventHandler(this.btn_close2_Click);
            // 
            // btn_close1
            // 
            this.btn_close1.Location = new System.Drawing.Point(208, 41);
            this.btn_close1.Name = "btn_close1";
            this.btn_close1.Size = new System.Drawing.Size(80, 23);
            this.btn_close1.TabIndex = 23;
            this.btn_close1.Text = "Close";
            this.btn_close1.UseVisualStyleBackColor = true;
            this.btn_close1.Click += new System.EventHandler(this.btn_close1_Click);
            // 
            // lbl_template
            // 
            this.lbl_template.AutoSize = true;
            this.lbl_template.Location = new System.Drawing.Point(435, 13);
            this.lbl_template.Name = "lbl_template";
            this.lbl_template.Size = new System.Drawing.Size(54, 13);
            this.lbl_template.TabIndex = 21;
            this.lbl_template.Text = "Template:";
            // 
            // cb_templateA
            // 
            this.cb_templateA.DataSource = this.bsTemplates;
            this.cb_templateA.FormattingEnabled = true;
            this.cb_templateA.Location = new System.Drawing.Point(495, 10);
            this.cb_templateA.Name = "cb_templateA";
            this.cb_templateA.Size = new System.Drawing.Size(151, 21);
            this.cb_templateA.TabIndex = 20;
            this.cb_templateA.SelectedIndexChanged += new System.EventHandler(this.cb_templateA_SelectedIndexChanged);
            // 
            // chkSyncScroll
            // 
            this.chkSyncScroll.AutoSize = true;
            this.chkSyncScroll.Location = new System.Drawing.Point(275, 12);
            this.chkSyncScroll.Name = "chkSyncScroll";
            this.chkSyncScroll.Size = new System.Drawing.Size(125, 17);
            this.chkSyncScroll.TabIndex = 18;
            this.chkSyncScroll.Text = "Synchronize scrolling";
            this.chkSyncScroll.UseVisualStyleBackColor = true;
            // 
            // lbl_hbfilename2
            // 
            this.lbl_hbfilename2.AutoSize = true;
            this.lbl_hbfilename2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_hbfilename2.Location = new System.Drawing.Point(640, 641);
            this.lbl_hbfilename2.Name = "lbl_hbfilename2";
            this.lbl_hbfilename2.Size = new System.Drawing.Size(30, 13);
            this.lbl_hbfilename2.TabIndex = 17;
            this.lbl_hbfilename2.Text = "N/A";
            // 
            // lbl_hbfilename1
            // 
            this.lbl_hbfilename1.AutoSize = true;
            this.lbl_hbfilename1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_hbfilename1.Location = new System.Drawing.Point(4, 641);
            this.lbl_hbfilename1.Name = "lbl_hbfilename1";
            this.lbl_hbfilename1.Size = new System.Drawing.Size(30, 13);
            this.lbl_hbfilename1.TabIndex = 15;
            this.lbl_hbfilename1.Text = "N/A";
            // 
            // rbtn_bytewidth16
            // 
            this.rbtn_bytewidth16.AutoSize = true;
            this.rbtn_bytewidth16.Location = new System.Drawing.Point(189, 11);
            this.rbtn_bytewidth16.Name = "rbtn_bytewidth16";
            this.rbtn_bytewidth16.Size = new System.Drawing.Size(60, 17);
            this.rbtn_bytewidth16.TabIndex = 14;
            this.rbtn_bytewidth16.Text = "16-byte";
            this.rbtn_bytewidth16.UseVisualStyleBackColor = true;
            this.rbtn_bytewidth16.CheckedChanged += new System.EventHandler(this.byteWidthCheckBoxes_CheckedChanged);
            // 
            // rbtn_bytewidth08
            // 
            this.rbtn_bytewidth08.AutoSize = true;
            this.rbtn_bytewidth08.Location = new System.Drawing.Point(129, 11);
            this.rbtn_bytewidth08.Name = "rbtn_bytewidth08";
            this.rbtn_bytewidth08.Size = new System.Drawing.Size(54, 17);
            this.rbtn_bytewidth08.TabIndex = 13;
            this.rbtn_bytewidth08.Text = "8-byte";
            this.rbtn_bytewidth08.UseVisualStyleBackColor = true;
            this.rbtn_bytewidth08.CheckedChanged += new System.EventHandler(this.byteWidthCheckBoxes_CheckedChanged);
            // 
            // rbtn_bytewidth04
            // 
            this.rbtn_bytewidth04.AutoSize = true;
            this.rbtn_bytewidth04.Checked = true;
            this.rbtn_bytewidth04.Location = new System.Drawing.Point(69, 11);
            this.rbtn_bytewidth04.Name = "rbtn_bytewidth04";
            this.rbtn_bytewidth04.Size = new System.Drawing.Size(54, 17);
            this.rbtn_bytewidth04.TabIndex = 12;
            this.rbtn_bytewidth04.TabStop = true;
            this.rbtn_bytewidth04.Text = "4-byte";
            this.rbtn_bytewidth04.UseVisualStyleBackColor = true;
            this.rbtn_bytewidth04.CheckedChanged += new System.EventHandler(this.byteWidthCheckBoxes_CheckedChanged);
            // 
            // lbl_bytewidth
            // 
            this.lbl_bytewidth.AutoSize = true;
            this.lbl_bytewidth.Location = new System.Drawing.Point(4, 13);
            this.lbl_bytewidth.Name = "lbl_bytewidth";
            this.lbl_bytewidth.Size = new System.Drawing.Size(59, 13);
            this.lbl_bytewidth.TabIndex = 11;
            this.lbl_bytewidth.Text = "Byte width:";
            // 
            // btn_save2
            // 
            this.btn_save2.Location = new System.Drawing.Point(729, 41);
            this.btn_save2.Name = "btn_save2";
            this.btn_save2.Size = new System.Drawing.Size(80, 23);
            this.btn_save2.TabIndex = 8;
            this.btn_save2.Text = "Save";
            this.btn_save2.UseVisualStyleBackColor = true;
            this.btn_save2.Click += new System.EventHandler(this.btn_save2_Click);
            // 
            // btn_open2
            // 
            this.btn_open2.Location = new System.Drawing.Point(643, 41);
            this.btn_open2.Name = "btn_open2";
            this.btn_open2.Size = new System.Drawing.Size(80, 23);
            this.btn_open2.TabIndex = 7;
            this.btn_open2.Text = "Open";
            this.btn_open2.UseVisualStyleBackColor = true;
            this.btn_open2.Click += new System.EventHandler(this.btn_open2_Click);
            // 
            // btn_save1
            // 
            this.btn_save1.Location = new System.Drawing.Point(93, 41);
            this.btn_save1.Name = "btn_save1";
            this.btn_save1.Size = new System.Drawing.Size(80, 23);
            this.btn_save1.TabIndex = 6;
            this.btn_save1.Text = "Save";
            this.btn_save1.UseVisualStyleBackColor = true;
            this.btn_save1.Click += new System.EventHandler(this.btn_save1_Click);
            // 
            // btn_open1
            // 
            this.btn_open1.Location = new System.Drawing.Point(7, 41);
            this.btn_open1.Name = "btn_open1";
            this.btn_open1.Size = new System.Drawing.Size(80, 23);
            this.btn_open1.TabIndex = 5;
            this.btn_open1.Text = "Open";
            this.btn_open1.UseVisualStyleBackColor = true;
            this.btn_open1.Click += new System.EventHandler(this.btn_open1_Click);
            // 
            // hexBox2
            // 
            this.hexBox2.ColumnInfoVisible = true;
            this.hexBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.hexBox2.GroupSeparatorVisible = true;
            this.hexBox2.GroupSize = 8;
            this.hexBox2.LineInfoVisible = true;
            this.hexBox2.Location = new System.Drawing.Point(643, 70);
            this.hexBox2.Name = "hexBox2";
            this.hexBox2.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox2.Size = new System.Drawing.Size(630, 568);
            this.hexBox2.StringViewVisible = true;
            this.hexBox2.TabIndex = 2;
            this.hexBox2.UseFixedBytesPerLine = true;
            this.hexBox2.VScrollBarVisible = true;
            this.hexBox2.ByteProviderWriteFinished += new System.EventHandler(this.hexBox_ByteProviderWriteFinished);
            this.hexBox2.VScrollBarChanged += new Be.Windows.Forms.HexBox.VScrollBarChangedEventHandler(this.hexBox2_VScrollBarChanged);
            this.hexBox2.ToggleSyncScrollPressed += new System.EventHandler(this.toggleSyncScrollPressed);
            this.hexBox2.MouseEnter += new System.EventHandler(this.hexBox_MouseEnter);
            // 
            // hexBox1
            // 
            this.hexBox1.ColumnInfoVisible = true;
            this.hexBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.hexBox1.GroupSeparatorVisible = true;
            this.hexBox1.GroupSize = 8;
            this.hexBox1.LineInfoVisible = true;
            this.hexBox1.Location = new System.Drawing.Point(7, 70);
            this.hexBox1.Name = "hexBox1";
            this.hexBox1.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox1.Size = new System.Drawing.Size(630, 568);
            this.hexBox1.StringViewVisible = true;
            this.hexBox1.TabIndex = 1;
            this.hexBox1.UseFixedBytesPerLine = true;
            this.hexBox1.VScrollBarVisible = true;
            this.hexBox1.ByteProviderWriteFinished += new System.EventHandler(this.hexBox_ByteProviderWriteFinished);
            this.hexBox1.VScrollBarChanged += new Be.Windows.Forms.HexBox.VScrollBarChangedEventHandler(this.hexBox1_VScrollBarChanged);
            this.hexBox1.ToggleSyncScrollPressed += new System.EventHandler(this.toggleSyncScrollPressed);
            this.hexBox1.MouseEnter += new System.EventHandler(this.hexBox_MouseEnter);
            // 
            // tpUtils
            // 
            this.tpUtils.Controls.Add(this.ucExplorer1);
            this.tpUtils.Location = new System.Drawing.Point(4, 22);
            this.tpUtils.Name = "tpUtils";
            this.tpUtils.Padding = new System.Windows.Forms.Padding(3);
            this.tpUtils.Size = new System.Drawing.Size(1279, 665);
            this.tpUtils.TabIndex = 6;
            this.tpUtils.Text = "Utils";
            this.tpUtils.UseVisualStyleBackColor = true;
            // 
            // ucExplorer1
            // 
            this.ucExplorer1.Location = new System.Drawing.Point(24, 24);
            this.ucExplorer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucExplorer1.Name = "ucExplorer1";
            this.ucExplorer1.Size = new System.Drawing.Size(1226, 622);
            this.ucExplorer1.TabIndex = 0;
            // 
            // tpSerial
            // 
            this.tpSerial.Controls.Add(this.flowLayoutPanel1);
            this.tpSerial.Location = new System.Drawing.Point(4, 22);
            this.tpSerial.Name = "tpSerial";
            this.tpSerial.Padding = new System.Windows.Forms.Padding(3);
            this.tpSerial.Size = new System.Drawing.Size(1279, 665);
            this.tpSerial.TabIndex = 5;
            this.tpSerial.Text = "Serial";
            this.tpSerial.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.gbAvailableCmds);
            this.flowLayoutPanel1.Controls.Add(this.gbSerial_interface);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1273, 659);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // gbAvailableCmds
            // 
            this.gbAvailableCmds.Controls.Add(this.tfSerialHelp);
            this.gbAvailableCmds.Controls.Add(this.linkRevG);
            this.gbAvailableCmds.Controls.Add(this.linkRevE);
            this.gbAvailableCmds.Location = new System.Drawing.Point(13, 13);
            this.gbAvailableCmds.Name = "gbAvailableCmds";
            this.gbAvailableCmds.Size = new System.Drawing.Size(190, 629);
            this.gbAvailableCmds.TabIndex = 7;
            this.gbAvailableCmds.TabStop = false;
            this.gbAvailableCmds.Text = "Available commands";
            this.toolTip1.SetToolTip(this.gbAvailableCmds, "Did you know that you can click on the command below to copy it to the command te" +
        "xtbox?");
            // 
            // tfSerialHelp
            // 
            this.tfSerialHelp.AutoScroll = true;
            this.tfSerialHelp.AutoSize = true;
            this.tfSerialHelp.AvailableCommands = null;
            this.tfSerialHelp.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tfSerialHelp.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tfSerialHelp.Location = new System.Drawing.Point(3, 71);
            this.tfSerialHelp.Name = "tfSerialHelp";
            this.tfSerialHelp.Size = new System.Drawing.Size(184, 555);
            this.tfSerialHelp.TabIndex = 8;
            this.tfSerialHelp.TextClick += new ChameleonMiniGUI.UcTextFlow.ClickHandler(this.tfSerialHelp_TextClick);
            // 
            // linkRevG
            // 
            this.linkRevG.AutoSize = true;
            this.linkRevG.Location = new System.Drawing.Point(14, 44);
            this.linkRevG.Name = "linkRevG";
            this.linkRevG.Size = new System.Drawing.Size(97, 13);
            this.linkRevG.TabIndex = 7;
            this.linkRevG.TabStop = true;
            this.linkRevG.Text = "Official Rev G Wiki";
            this.linkRevG.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRevG_LinkClicked);
            // 
            // linkRevE
            // 
            this.linkRevE.AutoSize = true;
            this.linkRevE.Location = new System.Drawing.Point(14, 22);
            this.linkRevE.Name = "linkRevE";
            this.linkRevE.Size = new System.Drawing.Size(106, 13);
            this.linkRevE.TabIndex = 4;
            this.linkRevE.TabStop = true;
            this.linkRevE.Text = "Rev E rebooted Wiki";
            this.linkRevE.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRevE_LinkClicked);
            // 
            // gbSerial_interface
            // 
            this.gbSerial_interface.Controls.Add(this.btnSerialSend);
            this.gbSerial_interface.Controls.Add(this.tbSerialCmd);
            this.gbSerial_interface.Controls.Add(this.btnClearCmd);
            this.gbSerial_interface.Controls.Add(this.tbSerialOutput);
            this.gbSerial_interface.Location = new System.Drawing.Point(209, 13);
            this.gbSerial_interface.Name = "gbSerial_interface";
            this.gbSerial_interface.Size = new System.Drawing.Size(1050, 629);
            this.gbSerial_interface.TabIndex = 8;
            this.gbSerial_interface.TabStop = false;
            this.gbSerial_interface.Text = "Serial interface";
            // 
            // btnSerialSend
            // 
            this.btnSerialSend.Enabled = false;
            this.btnSerialSend.Location = new System.Drawing.Point(252, 20);
            this.btnSerialSend.Name = "btnSerialSend";
            this.btnSerialSend.Size = new System.Drawing.Size(75, 23);
            this.btnSerialSend.TabIndex = 3;
            this.btnSerialSend.Text = "Send";
            this.btnSerialSend.UseVisualStyleBackColor = true;
            this.btnSerialSend.Click += new System.EventHandler(this.btnSerialSend_Click);
            // 
            // tbSerialCmd
            // 
            this.tbSerialCmd.Location = new System.Drawing.Point(3, 22);
            this.tbSerialCmd.Name = "tbSerialCmd";
            this.tbSerialCmd.Size = new System.Drawing.Size(229, 20);
            this.tbSerialCmd.TabIndex = 1;
            this.tbSerialCmd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSerialCmd_KeyPress);
            // 
            // btnClearCmd
            // 
            this.btnClearCmd.Location = new System.Drawing.Point(465, 20);
            this.btnClearCmd.Name = "btnClearCmd";
            this.btnClearCmd.Size = new System.Drawing.Size(75, 23);
            this.btnClearCmd.TabIndex = 2;
            this.btnClearCmd.Text = "Clear";
            this.btnClearCmd.UseVisualStyleBackColor = true;
            this.btnClearCmd.Click += new System.EventHandler(this.btnClearCmd_Click);
            // 
            // tbSerialOutput
            // 
            this.tbSerialOutput.BackColor = System.Drawing.Color.Gray;
            this.tbSerialOutput.ContextMenuStrip = this.menuClear;
            this.tbSerialOutput.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSerialOutput.ForeColor = System.Drawing.Color.White;
            this.tbSerialOutput.Location = new System.Drawing.Point(3, 53);
            this.tbSerialOutput.Margin = new System.Windows.Forms.Padding(10);
            this.tbSerialOutput.Name = "tbSerialOutput";
            this.tbSerialOutput.Size = new System.Drawing.Size(806, 573);
            this.tbSerialOutput.TabIndex = 0;
            this.tbSerialOutput.Text = "";
            // 
            // menuClear
            // 
            this.menuClear.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuClear.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_selectall,
            this.tsmi_clear,
            this.tsmi_copy});
            this.menuClear.Name = "menuClear";
            this.menuClear.Size = new System.Drawing.Size(123, 70);
            this.menuClear.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuClear_ItemClicked);
            // 
            // tsmi_selectall
            // 
            this.tsmi_selectall.Name = "tsmi_selectall";
            this.tsmi_selectall.Size = new System.Drawing.Size(122, 22);
            this.tsmi_selectall.Text = "Select All";
            // 
            // tsmi_clear
            // 
            this.tsmi_clear.Name = "tsmi_clear";
            this.tsmi_clear.Size = new System.Drawing.Size(122, 22);
            this.tsmi_clear.Text = "Clear";
            // 
            // tsmi_copy
            // 
            this.tsmi_copy.Name = "tsmi_copy";
            this.tsmi_copy.Size = new System.Drawing.Size(122, 22);
            this.tsmi_copy.Text = "Copy";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // frm_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1301, 889);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.gb_output);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1317, 928);
            this.Name = "frm_main";
            this.Activated += new System.EventHandler(this.frm_main_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frm_main_FormClosed);
            this.Load += new System.EventHandler(this.frm_main_Load);
            this.ResizeBegin += new System.EventHandler(this.frm_main_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.frm_main_ResizeEnd);
            this.gb_output.ResumeLayout(false);
            this.gb_output.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsLanguages)).EndInit();
            this.tpSettings.ResumeLayout(false);
            this.gb_language.ResumeLayout(false);
            this.gb_keepalive.ResumeLayout(false);
            this.gb_keepalive.PerformLayout();
            this.gb_connectionSettings.ResumeLayout(false);
            this.gb_connectionSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_device)).EndInit();
            this.gb_bootloader.ResumeLayout(false);
            this.gb_bootloader.PerformLayout();
            this.gb_defaultdownload.ResumeLayout(false);
            this.gb_defaultdownload.PerformLayout();
            this.gb_rssi.ResumeLayout(false);
            this.gb_rssi.PerformLayout();
            this.tpOperation.ResumeLayout(false);
            this.tpOperation.PerformLayout();
            this.gb_actions.ResumeLayout(false);
            this.gb_actions.PerformLayout();
            this.gb_tagslot6.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.gb_tagslot1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.gb_tagslot2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.gb_tagslot4.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.gb_tagslot3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.gb_tagslot5.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.gb_tagslot7.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.gb_tagslot8.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpDump.ResumeLayout(false);
            this.tpDump.PerformLayout();
            this.menuScroll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsTemplates)).EndInit();
            this.tpUtils.ResumeLayout(false);
            this.tpSerial.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.gbAvailableCmds.ResumeLayout(false);
            this.gbAvailableCmds.PerformLayout();
            this.gbSerial_interface.ResumeLayout(false);
            this.gbSerial_interface.PerformLayout();
            this.menuClear.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txt_output;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox gb_output;
        private System.Windows.Forms.TabPage tpSettings;
        private System.Windows.Forms.GroupBox gb_defaultdownload;
        private System.Windows.Forms.Label lbl_defaultdownload;
        private System.Windows.Forms.TextBox txt_defaultdownload;
        private System.Windows.Forms.Button btn_browsedownloads;
        private System.Windows.Forms.GroupBox gb_rssi;
        private System.Windows.Forms.Button btn_rssirefresh;
        private System.Windows.Forms.TextBox txt_rssi;
        private System.Windows.Forms.Label lbl_rssi;
        private System.Windows.Forms.TabPage tpOperation;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox gb_actions;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_keycalc;
        private System.Windows.Forms.Button btn_apply;
        private System.Windows.Forms.Button btn_download;
        private System.Windows.Forms.Button btn_upload;
        private GroupBoxEnhanced gb_tagslot6;
        private System.Windows.Forms.TextBox txt_size6;
        private System.Windows.Forms.Label lbl_size6;
        private System.Windows.Forms.ComboBox cb_Lbutton6;
        private System.Windows.Forms.Label lbl_button6;
        private System.Windows.Forms.TextBox txt_uid6;
        private System.Windows.Forms.Label lbl_uid6;
        private System.Windows.Forms.Label lbl_mode6;
        private System.Windows.Forms.ComboBox cb_mode6;
        private GroupBoxEnhanced gb_tagslot1;
        private System.Windows.Forms.Label lbl_mode1;
        private System.Windows.Forms.ComboBox cb_mode1;
        private System.Windows.Forms.TextBox txt_size1;
        private System.Windows.Forms.Label lbl_size1;
        private System.Windows.Forms.ComboBox cb_Lbutton1;
        private System.Windows.Forms.Label lbl_uid1;
        private System.Windows.Forms.Label lbl_button1;
        private System.Windows.Forms.TextBox txt_uid1;
        private GroupBoxEnhanced gb_tagslot2;
        private System.Windows.Forms.TextBox txt_size2;
        private System.Windows.Forms.Label lbl_size2;
        private System.Windows.Forms.ComboBox cb_Lbutton2;
        private System.Windows.Forms.Label lbl_button2;
        private System.Windows.Forms.TextBox txt_uid2;
        private System.Windows.Forms.Label lbl_uid2;
        private System.Windows.Forms.Label lbl_mode2;
        private System.Windows.Forms.ComboBox cb_mode2;
        private GroupBoxEnhanced gb_tagslot4;
        private System.Windows.Forms.TextBox txt_size4;
        private System.Windows.Forms.Label lbl_size4;
        private System.Windows.Forms.ComboBox cb_Lbutton4;
        private System.Windows.Forms.Label lbl_button4;
        private System.Windows.Forms.TextBox txt_uid4;
        private System.Windows.Forms.Label lbl_uid4;
        private System.Windows.Forms.Label lbl_mode4;
        private System.Windows.Forms.ComboBox cb_mode4;
        private GroupBoxEnhanced gb_tagslot3;
        private System.Windows.Forms.TextBox txt_size3;
        private System.Windows.Forms.Label lbl_size3;
        private System.Windows.Forms.ComboBox cb_Lbutton3;
        private System.Windows.Forms.Label lbl_button3;
        private System.Windows.Forms.TextBox txt_uid3;
        private System.Windows.Forms.Label lbl_uid3;
        private System.Windows.Forms.Label lbl_mode3;
        private System.Windows.Forms.ComboBox cb_mode3;
        private GroupBoxEnhanced gb_tagslot5;
        private System.Windows.Forms.TextBox txt_size5;
        private System.Windows.Forms.Label lbl_size5;
        private System.Windows.Forms.ComboBox cb_Lbutton5;
        private System.Windows.Forms.Label lbl_button5;
        private System.Windows.Forms.TextBox txt_uid5;
        private System.Windows.Forms.Label lbl_uid5;
        private System.Windows.Forms.Label lbl_mode5;
        private System.Windows.Forms.ComboBox cb_mode5;
        private GroupBoxEnhanced gb_tagslot7;
        private System.Windows.Forms.TextBox txt_size7;
        private System.Windows.Forms.Label lbl_size7;
        private System.Windows.Forms.ComboBox cb_Lbutton7;
        private System.Windows.Forms.Label lbl_button7;
        private System.Windows.Forms.TextBox txt_uid7;
        private System.Windows.Forms.Label lbl_uid7;
        private System.Windows.Forms.Label lbl_mode7;
        private System.Windows.Forms.ComboBox cb_mode7;
        private GroupBoxEnhanced gb_tagslot8;
        private System.Windows.Forms.TextBox txt_size8;
        private System.Windows.Forms.Label lbl_size8;
        private System.Windows.Forms.ComboBox cb_Lbutton8;
        private System.Windows.Forms.Label lbl_button8;
        private System.Windows.Forms.TextBox txt_uid8;
        private System.Windows.Forms.Label lbl_uid8;
        private System.Windows.Forms.Label lbl_mode8;
        private System.Windows.Forms.ComboBox cb_mode8;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox gb_bootloader;
        private System.Windows.Forms.Label lbl_upgrade;
        private System.Windows.Forms.Button btn_bootmode;
        private System.Windows.Forms.Button btn_exitboot;
        private System.Windows.Forms.Button btn_selectall;
        private System.Windows.Forms.Button btn_selectnone;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Label lbl_defaults;
        private System.Windows.Forms.Label lbl_reset;
        private System.Windows.Forms.Button btn_setactive;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox gb_keepalive;
        private System.Windows.Forms.GroupBox gb_connectionSettings;
        private System.Windows.Forms.Button btn_setInterval;
        private System.Windows.Forms.TextBox txt_interval;
        private System.Windows.Forms.Label lbl_interval;
        private System.Windows.Forms.CheckBox chk_keepalive;
        private System.Windows.Forms.TextBox txt_constatus;
        private System.Windows.Forms.Button btn_disconnect;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.TextBox tb_firmware;
        private System.Windows.Forms.TabPage tpDump;
        private Be.Windows.Forms.HexBox hexBox2;
        private Be.Windows.Forms.HexBox hexBox1;
        private System.Windows.Forms.Button btn_save2;
        private System.Windows.Forms.Button btn_open2;
        private System.Windows.Forms.Button btn_save1;
        private System.Windows.Forms.Button btn_open1;
        private System.Windows.Forms.ComboBox cb_Lbuttonlong6;
        private System.Windows.Forms.Label lbl_buttonlong6;
        private System.Windows.Forms.ComboBox cb_Lbuttonlong1;
        private System.Windows.Forms.Label lbl_buttonlong1;
        private System.Windows.Forms.ComboBox cb_Lbuttonlong2;
        private System.Windows.Forms.Label lbl_buttonlong2;
        private System.Windows.Forms.ComboBox cb_Lbuttonlong4;
        private System.Windows.Forms.Label lbl_buttonlong4;
        private System.Windows.Forms.ComboBox cb_Lbuttonlong3;
        private System.Windows.Forms.Label lbl_buttonlong3;
        private System.Windows.Forms.ComboBox cb_Lbuttonlong5;
        private System.Windows.Forms.Label lbl_buttonlong5;
        private System.Windows.Forms.ComboBox cb_Lbuttonlong7;
        private System.Windows.Forms.Label lbl_buttonlong7;
        private System.Windows.Forms.ComboBox cb_Lbuttonlong8;
        private System.Windows.Forms.Label lbl_buttonlong8;
        private System.Windows.Forms.RadioButton rbtn_bytewidth16;
        private System.Windows.Forms.RadioButton rbtn_bytewidth08;
        private System.Windows.Forms.RadioButton rbtn_bytewidth04;
        private System.Windows.Forms.Label lbl_bytewidth;
        private System.Windows.Forms.Label lbl_hbfilename2;
        private System.Windows.Forms.Label lbl_hbfilename1;
        private System.Windows.Forms.CheckBox chkSyncScroll;
        private System.Windows.Forms.PictureBox pb_device;
        private System.Windows.Forms.ContextMenuStrip menuScroll;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ComboBox cb_languages;
        private System.Windows.Forms.BindingSource bsLanguages;
        private System.Windows.Forms.GroupBox gb_language;
        private System.Windows.Forms.ComboBox cb_templateA;
        private System.Windows.Forms.BindingSource bsTemplates;
        private System.Windows.Forms.Button btnStartlocation;
        private System.Windows.Forms.Label lbl_template;
        private System.Windows.Forms.TabPage tpSerial;
        private System.Windows.Forms.RichTextBox tbSerialOutput;
        private System.Windows.Forms.TextBox tbSerialCmd;
        private System.Windows.Forms.Button btnClearCmd;
        private System.Windows.Forms.GroupBox gbAvailableCmds;
        private System.Windows.Forms.LinkLabel linkRevE;
        private System.Windows.Forms.LinkLabel linkRevG;
        private System.Windows.Forms.ContextMenuStrip menuClear;
        private System.Windows.Forms.ToolStripMenuItem tsmi_clear;
        private System.Windows.Forms.ToolStripMenuItem tsmi_copy;
        private System.Windows.Forms.GroupBox gbSerial_interface;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnSerialSend;
        private UcLegend ucLegend1;
        private System.Windows.Forms.Button btn_close2;
        private System.Windows.Forms.Button btn_close1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.TabPage tpUtils;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private UcExplorer ucExplorer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_Rbuttonlong1;
        private System.Windows.Forms.ComboBox cb_Rbutton1;
        private System.Windows.Forms.ComboBox cb_Rbuttonlong2;
        private System.Windows.Forms.ComboBox cb_Rbutton2;
        private System.Windows.Forms.ComboBox cb_Rbuttonlong3;
        private System.Windows.Forms.ComboBox cb_Rbutton3;
        private System.Windows.Forms.ComboBox cb_Rbuttonlong4;
        private System.Windows.Forms.ComboBox cb_Rbutton4;
        private System.Windows.Forms.ComboBox cb_Rbuttonlong6;
        private System.Windows.Forms.ComboBox cb_Rbutton6;
        private System.Windows.Forms.ComboBox cb_Rbuttonlong7;
        private System.Windows.Forms.ComboBox cb_Rbutton7;
        private System.Windows.Forms.ComboBox cb_Rbuttonlong8;
        private System.Windows.Forms.ComboBox cb_Rbutton8;
        private System.Windows.Forms.ComboBox cb_Rbuttonlong5;
        private System.Windows.Forms.ComboBox cb_Rbutton5;
        private System.Windows.Forms.ComboBox cb_ledred1;
        private System.Windows.Forms.ComboBox cb_ledgreen1;
        private System.Windows.Forms.Label lb_ledledred1;
        private System.Windows.Forms.Label lb_ledledgreen1;
        private System.Windows.Forms.ComboBox cb_ledred6;
        private System.Windows.Forms.ComboBox cb_ledgreen6;
        private System.Windows.Forms.Label lb_ledledred6;
        private System.Windows.Forms.Label lb_ledledgreen6;
        private System.Windows.Forms.ComboBox cb_ledred2;
        private System.Windows.Forms.ComboBox cb_ledgreen2;
        private System.Windows.Forms.Label lb_ledledred2;
        private System.Windows.Forms.Label lb_ledledgreen2;
        private System.Windows.Forms.ComboBox cb_ledred4;
        private System.Windows.Forms.ComboBox cb_ledgreen4;
        private System.Windows.Forms.Label lb_ledledred4;
        private System.Windows.Forms.Label lb_ledledgreen4;
        private System.Windows.Forms.ComboBox cb_ledred3;
        private System.Windows.Forms.ComboBox cb_ledgreen3;
        private System.Windows.Forms.Label lb_ledledred3;
        private System.Windows.Forms.Label lb_ledledgreen3;
        private System.Windows.Forms.ComboBox cb_ledred5;
        private System.Windows.Forms.ComboBox cb_ledgreen5;
        private System.Windows.Forms.Label lb_ledledred5;
        private System.Windows.Forms.Label lb_ledledgreen5;
        private System.Windows.Forms.ComboBox cb_ledred7;
        private System.Windows.Forms.ComboBox cb_ledgreen7;
        private System.Windows.Forms.Label lb_ledledred7;
        private System.Windows.Forms.Label lb_ledledgreen7;
        private System.Windows.Forms.ComboBox cb_ledred8;
        private System.Windows.Forms.ComboBox cb_ledgreen8;
        private System.Windows.Forms.Label lb_ledledred8;
        private System.Windows.Forms.Label lb_ledledgreen8;
        private System.Windows.Forms.ToolTip toolTip1;
        private UcTextFlow tfSerialHelp;
        private System.Windows.Forms.Button btn_identify;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.ToolStripMenuItem tsmi_selectall;
    }
}
