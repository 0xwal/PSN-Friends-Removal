namespace Demo
{
    partial class FrmMain
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtEmailBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassBox = new System.Windows.Forms.TextBox();
            this.listView = new System.Windows.Forms.ListView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblFriendsCount = new System.Windows.Forms.Label();
            this.txtFilterBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.chkOnlyOnline = new System.Windows.Forms.CheckBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Email";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(186, 75);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // txtEmailBox
            // 
            this.txtEmailBox.Location = new System.Drawing.Point(111, 23);
            this.txtEmailBox.Name = "txtEmailBox";
            this.txtEmailBox.Size = new System.Drawing.Size(150, 20);
            this.txtEmailBox.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Pass";
            // 
            // txtPassBox
            // 
            this.txtPassBox.Location = new System.Drawing.Point(111, 49);
            this.txtPassBox.Name = "txtPassBox";
            this.txtPassBox.PasswordChar = '*';
            this.txtPassBox.Size = new System.Drawing.Size(150, 20);
            this.txtPassBox.TabIndex = 1;
            // 
            // listView
            // 
            this.listView.Cursor = System.Windows.Forms.Cursors.Default;
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.Location = new System.Drawing.Point(2, 172);
            this.listView.Name = "listView";
            this.listView.ShowItemToolTips = true;
            this.listView.Size = new System.Drawing.Size(341, 360);
            this.listView.TabIndex = 3;
            this.listView.UseCompatibleStateImageBehavior = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(136, 537);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Get Friends";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // lblFriendsCount
            // 
            this.lblFriendsCount.AutoSize = true;
            this.lblFriendsCount.Location = new System.Drawing.Point(1, 156);
            this.lblFriendsCount.Name = "lblFriendsCount";
            this.lblFriendsCount.Size = new System.Drawing.Size(47, 13);
            this.lblFriendsCount.TabIndex = 5;
            this.lblFriendsCount.Text = "Count: 0";
            // 
            // txtFilterBox
            // 
            this.txtFilterBox.Location = new System.Drawing.Point(186, 151);
            this.txtFilterBox.Name = "txtFilterBox";
            this.txtFilterBox.Size = new System.Drawing.Size(157, 20);
            this.txtFilterBox.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblUsername);
            this.groupBox1.Location = new System.Drawing.Point(59, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(225, 96);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Authentication";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(14, 73);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(0, 13);
            this.lblUsername.TabIndex = 0;
            // 
            // chkOnlyOnline
            // 
            this.chkOnlyOnline.AutoSize = true;
            this.chkOnlyOnline.Location = new System.Drawing.Point(216, 541);
            this.chkOnlyOnline.Name = "chkOnlyOnline";
            this.chkOnlyOnline.Size = new System.Drawing.Size(89, 17);
            this.chkOnlyOnline.TabIndex = 7;
            this.chkOnlyOnline.Text = "Only Online ?";
            this.chkOnlyOnline.UseVisualStyleBackColor = true;
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox.Image = global::Demo.Properties.Resources.Ellipsis;
            this.pictureBox.Location = new System.Drawing.Point(2, 109);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(341, 23);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 8;
            this.pictureBox.TabStop = false;
            this.pictureBox.Visible = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 565);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.chkOnlyOnline);
            this.Controls.Add(this.txtFilterBox);
            this.Controls.Add(this.lblFriendsCount);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.txtPassBox);
            this.Controls.Add(this.txtEmailBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PSN Deleter v2";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtEmailBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassBox;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblFriendsCount;
        private System.Windows.Forms.TextBox txtFilterBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.CheckBox chkOnlyOnline;
        private volatile System.Windows.Forms.PictureBox pictureBox;
    }
}

