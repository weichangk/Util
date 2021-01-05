using UtilApp.ControlsEx;

namespace UtilApp.Forms
{
    partial class AppMainFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppMainFrm));
            this.PlBody = new System.Windows.Forms.Panel();
            this.TvMenu = new TreeViewEx();
            this.SuspendLayout();
            // 
            // PlBody
            // 
            this.PlBody.BackColor = System.Drawing.SystemColors.Control;
            this.PlBody.Location = new System.Drawing.Point(122, 0);
            this.PlBody.Name = "PlBody";
            this.PlBody.Size = new System.Drawing.Size(1180, 664);
            this.PlBody.TabIndex = 1;
            // 
            // TvMenu
            // 
            this.TvMenu.BackColor = System.Drawing.Color.White;
            this.TvMenu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TvMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.TvMenu.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.TvMenu.FullRowSelect = true;
            this.TvMenu.HideSelection = false;
            this.TvMenu.IsShowByCustomModel = true;
            this.TvMenu.IsShowTip = false;
            this.TvMenu.ItemHeight = 14;
            this.TvMenu.Location = new System.Drawing.Point(0, 0);
            this.TvMenu.LstTips = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("TvMenu.LstTips")));
            this.TvMenu.Name = "TvMenu";
            this.TvMenu.NodeBackgroundColor = System.Drawing.Color.White;
            this.TvMenu.NodeDownPic = null;
            this.TvMenu.NodeForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.TvMenu.NodeHeight = 25;
            this.TvMenu.NodeIsShowSplitLine = false;
            this.TvMenu.NodeSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.TvMenu.NodeSelectedForeColor = System.Drawing.Color.White;
            this.TvMenu.NodeSplitLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.TvMenu.NodeUpPic = null;
            this.TvMenu.ParentNodeCanSelect = true;
            this.TvMenu.ShowLines = false;
            this.TvMenu.ShowPlusMinus = false;
            this.TvMenu.ShowRootLines = false;
            this.TvMenu.Size = new System.Drawing.Size(121, 663);
            this.TvMenu.TabIndex = 2;
            this.TvMenu.TipFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.TvMenu.TipImage = null;
            this.TvMenu.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvMenu_AfterSelect);
            // 
            // AppMainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1296, 663);
            this.Controls.Add(this.TvMenu);
            this.Controls.Add(this.PlBody);
            this.Name = "AppMainFrm";
            this.Text = "UtilTestApp";
            this.Load += new System.EventHandler(this.AppMainFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel PlBody;
        private TreeViewEx TvMenu;
    }
}