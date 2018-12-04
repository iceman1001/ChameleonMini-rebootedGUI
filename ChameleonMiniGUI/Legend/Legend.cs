using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace ChameleonMiniGUI
{
    public class Legend
    {
        public string Title { get; set; }

        public bool IsLegendVisible { get; set; }

        public bool IsBorderVisible { get; set; }

        public Color LegendBackColor1 { get; set; }

        public Color LegendBackColor2 { get; set; }

        public Color LegendBorderColor { get; set; }

        public Font Legendfont { get; set; }

        public Point LegendPosition { get; set; }

        private float _spacing;

        public Legend()
        {
            Title = "Legend";
            LegendPosition = new Point(0, 0);
            Legendfont = SystemFonts.DefaultFont;
            LegendBackColor1 = Color.White;
            LegendBorderColor = Color.DarkGray;
        }

        public void AddLegend(Graphics g, List<IlegendItem> list)
        {
            this.AddLegend(g, LegendPosition, list);
        }

        public void AddLegend(Graphics g, Point legendPos, List<IlegendItem> list)
        {
            if (!IsLegendVisible) return;

            if (!list.Any()) return;

            var height = 0.0f;
            var box_Width = 0.0f;
            var box_height = 0.0f;

            foreach (var item in list)
            {
                var size = g.MeasureString(item.Description, Legendfont);

                if ((box_Width < size.Width))
                    box_Width = size.Width;

                if ((height < size.Height))
                    height = size.Height;
            }

            // legendWidth += 10 + _spacing + _spacing
            box_Width += (_spacing + _spacing);

            box_height = list.Count * height + 10.0F;   // add extra marginal

            box_Width += 26;

            var offset = 16.0f;
            var xc = (legendPos.X + offset);
            float yc = legendPos.Y;

            DrawLegend(g, xc, yc, box_Width, box_height, height, list);
        }

        public void DrawLegend(Graphics g, float pos_x, float pos_y, float legend_width, float legend_height, float lineHeight, List<IlegendItem> list)
        {
            if (!IsLegendVisible) return;

            var pen = new Pen(LegendBorderColor, 1.0F);
            SolidBrush brush = null;
            Rectangle legendRect;

            try
            {
                legendRect = new Rectangle((int) pos_x, (int) pos_y, (int) legend_width, (int) legend_height);

                brush = new SolidBrush(LegendBackColor1);

                g.FillRectangle(brush, legendRect);

                if (IsBorderVisible)
                    g.DrawRectangle(pen, legendRect);
            }
            finally
            {
                brush?.Dispose();
            }

            Brush txtBrush = null;

            try
            {
                var row_height = (int) (_spacing + lineHeight);

                var sq_X = legendRect.X + (int) _spacing;
                var sq_Y = legendRect.Y + row_height;

                var rect = new Rectangle(sq_X, sq_Y, (int)legend_width, row_height);
                var sf = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    Trimming = StringTrimming.EllipsisCharacter
                };

                foreach (var item in list)
                {
                    txtBrush = GetColor(item.ForeGroundColor);

                    //  Square
                    DrawSquare(g, rect, item.BackGroundColor);

                    g.DrawString(item.Description, Legendfont, txtBrush, rect.X + 18, rect.Y - 1, sf);
                    // *** One step down.
                    rect.Y += (int) (lineHeight + _spacing);
                }
            }
            finally
            {
                txtBrush?.Dispose();
            }
        }

        public void DrawSquare(Graphics g, Rectangle rect, string color)
        {
            if (g == null) return;

            try
            {
                g.FillRectangle(GetColor(color), rect);
            }
            catch (Exception ex)
            {
            }
        }

        private Brush GetColor(string color)
        {
            var b = Brushes.Aqua;
            if (!string.IsNullOrWhiteSpace(color))
            {
                b = new SolidBrush(Color.FromName(color));
            }
            return b;
        }

        public override string ToString()
        {
            return $"Legend: {Title}";
        }
    }
}