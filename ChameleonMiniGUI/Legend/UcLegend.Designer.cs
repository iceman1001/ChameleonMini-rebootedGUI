namespace ChameleonMiniGUI
{
    partial class UcLegend
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
            this.gpLegend = new System.Windows.Forms.GroupBox();
            this.flpLegend = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnToggle = new System.Windows.Forms.Button();
            this.gpLegend.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpLegend
            // 
            this.gpLegend.AutoSize = true;
            this.gpLegend.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gpLegend.Controls.Add(this.flpLegend);
            this.gpLegend.Controls.Add(this.panel2);
            this.gpLegend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpLegend.Location = new System.Drawing.Point(0, 0);
            this.gpLegend.MaximumSize = new System.Drawing.Size(180, 300);
            this.gpLegend.MinimumSize = new System.Drawing.Size(180, 24);
            this.gpLegend.Name = "gpLegend";
            this.gpLegend.Size = new System.Drawing.Size(180, 59);
            this.gpLegend.TabIndex = 0;
            this.gpLegend.TabStop = false;
            this.gpLegend.Text = "Color legend";
            // 
            // flpLegend
            // 
            this.flpLegend.AutoScroll = true;
            this.flpLegend.AutoSize = true;
            this.flpLegend.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpLegend.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpLegend.Location = new System.Drawing.Point(3, 40);
            this.flpLegend.Name = "flpLegend";
            this.flpLegend.Size = new System.Drawing.Size(0, 0);
            this.flpLegend.TabIndex = 0;
            this.flpLegend.WrapContents = false;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.btnToggle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(174, 23);
            this.panel2.TabIndex = 2;
            // 
            // btnToggle
            // 
            this.btnToggle.Location = new System.Drawing.Point(0, 0);
            this.btnToggle.Name = "btnToggle";
            this.btnToggle.Size = new System.Drawing.Size(20, 20);
            this.btnToggle.TabIndex = 1;
            this.btnToggle.Tag = "280";
            this.btnToggle.Text = "+";
            this.btnToggle.UseVisualStyleBackColor = true;
            this.btnToggle.Click += new System.EventHandler(this.button1_Click);
            // 
            // UcLegend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.gpLegend);
            this.MaximumSize = new System.Drawing.Size(180, 300);
            this.Name = "UcLegend";
            this.Size = new System.Drawing.Size(180, 59);
            this.gpLegend.ResumeLayout(false);
            this.gpLegend.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gpLegend;
        private System.Windows.Forms.FlowLayoutPanel flpLegend;
        private System.Windows.Forms.Button btnToggle;
        private System.Windows.Forms.Panel panel2;
    }
}
