namespace VPNResourceManage
{
    partial class EditEntityForm
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
            this.label_addr = new System.Windows.Forms.Label();
            this.label_ = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_Addr = new System.Windows.Forms.TextBox();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.textBox_Password = new System.Windows.Forms.TextBox();
            this.btn_Confirm = new System.Windows.Forms.Button();
            this.btn_Cancle = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_Key = new System.Windows.Forms.TextBox();
            this.textBox_Supplementary = new System.Windows.Forms.TextBox();
            this.radioBtn_PPTP = new System.Windows.Forms.RadioButton();
            this.radioBtn_L2TP = new System.Windows.Forms.RadioButton();
            this.groupBox_DialType = new System.Windows.Forms.GroupBox();
            this.groupBox_DialType.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_addr
            // 
            this.label_addr.AutoSize = true;
            this.label_addr.Location = new System.Drawing.Point(24, 27);
            this.label_addr.Name = "label_addr";
            this.label_addr.Size = new System.Drawing.Size(65, 12);
            this.label_addr.TabIndex = 0;
            this.label_addr.Text = "Address/IP";
            // 
            // label_
            // 
            this.label_.AutoSize = true;
            this.label_.Location = new System.Drawing.Point(24, 69);
            this.label_.Name = "label_";
            this.label_.Size = new System.Drawing.Size(77, 12);
            this.label_.TabIndex = 0;
            this.label_.Text = "Account Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "Account Password";
            // 
            // textBox_Addr
            // 
            this.textBox_Addr.Location = new System.Drawing.Point(188, 24);
            this.textBox_Addr.Name = "textBox_Addr";
            this.textBox_Addr.Size = new System.Drawing.Size(167, 21);
            this.textBox_Addr.TabIndex = 1;
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(188, 62);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(167, 21);
            this.textBox_Name.TabIndex = 2;
            // 
            // textBox_Password
            // 
            this.textBox_Password.Location = new System.Drawing.Point(188, 100);
            this.textBox_Password.Name = "textBox_Password";
            this.textBox_Password.Size = new System.Drawing.Size(167, 21);
            this.textBox_Password.TabIndex = 3;
            // 
            // btn_Confirm
            // 
            this.btn_Confirm.Location = new System.Drawing.Point(188, 269);
            this.btn_Confirm.Name = "btn_Confirm";
            this.btn_Confirm.Size = new System.Drawing.Size(75, 23);
            this.btn_Confirm.TabIndex = 8;
            this.btn_Confirm.Text = "Confirm";
            this.btn_Confirm.UseVisualStyleBackColor = true;
            this.btn_Confirm.Click += new System.EventHandler(this.btn_Confirm_Click);
            // 
            // btn_Cancle
            // 
            this.btn_Cancle.Location = new System.Drawing.Point(280, 269);
            this.btn_Cancle.Name = "btn_Cancle";
            this.btn_Cancle.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancle.TabIndex = 9;
            this.btn_Cancle.Text = "Cancel";
            this.btn_Cancle.UseVisualStyleBackColor = true;
            this.btn_Cancle.Click += new System.EventHandler(this.btn_Cancle_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dial Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 195);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Key";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 237);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "Supplementary Info";
            // 
            // textBox_Key
            // 
            this.textBox_Key.Location = new System.Drawing.Point(188, 192);
            this.textBox_Key.Name = "textBox_Key";
            this.textBox_Key.Size = new System.Drawing.Size(167, 21);
            this.textBox_Key.TabIndex = 6;
            // 
            // textBox_Supplementary
            // 
            this.textBox_Supplementary.Location = new System.Drawing.Point(188, 234);
            this.textBox_Supplementary.Name = "textBox_Supplementary";
            this.textBox_Supplementary.Size = new System.Drawing.Size(167, 21);
            this.textBox_Supplementary.TabIndex = 7;
            // 
            // radioBtn_PPTP
            // 
            this.radioBtn_PPTP.AutoSize = true;
            this.radioBtn_PPTP.Location = new System.Drawing.Point(6, 12);
            this.radioBtn_PPTP.Name = "radioBtn_PPTP";
            this.radioBtn_PPTP.Size = new System.Drawing.Size(47, 16);
            this.radioBtn_PPTP.TabIndex = 4;
            this.radioBtn_PPTP.TabStop = true;
            this.radioBtn_PPTP.Text = "PPTP";
            this.radioBtn_PPTP.UseVisualStyleBackColor = true;
            this.radioBtn_PPTP.CheckedChanged += new System.EventHandler(this.radioBtn_PPTP_CheckedChanged);
            // 
            // radioBtn_L2TP
            // 
            this.radioBtn_L2TP.AutoSize = true;
            this.radioBtn_L2TP.Location = new System.Drawing.Point(92, 12);
            this.radioBtn_L2TP.Name = "radioBtn_L2TP";
            this.radioBtn_L2TP.Size = new System.Drawing.Size(47, 16);
            this.radioBtn_L2TP.TabIndex = 5;
            this.radioBtn_L2TP.TabStop = true;
            this.radioBtn_L2TP.Text = "L2TP";
            this.radioBtn_L2TP.UseVisualStyleBackColor = true;
            this.radioBtn_L2TP.CheckedChanged += new System.EventHandler(this.radioBtn_L2TP_CheckedChanged);
            // 
            // groupBox_DialType
            // 
            this.groupBox_DialType.Controls.Add(this.radioBtn_PPTP);
            this.groupBox_DialType.Controls.Add(this.radioBtn_L2TP);
            this.groupBox_DialType.Location = new System.Drawing.Point(188, 141);
            this.groupBox_DialType.Name = "groupBox_DialType";
            this.groupBox_DialType.Size = new System.Drawing.Size(167, 34);
            this.groupBox_DialType.TabIndex = 7;
            this.groupBox_DialType.TabStop = false;
            // 
            // EditEntityForm
            // 
            this.AcceptButton = this.btn_Confirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 303);
            this.Controls.Add(this.groupBox_DialType);
            this.Controls.Add(this.textBox_Supplementary);
            this.Controls.Add(this.textBox_Key);
            this.Controls.Add(this.btn_Cancle);
            this.Controls.Add(this.btn_Confirm);
            this.Controls.Add(this.textBox_Password);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.textBox_Addr);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_);
            this.Controls.Add(this.label_addr);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditEntityForm";
            this.Text = "AddEntityForm";
            this.groupBox_DialType.ResumeLayout(false);
            this.groupBox_DialType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_addr;
        private System.Windows.Forms.Label label_;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_Addr;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.TextBox textBox_Password;
        private System.Windows.Forms.Button btn_Confirm;
        private System.Windows.Forms.Button btn_Cancle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_Key;
        private System.Windows.Forms.TextBox textBox_Supplementary;
        private System.Windows.Forms.RadioButton radioBtn_PPTP;
        private System.Windows.Forms.RadioButton radioBtn_L2TP;
        private System.Windows.Forms.GroupBox groupBox_DialType;
    }
}