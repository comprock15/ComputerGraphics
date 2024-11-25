using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB9
{
    internal partial class AddRotationFigurePolyhedronForm : Form
    {
        public Polyhedron cur_polyhedron;
        public AddRotationFigurePolyhedronForm()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }

        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            double num;
            if (double.TryParse((sender as TextBox).Text, out num) == false)
                (sender as TextBox).BackColor = Color.Red;
            else
                (sender as TextBox).BackColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double x, y, z;
            if (double.TryParse(textBox21.Text, out x) &&
                double.TryParse(textBox20.Text, out y) &&
                double.TryParse(textBox19.Text, out z))
            {
                listBox1.Items.Add("x:" + x.ToString() +
                                   " y:" + y.ToString() +
                                   " z:" + z.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex != -1 && listBox1.Items.Count > 1)
            {
                List<Vertex> v = new List<Vertex> { };
                foreach (var item in listBox1.Items)
                {
                    var s = (item as string).Split(' ');
                    v.Add(new Vertex(double.Parse(s[0].Substring(2)),
                                     double.Parse(s[1].Substring(2)),
                                     double.Parse(s[2].Substring(2))));
                }
                cur_polyhedron = PolyhedronCollection.MakeRotationFigure(comboBox5.SelectedItem as string, (int)numericUpDown1.Value, v);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

    }
}
