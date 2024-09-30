using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LAB4
{
    public partial class Form1 : Form
    {
        enum selectType { NONE, VERTEX, EDGE, POLY };
        enum taskType { DRAWING, P_CLASS_EDGE, P_CLASS_POLY, FIND_CROSS_POINT, SET_ROTATION_POINT, SET_SCALE_POINT }
        bool polygon_drawing;
        List<Polygon> polygons;
        Graphics g;
        Pen p_black, p_red;

        private PointF? tempLineStart = null;
        private PointF? tempLineEnd = null;

        selectType selectedItemType;
        string selectedItemPath;
        taskType cur_mode;

        public Form1()
        {
            InitializeComponent();
            polygon_drawing = false;
            polygons = new List<Polygon>();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = pictureBox1.CreateGraphics();
            p_black = new Pen(Color.Black, 2);
            p_red = new Pen(Color.OrangeRed, 4);

            selectedItemType = selectType.NONE;
            selectedItemPath = "";

            label13.Text = "";
            cur_mode = taskType.DRAWING;
        }

        public void RedrawField()
        {
            g.Clear(Color.White);
            foreach (Polygon polygon in polygons) 
            {
                if (polygon.vertices_count == 1)
                {
                    g.DrawEllipse(p_black, polygon.vertices[0].X, polygon.vertices[0].Y, 3,3);
                }
                else if (polygon.vertices_count == 2)
                {
                    g.DrawLine(p_black, polygon.vertices[0], polygon.vertices[1]);
                }
                else
                {
                    g.DrawPolygon(p_black, polygon.vertices.ToArray());
                    //foreach (Point point in polygon.vertices)
                    //    g.DrawEllipse(p_black, point.X, point.Y, 3, 3);
                }
            }

            if (tempLineStart != null && tempLineEnd != null)
                g.DrawLine(p_red, tempLineStart.Value, tempLineEnd.Value);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e) {
            switch (cur_mode) {
                case taskType.FIND_CROSS_POINT: {
                        if (tempLineStart == null) {
                            tempLineStart = e.Location;
                            tempLineEnd = null;
                        }
                        else if (tempLineEnd == null) {
                            tempLineEnd = e.Location;
                            RedrawField();
                            g.DrawLine(p_red, tempLineStart.Value, tempLineEnd.Value);
                            findCrossPoint(tempLineStart.Value, tempLineEnd.Value);

                            tempLineStart = null;
                            tempLineEnd = null;
                        }
                    }
                    break;
                case taskType.P_CLASS_EDGE: {
                        isPointOnLeftSide(e.Location);
                    }
                    break;
                case taskType.P_CLASS_POLY:
                    break;
                case taskType.SET_ROTATION_POINT:
                    affineRotationPointX.Value = e.X;
                    affineRotationPointY.Value = e.Y;
                    cur_mode = taskType.DRAWING;
                    pictureBox1.Cursor = Cursors.Default;
                    break;
                case taskType.SET_SCALE_POINT:
                    affineScalePointX.Value = e.X;
                    affineScalePointY.Value = e.Y;
                    cur_mode = taskType.DRAWING;
                    pictureBox1.Cursor = Cursors.Default;
                    break;
                default:
                    if (e.Button == MouseButtons.Right)
                        {
                        polygon_drawing = false;
                        TreeNode new_edge = new TreeNode("Edge " + (treeView1.Nodes[polygons.Count - 1].Nodes.Count + 1).ToString());

                        new_edge.Nodes.Add(new TreeNode("Vertex " + (polygons.Last().vertices_count).ToString()));
                        new_edge.Nodes.Add(new TreeNode("Vertex 1"));

                        treeView1.Nodes[polygons.Count - 1].Nodes.Add(new_edge);
                        polygons.Last().AddVertex(polygons.Last().vertices[0]);

                        //TODO проверка чтоб новые появившиеся грани не пересекали уже существующие!!
                    }
                    else
                    {
                        if (!polygon_drawing) {
                            polygon_drawing = true;
                            polygons.Add(new Polygon(e.Location));
                            treeView1.Nodes.Add(new TreeNode("Polygon " + polygons.Count.ToString())); ;
                        }
                        else {
                            polygons.Last().AddVertex(e.Location);

                            TreeNode new_edge = new TreeNode("Edge " + (treeView1.Nodes[polygons.Count - 1].Nodes.Count + 1).ToString());
                            new_edge.Nodes.Add(new TreeNode("Vertex " + (polygons.Last().vertices_count - 1).ToString()));
                            new_edge.Nodes.Add(new TreeNode("Vertex " + (polygons.Last().vertices_count).ToString()));

                            treeView1.Nodes[polygons.Count - 1].Nodes.Add(new_edge);

                        }
                        RedrawField();
                    }
                    break;
            }

        }

        public void ShowSelectedItem(Pen p)
        {
            switch (selectedItemType)
            {
                case selectType.POLY:
                    if (polygons[int.Parse(selectedItemPath.Remove(0, 8)) - 1].vertices.Count > 1)
                        g.DrawPolygon(p, polygons[int.Parse(selectedItemPath.Remove(0, 8)) - 1].vertices.ToArray());
                    else
                        g.DrawEllipse(p, polygons[int.Parse(selectedItemPath.Remove(0, 8)) - 1].vertices[0].X, polygons[int.Parse(selectedItemPath.Remove(0, 8)) - 1].vertices[0].Y, 5, 5);
                    break;
                case selectType.EDGE:
                    var poly = polygons[int.Parse(selectedItemPath.Split(' ')[1]) - 1];
                    PointF pnt1 = poly.vertices[int.Parse(selectedItemPath.Split(' ')[3]) - 1];
                    PointF pnt2 = poly.vertices[int.Parse(selectedItemPath.Split(' ')[3])];
                    g.DrawLine(p, pnt1, pnt2);
                    break;
                case selectType.VERTEX:
                    poly = polygons[int.Parse(selectedItemPath.Split(' ')[1]) - 1];
                    PointF pnt = poly.vertices[int.Parse(selectedItemPath.Split(' ')[3]) - 1];
                    g.DrawEllipse(p, pnt.X, pnt.Y, 5,5);
                    break;
                default:
                    break;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var selected_node = e.Node;
            selectedItemPath = selected_node.Text;

            if (selectedItemPath.Contains("Vertex"))
            {
                selectedItemType = selectType.VERTEX;

                if (selected_node.Parent.Parent != null)
                {
                    selectedItemPath = selected_node.Parent.Parent.Text + " " + selectedItemPath;
                }
                else
                {
                    selectedItemPath = selected_node.Parent.Text + " " + selectedItemPath;
                }
            }
            else if (selectedItemPath.Contains("Edge"))
            {
                selectedItemType = selectType.EDGE;
                selectedItemPath = selected_node.Parent.Text + " " + selectedItemPath;
            }
            else if (selectedItemPath.Contains("Polygon"))
            {
                selectedItemType = selectType.POLY;
            }
            ShowSelectedItem(p_red);
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            RedrawField();
        }

        private void isPointOnLeftSide(PointF point)
        {
            var ItemPath = label13.Text;
            var poly = polygons[int.Parse(ItemPath.Split(' ')[1]) - 1];
            var p1 = poly.vertices[int.Parse(ItemPath.Split(' ')[3]) - 1];
            var p2 = poly.vertices[int.Parse(ItemPath.Split(' ')[3])];

            var x = (point.Y - p2.Y) * (p1.X - p2.X) / (float)(p1.Y - p2.Y) + p2.X;
            var y = (point.X - p2.X) * (p1.Y - p2.Y) / (float)(p1.X - p2.X) + p2.Y;

            if (point.X > x)
            { textBox2.Text = "Точка (" + point.X + ", " + point.Y + ") справа от ребра"; }
            else
            { textBox2.Text = "Точка (" + point.X + ", " + point.Y + ") слева от ребра"; }
        }

        private void findCrossPoint(PointF p1, PointF p2) {
            var ItemPath = label14.Text;
            var poly = polygons[int.Parse(ItemPath.Split(' ')[1]) - 1];
            var polyP1 = poly.vertices[int.Parse(ItemPath.Split(' ')[3]) - 1];
            var polyP2 = poly.vertices[int.Parse(ItemPath.Split(' ')[3])];

            float A1 = p2.Y - p1.Y;
            float B1 = p1.X - p2.X;
            float C1 = A1 * p1.X + B1 * p1.Y;

            float A2 = polyP2.Y - polyP1.Y;
            float B2 = polyP1.X - polyP2.X;
            float C2 = A2 * polyP1.X + B2 * polyP1.Y;

            float det = A1 * B2 - A2 * B1;

            if (det == 0)
                textBox4.Text = "Рёбра параллельны";
            else {
                int x = (int)((B2 * C1 - B1 * C2) / det);
                int y = (int)((A1 * C2 - A2 * C1) / det);
                textBox4.Text = $"({x}, {y})";
                g.DrawEllipse(p_red, x - 2, y - 2, 5, 5);
            }

            textBox5.Text = "Готово";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {
            if (radioButton1.Checked) {
                if (selectedItemType == selectType.EDGE) {
                    cur_mode = taskType.FIND_CROSS_POINT;
                    label14.Text = selectedItemPath;
                    textBox5.Text = "Нарисуйте второе ребро";
                }
                else {
                    textBox5.Text = "Ребро не выбрано!";
                    radioButton1.Checked = false;
                    radioButton4.Checked = true;
                }
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            tempLineStart = null;
            tempLineEnd = null;
            RedrawField();

            if (radioButton3.Checked)
            {
                if (selectedItemType == selectType.EDGE)
                {
                    cur_mode = taskType.P_CLASS_EDGE;
                    label13.Text = selectedItemPath;
                    textBox2.Text = "Выберите точку на экране";
                }
                else
                { 
                    textBox2.Text = "Ребро не выбрано!";
                    radioButton3.Checked = false;
                    radioButton4.Checked = true;
                }
                    
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            cur_mode = taskType.DRAWING;
        }

        /// <summary>
        /// Применить афинное преобразование к полигону
        /// </summary>
        private void buttonApplyTransform_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null || !treeView1.SelectedNode.Text.Contains("Polygon"))
            {
                // TODO: Сообщить пользователю, что надо выбрать полигон
                return;
            };

            Polygon polygon = polygons[int.Parse(treeView1.SelectedNode.Text.Split(' ')[1]) - 1];

            // Смещение
            if (checkBoxOffset.Checked)
            {
                AffineTransformations.TransformOffset(ref polygon, 
                                                      (double)affineOffsetDx.Value, 
                                                      (double)affineOffsetDy.Value);
            }

            // Поворот вокруг заданной пользователем точки
            if (checkBoxRotationPoint.Checked)
            {
                AffineTransformations.TransformRotationPoint(ref polygon,
                                                             (double)affineRotationPointAngle.Value,
                                                             (double)affineRotationPointX.Value,
                                                             (double)affineRotationPointY.Value);
            }

            // Поворот вокруг своего центра
            if (checkBoxRotationCenter.Checked)
            {
                AffineTransformations.TransformRotationCenter(ref polygon,
                                                             (double)affineRotationCenterAngle.Value);
            }

            // Масштабирование относительно заданной пользователем точки
            if (checkBoxScalePoint.Checked)
            {
                AffineTransformations.TransformScalePoint(ref polygon,
                                                          (double)affineScalePoint.Value,
                                                          (double)affineScalePointX.Value,
                                                          (double)affineScalePointY.Value);
            }

            // Масштабирование относительно своего центра
            if (checkBoxScaleCenter.Checked)
            {
                AffineTransformations.TransformScaleCenter(ref polygon,
                                                          (double)affineScaleCenter.Value);
            }

            RedrawField();
            ShowSelectedItem(p_red);
        }

        private void buttonSetRotationPoint_Click(object sender, EventArgs e)
        {
            cur_mode = taskType.SET_ROTATION_POINT;
            pictureBox1.Cursor = Cursors.Cross;
        }

        private void buttonSetScalePoint_Click(object sender, EventArgs e)
        {
            cur_mode = taskType.SET_SCALE_POINT;
            pictureBox1.Cursor = Cursors.Cross;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            treeView1.Nodes.Clear();
            polygons.Clear();
            polygon_drawing = false;

            selectedItemType = selectType.NONE;
            selectedItemPath = "";

            label13.Text = "";
            cur_mode = taskType.DRAWING;
        }

    }
}
