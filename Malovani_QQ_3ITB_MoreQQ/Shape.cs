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
        public Pen pen;
        protected Brush brush;

        private bool highlighted;
        private static Pen outlinePen;

        private int dragOffsetX;
        private int dragOffsetY;

        public Shape(int x, int y, bool filled, Color color)
        {
            width = 100;
            height = 100;
            this.x = x - width / 2;
            this.y = y - height / 2;
            this.filled = filled;
            this.color = color;
            
            InitRuntimeValues();
        }

        public Shape(ShapeDTO data)
        {
            this.width = data.width;
            this.height = data.height;
            this.x = data.x;
            this.y = data.y;
            this.filled = data.filled;
            this.color = Color.FromArgb(data.r, data.g, data.b);

            InitRuntimeValues();
        }

        private void InitRuntimeValues()
        {
            pen = new Pen(color, 8f);
            brush = new SolidBrush(color);
            outlinePen = new Pen(Color.Black, 2f);
            outlinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            outlinePen.DashPattern = new float[] { 5, 5 };
        }


        public void Highlight(bool enable)
        {
            this.highlighted = enable;
        }

        public virtual void Draw(Graphics g)
        {
            if (highlighted)
            {
                g.DrawRectangle(outlinePen, x, y, width, height);
            }
        }

        public abstract bool IsMouseOver(int mx, int my);

        public void SetDragOffset(int mx, int my)
        {
            dragOffsetX = mx - x;
            dragOffsetY = my - y;
        }

        public void Move(int mx, int my)
        {
            this.x = mx - dragOffsetX;
            this.y = my - dragOffsetY;
        }

        public ShapeDTO GetDTO()
        {
            return new ShapeDTO(this);
        }

        public class ShapeDTO
        {
            public int x;
            public int y;
            public int width;
            public int height;
            public bool filled;
            public int r;
            public int g;
            public int b;

            public string shapeType;

            public ShapeDTO() { }

            public ShapeDTO(Shape s)
            {
                this.x = s.x;
                this.y = s.y;
                this.width = s.width;
                this.height = s.height;
                this.filled = s.filled;
                this.r = s.color.R;
                this.g = s.color.G;
                this.b = s.color.B;
                this.shapeType = s.GetType().ToString();
            }
        }
    }
}


