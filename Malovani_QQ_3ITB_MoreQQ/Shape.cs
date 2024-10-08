using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malovani_QQ_3ITB_MoreQQ
{
    public abstract class Shape
    {
        protected int x;
        protected int y;
        protected int width;
        protected int height;
        protected bool filled;
        protected Color color;
        protected Pen pen;
        protected Brush brush;

        private bool highlighted;

        public Shape(int x, int y, bool filled, Color color)
        {
            width = 100;
            height = 100;
            this.x = x - width / 2;
            this.y = y - height / 2;
            this.filled = filled;
            this.color = color;

            pen = new Pen(color, 8f);
            brush = new SolidBrush(color);
        }

        public void Highlight(bool enable)
        {
            this.highlighted = enable;
        }

        public virtual void Draw(Graphics g)
        {
            if(highlighted)
            {
                g.DrawRectangle(Pens.Black, x, y, width, height);
            }
        }

        public abstract bool IsMouseOver(int mx, int my);
    }
}
