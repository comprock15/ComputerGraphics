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
    internal partial class AddFunctionGraphicForm : Form
    {
        public Polyhedron cur_polyhedron;
        public int w; public int h;
        public AddFunctionGraphicForm(int pbW, int pbH)
        {
            InitializeComponent();
            w= pbW; h= pbH;
        }

        

        private void showPlotButton_Click(object sender, EventArgs e)
        {
            double x0 = (double)numericUpDownX0.Value;
            double x1 = (double)numericUpDownX1.Value;
            double y0 = (double)numericUpDownY0.Value;
            double y1 = (double)numericUpDownY1.Value;

            if (x1 < x0)
            {
                numericUpDownX0.BackColor = Color.Red;
                numericUpDownX1.BackColor = Color.Red;
                return;
            }
            else
            {
                numericUpDownX_ValueChanged(sender, e);
            }

            if (y1 < y0)
            {
                numericUpDownY0.BackColor = Color.Red;
                numericUpDownY1.BackColor = Color.Red;
                return;
            }
            else
            {
                numericUpDownY_ValueChanged(sender, e);
            }

            // Получение графика
            try
            {
                cur_polyhedron = FunctionPlotting.GetPlot(textBoxFunc.Text, x0, x1, y0, y1,
                                                      (int)numericUpDownStep.Value);
                textBoxFunc_TextChanged(sender, e);
                
            }
            catch (Exception ex)
            {
                textBoxFunc.BackColor = Color.Red;
                return;
            }

            // Настройка приближения
            double scale = Math.Min(w / 2.0 / ((double)numericUpDownX1.Value - (double)numericUpDownX0.Value),
                                    h / 2.0 / ((double)numericUpDownY1.Value - (double)numericUpDownY0.Value));
            AffineTransformations.Scale(ref cur_polyhedron, scale);

            // Поворот
            if (plottingCheckBox.Checked)
            {
                AffineTransformations.RotateAroundCenter(ref cur_polyhedron, 70, 0);
                AffineTransformations.RotateAroundCenter(ref cur_polyhedron, 30, 1);
            }

            // Центрирование
            var center = AffineTransformations.CalculateCenterCoords(cur_polyhedron);
            var pbCenter = new double[2] { w / 2, h / 2 };
            AffineTransformations.Translation(ref cur_polyhedron, pbCenter[0] - center[0, 0], pbCenter[1] - center[0, 1], 0);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBoxFunc_TextChanged(object sender, EventArgs e)
        {
            textBoxFunc.BackColor = Color.White;
        }

        private void numericUpDownX_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownX0.BackColor = Color.White;
            numericUpDownX1.BackColor = Color.White;
        }

        private void numericUpDownY_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownY0.BackColor = Color.White;
            numericUpDownY1.BackColor = Color.White;
        }
    }
}
