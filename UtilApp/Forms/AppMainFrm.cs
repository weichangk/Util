using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UtilApp.Forms
{
    public partial class AppMainFrm : Form
    {
        public AppMainFrm()
        {
            InitializeComponent();
        }

        private void AddControl(Form form, Panel p)
        {
            foreach (Form f in p.Controls)
            {
                f.Close();
            }
            p.Controls.Clear();

            form.FormBorderStyle = FormBorderStyle.None;
            form.StartPosition = FormStartPosition.Manual;
            form.Dock = DockStyle.Fill;
            form.TopLevel = false;
            p.Controls.Add(form);
            form.Show();
            this.Refresh();
        }

        private void AppMainFrm_Load(object sender, EventArgs e)
        {
            this.TvMenu.Nodes.Add("XlsIOHelperTest");
            //TreeNode tnForm = new TreeNode("  窗体");
            //tnForm.Nodes.Add("提示窗体");
            //tnForm.Nodes.Add("多输入窗体");
            //tnForm.Nodes.Add("气泡提示窗体");
            //tnForm.Nodes.Add("有返回的窗体");
            //tnForm.Nodes.Add("等待窗体");
            //tnForm.Nodes.Add("仅有标题的窗体");
            //tnForm.Nodes.Add("确定取消窗体1");
            //tnForm.Nodes.Add("确定取消窗体2");
            //tnForm.Nodes.Add("资源加载窗体");
            //this.TvMenu.Nodes.Add(tnForm);
            this.TvMenu.Nodes.Add("SecurityTest");
            this.TvMenu.Nodes.Add("MailTest");
        }

        private void TvMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            PlBody.Controls.Clear();
            string strName = e.Node.Text.Trim();
            this.Text = "UtilTestApp--" + strName;
            switch (strName)
            {
                case "XlsIOHelperTest":
                    AddControl(new XlsIOHelperTestFrm(), this.PlBody);
                    break;
                case "SecurityTest":
                    AddControl(new SecurityFrm(), this.PlBody);
                    break;
                case "MailTest":
                    AddControl(new MailFrm(), this.PlBody);
                    break;
            }
        }
    }
}
