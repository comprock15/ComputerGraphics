using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB7
{
    internal partial class CameraForm : Form
    {
        Camera camera;
        List<Polyhedron> polyhedrons = new List<Polyhedron>();
        ListBox.ObjectCollection objectCollection;
        Graphics g;
        Pen pen = new Pen(Color.Black);

        public CameraForm(ListBox.ObjectCollection objects_list)
        {
            InitializeComponent();
            objectCollection = objects_list;

            foreach (Polyhedron polyhedron in objects_list)
            {
                polyhedrons.Add(polyhedron);
                var c = AffineTransformations.CalculateCenterCoords(polyhedron);
            }

            camera = new Camera(new Camera.Vector3(pictureBox1.Width / 2, pictureBox1.Height / 2, 500),
                                new Camera.Vector3(0, 0, 0));

            g = pictureBox1.CreateGraphics();
            RedrawField();
        }

        public void RedrawField()
        {
            g.Clear(Color.White);
            List<Polyhedron> newPolyhedrons = camera.GetPolyhedronsInCameraCoordinates(polyhedrons);

            double[,] projectionMatrix = new double[4, 4] {
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 0, -1/1000 },
                { pictureBox1.Width / 2, pictureBox1.Height / 2, 0, 1 }
            };

            foreach (Polyhedron polyhedron in newPolyhedrons)
            {
                foreach (Vertex v in polyhedron.vertices)
                {
                    var coords = AffineTransformations.Multiply(new double[1, 4] { { v.x, v.y, v.z, 1 } }, projectionMatrix);
                    //v.x = coords[0, 0] / coords[0, 3];
                    //v.y = coords[0, 1] / coords[0, 3];
                    //v.z = coords[0, 2];
                    v.x = coords[0, 0];
                    v.y = coords[0, 1];
                    v.z = coords[0, 2];
                }

                foreach (List<int> face in polyhedron.faces)
                {
                    g.DrawLines(pen, face.Select(i => new PointF(
                        (float)(polyhedron.vertices[i].x), 
                        (float)(polyhedron.vertices[i].y))).ToArray());
                    g.DrawLine(pen, (float)polyhedron.vertices[face[0]].x,
                                    (float)polyhedron.vertices[face[0]].y,
                                    (float)polyhedron.vertices[face[face.Count - 1]].x,
                                    (float)polyhedron.vertices[face[face.Count - 1]].y);
                }
            }

            //float sinz = 10*(float)Math.Sin(camera.rotation.z);
            //float cosz = 10*(float)Math.Cos(camera.rotation.z);
            //g.DrawLine(new Pen(Color.Red), pictureBox1.Width / 2 - cosz, pictureBox1.Height / 2 - sinz, pictureBox1.Width / 2 + cosz, pictureBox1.Height / 2 + sinz);

            //sinz = 10 * (float)Math.Sin(camera.rotation.z + Math.PI / 2);
            //cosz = 10 * (float)Math.Cos(camera.rotation.z + Math.PI / 2);
            //g.DrawLine(new Pen(Color.Blue), pictureBox1.Width / 2 - cosz, pictureBox1.Height / 2 - sinz, pictureBox1.Width / 2, pictureBox1.Height / 2);

            label4.Text = $"Позиция камеры: ({camera.position.x}, {camera.position.y}, {camera.position.z})";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RedrawField();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //camera.rotation.x += AffineTransformations.DegreesToRadians(2);
            camera.Rotate(2, 0);
            RedrawField();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //camera.rotation.y += AffineTransformations.DegreesToRadians(2);
            camera.Rotate(0, 2);
            RedrawField();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //camera.rotation.z += AffineTransformations.DegreesToRadians(5);
            camera.Rotate(AffineTransformations.DegreesToRadians(2), 0);
            if (sender != null)
                RedrawField();
            System.Threading.Thread.Sleep(100);
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            if (g != null)
                g.Dispose();
            g = pictureBox1.CreateGraphics();
        }


        private async void MoveIt()
        {
            while (checkBox1.Checked)
            {
                await Task.Run(() => button3_Click(null, null));
                RedrawField();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            MoveIt();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            camera.position.x += (double)numericUpDown1.Value;
            camera.position.y += (double)numericUpDown2.Value;
            camera.position.z += (double)numericUpDown3.Value;
            RedrawField();
        }
    }
}
