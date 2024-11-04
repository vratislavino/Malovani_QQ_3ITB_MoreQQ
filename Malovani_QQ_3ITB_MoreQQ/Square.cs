using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malovani_QQ_3ITB_MoreQQ
{
    public class Square : Shape
    {
        public Square(int x, int y, bool filled, Color color) : base(x, y, filled, color)
        {
        }

        public Square(ShapeDTO data) : base(data)
        {
        }

        public override void Draw(Graphics g)
        {
            if (filled)
            {
                g.FillRectangle(brush, x, y, width, height);
            }
            else
            {
                g.DrawRectangle(
                    pen,
                    x + pen.Width / 2,
                    y + pen.Width / 2,
                    width - pen.Width,
                    height - pen.Width
                    );
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