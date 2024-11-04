using System.Diagnostics;
using System.Reflection;

namespace Malovani_QQ_3ITB_MoreQQ
{
    public partial class Form1 : Form
    {
        /*
     Zeptat se DJ na sv�tla
     TODO: 
     * volba barvy ---
     * hez�� vykreslen� ---
     * hez�� ��ra ---
     * p�esouv�n� objekt� ---
     * clear objekt� ---
     * square ---
     * reflexe - (assembly)
     * ukl�d�n�
     */

        FileManager fileManager = new FileManager();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Assembly ass = Assembly.GetExecutingAssembly();
            LoadTypesFromAssembly(ass);

            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void LoadTypesFromAssembly(Assembly ass)
        {
            var types = ass.GetTypes();
            var shapeTypes = types.Where(t => t.IsSubclassOf(typeof(Shape)));

            comboBox1.Items.AddRange(shapeTypes.ToArray());

            // Select the added assembly in comboBox1
            comboBox1.SelectedItem = ass;
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
                if (newShape != null)
                {
                    canvas1.AddShape(newShape);
                }
                else
                {
                    MessageBox.Show("Failed to create a new shape.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
            sfd.FileName = "shapes";
            sfd.Filter = "Shapes JSON (.json) |*.json";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string path = sfd.FileName;
                fileManager.SaveShapes(path, canvas1.Shapes);
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Shapes JSON (.json) |*.json";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string path = ofd.FileName;
                var shapes = fileManager.LoadShapes(path);
                foreach (var shape in shapes)
                {
                    canvas1.AddShape(shape);
                }
            }
        }

        private void addMoreShapesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Shapes Library (.dll) |*.dll";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string path = ofd.FileName;
                Assembly ass = fileManager.LoadAssemblyFromFile(path);
                if (ass != null)
                {
                    LoadTypesFromAssembly(ass);

              
                }
                else
                {
                    MessageBox.Show("Error loading dll!");
                }
            }
        }
    }
}
