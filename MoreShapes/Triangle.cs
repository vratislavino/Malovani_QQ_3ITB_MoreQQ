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
        public Triangle(int x, int y, bool filled, Color color) : base(x, y, filled, color)
        {
        }

        public Triangle(ShapeDTO data) : base(data)
        {
        }

        public override void Draw(Graphics g)
        {
            if(filled)
            {
                g.FillPolygon(brush, new Point[] { 
                    new Point(x, y + height),
                    new Point(x + width, y + height),
                    new Point(x + width / 2, y)
                });
            } else
            {
                g.DrawPolygon(pen, new Point[] {
                    new Point(x, y + height),
                    new Point(x + width, y + height),
                    new Point(x + width / 2, y)
                });
            }
            base.Draw(g);
        }

        public override bool IsMouseOver(int mx, int my)
        {
            return 
                mx >= x && 
                mx <= x + width &&
                my >= y &&
                my <= y + height;
        }
    }
}
