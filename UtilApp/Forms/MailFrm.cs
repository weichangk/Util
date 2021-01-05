using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Util;

namespace UtilApp.Forms
{
    public partial class MailFrm : Form
    {
        Log log = LogFactory.GetLogger("MailFrm");
        public MailFrm()
        {
            InitializeComponent();
        }

        private void BtnSyncSend_Click(object sender, EventArgs e)
        {
            try
            {
                //MailHelper.Send(this.TxbTo.Text, this.TxbSubject.Text, this.RichtxbBody.Text, "UTF-8", true, true);
                if (MailHelper.Send(this.TxbTo.Text, this.TxbSubject.Text, this.RichtxbBody.Text))
                {
                    log.Info("邮件发送成功:" + Environment.NewLine +
                        this.TxbTo.Text + Environment.NewLine +
                        this.TxbSubject.Text + Environment.NewLine +
                        this.RichtxbBody.Text + Environment.NewLine
                        );
                    MessageBox.Show("邮件发送成功");
                }
            }
            catch (Exception ex)
            {
                log.Error("邮件发送失败: "  + ex.Message);
                MessageBox.Show("邮件发送失败");
            }

        }

        private void BtnAsynSend_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                this.TxbSubject.Text += i;
                this.RichtxbBody.Text += i;
                MailHelper.AsynSend((to, subject, body) => {
                    log.Info("邮件发送成功:" + Environment.NewLine +
                        to + Environment.NewLine +
                        subject + Environment.NewLine +
                        body + Environment.NewLine
                        );
                    MessageBox.Show("邮件发送成功");
                }, (ex, to, subject, body) => {
                    log.Error("邮件发送失败: " + ex.Message + Environment.NewLine +
                        to + Environment.NewLine +
                        subject + Environment.NewLine +
                        body + Environment.NewLine
                        );
                    MessageBox.Show(ex.Message);
                }, this.TxbTo.Text, this.TxbSubject.Text, this.RichtxbBody.Text);
            }
        }
    }
}
