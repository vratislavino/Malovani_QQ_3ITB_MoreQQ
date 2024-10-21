using System.Reflection;

namespace Malovani_QQ_3ITB_MoreQQ
{
    public partial class Form1 : Form
    {
        /*
     Zeptat se DJ na svìtla
     TODO: 
     * volba barvy ---
     * hezèí vykreslení ---
     * hezèí èára ---
     * pøesouvání objektù ---
     * clear objektù ---
     * square ---
     * reflexe - (assembly)
     * ukládání
     */

        FileManager fileManager = new FileManager();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Assembly ass = Assembly.GetExecutingAssembly();
            var types = ass.GetTypes();
            var shapeTypes = types.Where(t => t.IsSubclassOf(typeof(Shape)));

            comboBox1.Items.AddRange(shapeTypes.ToArray());

            if (shapeTypes.Count() > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selectedType = comboBox1.SelectedItem as Type;
            if (selectedType != null)
            {
                var newShape = Activator.CreateInstance(
                    selectedType,
                    canvas1.Width / 2,
                    canvas1.Height / 2,
                    checkBox1.Checked,
                    button1.BackColor
                    ) as Shape;
                canvas1.AddShape(newShape);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button1.BackColor = colorDialog1.Color;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Do you really want to destroy all your work?",
                "Are you sure?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
                ) == DialogResult.Yes)
            {
                canvas1.ClearShapes();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            //sfd.FileName = ""; // default path
            sfd.Filter = "Shapes JSON (.json)|*.json";
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                string path = sfd.FileName;
                fileManager.SaveShapes(path, canvas1.Shapes);
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Shapes JSON (.json)|*.json";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                string path = ofd.FileName;
                var shapes = fileManager.LoadShapes(path);
                foreach(var shape in shapes)
                {
                    canvas1.AddShape(shape);
                }
            }
        }
    }
}
