namespace SendPushNotificationPoc
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SendPushNotificationBtn = new Button();
            txtDeviceToken = new TextBox();
            lblDeviceToken = new Label();
            txtNotificationTitle = new TextBox();
            lblNotificationTitle = new Label();
            txtNotificationBody = new TextBox();
            lblNotificationBody = new Label();
            SuspendLayout();
            // 
            // SendPushNotificationBtn
            // 
            SendPushNotificationBtn.Location = new Point(261, 338);
            SendPushNotificationBtn.Name = "SendPushNotificationBtn";
            SendPushNotificationBtn.Size = new Size(261, 41);
            SendPushNotificationBtn.TabIndex = 0;
            SendPushNotificationBtn.Text = "Send Push Notification";
            SendPushNotificationBtn.UseVisualStyleBackColor = true;
            SendPushNotificationBtn.Click += SendPushButton_Click;
            // 
            // txtDeviceToken
            // 
            txtDeviceToken.Location = new Point(26, 53);
            txtDeviceToken.Name = "txtDeviceToken";
            txtDeviceToken.Size = new Size(720, 27);
            txtDeviceToken.TabIndex = 1;
            // 
            // lblDeviceToken
            // 
            lblDeviceToken.AutoSize = true;
            lblDeviceToken.Location = new Point(27, 24);
            lblDeviceToken.Name = "lblDeviceToken";
            lblDeviceToken.Size = new Size(97, 20);
            lblDeviceToken.TabIndex = 2;
            lblDeviceToken.Text = "Device Token";
            // 
            // txtNotificationTitle
            // 
            txtNotificationTitle.Location = new Point(26, 120);
            txtNotificationTitle.Name = "txtNotificationTitle";
            txtNotificationTitle.Size = new Size(718, 27);
            txtNotificationTitle.TabIndex = 3;
            txtNotificationTitle.Text = "Hello Push Notification";
            // 
            // lblNotificationTitle
            // 
            lblNotificationTitle.AutoSize = true;
            lblNotificationTitle.Location = new Point(28, 94);
            lblNotificationTitle.Name = "lblNotificationTitle";
            lblNotificationTitle.Size = new Size(121, 20);
            lblNotificationTitle.TabIndex = 4;
            lblNotificationTitle.Text = "Notification Title";
            // 
            // txtNotificationBody
            // 
            txtNotificationBody.Location = new Point(28, 187);
            txtNotificationBody.Name = "txtNotificationBody";
            txtNotificationBody.Size = new Size(716, 27);
            txtNotificationBody.TabIndex = 5;
            txtNotificationBody.Text = "This is notification content";
            // 
            // lblNotificationBody
            // 
            lblNotificationBody.AutoSize = true;
            lblNotificationBody.Location = new Point(28, 161);
            lblNotificationBody.Name = "lblNotificationBody";
            lblNotificationBody.Size = new Size(126, 20);
            lblNotificationBody.TabIndex = 6;
            lblNotificationBody.Text = "Notification Body";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblNotificationBody);
            Controls.Add(txtNotificationBody);
            Controls.Add(lblNotificationTitle);
            Controls.Add(txtNotificationTitle);
            Controls.Add(lblDeviceToken);
            Controls.Add(txtDeviceToken);
            Controls.Add(SendPushNotificationBtn);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SendPushNotificationBtn;
        private TextBox txtDeviceToken;
        private Label lblDeviceToken;
        private TextBox txtNotificationTitle;
        private Label lblNotificationTitle;
        private TextBox txtNotificationBody;
        private Label lblNotificationBody;
    }
}
