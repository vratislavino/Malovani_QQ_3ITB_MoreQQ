namespace Malovani_QQ_3ITB_MoreQQ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            canvas1.AddShape(
                new Circle(
                    canvas1.Width / 2, 
                    canvas1.Height / 2, 
                    checkBox1.Checked, 
                    button1.BackColor)
                );
        }
    }
}
