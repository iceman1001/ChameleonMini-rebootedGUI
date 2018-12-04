using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Be.Windows.Forms
{
    class HighlightCollection : Dictionary<long, Highlight>
    {
        public void Add(long position, long length, Color foreColor, Color backColor)
        {
            base.Add(position, new Highlight(position, length, foreColor, backColor));
        }

        public Highlight getHighlight(long position)
        {
            Highlight retval = null;

            foreach (int pos in this.Keys)
            {
                if (position < pos)
                    continue;
                if (position >= this[pos].Position && position <= this[pos].EndPosition && (retval == null || this[pos].Position > retval.Position))
                    retval = this[pos];
            }

            return retval;
        }
    }
}
