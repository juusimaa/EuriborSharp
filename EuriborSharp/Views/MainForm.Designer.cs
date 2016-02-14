namespace EuriborSharp.Views
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.normalLineStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dotLineStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smoothToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rendererToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xkcdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateIntervalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oneDayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.twelveHoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sixHoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allValuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.last30DaysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip.SuspendLayout();
            this.mainTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.mainTableLayoutPanel.SetColumnSpan(this.mainTabControl, 2);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(3, 3);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTableLayoutPanel.SetRowSpan(this.mainTabControl, 2);
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(778, 632);
            this.mainTabControl.TabIndex = 0;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.lineTypeToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(784, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // lineTypeToolStripMenuItem
            // 
            this.lineTypeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.rendererToolStripMenuItem,
            this.updateIntervalToolStripMenuItem,
            this.updateToolStripMenuItem});
            this.lineTypeToolStripMenuItem.Name = "lineTypeToolStripMenuItem";
            this.lineTypeToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.lineTypeToolStripMenuItem.Text = "&Options";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.smoothToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(154, 22);
            this.toolStripMenuItem1.Text = "&Line";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalLineStyleToolStripMenuItem,
            this.dotLineStyleToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem2.Text = "&Style";
            // 
            // normalLineStyleToolStripMenuItem
            // 
            this.normalLineStyleToolStripMenuItem.CheckOnClick = true;
            this.normalLineStyleToolStripMenuItem.Name = "normalLineStyleToolStripMenuItem";
            this.normalLineStyleToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.normalLineStyleToolStripMenuItem.Text = "&Normal";
            this.normalLineStyleToolStripMenuItem.Click += new System.EventHandler(this.normalLineStyleToolStripMenuItem_Click);
            // 
            // dotLineStyleToolStripMenuItem
            // 
            this.dotLineStyleToolStripMenuItem.CheckOnClick = true;
            this.dotLineStyleToolStripMenuItem.Name = "dotLineStyleToolStripMenuItem";
            this.dotLineStyleToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.dotLineStyleToolStripMenuItem.Text = "&Dot";
            this.dotLineStyleToolStripMenuItem.Click += new System.EventHandler(this.dotLineStyleToolStripMenuItem_Click);
            // 
            // smoothToolStripMenuItem
            // 
            this.smoothToolStripMenuItem.CheckOnClick = true;
            this.smoothToolStripMenuItem.Name = "smoothToolStripMenuItem";
            this.smoothToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.smoothToolStripMenuItem.Text = "S&mooth";
            this.smoothToolStripMenuItem.Click += new System.EventHandler(this.smoothToolStripMenuItem_Click);
            // 
            // rendererToolStripMenuItem
            // 
            this.rendererToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalToolStripMenuItem,
            this.xkcdToolStripMenuItem});
            this.rendererToolStripMenuItem.Name = "rendererToolStripMenuItem";
            this.rendererToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.rendererToolStripMenuItem.Text = "&Renderer";
            // 
            // normalToolStripMenuItem
            // 
            this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            this.normalToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.normalToolStripMenuItem.Text = "&Normal";
            this.normalToolStripMenuItem.Click += new System.EventHandler(this.normalToolStripMenuItem_Click);
            // 
            // xkcdToolStripMenuItem
            // 
            this.xkcdToolStripMenuItem.Name = "xkcdToolStripMenuItem";
            this.xkcdToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.xkcdToolStripMenuItem.Text = "&Xkcd";
            this.xkcdToolStripMenuItem.Click += new System.EventHandler(this.xkcdToolStripMenuItem_Click);
            // 
            // updateIntervalToolStripMenuItem
            // 
            this.updateIntervalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oneDayToolStripMenuItem,
            this.twelveHoursToolStripMenuItem,
            this.sixHoursToolStripMenuItem});
            this.updateIntervalToolStripMenuItem.Name = "updateIntervalToolStripMenuItem";
            this.updateIntervalToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.updateIntervalToolStripMenuItem.Text = "&Update interval";
            // 
            // oneDayToolStripMenuItem
            // 
            this.oneDayToolStripMenuItem.CheckOnClick = true;
            this.oneDayToolStripMenuItem.Name = "oneDayToolStripMenuItem";
            this.oneDayToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.oneDayToolStripMenuItem.Text = "&1 day";
            this.oneDayToolStripMenuItem.Click += new System.EventHandler(this.oneDayToolStripMenuItem_Click);
            // 
            // twelveHoursToolStripMenuItem
            // 
            this.twelveHoursToolStripMenuItem.CheckOnClick = true;
            this.twelveHoursToolStripMenuItem.Name = "twelveHoursToolStripMenuItem";
            this.twelveHoursToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.twelveHoursToolStripMenuItem.Text = "1&2 hours";
            this.twelveHoursToolStripMenuItem.Click += new System.EventHandler(this.twelveHoursToolStripMenuItem_Click);
            // 
            // sixHoursToolStripMenuItem
            // 
            this.sixHoursToolStripMenuItem.CheckOnClick = true;
            this.sixHoursToolStripMenuItem.Name = "sixHoursToolStripMenuItem";
            this.sixHoursToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.sixHoursToolStripMenuItem.Text = "&6 hours";
            this.sixHoursToolStripMenuItem.Click += new System.EventHandler(this.sixHoursToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.updateToolStripMenuItem.Text = "U&pdate";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allValuesToolStripMenuItem,
            this.last30DaysToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // allValuesToolStripMenuItem
            // 
            this.allValuesToolStripMenuItem.Name = "allValuesToolStripMenuItem";
            this.allValuesToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.allValuesToolStripMenuItem.Text = "All values";
            // 
            // last30DaysToolStripMenuItem
            // 
            this.last30DaysToolStripMenuItem.Name = "last30DaysToolStripMenuItem";
            this.last30DaysToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.last30DaysToolStripMenuItem.Text = "Last 30 days";
            this.last30DaysToolStripMenuItem.Click += new System.EventHandler(this.last30DaysToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.ColumnCount = 2;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTableLayoutPanel.Controls.Add(this.mainTabControl, 0, 0);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 24);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 2;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(784, 638);
            this.mainTableLayoutPanel.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(800, 700);
            this.Name = "MainForm";
            this.Text = "EuroborSharp";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lineTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem normalLineStyleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dotLineStyleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smoothToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rendererToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xkcdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateIntervalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oneDayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem twelveHoursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sixHoursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem last30DaysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allValuesToolStripMenuItem;

    }
}

