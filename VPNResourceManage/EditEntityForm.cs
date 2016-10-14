using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPNResourceManage
{
    public partial class EditEntityForm : Form
    {
        
        public EditEntityForm()
        {
            InitializeComponent();
        }

        public String InputAddress
        {
            get { return textBox_Addr.Text; }
            set { textBox_Addr.Text = value; }
        }

        public String InputAccountName
        {
            get { return textBox_Name.Text; }
            set { textBox_Name.Text = value; }
        }

        public String InputAccountPassword
        {
            get { return textBox_Password.Text; }
            set { textBox_Password.Text = value; }
        }

        public String InputDialType
        {
            get {
                return this.radioBtn_PPTP.Checked ? "PPTP" : "L2TP";
                }

            set
            {
                if(value.Equals( "L2TP"))
                {
                    this.radioBtn_L2TP.Checked = true; 
                    this.radioBtn_PPTP.Checked = false;
                }
                else
                { 
                    this.radioBtn_PPTP.Checked = true;
                    this.radioBtn_L2TP.Checked = false;
                }
            }
        }

        public String InputKey
        {
            get { return textBox_Key.Text; }
            set { textBox_Key.Text = value; }
        }

        public String InputSupplementary
        {
            get { return textBox_Supplementary.Text; }
            set {textBox_Supplementary.Text = value;}
        }

        private void btn_Confirm_Click(object sender, EventArgs e)
        {

            if(String.IsNullOrEmpty(this.textBox_Addr.Text) ||
                String.IsNullOrEmpty(this.textBox_Name.Text) ||
                String.IsNullOrEmpty(this.textBox_Password.Text))
            {
                MessageBox.Show("One or more fileds left empty!","Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(this.radioBtn_L2TP.Checked == false && this.radioBtn_PPTP.Checked == false)
            {
                MessageBox.Show("Please choose the type of vpn!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if((this.radioBtn_L2TP.Checked == true) && String.IsNullOrEmpty(this.textBox_Key.Text))
            {
                MessageBox.Show("Type of L2TP needs a key!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void radioBtn_PPTP_CheckedChanged(object sender, EventArgs e)
        {
            if(radioBtn_PPTP.Checked == true)
            {
                this.textBox_Key.Enabled = false;
                this.textBox_Key.Text = "";
            }
            else
            {
                this.textBox_Key.Enabled = true;
            }
        }

        private void radioBtn_L2TP_CheckedChanged(object sender, EventArgs e)
        {
            if(radioBtn_L2TP.Checked == true)
            {
                this.textBox_Key.Enabled = true;
            }
            else
            {
                this.textBox_Key.Enabled = false;
                this.textBox_Key.Text = "";
            }
        }
        
        public void SetFormByVPNEntity(VPNEntity entity)
        {
            this.InputAddress = entity.ConnectionAddr;
            this.InputAccountName = entity.AccountName;
            this.InputAccountPassword = entity.AccountPassword;

            this.InputDialType = entity.DialType;
            this.InputKey = entity.Key;
            this.InputSupplementary = entity.Supplementary;
        }
    }
}
