namespace ASCOM.QAstroMeteo
{
    partial class ServerSetupDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerSetupDialog));
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.picASCOM = new System.Windows.Forms.PictureBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnKdefaults = new System.Windows.Forms.Button();
            this.txtK7 = new System.Windows.Forms.TextBox();
            this.txtK6 = new System.Windows.Forms.TextBox();
            this.lblK7 = new System.Windows.Forms.Label();
            this.lblK6 = new System.Windows.Forms.Label();
            this.txtK5 = new System.Windows.Forms.TextBox();
            this.lblK5 = new System.Windows.Forms.Label();
            this.txtK4 = new System.Windows.Forms.TextBox();
            this.lblK4 = new System.Windows.Forms.Label();
            this.txtK3 = new System.Windows.Forms.TextBox();
            this.lblK3 = new System.Windows.Forms.Label();
            this.txtK2 = new System.Windows.Forms.TextBox();
            this.lblK2 = new System.Windows.Forms.Label();
            this.txtK1 = new System.Windows.Forms.TextBox();
            this.lblK1 = new System.Windows.Forms.Label();
            this.lblComPort = new System.Windows.Forms.Label();
            this.grpConnection = new System.Windows.Forms.GroupBox();
            this.rdTelnet = new System.Windows.Forms.RadioButton();
            this.rdSerial = new System.Windows.Forms.RadioButton();
            this.ComPortComboBox = new System.Windows.Forms.ComboBox();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCloudsDefault = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCFP = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCTO = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCTC = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtLux = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkCSV = new System.Windows.Forms.CheckBox();
            this.chkTrace = new System.Windows.Forms.CheckBox();
            this.btnLux = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grpConnection.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(237, 402);
            this.cmdOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(88, 38);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(363, 402);
            this.cmdCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(88, 38);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // picASCOM
            // 
            this.picASCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picASCOM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picASCOM.Image = global::ASCOM.QAstroMeteo.Properties.Resources.QuidneIT_SQR;
            this.picASCOM.Location = new System.Drawing.Point(13, 394);
            this.picASCOM.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picASCOM.Name = "picASCOM";
            this.picASCOM.Size = new System.Drawing.Size(48, 48);
            this.picASCOM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picASCOM.TabIndex = 3;
            this.picASCOM.TabStop = false;
            this.picASCOM.Click += new System.EventHandler(this.BrowseToAscom);
            this.picASCOM.DoubleClick += new System.EventHandler(this.BrowseToAscom);
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(6, 120);
            this.lblServer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(78, 20);
            this.lblServer.TabIndex = 8;
            this.lblServer.Text = "Server IP";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnKdefaults);
            this.groupBox1.Controls.Add(this.txtK7);
            this.groupBox1.Controls.Add(this.txtK6);
            this.groupBox1.Controls.Add(this.lblK7);
            this.groupBox1.Controls.Add(this.lblK6);
            this.groupBox1.Controls.Add(this.txtK5);
            this.groupBox1.Controls.Add(this.lblK5);
            this.groupBox1.Controls.Add(this.txtK4);
            this.groupBox1.Controls.Add(this.lblK4);
            this.groupBox1.Controls.Add(this.txtK3);
            this.groupBox1.Controls.Add(this.lblK3);
            this.groupBox1.Controls.Add(this.txtK2);
            this.groupBox1.Controls.Add(this.lblK2);
            this.groupBox1.Controls.Add(this.txtK1);
            this.groupBox1.Controls.Add(this.lblK1);
            this.groupBox1.Location = new System.Drawing.Point(349, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(333, 215);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cloud Model Values";
            // 
            // btnKdefaults
            // 
            this.btnKdefaults.Location = new System.Drawing.Point(172, 158);
            this.btnKdefaults.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnKdefaults.Name = "btnKdefaults";
            this.btnKdefaults.Size = new System.Drawing.Size(140, 42);
            this.btnKdefaults.TabIndex = 14;
            this.btnKdefaults.Text = "K defaults";
            this.btnKdefaults.UseVisualStyleBackColor = true;
            this.btnKdefaults.Click += new System.EventHandler(this.btnKdefaults_Click);
            // 
            // txtK7
            // 
            this.txtK7.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ASCOM.QAstroMeteo.Properties.Settings.Default, "K7", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtK7.Location = new System.Drawing.Point(260, 103);
            this.txtK7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtK7.MaxLength = 3;
            this.txtK7.Name = "txtK7";
            this.txtK7.Size = new System.Drawing.Size(52, 26);
            this.txtK7.TabIndex = 13;
            this.txtK7.Text = global::ASCOM.QAstroMeteo.Properties.Settings.Default.K7;
            this.txtK7.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.valK7value);
            // 
            // txtK6
            // 
            this.txtK6.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ASCOM.QAstroMeteo.Properties.Settings.Default, "K6", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtK6.Location = new System.Drawing.Point(260, 63);
            this.txtK6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtK6.Name = "txtK6";
            this.txtK6.Size = new System.Drawing.Size(52, 26);
            this.txtK6.TabIndex = 11;
            this.txtK6.Text = global::ASCOM.QAstroMeteo.Properties.Settings.Default.K6;
            this.txtK6.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.valK6value);
            // 
            // lblK7
            // 
            this.lblK7.AutoSize = true;
            this.lblK7.Location = new System.Drawing.Point(220, 108);
            this.lblK7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblK7.Name = "lblK7";
            this.lblK7.Size = new System.Drawing.Size(29, 20);
            this.lblK7.TabIndex = 12;
            this.lblK7.Text = "K7";
            // 
            // lblK6
            // 
            this.lblK6.AutoSize = true;
            this.lblK6.Location = new System.Drawing.Point(220, 68);
            this.lblK6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblK6.Name = "lblK6";
            this.lblK6.Size = new System.Drawing.Size(29, 20);
            this.lblK6.TabIndex = 10;
            this.lblK6.Text = "K6";
            // 
            // txtK5
            // 
            this.txtK5.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ASCOM.QAstroMeteo.Properties.Settings.Default, "K5", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtK5.Location = new System.Drawing.Point(260, 25);
            this.txtK5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtK5.Name = "txtK5";
            this.txtK5.Size = new System.Drawing.Size(52, 26);
            this.txtK5.TabIndex = 9;
            this.txtK5.Text = global::ASCOM.QAstroMeteo.Properties.Settings.Default.K5;
            this.txtK5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.valK5value);
            // 
            // lblK5
            // 
            this.lblK5.AutoSize = true;
            this.lblK5.Location = new System.Drawing.Point(220, 29);
            this.lblK5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblK5.Name = "lblK5";
            this.lblK5.Size = new System.Drawing.Size(29, 20);
            this.lblK5.TabIndex = 8;
            this.lblK5.Text = "K5";
            // 
            // txtK4
            // 
            this.txtK4.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ASCOM.QAstroMeteo.Properties.Settings.Default, "K4", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtK4.Location = new System.Drawing.Point(50, 149);
            this.txtK4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtK4.Name = "txtK4";
            this.txtK4.Size = new System.Drawing.Size(52, 26);
            this.txtK4.TabIndex = 7;
            this.txtK4.Text = global::ASCOM.QAstroMeteo.Properties.Settings.Default.K4;
            this.txtK4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.valK4value);
            // 
            // lblK4
            // 
            this.lblK4.AutoSize = true;
            this.lblK4.Location = new System.Drawing.Point(10, 154);
            this.lblK4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblK4.Name = "lblK4";
            this.lblK4.Size = new System.Drawing.Size(29, 20);
            this.lblK4.TabIndex = 6;
            this.lblK4.Text = "K4";
            // 
            // txtK3
            // 
            this.txtK3.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ASCOM.QAstroMeteo.Properties.Settings.Default, "K3", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtK3.Location = new System.Drawing.Point(50, 109);
            this.txtK3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtK3.Name = "txtK3";
            this.txtK3.Size = new System.Drawing.Size(52, 26);
            this.txtK3.TabIndex = 5;
            this.txtK3.Text = global::ASCOM.QAstroMeteo.Properties.Settings.Default.K3;
            this.txtK3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.valK3value);
            // 
            // lblK3
            // 
            this.lblK3.AutoSize = true;
            this.lblK3.Location = new System.Drawing.Point(10, 114);
            this.lblK3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblK3.Name = "lblK3";
            this.lblK3.Size = new System.Drawing.Size(29, 20);
            this.lblK3.TabIndex = 4;
            this.lblK3.Text = "K3";
            // 
            // txtK2
            // 
            this.txtK2.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ASCOM.QAstroMeteo.Properties.Settings.Default, "K2", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtK2.Location = new System.Drawing.Point(50, 69);
            this.txtK2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtK2.Name = "txtK2";
            this.txtK2.Size = new System.Drawing.Size(52, 26);
            this.txtK2.TabIndex = 3;
            this.txtK2.Text = global::ASCOM.QAstroMeteo.Properties.Settings.Default.K2;
            this.txtK2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.valK2value);
            // 
            // lblK2
            // 
            this.lblK2.AutoSize = true;
            this.lblK2.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblK2.Location = new System.Drawing.Point(10, 74);
            this.lblK2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblK2.Name = "lblK2";
            this.lblK2.Size = new System.Drawing.Size(29, 20);
            this.lblK2.TabIndex = 2;
            this.lblK2.Text = "K2";
            // 
            // txtK1
            // 
            this.txtK1.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ASCOM.QAstroMeteo.Properties.Settings.Default, "K1", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtK1.Location = new System.Drawing.Point(50, 31);
            this.txtK1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtK1.Name = "txtK1";
            this.txtK1.Size = new System.Drawing.Size(52, 26);
            this.txtK1.TabIndex = 1;
            this.txtK1.Text = global::ASCOM.QAstroMeteo.Properties.Settings.Default.K1;
            this.txtK1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.valK1value);
            // 
            // lblK1
            // 
            this.lblK1.AutoSize = true;
            this.lblK1.Location = new System.Drawing.Point(10, 35);
            this.lblK1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblK1.Name = "lblK1";
            this.lblK1.Size = new System.Drawing.Size(29, 20);
            this.lblK1.TabIndex = 0;
            this.lblK1.Text = "K1";
            // 
            // lblComPort
            // 
            this.lblComPort.AutoSize = true;
            this.lblComPort.Location = new System.Drawing.Point(8, 77);
            this.lblComPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblComPort.Name = "lblComPort";
            this.lblComPort.Size = new System.Drawing.Size(80, 20);
            this.lblComPort.TabIndex = 12;
            this.lblComPort.Text = "Com Port";
            // 
            // grpConnection
            // 
            this.grpConnection.Controls.Add(this.rdTelnet);
            this.grpConnection.Controls.Add(this.rdSerial);
            this.grpConnection.Controls.Add(this.lblComPort);
            this.grpConnection.Controls.Add(this.lblServer);
            this.grpConnection.Controls.Add(this.ComPortComboBox);
            this.grpConnection.Controls.Add(this.txtServerIP);
            this.grpConnection.Location = new System.Drawing.Point(13, 9);
            this.grpConnection.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpConnection.Name = "grpConnection";
            this.grpConnection.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpConnection.Size = new System.Drawing.Size(322, 162);
            this.grpConnection.TabIndex = 13;
            this.grpConnection.TabStop = false;
            this.grpConnection.Text = "Connection Type";
            // 
            // rdTelnet
            // 
            this.rdTelnet.AutoSize = true;
            this.rdTelnet.Location = new System.Drawing.Point(112, 29);
            this.rdTelnet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdTelnet.Name = "rdTelnet";
            this.rdTelnet.Size = new System.Drawing.Size(142, 24);
            this.rdTelnet.TabIndex = 14;
            this.rdTelnet.TabStop = true;
            this.rdTelnet.Text = "Network (WiFi)";
            this.rdTelnet.UseVisualStyleBackColor = true;
            this.rdTelnet.CheckedChanged += new System.EventHandler(this.rdTelnet_CheckedChanged);
            // 
            // rdSerial
            // 
            this.rdSerial.AutoSize = true;
            this.rdSerial.Location = new System.Drawing.Point(27, 29);
            this.rdSerial.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdSerial.Name = "rdSerial";
            this.rdSerial.Size = new System.Drawing.Size(73, 24);
            this.rdSerial.TabIndex = 13;
            this.rdSerial.TabStop = true;
            this.rdSerial.Text = "Serial";
            this.rdSerial.UseVisualStyleBackColor = true;
            this.rdSerial.CheckedChanged += new System.EventHandler(this.rdSerial_CheckedChanged);
            // 
            // ComPortComboBox
            // 
            this.ComPortComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ASCOM.QAstroMeteo.Properties.Settings.Default, "COMPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ComPortComboBox.FormattingEnabled = true;
            this.ComPortComboBox.Location = new System.Drawing.Point(93, 72);
            this.ComPortComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ComPortComboBox.Name = "ComPortComboBox";
            this.ComPortComboBox.Size = new System.Drawing.Size(180, 28);
            this.ComPortComboBox.TabIndex = 11;
            this.ComPortComboBox.Text = global::ASCOM.QAstroMeteo.Properties.Settings.Default.COMPort;
            this.ComPortComboBox.SelectedIndexChanged += new System.EventHandler(this.ComPortComboBox_SelectedIndexChanged);
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(92, 117);
            this.txtServerIP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(220, 26);
            this.txtServerIP.TabIndex = 9;
            this.txtServerIP.Text = global::ASCOM.QAstroMeteo.Properties.Settings.Default.Server;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCloudsDefault);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtCFP);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtCTO);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtCTC);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(13, 181);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(322, 203);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cloud Threshold Limits";
            // 
            // btnCloudsDefault
            // 
            this.btnCloudsDefault.Location = new System.Drawing.Point(147, 142);
            this.btnCloudsDefault.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCloudsDefault.Name = "btnCloudsDefault";
            this.btnCloudsDefault.Size = new System.Drawing.Size(102, 42);
            this.btnCloudsDefault.TabIndex = 16;
            this.btnCloudsDefault.Text = "Defaults";
            this.btnCloudsDefault.UseVisualStyleBackColor = true;
            this.btnCloudsDefault.Click += new System.EventHandler(this.btnCloudsDefault_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(228, 111);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 20);
            this.label6.TabIndex = 15;
            this.label6.Text = "%";
            // 
            // txtCFP
            // 
            this.txtCFP.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ASCOM.QAstroMeteo.Properties.Settings.Default, "CFP", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCFP.Location = new System.Drawing.Point(174, 106);
            this.txtCFP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCFP.Name = "txtCFP";
            this.txtCFP.Size = new System.Drawing.Size(52, 26);
            this.txtCFP.TabIndex = 14;
            this.txtCFP.Text = global::ASCOM.QAstroMeteo.Properties.Settings.Default.CFP;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(228, 74);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "C";
            // 
            // txtCTO
            // 
            this.txtCTO.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ASCOM.QAstroMeteo.Properties.Settings.Default, "CTO", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCTO.Location = new System.Drawing.Point(174, 69);
            this.txtCTO.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCTO.Name = "txtCTO";
            this.txtCTO.Size = new System.Drawing.Size(52, 26);
            this.txtCTO.TabIndex = 12;
            this.txtCTO.Text = global::ASCOM.QAstroMeteo.Properties.Settings.Default.CTO;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(228, 35);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "C";
            // 
            // txtCTC
            // 
            this.txtCTC.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ASCOM.QAstroMeteo.Properties.Settings.Default, "CTC", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCTC.Location = new System.Drawing.Point(174, 31);
            this.txtCTC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCTC.Name = "txtCTC";
            this.txtCTC.Size = new System.Drawing.Size(52, 26);
            this.txtCTC.TabIndex = 10;
            this.txtCTC.Text = global::ASCOM.QAstroMeteo.Properties.Settings.Default.CTC;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 111);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Overcast Threshold";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 74);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Cloudy Threshold";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Clear Threshold";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnLux);
            this.groupBox3.Controls.Add(this.txtLux);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(349, 233);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(333, 80);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Lux Adjustment";
            // 
            // txtLux
            // 
            this.txtLux.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ASCOM.QAstroMeteo.Properties.Settings.Default, "Lux", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtLux.Location = new System.Drawing.Point(127, 36);
            this.txtLux.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLux.Name = "txtLux";
            this.txtLux.Size = new System.Drawing.Size(52, 26);
            this.txtLux.TabIndex = 3;
            this.txtLux.Text = global::ASCOM.QAstroMeteo.Properties.Settings.Default.Lux;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 39);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "Lux Multiplier";
            // 
            // chkCSV
            // 
            this.chkCSV.AutoSize = true;
            this.chkCSV.Checked = global::ASCOM.QAstroMeteo.Properties.Settings.Default.CSVData;
            this.chkCSV.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ASCOM.QAstroMeteo.Properties.Settings.Default, "CSVData", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkCSV.Location = new System.Drawing.Point(515, 333);
            this.chkCSV.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkCSV.Name = "chkCSV";
            this.chkCSV.Size = new System.Drawing.Size(146, 24);
            this.chkCSV.TabIndex = 15;
            this.chkCSV.Text = "CSV Recording";
            this.chkCSV.UseVisualStyleBackColor = true;
            // 
            // chkTrace
            // 
            this.chkTrace.AutoSize = true;
            this.chkTrace.Checked = global::ASCOM.QAstroMeteo.Properties.Settings.Default.TraceLevel;
            this.chkTrace.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ASCOM.QAstroMeteo.Properties.Settings.Default, "TraceLevel", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkTrace.Location = new System.Drawing.Point(363, 333);
            this.chkTrace.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkTrace.Name = "chkTrace";
            this.chkTrace.Size = new System.Drawing.Size(97, 24);
            this.chkTrace.TabIndex = 6;
            this.chkTrace.Text = "Trace on";
            this.chkTrace.UseVisualStyleBackColor = true;
            // 
            // btnLux
            // 
            this.btnLux.Location = new System.Drawing.Point(207, 32);
            this.btnLux.Name = "btnLux";
            this.btnLux.Size = new System.Drawing.Size(105, 34);
            this.btnLux.TabIndex = 4;
            this.btnLux.Text = "Default";
            this.btnLux.UseVisualStyleBackColor = true;
            this.btnLux.Click += new System.EventHandler(this.btnLux_Click);
            // 
            // ServerSetupDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 452);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.chkCSV);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpConnection);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkTrace);
            this.Controls.Add(this.picASCOM);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServerSetupDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Q-Astro Meteo Setup";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpConnection.ResumeLayout(false);
            this.grpConnection.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.PictureBox picASCOM;
        private System.Windows.Forms.CheckBox chkTrace;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnKdefaults;
        private System.Windows.Forms.TextBox txtK7;
        private System.Windows.Forms.TextBox txtK6;
        private System.Windows.Forms.Label lblK7;
        private System.Windows.Forms.Label lblK6;
        private System.Windows.Forms.TextBox txtK5;
        private System.Windows.Forms.Label lblK5;
        private System.Windows.Forms.TextBox txtK4;
        private System.Windows.Forms.Label lblK4;
        private System.Windows.Forms.TextBox txtK3;
        private System.Windows.Forms.Label lblK3;
        private System.Windows.Forms.TextBox txtK2;
        private System.Windows.Forms.Label lblK2;
        private System.Windows.Forms.TextBox txtK1;
        private System.Windows.Forms.Label lblK1;
        private System.Windows.Forms.ComboBox ComPortComboBox;
        private System.Windows.Forms.Label lblComPort;
        private System.Windows.Forms.GroupBox grpConnection;
        private System.Windows.Forms.RadioButton rdTelnet;
        private System.Windows.Forms.RadioButton rdSerial;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCloudsDefault;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCFP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCTO;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCTC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkCSV;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtLux;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnLux;
    }
}