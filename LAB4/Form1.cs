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

namespace LAB4
{
    public partial class Form1 : Form
    {
        enum selectType { NONE, VERTEX, EDGE, POLY };
        bool polygon_drawing;
        List<Polygon> polygons;
        Graphics g;
        Pen p_black, p_red;

        selectType selectedItemType;
        string selectedItemPath;

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
        }

        

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
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
                if (!polygon_drawing)
                {
                    polygon_drawing = true;
                    polygons.Add(new Polygon(e.Location));
                    treeView1.Nodes.Add(new TreeNode("Polygon " + polygons.Count.ToString())); ;
                }
                else
                {
                    polygons.Last().AddVertex(e.Location);

                    TreeNode new_edge = new TreeNode("Edge " + (treeView1.Nodes[polygons.Count - 1].Nodes.Count+1).ToString());
                    new_edge.Nodes.Add(new TreeNode("Vertex " + (polygons.Last().vertices_count - 1).ToString()));
                    new_edge.Nodes.Add(new TreeNode("Vertex " + (polygons.Last().vertices_count).ToString()));

                    treeView1.Nodes[polygons.Count - 1].Nodes.Add(new_edge);
                    
                }
                RedrawField();
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
                    Point pnt1 = poly.vertices[int.Parse(selectedItemPath.Split(' ')[3]) - 1];
                    Point pnt2 = poly.vertices[int.Parse(selectedItemPath.Split(' ')[3])];
                    g.DrawLine(p, pnt1, pnt2);
                    break;
                case selectType.VERTEX:
                    poly = polygons[int.Parse(selectedItemPath.Split(' ')[1]) - 1];
                    Point pnt = poly.vertices[int.Parse(selectedItemPath.Split(' ')[3]) - 1];
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

        private void buttonClear_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            treeView1.Nodes.Clear();
            polygons.Clear();
            polygon_drawing = false;
        }

    }
}
