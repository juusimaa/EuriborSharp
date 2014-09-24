namespace EuriborSharp.Views
{
    sealed partial class AboutForm
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.oxyLinkLabel = new System.Windows.Forms.LinkLabel();
            this.componentsLabel = new System.Windows.Forms.Label();
            this.copyLabel = new System.Windows.Forms.Label();
            this.moreLinqLinkLabel = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.21008F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.78992F));
            this.tableLayoutPanel.Controls.Add(this.pictureBox, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.titleLabel, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.versionLabel, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.copyLabel, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.componentsLabel, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.oxyLinkLabel, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.moreLinqLinkLabel, 1, 5);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 6;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(357, 183);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Image = global::EuriborSharp.Properties.Resources.euro_1;
            this.pictureBox.Location = new System.Drawing.Point(3, 3);
            this.pictureBox.Name = "pictureBox";
            this.tableLayoutPanel.SetRowSpan(this.pictureBox, 3);
            this.pictureBox.Size = new System.Drawing.Size(84, 90);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(93, 11);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(78, 19);
            this.titleLabel.TabIndex = 3;
            this.titleLabel.Text = "titleLabel";
            // 
            // versionLabel
            // 
            this.versionLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.versionLabel.AutoSize = true;
            this.versionLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLabel.Location = new System.Drawing.Point(93, 35);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(108, 19);
            this.versionLabel.TabIndex = 4;
            this.versionLabel.Text = "versionLabel";
            // 
            // oxyLinkLabel
            // 
            this.oxyLinkLabel.AutoSize = true;
            this.oxyLinkLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oxyLinkLabel.Location = new System.Drawing.Point(93, 121);
            this.oxyLinkLabel.Name = "oxyLinkLabel";
            this.oxyLinkLabel.Size = new System.Drawing.Size(78, 15);
            this.oxyLinkLabel.TabIndex = 1;
            this.oxyLinkLabel.TabStop = true;
            this.oxyLinkLabel.Text = "oxyLinkLabel";
            // 
            // componentsLabel
            // 
            this.componentsLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.componentsLabel.AutoSize = true;
            this.componentsLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.componentsLabel.Location = new System.Drawing.Point(93, 101);
            this.componentsLabel.Name = "componentsLabel";
            this.componentsLabel.Size = new System.Drawing.Size(159, 15);
            this.componentsLabel.TabIndex = 2;
            this.componentsLabel.Text = "Open Source Components:";
            // 
            // copyLabel
            // 
            this.copyLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.copyLabel.AutoSize = true;
            this.copyLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copyLabel.Location = new System.Drawing.Point(93, 70);
            this.copyLabel.Name = "copyLabel";
            this.copyLabel.Size = new System.Drawing.Size(74, 16);
            this.copyLabel.TabIndex = 5;
            this.copyLabel.Text = "copyLabel";
            // 
            // moreLinqLinkLabel
            // 
            this.moreLinqLinkLabel.AutoSize = true;
            this.moreLinqLinkLabel.Location = new System.Drawing.Point(93, 143);
            this.moreLinqLinkLabel.Name = "moreLinqLinkLabel";
            this.moreLinqLinkLabel.Size = new System.Drawing.Size(96, 13);
            this.moreLinqLinkLabel.TabIndex = 6;
            this.moreLinqLinkLabel.TabStop = true;
            this.moreLinqLinkLabel.Text = "moreLinqLinkLabel";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 183);
            this.Controls.Add(this.tableLayoutPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowIcon = false;
            this.Text = "AboutForm";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.LinkLabel oxyLinkLabel;
        private System.Windows.Forms.Label componentsLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label copyLabel;
        private System.Windows.Forms.LinkLabel moreLinqLinkLabel;
    }
}