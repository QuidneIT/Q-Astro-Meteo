using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ASCOM.Utilities;
using System.Diagnostics;

namespace ASCOM.QAstroMeteo
{
//    [ComVisible(false)]					// Form not registered for COM!
    public partial class ServerSetupDialog : Form
    {
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        internal List<COMPortInfo> comPorts;
        internal COMPortInfo comPort;
//        internal ASCOM.Utilities.Serial serPort;

        public ServerSetupDialog()
        {
            InitializeComponent();
            // Initialise current values of user settings from the ASCOM Profile
            InitUI();
        }

        private void cmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            // Place any validation constraint checks here
            // Update the state variables with results from the dialogue
            Properties.Settings.Default.Server = txtServerIP.Text;
            Properties.Settings.Default.TraceLevel = chkTrace.Checked;
            Properties.Settings.Default.K1 = txtK1.Text;
            Properties.Settings.Default.K2 = txtK2.Text;
            Properties.Settings.Default.K3 = txtK3.Text;
            Properties.Settings.Default.K4 = txtK4.Text;
            Properties.Settings.Default.K5 = txtK5.Text;
            Properties.Settings.Default.K6 = txtK6.Text;
            Properties.Settings.Default.K7 = txtK7.Text;
            Properties.Settings.Default.CTC = txtCTC.Text;
            Properties.Settings.Default.CTO = txtCTO.Text;
            Properties.Settings.Default.CFP = txtCFP.Text;
            Properties.Settings.Default.SerialConnection = rdSerial.Checked;
            Properties.Settings.Default.CSVData = chkCSV.Checked;
            Properties.Settings.Default.Lux = txtLux.Text;

            Properties.Settings.Default.Save();
        }

        private void cmdCancel_Click(object sender, EventArgs e) // Cancel button event handler
        {
            Properties.Settings.Default.Reload();
            Close();
        }

        private void BrowseToAscom(object sender, EventArgs e) // Click on ASCOM logo event handler
        {
            try
            {
                System.Diagnostics.Process.Start("http://ascom-standards.org/");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void InitUI()
        {
            comPort = new COMPortInfo();
            comPorts = new List<COMPortInfo>();

            comPorts = COMPortInfo.GetCOMPortsInfo();

            // set the list of com ports to those that are currently available
            ComPortComboBox.Items.Clear();

            foreach (COMPortInfo cport in comPorts)
                ComPortComboBox.Items.Add(cport.Name);

            // select the current port if possible
            if (comPort != null)
            {
                if (ComPortComboBox.Items.Contains(Properties.Settings.Default.TraceLevel))
                    ComPortComboBox.SelectedItem = Properties.Settings.Default.TraceLevel;
            }

            chkTrace.Checked = Properties.Settings.Default.TraceLevel;
            txtServerIP.Text = Properties.Settings.Default.Server;
            txtK1.Text = Properties.Settings.Default.K1;
            txtK2.Text = Properties.Settings.Default.K2;
            txtK3.Text = Properties.Settings.Default.K3;
            txtK4.Text = Properties.Settings.Default.K4;
            txtK5.Text = Properties.Settings.Default.K5;
            txtK6.Text = Properties.Settings.Default.K6;
            txtK7.Text = Properties.Settings.Default.K7;
            txtCFP.Text = Properties.Settings.Default.CFP;
            txtCTO.Text = Properties.Settings.Default.CTO;
            txtCTC.Text = Properties.Settings.Default.CTC;
            txtLux.Text = Properties.Settings.Default.Lux;

            rdSerial.Checked = Properties.Settings.Default.SerialConnection;
            rdTelnet.Checked = !rdSerial.Checked;
            ComPortComboBox.Enabled = rdSerial.Checked;
            txtServerIP.Enabled = !rdSerial.Checked;
            chkCSV.Checked = Properties.Settings.Default.CSVData;
        }

        private void btnKdefaults_Click(object sender, EventArgs e)
        {
            txtK1.Text = "33";
            txtK2.Text = "0";
            txtK3.Text = "4";
            txtK4.Text = "100";
            txtK5.Text = "100";
            txtK6.Text = "0";
            txtK7.Text = "0";
        }

        private void valK1value(object sender, EventArgs e)
        {
            txtK1.Text = validateKvalue(txtK1.Text);
        }

        private void valK2value(object sender, EventArgs e)
        {
            txtK2.Text = validateKvalue(txtK2.Text);
        }

        private void valK3value(object sender, EventArgs e)
        {
            txtK3.Text = validateKvalue(txtK3.Text);
        }

        private void valK4value(object sender, EventArgs e)
        {
            txtK4.Text = validateKvalue(txtK4.Text);
        }

        private void valK5value(object sender, EventArgs e)
        {
            txtK5.Text = validateKvalue(txtK5.Text);
        }

        private void valK6value(object sender, EventArgs e)
        {
            txtK6.Text = validateKvalue(txtK6.Text);
        }

        private void valK7value(object sender, EventArgs e)
        {
            txtK7.Text = validateKvalue(txtK7.Text);
        }

        private string validateKvalue(string kValue)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(kValue, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                kValue = kValue.Remove(kValue.Length - 1);
            }
            return kValue;
        }

        private void ComPortComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            comPort.Name = ComPortComboBox.GetItemText(this.ComPortComboBox.SelectedItem);
        }

        private void rdSerial_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSerial.Checked)
                rdTelnet.Checked = false;

            ComPortComboBox.Enabled = rdSerial.Checked;
            txtServerIP.Enabled = !rdSerial.Checked;
        }

        private void rdTelnet_CheckedChanged(object sender, EventArgs e)
        {
            if (rdTelnet.Checked)
                rdSerial.Checked = false;

            ComPortComboBox.Enabled = rdSerial.Checked;
            txtServerIP.Enabled = !rdSerial.Checked;
        }

        private void btnCloudsDefault_Click(object sender, EventArgs e)
        {
            txtCTC.Text = "-1";
            txtCTO.Text = "0";
            txtCFP.Text = "30";
        }

        private void valK1value(object sender, KeyPressEventArgs e)
        {

        }

        private void valK7value(object sender, KeyPressEventArgs e)
        {

        }

        private void valK6value(object sender, KeyPressEventArgs e)
        {

        }

        private void valK5value(object sender, KeyPressEventArgs e)
        {

        }

        private void valK4value(object sender, KeyPressEventArgs e)
        {

        }

        private void valK3value(object sender, KeyPressEventArgs e)
        {

        }

        private void valK2value(object sender, KeyPressEventArgs e)
        {

        }

        private void btnLux_Click(object sender, EventArgs e)
        {
            txtLux.Text = "1.0";
        }
    }
}