using DotRas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPNResourceManage
{
    public partial class ManageForm : Form
    {
        private ManageControler controler;
        private ImageList largeImgList = new ImageList();
        private ImageList smallImgList = new ImageList();

        internal ManageControler Controler
        {
            get { return controler; }
            set { controler = value; }
        }
        public ManageForm()
        {
            InitializeComponent();


        }


        private void ManageForm_Load(object sender, EventArgs e)
        {


            SetUIWhenMainFormLoad();

            LoadImageList();
            LoadMainFormBackgroundPicture();
            LoadNotifyIcon();


            if (File.Exists(Global.defaultVPNResourceFile))
            {
                LoadResourceFromFile(Global.defaultVPNResourceFile);
                SetUIAfterLoadVPNResource();
                label_Status.Text = "Load file: default";
            }


        }


        private void SetUIWhenMainFormLoad()
        {
            //customize listview
            this.listView_VPNSourceList.Clear();
            this.listView_VPNSourceList.View = View.List;
            this.listView_VPNSourceList.FullRowSelect = true;
            this.listView_VPNSourceList.GridLines = true;

            this.listView_VPNSourceList.Columns.Add("Address", 150, HorizontalAlignment.Center);
            this.listView_VPNSourceList.Columns.Add("User", 120, HorizontalAlignment.Center);
            this.listView_VPNSourceList.Columns.Add("Password", 120, HorizontalAlignment.Center);
            this.listView_VPNSourceList.Columns.Add("Type", 120, HorizontalAlignment.Center);
            this.listView_VPNSourceList.Columns.Add("Key", 120, HorizontalAlignment.Center);
            this.listView_VPNSourceList.Columns.Add("Supplementary", 120, HorizontalAlignment.Center);

            this.listView_VPNSourceList.Enabled = false;

            //customize menu
            this.fileToolStripMenuItem.Enabled = true;
            this.fileToolStripMenuItem.DropDownItems[0].Enabled = true;
            this.fileToolStripMenuItem.DropDownItems[1].Enabled = true;
            this.fileToolStripMenuItem.DropDownItems[2].Enabled = false;
            this.fileToolStripMenuItem.DropDownItems[3].Enabled = true;

            this.editToolStripMenuItem.Enabled = false;

            this.viewToolStripMenuItem.Enabled = false;

            this.listToolStripMenuItem.Checked = true;
        }



        private void LoadNotifyIcon()
        {
            Icon ico = new Icon(Global.notifyIconPicture);
            notifyIcon.Icon = ico;
        }

        private void LoadMainFormBackgroundPicture()
        {
            if(File.Exists(Global.mainFormBackgroundPicture))
            {
                this.BackgroundImage = Image.FromFile(Global.loginFormBackgroundPicture);
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //for test
            openFileDialog.InitialDirectory = Global.startPath;
            openFileDialog.Filter = "文本文件|*.*|XML文件|*.xml|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //this.listView_VPNSourceList.Enabled = true;

                String fName = openFileDialog.FileName;
                label_Status.Text = "Load file: " + fName;

                if(LoadResourceFromFile(fName))
                {
                
                    SetUIAfterLoadVPNResource();

                }

            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Global.startPath;
            sfd.Filter = "文本文件|*.*|XML文件|*.xml|所有文件|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                String fName = sfd.FileName;
                label_Status.Text = fName;

                controler.SaveVPNSourceToFile(fName);
            }
        }


        private bool LoadResourceFromFile(String vpnResourceFilePath)
        {
            if(controler.LoadVPNResourceFromFile(vpnResourceFilePath))
            {
            
                controler.RefreshVPNListToView();
                return true;
            }
            else
            {
                MessageBox.Show("Can not load resource file!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
        }
        private void SetUIAfterLoadVPNResource()
        {
            this.listView_VPNSourceList.Enabled = true;

            //customize popup menu
            this.listView_VPNSourceList.ContextMenuStrip = this.contextMenuStrip_Edit;
            this.contextMenuStrip_Edit.Items[0].Enabled = true;
            this.contextMenuStrip_Edit.Items[1].Enabled = false;
            this.contextMenuStrip_Edit.Items[2].Enabled = false;


            //customize edit menu
            this.editToolStripMenuItem.Enabled = true;
            this.editToolStripMenuItem.DropDownItems[0].Enabled = true;
            this.editToolStripMenuItem.DropDownItems[1].Enabled = false;
            this.editToolStripMenuItem.DropDownItems[2].Enabled = false;

            //customize file menu
            this.saveAsToolStripMenuItem.Enabled = true;
            this.viewToolStripMenuItem.Enabled = true;
        }

        private void delToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VPNEntity entity = new VPNEntity();
            entity.ConnectionAddr = this.listView_VPNSourceList.SelectedItems[0].SubItems[0].Text;
            entity.AccountName = this.listView_VPNSourceList.SelectedItems[0].SubItems[1].Text;
            entity.AccountPassword = this.listView_VPNSourceList.SelectedItems[0].SubItems[2].Text;
            entity.DialType = this.listView_VPNSourceList.SelectedItems[0].SubItems[3].Text;
            entity.Key = this.listView_VPNSourceList.SelectedItems[0].SubItems[4].Text;
            entity.Supplementary = this.listView_VPNSourceList.SelectedItems[0].SubItems[5].Text;



            controler.DelVpnEntityFromSource(entity);
            controler.SaveVPNSourceToFile();
            controler.RefreshVPNListToView();


            if (listView_VPNSourceList.Items.Count <= 0)
            {
                this.contextMenuStrip_Edit.Items[0].Enabled = true;
                this.contextMenuStrip_Edit.Items[1].Enabled = false;
                this.contextMenuStrip_Edit.Items[2].Enabled = false;

                this.editToolStripMenuItem.DropDownItems[0].Enabled = true;
                this.editToolStripMenuItem.DropDownItems[1].Enabled = false;
                this.editToolStripMenuItem.DropDownItems[2].Enabled = false;
            }
        }

        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditEntityForm modifyEntityForm = new EditEntityForm();
            modifyEntityForm.Text = "Modify entity";

            VPNEntity originalEntity = new VPNEntity();
            originalEntity.ConnectionAddr = this.listView_VPNSourceList.SelectedItems[0].SubItems[0].Text;
            originalEntity.AccountName = this.listView_VPNSourceList.SelectedItems[0].SubItems[1].Text;
            originalEntity.AccountPassword = this.listView_VPNSourceList.SelectedItems[0].SubItems[2].Text;
            originalEntity.DialType = this.listView_VPNSourceList.SelectedItems[0].SubItems[3].Text;
            originalEntity.Key = this.listView_VPNSourceList.SelectedItems[0].SubItems[4].Text;
            originalEntity.Supplementary = this.listView_VPNSourceList.SelectedItems[0].SubItems[5].Text;

            modifyEntityForm.SetFormByVPNEntity(originalEntity);


            if (modifyEntityForm.ShowDialog(this) == DialogResult.OK)
            {
                VPNEntity modifiedEntity = new VPNEntity();
                modifiedEntity.ConnectionAddr = modifyEntityForm.InputAddress;
                modifiedEntity.AccountName = modifyEntityForm.InputAccountName;
                modifiedEntity.AccountPassword = modifyEntityForm.InputAccountPassword;
                modifiedEntity.DialType = modifyEntityForm.InputDialType;
                modifiedEntity.Key = modifyEntityForm.InputKey;
                modifiedEntity.Supplementary = modifyEntityForm.InputSupplementary;

                if (originalEntity.Equals(modifiedEntity))
                {
                    return;
                }
                else
                {
                    controler.ModifyVpnEntityInSource(originalEntity, modifiedEntity);
                    controler.SaveVPNSourceToFile();
                    controler.RefreshVPNListToView();
                }


            }
        }




        private void LoadImageList()
        {

            this.largeImgList.ImageSize = new Size(50, 50);
            this.largeImgList.Images.Add(Image.FromFile(Global.largeIconPicture));
            this.listView_VPNSourceList.LargeImageList = this.largeImgList;

            this.smallImgList.ImageSize = new Size(30, 30);
            this.smallImgList.Images.Add(Image.FromFile(Global.smallIconPicture));
            this.listView_VPNSourceList.SmallImageList = this.smallImgList;

        }

        public void UpdateSourceList(List<VPNEntity> vpnList)
        {
            this.listView_VPNSourceList.Items.Clear();

            this.listView_VPNSourceList.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度  

            foreach (VPNEntity entity in vpnList)
            {
                ListViewItem lvi = new ListViewItem();

                //lvi.ImageIndex = i;     //通过与imageList绑定，显示imageList中第i项图标  

                lvi.Text = entity.ConnectionAddr;

                lvi.SubItems.Add(entity.AccountName);

                lvi.SubItems.Add(entity.AccountPassword);

                lvi.SubItems.Add(entity.DialType);

                lvi.SubItems.Add(entity.Key);

                lvi.SubItems.Add(entity.Supplementary);

                lvi.ImageIndex = 0;

                this.listView_VPNSourceList.Items.Add(lvi);
            }

            this.listView_VPNSourceList.EndUpdate();  //结束数据处理，UI界面一次性绘制。 
        }



        private void listView_VPNSourceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView_VPNSourceList.SelectedIndices.Count > 0)
            {
                //有选中项
                this.contextMenuStrip_Edit.Items[0].Enabled = true;
                this.contextMenuStrip_Edit.Items[1].Enabled = true;
                this.contextMenuStrip_Edit.Items[2].Enabled = true;

                this.editToolStripMenuItem.DropDownItems[0].Enabled = true;
                this.editToolStripMenuItem.DropDownItems[1].Enabled = true;
                this.editToolStripMenuItem.DropDownItems[2].Enabled = true;
            }
            else
            {
                //无选中项
                this.contextMenuStrip_Edit.Items[0].Enabled = true;
                this.contextMenuStrip_Edit.Items[1].Enabled = false;
                this.contextMenuStrip_Edit.Items[2].Enabled = false;

                this.editToolStripMenuItem.DropDownItems[0].Enabled = true;
                this.editToolStripMenuItem.DropDownItems[1].Enabled = false;
                this.editToolStripMenuItem.DropDownItems[2].Enabled = false;
            }
        }

        //private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    controler.SaveVPNSourceToFile();
        //    this.saveToolStripMenuItem.Enabled = false;
        //}

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.controler.ClearVPNEntityFromPhoneBook();
            this.Close();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditEntityForm addEntityForm = new EditEntityForm();
            addEntityForm.Text = "Add new entity";
            if (addEntityForm.ShowDialog(this) == DialogResult.OK)
            {
                VPNEntity entity = new VPNEntity();
                entity.ConnectionAddr = addEntityForm.InputAddress;
                entity.AccountName = addEntityForm.InputAccountName;
                entity.AccountPassword = addEntityForm.InputAccountPassword;
                entity.DialType = addEntityForm.InputDialType;
                entity.Key = addEntityForm.InputKey;
                entity.Supplementary = addEntityForm.InputSupplementary;

                this.controler.AddVpnEntityToSource(entity);
                this.controler.SaveVPNSourceToFile();
                this.controler.RefreshVPNListToView();


            }


        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.controler.CurrentResourceFile = Global.defaultVPNResourceFile;
            controler.SaveVPNSourceToFile();
            SetUIAfterLoadVPNResource();

        }

        private void AddPopUpMenuItem_Click(object sender, EventArgs e)
        {
            addToolStripMenuItem_Click(sender, e);
        }

        private void DelPopUpMenuItem_Click(object sender, EventArgs e)
        {
            delToolStripMenuItem_Click(sender, e);
        }

        private void ModifyPopUpMenuItem_Click(object sender, EventArgs e)
        {
            modifyToolStripMenuItem_Click(sender, e);
        }

        private void listView_VPNSourceList_DoubleClick(object sender, EventArgs e)
        {

            if (btn_Connect.Text == "Connect")
            {
                if (this.listView_VPNSourceList.SelectedIndices.Count > 0)
                {
                    HandleAction_Connect();
                    this.btn_Connect.Text = "Cancel";
                    this.listView_VPNSourceList.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Please choose a vpn entity!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void largeIconToolStripMenuItem_Click(object sender, EventArgs e)
        {

            foreach (ToolStripMenuItem item in this.viewToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }

            largeIconToolStripMenuItem.Checked = true;
            this.listView_VPNSourceList.View = View.LargeIcon;
            this.Size = new Size(750, 625);
            //this.label_Status.Show();
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in this.viewToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }

            detailsToolStripMenuItem.Checked = true;
            this.listView_VPNSourceList.View = View.Details;
            this.Size = new Size(850, 625);
           // this.label_Status.Show();
        }

        private void smallIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in this.viewToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }

            smallIconToolStripMenuItem.Checked = true;
            this.listView_VPNSourceList.View = View.SmallIcon;
            this.Size = new Size(750, 625);
           // this.label_Status.Show();
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in this.viewToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }

            listToolStripMenuItem.Checked = true;
            this.listView_VPNSourceList.View = View.List;
            this.Size = new Size(150, 500);
            this.MinimumSize = new Size(176, 600);
            //this.label_Status.Hide();
            
        }

        private void titleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in this.viewToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }

            titleToolStripMenuItem.Checked = true;
            this.listView_VPNSourceList.View = View.Tile;
            this.Size = new Size(750, 625);
            //this.label_Status.Show();
        }


        public void Dialer_StateChanged(object sender, StateChangedEventArgs e)
        {

            if(this.label_Status.InvokeRequired)
            {
                while (!this.label_Status.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.label_Status.Disposing || this.label_Status.IsDisposed)
                        return;
                }

                EventHandler<DotRas.StateChangedEventArgs> func = new EventHandler<StateChangedEventArgs>(Dialer_StateChanged);
                this.label_Status.Invoke(func, new object[] { sender, e });
            }
            else
            {
                this.label_Status.Text = e.State.ToString();
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// Occurs when the dialer has completed a dialing operation.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">An <see cref="DotRas.DialCompletedEventArgs"/> containing event data.</param>
        public void Dialer_DialCompleted(object sender, DialCompletedEventArgs e)
        {

            if (e.Cancelled)
            {
                MessageBox.Show("Cancelled", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.listView_VPNSourceList.Enabled = true;
                this.label_Status.Text = "...";
            }
            else if (e.TimedOut)
            {
                MessageBox.Show("Connection attempt timed out!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.listView_VPNSourceList.Enabled = true;
                this.label_Status.Text = "...";
            }
            else if (e.Error != null)
            {
                MessageBox.Show(e.Error.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.listView_VPNSourceList.Enabled = true;
                this.label_Status.Text = "...";
            }
            else if (e.Connected)
            {  
                this.label_Status.Text = "Connection successful!";
                this.btn_Connect.Enabled = true;
                this.btn_Connect.Text = "Disconnect";
                this.listView_VPNSourceList.Enabled = false;
                //this.listView_VPNSourceList.SelectedItems[0].BackColor = Color.DarkGreen;

                MessageBox.Show("Connection Completed!", "Success!", MessageBoxButtons.OK);
                this.label_Status.Text = "...";

            }

            if (!e.Connected)
            {
                this.btn_Connect.Text = "Connect";
                this.btn_Connect.Enabled = true;
                this.label_Status.Text = "...";
                this.listView_VPNSourceList.Enabled = true;
            }

        }


        public void Dialer_Error(object source, ErrorEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            if(btn_Connect.Text == "Connect")
            {
                if (this.listView_VPNSourceList.SelectedIndices.Count > 0)
                {
                    HandleAction_Connect();
                    this.btn_Connect.Text = "Cancel";
                    this.listView_VPNSourceList.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Please choose a vpn entity!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return;
            }

            if(btn_Connect.Text == "Cancel")
            {
                HandleAction_Cancel();
                this.btn_Connect.Text = "Connect";
                this.btn_Connect.Enabled = false;
                this.listView_VPNSourceList.Enabled = true;
                return;
            }

            if(btn_Connect.Text == "Disconnect")
            {
                HandleAction_Disconnect();
                this.btn_Connect.Text = "Connect";
                this.listView_VPNSourceList.Enabled = true;
                this.label_Status.Text = "...";
                MessageBox.Show("Connection released!");
                return;
            }
            
        }


        private void HandleAction_Connect()
        {

            String ip = this.listView_VPNSourceList.SelectedItems[0].SubItems[0].Text;
            String user = this.listView_VPNSourceList.SelectedItems[0].SubItems[1].Text;
            String password = this.listView_VPNSourceList.SelectedItems[0].SubItems[2].Text;

            if (String.IsNullOrEmpty(ip) || String.IsNullOrEmpty(user) || String.IsNullOrEmpty(password))
            {
                MessageBox.Show("Incomplete elements!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            String dialTpye = this.listView_VPNSourceList.SelectedItems[0].SubItems[3].Text;

            if (dialTpye == "PPTP")
            {
                this.controler.DialVPN_PPTP(ip, user, password);
            }

            if (dialTpye == "L2TP")
            {
                String key = listView_VPNSourceList.SelectedItems[0].SubItems[4].Text;
                if (String.IsNullOrEmpty(key))
                {
                    MessageBox.Show("Key lost!!!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.controler.DialVPN_L2TP(ip, user, password, key);
            }

        }

        private void HandleAction_Cancel()
        {
            this.controler.AbortDialing();
        }

        private void HandleAction_Disconnect()
        {
            this.controler.DisconnectVPN();
        }

        private void ManageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("确定要退出吗？","提示",MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
            
                e.Cancel = true;

            }
            else
            {
                this.controler.DisconnectVPN();
                this.controler.ClearVPNEntityFromPhoneBook();
            }
        }

        private void ManageForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //隐藏任务栏区图标 
                this.ShowInTaskbar = false;
                //图标显示在托盘区 
                notifyIcon.Visible = true;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示 
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点 
                this.Activate();
                //任务栏区显示图标 
                this.ShowInTaskbar = true;
                //托盘区图标隐藏 
                notifyIcon.Visible = false;
            }
        }




    }
}
