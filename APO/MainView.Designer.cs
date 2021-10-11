namespace APO {
    partial class MainView {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.OpenImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.histogramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramGray = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramRed = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramGreen = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramBlue = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicate = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenImage,
            this.duplicate,
            this.toolStripDropDownButton1});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // OpenImage
            // 
            this.OpenImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.OpenImage, "OpenImage");
            this.OpenImage.Name = "OpenImage";
            this.OpenImage.Padding = new System.Windows.Forms.Padding(2);
            this.OpenImage.Click += new System.EventHandler(this.OpenImage_Click_1);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.histogramToolStripMenuItem});
            resources.ApplyResources(this.toolStripDropDownButton1, "toolStripDropDownButton1");
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Padding = new System.Windows.Forms.Padding(2);
            // 
            // histogramToolStripMenuItem
            // 
            this.histogramToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.histogramGray,
            this.histogramRed,
            this.histogramGreen,
            this.histogramBlue});
            this.histogramToolStripMenuItem.Name = "histogramToolStripMenuItem";
            resources.ApplyResources(this.histogramToolStripMenuItem, "histogramToolStripMenuItem");
            // 
            // histogramGray
            // 
            this.histogramGray.Name = "histogramGray";
            resources.ApplyResources(this.histogramGray, "histogramGray");
            this.histogramGray.Click += new System.EventHandler(this.grayscaleToolStripMenuItem_Click);
            // 
            // histogramRed
            // 
            this.histogramRed.Name = "histogramRed";
            resources.ApplyResources(this.histogramRed, "histogramRed");
            this.histogramRed.Click += new System.EventHandler(this.redToolStripMenuItem_Click);
            // 
            // histogramGreen
            // 
            this.histogramGreen.Name = "histogramGreen";
            resources.ApplyResources(this.histogramGreen, "histogramGreen");
            this.histogramGreen.Click += new System.EventHandler(this.greenToolStripMenuItem_Click);
            // 
            // histogramBlue
            // 
            this.histogramBlue.Name = "histogramBlue";
            resources.ApplyResources(this.histogramBlue, "histogramBlue");
            this.histogramBlue.Click += new System.EventHandler(this.blueToolStripMenuItem_Click);
            // 
            // duplicate
            // 
            this.duplicate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.duplicate, "duplicate");
            this.duplicate.Name = "duplicate";
            this.duplicate.Padding = new System.Windows.Forms.Padding(2);
            this.duplicate.Click += new System.EventHandler(this.duplicate_Click);
            // 
            // MainView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.IsMdiContainer = true;
            this.Name = "MainView";
            this.Load += new System.EventHandler(this.MainView_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton OpenImage;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem histogramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histogramGray;
        private System.Windows.Forms.ToolStripMenuItem histogramRed;
        private System.Windows.Forms.ToolStripMenuItem histogramGreen;
        private System.Windows.Forms.ToolStripMenuItem histogramBlue;
        private System.Windows.Forms.ToolStripButton duplicate;
    }
}

