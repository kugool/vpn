using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPNResourceManage
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(textBox_User.Text) || String.IsNullOrEmpty(textBox_Password.Text))
            {
                MessageBox.Show("There is something left empty!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return;
            }

            if(textBox_Password.Text.Length < 8)
            {
                MessageBox.Show("password length is too short, at least 8 characters!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return;
            }

            String md5String = EncryptStringToMD5(this.textBox_User.Text + ":" + this.textBox_Password.Text, this.textBox_Password.Text);

            //String hashFilePath = System.Windows.Forms.Application.StartupPath + @"\account";
            //String hashFilePath = Global.startPath + @"\account";
            if (CheckUserHashPair(Global.hashFilePath, this.textBox_User.Text, md5String))
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Invalid user name or password, please try again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }


        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private  String EncryptStringToMD5(String sinputString, String Skey)
        {
            byte[] data = Encoding.UTF8.GetBytes(sinputString);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(Skey.Substring(0, 8));
            DES.IV = new byte[] { 0x01, 0x02, 0x03 , 0x83, 0x23, 0x67, 0x09, 0x12};
            ICryptoTransform desEncrypt = DES.CreateEncryptor();
            byte[] result = desEncrypt.TransformFinalBlock(data, 0, data.Length);

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(data);
            String encryptResult = BitConverter.ToString(output).Replace("-", "");

            return encryptResult;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {;
            this.BackgroundImage = Image.FromFile(Global.loginFormBackgroundPicture);
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }


        private bool CheckUserHashPair(String hashFile,String userName, String hashCode)
        {
            Dictionary<String, String> userHashPairs = null;


            if(!File.Exists(hashFile))
            {
                MessageBox.Show("hash file not exists!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            userHashPairs = GetUserHashPairsFromFile(hashFile);


            if(userHashPairs.ContainsKey(userName) && userHashPairs[userName].Equals(hashCode))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private Dictionary<String, String> GetUserHashPairsFromFile(String filePath)
        {
            Dictionary<String, String> userHashPairs = new Dictionary<string, string>();

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null && (line.Trim() != ""))
                    {
                        String[] userWithHash = line.Split(new char[] { ':' });

                        userHashPairs.Add(userWithHash[0], userWithHash[1]);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                userHashPairs.Clear();
            }


            return userHashPairs;
        }

    }
}
