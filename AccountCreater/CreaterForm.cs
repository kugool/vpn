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

namespace AccountCreater
{
    public partial class CreaterForm : Form
    {
        public CreaterForm()
        {
            InitializeComponent();
        }

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbx_Name.Text) || String.IsNullOrEmpty(tbx_Password.Text))
            {
                MessageBox.Show("There is something left empty!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if(tbx_Name.Text.Contains(":"))
            {
                MessageBox.Show("You have an invalid character \':\' in user name!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tbx_Password.Text.Length < 8)
            {
                MessageBox.Show("password length is too short, at least 8 characters!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            String md5String = EncryptStringToMD5(this.tbx_Name.Text + ":" + this.tbx_Password.Text, this.tbx_Password.Text);

            if(!String.IsNullOrEmpty(md5String))
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = "E:\\";
                sfd.Filter = "文本文件|*.*|XML文件|*.xml|所有文件|*.*";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    String fName = sfd.FileName;
                    SaveToFile(fName, this.tbx_Name.Text, md5String);
                    this.Close();
                }
            }


        }

        private void SaveToFile(String savePath , String userName, String md5Hash)
        {

            if (File.Exists(savePath))
            {
                UpdateHashFile(savePath, userName, md5Hash);
            }
            else
            {
                FileStream fs = new FileStream(savePath, FileMode.CreateNew);
                StreamWriter sw = new StreamWriter(fs);
                
                sw.WriteLine(String.Format("{0}:{1}", userName, md5Hash));
                sw.Flush();
                sw.Close();
                fs.Close();
            }
        }


        private void UpdateHashFile(String filePath, String userName, String md5Hash)
        {
            Dictionary<String, String> nameHashPairs = GetNameHashPairsFromFile(filePath);

            nameHashPairs[userName] = md5Hash;

            WriteDataToFile(filePath, nameHashPairs);
        }

        private void WriteDataToFile(String filePath, Dictionary<String, String> nameHashPairs)
        {
            FileStream fs = new FileStream(filePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            foreach(String key in nameHashPairs.Keys)
            {
                sw.WriteLine(String.Format("{0}:{1}", key, nameHashPairs[key]));
            }

            sw.Flush();
            sw.Close();
            fs.Close();
        }

        private Dictionary<String, String> GetNameHashPairsFromFile(String filePath)
        {
            Dictionary<String, String> userHashPair = new Dictionary<string, string>();

            try 
            {
                using (StreamReader sr = new StreamReader(filePath)) 
                {
                    String line;
                    while ((line = sr.ReadLine()) != null && (line.Trim() != "")) 
                    {
                        String[] userWithHash = line.Split(new char[] { ':' });
                        
                        userHashPair.Add(userWithHash[0], userWithHash[1]);
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
                userHashPair.Clear();
            }


            return userHashPair;

        }

        private  String EncryptStringToMD5(String sinputString, String Skey)
        {
            byte[] data = Encoding.UTF8.GetBytes(sinputString);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(Skey.Substring(0, 8));
            DES.IV = new byte[] { 0x01, 0x02, 0x03, 0x83, 0x23, 0x67, 0x09, 0x12 };
            ICryptoTransform desEncrypt = DES.CreateEncryptor();
            byte[] result = desEncrypt.TransformFinalBlock(data, 0, data.Length);

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(data);
            String encryptResult = BitConverter.ToString(output).Replace("-", "");

            return encryptResult;
        }
    }
}
