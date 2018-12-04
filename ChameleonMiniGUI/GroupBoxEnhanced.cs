using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

/// <summary>
/// Modified GroupBox with editable border colour
/// </summary>
namespace ChameleonMiniGUI
{
    internal class GroupBoxEnhanced : GroupBox
    {
        private Color borderColor;
        private Color borderColorLight;
        private Int32 borderRadius;
        private float borderWidth;

        /// <summary>Colour of the border</summary>
        public Color BorderColor
        {
            get { return this.borderColor; }
            set { this.borderColor = value; }
        }

        /// <summary>Colour of second border for decoration purposes.</summary>
        public Color BorderColorLight
        {
            get { return borderColorLight; }
            set { borderColorLight = value; }
        }

        public Int32 BorderRadius
        {
            get { return borderRadius; }
            set { borderRadius = value; }
        }

        public float BorderWidth
        {
            get { return borderWidth; }
            set { borderWidth = value; }
        }

        public GroupBoxEnhanced()
        {
            // default colours
            this.borderColor = SystemColors.ControlDarkDark;
            this.borderColorLight = SystemColors.ControlLightLight;
            this.borderRadius = 5;
            this.borderWidth = 1;
            this.DoubleBuffered = true; // prevent flickering
            this.ResizeRedraw = true;   // prevent pixel errors when window moves/resizes
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // don't modify if visual styles are disabled   (in this case the default border has a good visibility)
            if (Application.RenderWithVisualStyles == false)
            {
                base.OnPaint(e);
                return;
            }

            Size textSize = TextRenderer.MeasureText(this.Text, this.Font);

            // dark border (main)
            Rectangle borderRect = new Rectangle(
                0,
                textSize.Height / 2,
                this.ClientRectangle.Width - 1,
                this.ClientRectangle.Height - (textSize.Height / 2) - 2
                );

            // light border (decoration)
            Rectangle borderRect2 = new Rectangle(
                1,
                borderRect.Y + 1,
                borderRect.Width - 2,
                borderRect.Height
                );

            // create path
            GraphicsPath gPathDark = CreatePath(borderRect, borderRadius);
            GraphicsPath gPathLight = CreatePath(borderRect2, borderRadius);

            // draw border
            e.Graphics.FillPath(new SolidBrush(this.BackColor), gPathDark);  // background
            e.Graphics.DrawPath(new Pen(this.borderColorLight,this.borderWidth), gPathLight); // light line
            e.Graphics.DrawPath(new Pen(this.borderColor, this.borderWidth), gPathDark);       // dark line

            // draw text
            RectangleF textRect = e.ClipRectangle;
            textRect.X += 6;
            textRect.Width = textSize.Width;
            textRect.Width += 5;  // be shure to draw the last letter
            textRect.Height = textSize.Height;
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), textRect);
            e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), textRect);

            // NOTE:
            // To avoid flickering and pixel errors
            // enable the flags "this.DoubleBuffered" and "this.ResizeRedraw" in the constructor of this class.
        } //  OnPaint()

        /// <summary>
        /// Create "box" with round corners
        /// </summary>
        private GraphicsPath CreatePath(Rectangle borderRectangle, Int32 radius)
        {
            return CreatePath(borderRectangle.X, borderRectangle.Y, borderRectangle.Width, borderRectangle.Height, radius);
        }

        /// <summary>
        /// Create "box" with round corners
        /// </summary>
        private GraphicsPath CreatePath(Int32 x, Int32 y, Int32 width, Int32 height, Int32 radius)
        {
            // originally published by:  deep.ashar
            // source:  http://social.msdn.microsoft.com/forums/en-US/winforms/thread/cfd34dd1-b6e5-4b56-9901-0dc3d2ca5788
            // modified by Beauty

            Boolean RoundTopLeft = true;
            Boolean RoundTopRight = true;
            Boolean RoundBottomRight = true;
            Boolean RoundBottomLeft = true;

            Int32 xw = x + width;
            Int32 yh = y + height;
            Int32 xwr = xw - radius;
            Int32 yhr = yh - radius;
            Int32 xr = x + radius;
            Int32 yr = y + radius;
            Int32 r2 = radius * 2;
            Int32 xwr2 = xw - r2;
            Int32 yhr2 = yh - r2;

            GraphicsPath p = new GraphicsPath();
            p.StartFigure();

            //Top Left Corner

            if (RoundTopLeft)
            {
                p.AddArc(x, y, r2, r2, 180, 90);
            }
            else
            {
                p.AddLine(x, yr, x, y);
                p.AddLine(x, y, xr, y);
            }

            //Top Edge
            p.AddLine(xr, y, xwr, y);

            //Top Right Corner

            if (RoundTopRight)
            {
                p.AddArc(xwr2, y, r2, r2, 270, 90);
            }
            else
            {
                p.AddLine(xwr, y, xw, y);
                p.AddLine(xw, y, xw, yr);
            }

            //Right Edge
            p.AddLine(xw, yr, xw, yhr);

            //Bottom Right Corner

            if (RoundBottomRight)
            {
                p.AddArc(xwr2, yhr2, r2, r2, 0, 90);
            }
            else
            {
                p.AddLine(xw, yhr, xw, yh);
                p.AddLine(xw, yh, xwr, yh);
            }

            //Bottom Edge
            p.AddLine(xwr, yh, xr, yh);

            //Bottom Left Corner

            if (RoundBottomLeft)
            {
                p.AddArc(x, yhr2, r2, r2, 90, 90);
            }
            else
            {
                p.AddLine(xr, yh, x, yh);
                p.AddLine(x, yh, x, yhr);
            }

            //Left Edge
            p.AddLine(x, yhr, x, yr);

            p.CloseFigure();
            return p;
        } //  CreatePath()

        /// <summary>
        /// Manual refresh of modified GroupBox controls (to avoid pixel faults). <br/>
        /// This should be called when main window was activated, moved or resized. <br/>
        /// For this create callback method by the events Form.Activated, Form.Resize and Form.Move
        /// which calls this method.
        /// If you have more than 1 panel (or other controls), which contains the GroupBoxMOD,
        /// then call this method for each panel.
        /// </summary>
        /// <param name="controlWithGroupboxes">Control, which contains GroupBoxMOD controls</param>
        public static void RedrawGroupBoxDisplay(Control controlWithGroupboxes)
        {
            foreach (Control control in controlWithGroupboxes.Controls)
                if (control.GetType() == typeof(GroupBoxEnhanced))
                    ((GroupBoxEnhanced)control).Invalidate();
        }
    } // GroupBoxMOD
} //NameSpace