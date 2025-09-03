namespace CommonForms.Views
{
    partial class LoginForm
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
            btnLogin = new DevExpress.XtraEditors.SimpleButton();
            btnCancel = new DevExpress.XtraEditors.SimpleButton();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            txtPassword = new DevExpress.XtraEditors.TextEdit();
            txtUsername = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)txtPassword.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtUsername.Properties).BeginInit();
            SuspendLayout();
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(12, 127);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(75, 23);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Войти";
            btnLogin.Click += btnLogin_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(154, 127);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Отмена";
            btnCancel.Click += btnCancel_Click;
            // 
            // labelControl1
            // 
            labelControl1.Location = new Point(12, 20);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new Size(30, 13);
            labelControl1.TabIndex = 2;
            labelControl1.Text = "Логин";
            // 
            // labelControl2
            // 
            labelControl2.Location = new Point(12, 65);
            labelControl2.Name = "labelControl2";
            labelControl2.Size = new Size(37, 13);
            labelControl2.TabIndex = 3;
            labelControl2.Text = "Пароль";
            // 
            // txtPassword
            // 
            txtPassword.EnterMoveNextControl = true;
            txtPassword.Location = new Point(12, 84);
            txtPassword.Name = "txtPassword";
            txtPassword.Properties.Appearance.Options.UseTextOptions = true;
            txtPassword.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtPassword.Properties.PasswordChar = '*';
            txtPassword.Size = new Size(217, 20);
            txtPassword.TabIndex = 1;
            // 
            // txtUsername
            // 
            txtUsername.EditValue = "";
            txtUsername.EnterMoveNextControl = true;
            txtUsername.Location = new Point(12, 39);
            txtUsername.Name = "txtUsername";
            txtUsername.Properties.Appearance.Options.UseTextOptions = true;
            txtUsername.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtUsername.Size = new Size(217, 20);
            txtUsername.TabIndex = 0;
            // 
            // LoginForm
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(241, 168);
            Controls.Add(txtUsername);
            Controls.Add(txtPassword);
            Controls.Add(labelControl2);
            Controls.Add(labelControl1);
            Controls.Add(btnCancel);
            Controls.Add(btnLogin);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "LoginForm";
            ((System.ComponentModel.ISupportInitialize)txtPassword.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtUsername.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.TextEdit txtUsername;
    }
}