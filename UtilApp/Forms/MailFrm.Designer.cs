namespace UtilApp.Forms
{
    partial class MailFrm
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
            this.BtnSyncSend = new System.Windows.Forms.Button();
            this.BtnAsynSend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxbTo = new System.Windows.Forms.TextBox();
            this.TxbSubject = new System.Windows.Forms.TextBox();
            this.RichtxbBody = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // BtnSyncSend
            // 
            this.BtnSyncSend.Location = new System.Drawing.Point(33, 151);
            this.BtnSyncSend.Name = "BtnSyncSend";
            this.BtnSyncSend.Size = new System.Drawing.Size(75, 23);
            this.BtnSyncSend.TabIndex = 0;
            this.BtnSyncSend.Text = "同步发送";
            this.BtnSyncSend.UseVisualStyleBackColor = true;
            this.BtnSyncSend.Click += new System.EventHandler(this.BtnSyncSend_Click);
            // 
            // BtnAsynSend
            // 
            this.BtnAsynSend.Location = new System.Drawing.Point(33, 180);
            this.BtnAsynSend.Name = "BtnAsynSend";
            this.BtnAsynSend.Size = new System.Drawing.Size(75, 23);
            this.BtnAsynSend.TabIndex = 1;
            this.BtnAsynSend.Text = "异步发送";
            this.BtnAsynSend.UseVisualStyleBackColor = true;
            this.BtnAsynSend.Click += new System.EventHandler(this.BtnAsynSend_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "收件人";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "主题";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "正文";
            // 
            // TxbTo
            // 
            this.TxbTo.Location = new System.Drawing.Point(121, 25);
            this.TxbTo.Name = "TxbTo";
            this.TxbTo.Size = new System.Drawing.Size(432, 21);
            this.TxbTo.TabIndex = 5;
            // 
            // TxbSubject
            // 
            this.TxbSubject.Location = new System.Drawing.Point(121, 67);
            this.TxbSubject.Name = "TxbSubject";
            this.TxbSubject.Size = new System.Drawing.Size(432, 21);
            this.TxbSubject.TabIndex = 6;
            this.TxbSubject.Text = "主题1";
            // 
            // RichtxbBody
            // 
            this.RichtxbBody.Location = new System.Drawing.Point(121, 109);
            this.RichtxbBody.Name = "RichtxbBody";
            this.RichtxbBody.Size = new System.Drawing.Size(432, 96);
            this.RichtxbBody.TabIndex = 7;
            this.RichtxbBody.Text = "正文1";
            // 
            // MailFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 287);
            this.Controls.Add(this.RichtxbBody);
            this.Controls.Add(this.TxbSubject);
            this.Controls.Add(this.TxbTo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnAsynSend);
            this.Controls.Add(this.BtnSyncSend);
            this.Name = "MailFrm";
            this.Text = "MailFrm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnSyncSend;
        private System.Windows.Forms.Button BtnAsynSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxbTo;
        private System.Windows.Forms.TextBox TxbSubject;
        private System.Windows.Forms.RichTextBox RichtxbBody;
    }
}