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
            this.duplicate = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.histogramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramGray = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramRed = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramGreen = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramBlue = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.magnify25 = new System.Windows.Forms.ToolStripMenuItem();
            this.magnify50 = new System.Windows.Forms.ToolStripMenuItem();
            this.magnify150 = new System.Windows.Forms.ToolStripMenuItem();
            this.magnify200 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.multiargumemtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSaturatedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNotSaturatedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.substractAbsoluteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.divideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multiplyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.binaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aNDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xORToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.negationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.singleArgumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.singleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.singleEqulizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doubleEqualizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
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
            this.toolStripDropDownButton1,
            this.toolStripButton1,
            this.toolStripDropDownButton2,
            this.toolStripDropDownButton3});
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // OpenImage
            // 
            this.OpenImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.OpenImage, "OpenImage");
            this.OpenImage.Name = "OpenImage";
            this.OpenImage.Padding = new System.Windows.Forms.Padding(2);
            this.OpenImage.Click += new System.EventHandler(this.OpenImage_Click_1);
            // 
            // duplicate
            // 
            this.duplicate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.duplicate, "duplicate");
            this.duplicate.Name = "duplicate";
            this.duplicate.Padding = new System.Windows.Forms.Padding(2);
            this.duplicate.Click += new System.EventHandler(this.duplicate_Click);
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
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.magnify25,
            this.magnify50,
            this.magnify150,
            this.magnify200});
            resources.ApplyResources(this.toolStripDropDownButton2, "toolStripDropDownButton2");
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            // 
            // magnify25
            // 
            this.magnify25.Name = "magnify25";
            resources.ApplyResources(this.magnify25, "magnify25");
            this.magnify25.Click += new System.EventHandler(this.magnify25_Click);
            // 
            // magnify50
            // 
            this.magnify50.Name = "magnify50";
            resources.ApplyResources(this.magnify50, "magnify50");
            this.magnify50.Click += new System.EventHandler(this.magnify50_Click);
            // 
            // magnify150
            // 
            this.magnify150.Name = "magnify150";
            resources.ApplyResources(this.magnify150, "magnify150");
            this.magnify150.Click += new System.EventHandler(this.magnify150_Click);
            // 
            // magnify200
            // 
            this.magnify200.Name = "magnify200";
            resources.ApplyResources(this.magnify200, "magnify200");
            this.magnify200.Click += new System.EventHandler(this.magnify200_Click);
            // 
            // toolStripDropDownButton3
            // 
            this.toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.singleArgumentToolStripMenuItem,
            this.multiargumemtToolStripMenuItem,
            this.binaryToolStripMenuItem});
            resources.ApplyResources(this.toolStripDropDownButton3, "toolStripDropDownButton3");
            this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.Click += new System.EventHandler(this.toolStripDropDownButton3_Click);
            // 
            // multiargumemtToolStripMenuItem
            // 
            this.multiargumemtToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSaturatedToolStripMenuItem,
            this.addNotSaturatedToolStripMenuItem,
            this.substractAbsoluteToolStripMenuItem,
            this.mathToolStripMenuItem});
            this.multiargumemtToolStripMenuItem.Name = "multiargumemtToolStripMenuItem";
            resources.ApplyResources(this.multiargumemtToolStripMenuItem, "multiargumemtToolStripMenuItem");
            // 
            // addSaturatedToolStripMenuItem
            // 
            this.addSaturatedToolStripMenuItem.Name = "addSaturatedToolStripMenuItem";
            resources.ApplyResources(this.addSaturatedToolStripMenuItem, "addSaturatedToolStripMenuItem");
            this.addSaturatedToolStripMenuItem.Click += new System.EventHandler(this.addSaturatedToolStripMenuItem_Click);
            // 
            // addNotSaturatedToolStripMenuItem
            // 
            this.addNotSaturatedToolStripMenuItem.Name = "addNotSaturatedToolStripMenuItem";
            resources.ApplyResources(this.addNotSaturatedToolStripMenuItem, "addNotSaturatedToolStripMenuItem");
            this.addNotSaturatedToolStripMenuItem.Click += new System.EventHandler(this.addNotSaturatedToolStripMenuItem_Click);
            // 
            // substractAbsoluteToolStripMenuItem
            // 
            this.substractAbsoluteToolStripMenuItem.Name = "substractAbsoluteToolStripMenuItem";
            resources.ApplyResources(this.substractAbsoluteToolStripMenuItem, "substractAbsoluteToolStripMenuItem");
            this.substractAbsoluteToolStripMenuItem.Click += new System.EventHandler(this.substractAbsoluteToolStripMenuItem_Click);
            // 
            // mathToolStripMenuItem
            // 
            this.mathToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.divideToolStripMenuItem,
            this.multiplyToolStripMenuItem});
            this.mathToolStripMenuItem.Name = "mathToolStripMenuItem";
            resources.ApplyResources(this.mathToolStripMenuItem, "mathToolStripMenuItem");
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            resources.ApplyResources(this.addToolStripMenuItem, "addToolStripMenuItem");
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // divideToolStripMenuItem
            // 
            this.divideToolStripMenuItem.Name = "divideToolStripMenuItem";
            resources.ApplyResources(this.divideToolStripMenuItem, "divideToolStripMenuItem");
            // 
            // multiplyToolStripMenuItem
            // 
            this.multiplyToolStripMenuItem.Name = "multiplyToolStripMenuItem";
            resources.ApplyResources(this.multiplyToolStripMenuItem, "multiplyToolStripMenuItem");
            // 
            // binaryToolStripMenuItem
            // 
            this.binaryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.notToolStripMenuItem,
            this.aNDToolStripMenuItem,
            this.oRToolStripMenuItem,
            this.xORToolStripMenuItem});
            this.binaryToolStripMenuItem.Name = "binaryToolStripMenuItem";
            resources.ApplyResources(this.binaryToolStripMenuItem, "binaryToolStripMenuItem");
            // 
            // notToolStripMenuItem
            // 
            this.notToolStripMenuItem.Name = "notToolStripMenuItem";
            resources.ApplyResources(this.notToolStripMenuItem, "notToolStripMenuItem");
            this.notToolStripMenuItem.Click += new System.EventHandler(this.notToolStripMenuItem_Click);
            // 
            // aNDToolStripMenuItem
            // 
            this.aNDToolStripMenuItem.Name = "aNDToolStripMenuItem";
            resources.ApplyResources(this.aNDToolStripMenuItem, "aNDToolStripMenuItem");
            // 
            // oRToolStripMenuItem
            // 
            this.oRToolStripMenuItem.Name = "oRToolStripMenuItem";
            resources.ApplyResources(this.oRToolStripMenuItem, "oRToolStripMenuItem");
            // 
            // xORToolStripMenuItem
            // 
            this.xORToolStripMenuItem.Name = "xORToolStripMenuItem";
            resources.ApplyResources(this.xORToolStripMenuItem, "xORToolStripMenuItem");
            // 
            // negationToolStripMenuItem
            // 
            this.negationToolStripMenuItem.Name = "negationToolStripMenuItem";
            resources.ApplyResources(this.negationToolStripMenuItem, "negationToolStripMenuItem");
            this.negationToolStripMenuItem.Click += new System.EventHandler(this.negationToolStripMenuItem_Click);
            // 
            // progToolStripMenuItem
            // 
            this.progToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.singleToolStripMenuItem,
            this.singleEqulizeToolStripMenuItem,
            this.doubleEqualizeToolStripMenuItem});
            this.progToolStripMenuItem.Name = "progToolStripMenuItem";
            resources.ApplyResources(this.progToolStripMenuItem, "progToolStripMenuItem");
            this.progToolStripMenuItem.Click += new System.EventHandler(this.progToolStripMenuItem_Click);
            // 
            // singleArgumentToolStripMenuItem
            // 
            this.singleArgumentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.negationToolStripMenuItem,
            this.progToolStripMenuItem});
            this.singleArgumentToolStripMenuItem.Name = "singleArgumentToolStripMenuItem";
            resources.ApplyResources(this.singleArgumentToolStripMenuItem, "singleArgumentToolStripMenuItem");
            // 
            // singleToolStripMenuItem
            // 
            this.singleToolStripMenuItem.Name = "singleToolStripMenuItem";
            resources.ApplyResources(this.singleToolStripMenuItem, "singleToolStripMenuItem");
            this.singleToolStripMenuItem.Click += new System.EventHandler(this.singleToolStripMenuItem_Click);
            // 
            // singleEqulizeToolStripMenuItem
            // 
            this.singleEqulizeToolStripMenuItem.Name = "singleEqulizeToolStripMenuItem";
            resources.ApplyResources(this.singleEqulizeToolStripMenuItem, "singleEqulizeToolStripMenuItem");
            // 
            // doubleEqualizeToolStripMenuItem
            // 
            this.doubleEqualizeToolStripMenuItem.Name = "doubleEqualizeToolStripMenuItem";
            resources.ApplyResources(this.doubleEqualizeToolStripMenuItem, "doubleEqualizeToolStripMenuItem");
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
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
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem magnify25;
        private System.Windows.Forms.ToolStripMenuItem magnify50;
        private System.Windows.Forms.ToolStripMenuItem magnify150;
        private System.Windows.Forms.ToolStripMenuItem magnify200;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripMenuItem multiargumemtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSaturatedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNotSaturatedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem substractAbsoluteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem binaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem notToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aNDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xORToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem divideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem multiplyToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem singleArgumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem negationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem progToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem singleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem singleEqulizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doubleEqualizeToolStripMenuItem;
    }
}

