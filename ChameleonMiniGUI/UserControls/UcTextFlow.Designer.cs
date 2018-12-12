namespace ChameleonMiniGUI
{
    partial class UcTextFlow
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
            this.fLPContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // fLPContainer
            // 
            this.fLPContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fLPContainer.AutoScroll = true;
            this.fLPContainer.AutoSize = true;
            this.fLPContainer.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLPContainer.Location = new System.Drawing.Point(3, 3);
            this.fLPContainer.Name = "fLPContainer";
            this.fLPContainer.Size = new System.Drawing.Size(64, 57);
            this.fLPContainer.TabIndex = 0;
            // 
            // UcTextFlow
            // 
            this.AutoSize = true;
            this.Controls.Add(this.fLPContainer);
            this.Name = "UcTextFlow";
            this.Size = new System.Drawing.Size(73, 64);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel fLPContainer;
    }
}
