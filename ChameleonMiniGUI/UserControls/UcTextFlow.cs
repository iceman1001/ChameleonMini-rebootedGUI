using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ChameleonMiniGUI
{
    public partial class UcTextFlow : UserControl
    {
        public delegate void ClickHandler(object sender, EventArgs e);

        public event ClickHandler TextClick;
        public List<string> AvailableCommands { set; get; }

        public UcTextFlow()
        {
            InitializeComponent();
            AutoScroll = true;
            //dock = fill?
            //this.Dock = DockStyle.Fill;
        }

        #region EVENTS

        private void Tb_OnMouseLeave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb == null) return;

            if (base.BackColor == Color.Transparent)
                return;

            tb.BackColor = base.BackColor;
        }

        private void Tb_OnMouseEnter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb == null) return;

            tb.BackColor = SystemColors.ControlDark;
        }

        void Tb_OnClick(object sender, EventArgs e)
        {
            TextClick?.Invoke(sender, e);
        }

        #endregion

        #region Methods

        public void Addline(string noteText)
        {
            var tb = new TextBox
            {
                Multiline = true,
                Text = noteText,
                ReadOnly = true,
                BorderStyle = 0,
                TabStop = false,
                Margin = new Padding(1, 1, 1, 1),
            };

            var size = TextRenderer.MeasureText(tb.Text, tb.Font);
            tb.Width = size.Width;
            tb.Height = size.Height;

            tb.Click += Tb_OnClick;
            tb.MouseEnter += Tb_OnMouseEnter;
            tb.MouseLeave += Tb_OnMouseLeave;

            fLPContainer.Controls.Add(tb);

            // the below might cause an exception of the BackColor of the FLP is eg Transparent
            if (base.BackColor == Color.Transparent)
                return;

            
            tb.BackColor = base.BackColor;
            
        }

        public void SetList()
        {
            fLPContainer.Controls.Clear();

            if (AvailableCommands != null && AvailableCommands.Any())
            {
                AvailableCommands.ForEach(Addline);
            }
            else
            {
                Addline("N/A");

            }
        }

        #endregion
    }
}
