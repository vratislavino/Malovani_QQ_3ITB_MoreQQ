using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Malovani_QQ_3ITB_MoreQQ
{
    public partial class Canvas : UserControl
    {
        private List<Shape> shapes = new List<Shape>();

        Shape currentShape = null;

        public Canvas()
        {
            InitializeComponent();
        }

        public void AddShape(Shape shape)
        {
            shapes.Add(shape);
            Invalidate();
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            var shape = shapes.FirstOrDefault(s => s.IsMouseOver(e.X, e.Y));
            if (shape != null)
            {
                if(currentShape != null)
                    currentShape.Highlight(false);

                currentShape = shape;
                currentShape.Highlight(true);
            } else
            {
                if (currentShape != null)
                {
                    currentShape.Highlight(false);
                    currentShape = null;
                }
            }
            Invalidate();
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            if (shapes.Count == 0) return;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            shapes.ForEach(shape => shape.Draw(e.Graphics));
        }
    }
}
