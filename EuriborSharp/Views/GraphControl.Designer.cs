namespace EuriborSharp.Views
{
    partial class GraphControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.graphTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // graphTableLayoutPanel
            // 
            this.graphTableLayoutPanel.ColumnCount = 2;
            this.graphTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.graphTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.graphTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.graphTableLayoutPanel.Name = "graphTableLayoutPanel";
            this.graphTableLayoutPanel.RowCount = 2;
            this.graphTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.graphTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.graphTableLayoutPanel.Size = new System.Drawing.Size(461, 318);
            this.graphTableLayoutPanel.TabIndex = 0;
            // 
            // GraphControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.graphTableLayoutPanel);
            this.Name = "GraphControl";
            this.Size = new System.Drawing.Size(461, 318);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel graphTableLayoutPanel;
    }
}
