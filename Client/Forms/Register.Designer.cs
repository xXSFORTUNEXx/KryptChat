namespace Client
{
    partial class Register
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
            this.lblRUser = new System.Windows.Forms.Label();
            this.lblRPass = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblREmail = new System.Windows.Forms.Label();
            this.txtRUser = new System.Windows.Forms.TextBox();
            this.txtRPass = new System.Windows.Forms.TextBox();
            this.txtRRPass = new System.Windows.Forms.TextBox();
            this.txtREmail = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.stpRStatus = new System.Windows.Forms.StatusStrip();
            this.slblRStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.stpRStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRUser
            // 
            this.lblRUser.AutoSize = true;
            this.lblRUser.Location = new System.Drawing.Point(48, 37);
            this.lblRUser.Name = "lblRUser";
            this.lblRUser.Size = new System.Drawing.Size(97, 13);
            this.lblRUser.TabIndex = 0;
            this.lblRUser.Text = "Desired Username:";
            // 
            // lblRPass
            // 
            this.lblRPass.AutoSize = true;
            this.lblRPass.Location = new System.Drawing.Point(48, 76);
            this.lblRPass.Name = "lblRPass";
            this.lblRPass.Size = new System.Drawing.Size(56, 13);
            this.lblRPass.TabIndex = 1;
            this.lblRPass.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Re-Type Password:";
            // 
            // lblREmail
            // 
            this.lblREmail.AutoSize = true;
            this.lblREmail.Location = new System.Drawing.Point(48, 154);
            this.lblREmail.Name = "lblREmail";
            this.lblREmail.Size = new System.Drawing.Size(76, 13);
            this.lblREmail.TabIndex = 3;
            this.lblREmail.Text = "Email Address:";
            // 
            // txtRUser
            // 
            this.txtRUser.Location = new System.Drawing.Point(51, 53);
            this.txtRUser.Name = "txtRUser";
            this.txtRUser.Size = new System.Drawing.Size(177, 20);
            this.txtRUser.TabIndex = 4;
            this.txtRUser.Text = "sfortune";
            // 
            // txtRPass
            // 
            this.txtRPass.Location = new System.Drawing.Point(51, 92);
            this.txtRPass.Name = "txtRPass";
            this.txtRPass.PasswordChar = '*';
            this.txtRPass.Size = new System.Drawing.Size(177, 20);
            this.txtRPass.TabIndex = 5;
            this.txtRPass.Text = "fortune1";
            // 
            // txtRRPass
            // 
            this.txtRRPass.Location = new System.Drawing.Point(51, 131);
            this.txtRRPass.Name = "txtRRPass";
            this.txtRRPass.PasswordChar = '*';
            this.txtRRPass.Size = new System.Drawing.Size(177, 20);
            this.txtRRPass.TabIndex = 6;
            this.txtRRPass.Text = "fortune1";
            // 
            // txtREmail
            // 
            this.txtREmail.Location = new System.Drawing.Point(51, 170);
            this.txtREmail.Name = "txtREmail";
            this.txtREmail.Size = new System.Drawing.Size(177, 20);
            this.txtREmail.TabIndex = 7;
            this.txtREmail.Text = "sfortune@fortune.naw";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(153, 215);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(51, 215);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(75, 23);
            this.btnRegister.TabIndex = 9;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // stpRStatus
            // 
            this.stpRStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slblRStatus});
            this.stpRStatus.Location = new System.Drawing.Point(0, 250);
            this.stpRStatus.Name = "stpRStatus";
            this.stpRStatus.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.stpRStatus.Size = new System.Drawing.Size(275, 22);
            this.stpRStatus.TabIndex = 10;
            this.stpRStatus.Text = "Status";
            // 
            // slblRStatus
            // 
            this.slblRStatus.Name = "slblRStatus";
            this.slblRStatus.Size = new System.Drawing.Size(186, 17);
            this.slblRStatus.Text = "Status: Enter Account Information";
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 272);
            this.ControlBox = false;
            this.Controls.Add(this.stpRStatus);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtREmail);
            this.Controls.Add(this.txtRRPass);
            this.Controls.Add(this.txtRPass);
            this.Controls.Add(this.txtRUser);
            this.Controls.Add(this.lblREmail);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblRPass);
            this.Controls.Add(this.lblRUser);
            this.Name = "Register";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register";
            this.stpRStatus.ResumeLayout(false);
            this.stpRStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRUser;
        private System.Windows.Forms.Label lblRPass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblREmail;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.StatusStrip stpRStatus;
        private System.Windows.Forms.ToolStripStatusLabel slblRStatus;
        public System.Windows.Forms.TextBox txtRUser;
        public System.Windows.Forms.TextBox txtRPass;
        public System.Windows.Forms.TextBox txtRRPass;
        public System.Windows.Forms.TextBox txtREmail;
    }
}