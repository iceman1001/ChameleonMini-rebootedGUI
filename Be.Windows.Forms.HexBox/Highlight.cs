using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Be.Windows.Forms
{
    class Highlight
    {
        public long Position;
        public long EndPosition;
        public long Length;
        public Brush ForeBrush;
        public Brush BackBrush;

        public Highlight(long position, long length, Color foreColor, Color backColor)
        {
            this.Position = position;
            this.Length = length;
            this.ForeBrush = new SolidBrush(foreColor);
            this.BackBrush = new SolidBrush(backColor);
            this.EndPosition = position + length - 1;
        }
    }
}
