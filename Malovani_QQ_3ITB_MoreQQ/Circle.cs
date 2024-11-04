using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malovani_QQ_3ITB_MoreQQ
{
    public class Circle : Shape
    {
        public Circle(int x, int y, bool filled, Color color) : base(x, y, filled, color)
        {
        }

        public Circle (ShapeDTO data) :base(data)
        {
        }

        public override void Draw(Graphics g)
        {
            if(filled)
            {
                g.FillEllipse(brush, x, y, width, height);
            } else
            {
                g.DrawEllipse(
                    pen, 
                    x + pen.Width / 2, 
                    y + pen.Width / 2, 
                    width - pen.Width, 
                    height- pen.Width
                    );
            }
            base.Draw(g);
        }

        public override bool IsMouseOver(int mx, int my)
        {
            double a = mx - (x + width / 2);
            double b = my - (y + height / 2);
            return Math.Sqrt(a * a + b * b) <= width / 2;
        }
    }
}
