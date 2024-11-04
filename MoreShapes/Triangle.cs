using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malovani_QQ_3ITB_MoreQQ;

namespace MoreShapes
{
    public class Triangle : Shape
    {
        public override bool IsMouseOver(int mx, int my)
        {
            return
                mx >= x &&
                mx <= x + width &&
                my >= y &&
                my <= y + height;
        }

        public Triangle(int x, int y, bool filled, Color color) : base(x, y, filled, color)
        {
        }

        public Triangle(ShapeDTO data) : base(data)
        {
        }

        public override void Draw(Graphics g)
        {
            float shrinkAmount = pen.Width / 2;
            Point[] points = new[]
            {
                    new Point((int)(x + width / 2), (int)(y + shrinkAmount)),                         
                    new Point((int)(x + width - shrinkAmount), (int)(y + height / 2)),               
                    new Point((int)(x + (width * 0.8) - shrinkAmount), (int)(y + height - shrinkAmount)), 
                    new Point((int)(x + (width * 0.2) + shrinkAmount), (int)(y + height - shrinkAmount)), 
                    new Point((int)(x + shrinkAmount), (int)(y + height / 2))
            };

            if (filled)
            {
                g.FillPolygon(brush, points);
            }
            else
            {
                g.DrawPolygon(pen, points);
            }
            base.Draw(g);
        }
    }
}
