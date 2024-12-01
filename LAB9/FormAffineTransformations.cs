using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LAB9
{
    public partial class Form1 : Form
    {

        /// <summary>
        /// Применить афинные преобразования
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            if (cur_polyhedron == null) return;

            if (checkBox1.Checked)
                MakeTranslation();

            if (checkBox2.Checked)
                MakeRotation();

            if (checkBox3.Checked)
                MakeScaling();

            if (checkBox9.Checked)
                MakeReflection();

            Redraw();
            RedrawCamryField();
        }

        void MakeTranslation()
        {
            double dx, dy, dz = 0.0;
            double.TryParse(textBox1.Text, out dx);
            double.TryParse(textBox2.Text, out dy);
            double.TryParse(textBox3.Text, out dz);

            AffineTransformations.Translation(ref cur_polyhedron, dx, dy, dz);
        }


        void MakeRotation()
        {
            double angle = 0.0;

            if (checkBox4.Checked)
            {
                double.TryParse(textBox4.Text, out angle);
                AffineTransformations.RotationAboutXAxis(ref cur_polyhedron, angle);
            }
            if (checkBox5.Checked)
            {
                double.TryParse(textBox5.Text, out angle);
                AffineTransformations.RotationAboutYAxis(ref cur_polyhedron, angle);
            }
            if (checkBox6.Checked)
            {
                double.TryParse(textBox6.Text, out angle);
                AffineTransformations.RotationAboutZAxis(ref cur_polyhedron, angle);
            }
            if (checkBox7.Checked)
            {
                double.TryParse(textBox7.Text, out angle);

                switch (comboBox3.SelectedIndex)
                {
                    case 0:
                        AffineTransformations.RotateAroundCenter(ref cur_polyhedron, angle, 0);
                        break;
                    case 1:
                        AffineTransformations.RotateAroundCenter(ref cur_polyhedron, angle, 1);
                        break;
                    case 2:
                        AffineTransformations.RotateAroundCenter(ref cur_polyhedron, angle, 2);
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
                    AffineTransformations.RotateAboutLine(ref cur_polyhedron, angle, x1, y1, z1, x2, y2, z2);
                }
            }
        }

        void MakeScaling()
        {
            double scaleX = 1, scaleY = 1, scaleZ = 1, scale = 1;

            if (checkBox12.Checked)
                double.TryParse(textBox17.Text, out scaleX);
            if (checkBox11.Checked)
                double.TryParse(textBox16.Text, out scaleY);
            if (checkBox10.Checked)
                double.TryParse(textBox15.Text, out scaleZ);
            if (checkBox13.Checked)
            {
                double.TryParse(textBox18.Text, out scale);
                AffineTransformations.Scale(ref cur_polyhedron, scale);
            }

            AffineTransformations.Scale(ref cur_polyhedron, scaleX, scaleY, scaleZ);
        }

        void MakeReflection()
        {
            double[,] matrix = new double[4, 4];

            switch (comboBox4.SelectedIndex)
            {
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

            AffineTransformations.Reflect(ref cur_polyhedron, matrix);
        }


    }
}

//internal class HelpingDrawingFun
//{
//    internal static Bitmap DrawEdges(double[,] matrix, ListBox.ObjectCollection polyhedrons, int width, int height)
//    {
//        var bmp = new Bitmap(width, height);



//        return bmp;
//    }

//    internal static double[,] GetProjectionMatrix(int index)
//    {
//        switch (index)
//        {
//            case 0: // перспективная
//                double c = 1000;
//                return new double[,] {
//                        { 1, 0, 0, 0 },
//                        { 0, 1, 0, 0 },
//                        { 0, 0, 0, -1 / c },
//                        { 0, 0, 0, 1 }
//                    };

//            case 1: //аксонометрическая - изометрическая
//                var psi = AffineTransformations.DegreesToRadians(45);
//                var phi = AffineTransformations.DegreesToRadians(35);
//                return new double[,] {
//                        { Math.Cos(psi), Math.Sin(phi)*Math.Cos(psi), 0, 0 },
//                        { 0,             Math.Cos(phi),               0, 0 },
//                        { Math.Sin(psi),-Math.Sin(phi)*Math.Cos(psi), 0, 0 },
//                        { 0,                                       0, 0, 1 }
//                    };

//            default:
//                return null;
//        }
//    }

//}
