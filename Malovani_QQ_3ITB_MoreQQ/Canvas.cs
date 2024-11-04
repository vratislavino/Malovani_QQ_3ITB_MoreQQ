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
        public event Action ShapeChanged;

        private List<Shape> shapes = new List<Shape>();
        public IReadOnlyList<Shape> Shapes => shapes;

        Shape? currentShape = null;
        bool isDragging = false;

        public Canvas()
        {
            InitializeComponent();
        }

        public void AddShape(Shape shape)
        {
            shapes.Add(shape);
            Invalidate();
            ShapeChanged?.Invoke();
        }

        public void ClearShapes()
        {
            shapes.Clear();
            currentShape = null;
            Invalidate();
            ShapeChanged?.Invoke();
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (currentShape != null)
                {
                    currentShape.SetDragOffset(e.X, e.Y);
                    isDragging = true;
                }
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (shapes.Count == 0) return;

            if (isDragging && currentShape != null)
            {
                currentShape.Move(e.X, e.Y);
            }
            else
            {
                var shape = shapes.FirstOrDefault(s => s.IsMouseOver(e.X, e.Y));
                if (shape != null)
                {
                    currentShape?.Highlight(false);
                    currentShape = shape;
                    currentShape.Highlight(true);
                }
                else
                {
                    if (currentShape != null)
                    {
                        currentShape.Highlight(false);
                        currentShape = null;
                    }
                }
            }
            Invalidate();
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            if (shapes.Count == 0) return;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            shapes.ForEach(shape => shape.Draw(e.Graphics));
        }

        private void Canvas_Load(object sender, EventArgs e)
        {

        }
    }
}