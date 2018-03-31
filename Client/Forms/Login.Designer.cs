namespace Client
{
    partial class Login
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
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkRemember = new System.Windows.Forms.CheckBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.stpStatus = new System.Windows.Forms.StatusStrip();
            this.slblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrConnect = new System.Windows.Forms.Timer(this.components);
            this.lblNewUser = new System.Windows.Forms.Label();
            this.stpStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(37, 32);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 13);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Username:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(37, 71);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "Password:";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(40, 189);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(142, 189);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Quit";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkRemember
            // 
            this.chkRemember.AutoSize = true;
            this.chkRemember.Location = new System.Drawing.Point(40, 113);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Size = new System.Drawing.Size(100, 17);
            this.chkRemember.TabIndex = 4;
            this.chkRemember.Text = "Remember me?";
            this.chkRemember.UseVisualStyleBackColor = true;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(40, 48);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(177, 20);
            this.txtUsername.TabIndex = 5;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(40, 87);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(177, 20);
            this.txtPassword.TabIndex = 6;
            // 
            // stpStatus
            // 
            this.stpStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slblStatus});
            this.stpStatus.Location = new System.Drawing.Point(0, 252);
            this.stpStatus.Name = "stpStatus";
            this.stpStatus.Size = new System.Drawing.Size(257, 22);
            this.stpStatus.TabIndex = 7;
            this.stpStatus.Text = "Status";
            // 
            // slblStatus
            // 
            this.slblStatus.Name = "slblStatus";
            this.slblStatus.Size = new System.Drawing.Size(152, 17);
            this.slblStatus.Text = "Server Status: Disconnected";
            // 
            // tmrConnect
            // 
            this.tmrConnect.Enabled = true;
            this.tmrConnect.Interval = 4000;
            this.tmrConnect.Tick += new System.EventHandler(this.tmrConnect_Tick);
            // 
            // lblNewUser
            // 
            this.lblNewUser.AutoSize = true;
            this.lblNewUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblNewUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewUser.ForeColor = System.Drawing.Color.Blue;
            this.lblNewUser.Location = new System.Drawing.Point(71, 149);
            this.lblNewUser.Name = "lblNewUser";
            this.lblNewUser.Size = new System.Drawing.Size(111, 13);
            this.lblNewUser.TabIndex = 8;
            this.lblNewUser.Text = "New user? Click here!";
            this.lblNewUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNewUser.Click += new System.EventHandler(this.lblNewUser_Click);
            this.lblNewUser.MouseLeave += new System.EventHandler(this.lblNewUser_MouseLeave);
            this.lblNewUser.MouseHover += new System.EventHandler(this.lblNewUser_MouseHover);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 274);
            this.Controls.Add(this.lblNewUser);
            this.Controls.Add(this.stpStatus);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.chkRemember);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.stpStatus.ResumeLayout(false);
            this.stpStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.StatusStrip stpStatus;
        public System.Windows.Forms.Timer tmrConnect;
        public System.Windows.Forms.ToolStripStatusLabel slblStatus;
        public System.Windows.Forms.Label lblNewUser;
        public System.Windows.Forms.CheckBox chkRemember;
    }
}