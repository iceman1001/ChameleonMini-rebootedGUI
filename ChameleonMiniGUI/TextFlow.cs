using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChameleonMiniGUI
{
    public partial class textFlow : UserControl
    {
        public delegate void ClickHandler(object sender, EventArgs e);
        public event ClickHandler TextClick;

        public textFlow()
        {
            InitializeComponent();
            AutoScroll = true;
        }

        public void Add(string noteText)
        {
            TextBox TextBox1 = new TextBox();
            TextBox1.Multiline = true;
            TextBox1.Text = noteText;
            TextBox1.ReadOnly = true;
            TextBox1.BorderStyle = 0;
            TextBox1.TabStop = false;
            Size size = TextRenderer.MeasureText(TextBox1.Text, TextBox1.Font);
            TextBox1.Width = size.Width;
            TextBox1.Height = size.Height;
            TextBox1.Click += new EventHandler(TextBox1_Click);
            TextBox1.Margin = new Padding(1, 1, 1, 1);
            fLPContainer.Controls.Add(TextBox1);
            try
            {
                // the below might cause an exception of the BackColor of the FLP is eg Transparent
                if (base.BackColor != System.Drawing.Color.Transparent)
                {
                    TextBox1.BackColor = base.BackColor;
                }
            }
            catch { }
        }

        public void Clear()
        {
           Controls.Clear();
        }

        void TextBox1_Click(object sender, EventArgs e)
        {
            if (TextClick != null)
            {
                TextClick(sender, e);
            }
        }
    }
}
