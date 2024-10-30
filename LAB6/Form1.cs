using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB6
{
    public partial class Form1 : Form
    {
        Polyhedron polyhedron;
        Graphics g;
        Pen p;
        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            p = new Pen(Color.Black,2);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex) 
            {
                case 0:
                    polyhedron = PolyhedronCollection.MakeTetrahedron();
                    break;
                case 1:
                    polyhedron = PolyhedronCollection.MakeHexahedron();
                    break;
                case 2:
                    polyhedron = PolyhedronCollection.MakeOctahedron();
                    break;
                case 3:
                    polyhedron = PolyhedronCollection.MakeIcosahedron();
                    break;
                case 4:
                    polyhedron = PolyhedronCollection.MakeDodecahedron();
                    break;
                default:
                    break;
            }
            
            RedrawField();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            RedrawField();
        }

        // Отрисовывает поле в зависимости от выбранного типа проекции
        public void RedrawField()
        {
            if (polyhedron == null)
                return;

            double[,] matrix;

            switch (comboBox2.SelectedIndex) 
            {
                case 0: // перспективная
                    double c = 1000;
                    matrix = new double[,] { { 1, 0, 0, 0 },
                                             { 0, 1, 0, 0 },
                                             { 0, 0, 0, -1 / c },
                                             { 0, 0, 0, 1 } };    
                    break;
                case 1: //аксонометрическая - изометрическая
                    //matrix = new double[,] {
                    //    { Math.Sqrt(0.5), 0, -Math.Sqrt(0.5), 0 },
                    //    { 1 / Math.Sqrt(6), 2 / Math.Sqrt(6), 1 / Math.Sqrt(6), 0 },
                    //    { 1 / Math.Sqrt(3), -1 / Math.Sqrt(3), 1 / Math.Sqrt(3), 0 },
                    //    { 0, 0, 0, 1 } 
                    //};
                    var psi = AffineTransformations.DegreesToRadians(45);
                    var phi = AffineTransformations.DegreesToRadians(35);
                    matrix = new double[,] {
                        { Math.Cos(psi), Math.Sin(phi)*Math.Cos(psi), 0, 0 },
                        { 0,             Math.Cos(phi),               0, 0 },
                        { Math.Sin(psi),-Math.Sin(phi)*Math.Cos(psi), 0, 0 },
                        { 0,                                       0, 0, 1 }
                    };
                    break;
                default:
                    return;
            }

            g.Clear(Color.White);

            double[,] cur_m;
            Vertex line_start;
            Vertex line_end;
            for (int i = 0; i < polyhedron.vertices.Count; i++)
            {
                cur_m = AffineTransformations.Multiply(new double[,] {{ polyhedron.vertices[i].x, 
                                                                                polyhedron.vertices[i].y, 
                                                                                polyhedron.vertices[i].z, 
                                                                                1 }}, matrix);
                //line_start = new Vertex(cur_m[0,0]/ cur_m[0,3], cur_m[0, 1] / cur_m[0, 3], 0);
                line_start = new Vertex(cur_m[0, 0], cur_m[0, 1], 0);

                //пробегает по всем граничным точкам и рисует линию
                for (int j = 0; j < polyhedron.edges[i].Count;  j++) 
                {
                    var ind = polyhedron.edges[i][j];
                    cur_m = AffineTransformations.Multiply(new double[,] {{ polyhedron.vertices[ind].x,
                                                                            polyhedron.vertices[ind].y,
                                                                            polyhedron.vertices[ind].z,
                                                                            1 }}, matrix);
                    //line_end = new Vertex(cur_m[0, 0] / cur_m[0, 3], cur_m[0, 1] / cur_m[0, 3], 0);
                    line_end = new Vertex(cur_m[0, 0], cur_m[0, 1], 0);
                    g.DrawLine(p, (float)line_start.x, (float)line_start.y, (float)line_end.x, (float)line_end.y);
                }
            }

            //Рисование доп прямых:
            if (checkBox8.Checked)
            {
                double  x1, x2, y1, y2;
                if (double.TryParse(textBox10.Text, out x1) && double.TryParse(textBox8.Text, out y1) &&
                    double.TryParse(textBox13.Text, out x2) && double.TryParse(textBox11.Text, out y2))
                {
                    g.DrawLine(new Pen(Color.Green, 3), new Point((int)x1, (int)y1), new Point((int)x2, (int)y2));
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                MakeTranslation();

            if (checkBox2.Checked)
                MakeRotation();

            if (checkBox3.Checked)
                MakeScaling();

            if (checkBox9.Checked)
                MakeReflection();

            RedrawField();
        }

        void MakeTranslation()
        {
            double dx, dy, dz = 0.0;
            double.TryParse(textBox1.Text, out dx);
            double.TryParse(textBox2.Text, out dy);
            double.TryParse(textBox3.Text, out dz);
                
            AffineTransformations.Translation(ref polyhedron, dx, dy, dz);
        }

        void MakeRotation()
        {
            double angle = 0.0;

            if (checkBox4.Checked)
            {
                double.TryParse(textBox4.Text, out angle);
                AffineTransformations.RotationAboutXAxis(ref polyhedron, angle);
            }
            if (checkBox5.Checked)
            {
                double.TryParse(textBox5.Text, out angle);
                AffineTransformations.RotationAboutYAxis(ref polyhedron, angle);
            }
            if (checkBox6.Checked)
            {
                double.TryParse(textBox6.Text, out angle);
                AffineTransformations.RotationAboutZAxis(ref polyhedron, angle);
            }
            if (checkBox7.Checked) {
                double.TryParse(textBox7.Text, out angle);

                switch (comboBox3.SelectedIndex) {
                    case 0:
                        AffineTransformations.RotateAroundCenter(ref polyhedron, angle, 0);
                        break;
                    case 1:
                        AffineTransformations.RotateAroundCenter(ref polyhedron, angle, 1);
                        break;
                    case 2:
                        AffineTransformations.RotateAroundCenter(ref polyhedron, angle, 2);
                        break;
                }
            }
            if (checkBox8.Checked)
            {
                double x1, x2, y1, y2, z1, z2;
                if (double.TryParse(textBox10.Text, out x1) && double.TryParse(textBox8.Text, out y1) && double.TryParse(textBox9.Text, out z1) &&
                    double.TryParse(textBox13.Text, out x2) && double.TryParse(textBox11.Text, out y2) && double.TryParse(textBox12.Text, out z2) &&
                    double.TryParse(textBox14.Text, out angle))
                {
                    AffineTransformations.RotateAboutLine(ref polyhedron, angle, x1, y1, z1, x2, y2, z2);
                }  
            }
        }

        void MakeScaling() {
            double scaleX = 1, scaleY = 1, scaleZ = 1, scale = 1;

            if (checkBox12.Checked)
                double.TryParse(textBox17.Text, out scaleX);
            if (checkBox11.Checked)
                double.TryParse(textBox16.Text, out scaleY);
            if (checkBox10.Checked)
                double.TryParse(textBox15.Text, out scaleZ);
            if (checkBox13.Checked) {
                double.TryParse(textBox18.Text, out scale);
                AffineTransformations.Scale(ref polyhedron, scale);
            }

            AffineTransformations.Scale(ref polyhedron, scaleX, scaleY, scaleZ);
        }

        void MakeReflection() {
            double[,] matrix = new double[4, 4];

            switch (comboBox4.SelectedIndex) {
                case 0:
                    matrix = new double[,] {
                        { 1, 0, 0, 0 },
                        { 0, 1, 0, 0 },
                        { 0, 0, -1, 0 },
                        { 0, 0, 0, 1 }
                    };
                    break;
                case 1:
                    matrix = new double[,] {
                        { -1, 0, 0, 0 },
                        { 0, 1, 0, 0 },
                        { 0, 0, 1, 0 },
                        { 0, 0, 0, 1 }
                    };
                    break;
                case 2:
                    matrix = new double[,] {
                        { 1, 0, 0, 0 },
                        { 0, -1, 0, 0 },
                        { 0, 0, 1, 0 },
                        { 0, 0, 0, 1 }
                    };
                    break;
            }

            AffineTransformations.Reflect(ref polyhedron, matrix);
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            double num;
            if (double.TryParse((sender as TextBox).Text, out num) == false)
                (sender as TextBox).BackColor = Color.Red;
            else
                (sender as TextBox).BackColor = Color.White;
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e) {
            if (g != null)
                g.Dispose();
            g = pictureBox1.CreateGraphics();
        }
    }
}