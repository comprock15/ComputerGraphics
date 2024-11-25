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
    internal partial class CameraForm : Form
    {
        Camera camera;
        List<Polyhedron> polyhedrons = new List<Polyhedron>();
        ListBox.ObjectCollection objectCollection;
        Graphics g;
        Pen pen = new Pen(Color.Black);
        int sleeptime = 200;

        public CameraForm(ListBox.ObjectCollection objects_list)
        {
            InitializeComponent();
            objectCollection = objects_list;

            foreach (Polyhedron polyhedron in objects_list)
            {
                polyhedrons.Add(polyhedron);
                var c = AffineTransformations.CalculateCenterCoords(polyhedron);
            }

            camera = new Camera(new Vector3(0, 0, -100),
                                new Vector3(0, 0, 0));

            g = pictureBox1.CreateGraphics();
            RedrawField();
        }

        public void RedrawField()
        {
            
            List<Polyhedron> newPolyhedrons = camera.GetPolyhedronsInCameraCoordinates(polyhedrons);

            //List<Polyhedron> newPolyhedrons = polyhedrons.Select(p => new Polyhedron(p)).ToList();

            double[,] projectionMatrix = new double[4, 4] {
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 0, -1/1000 },
                { pictureBox1.Width / 2, pictureBox1.Height / 2, 0, 1 }
            };

            ListBox objects_list = new ListBox();
            objects_list.Items.AddRange(newPolyhedrons.ToArray());
                var bmp = ZBuffer.ZBuff(projectionMatrix, objects_list.Items, pictureBox1.Width, pictureBox1.Height);
            //g.Clear(Color.White);
            if (pictureBox1.Image != null)
                pictureBox1.Image.Dispose();
            pictureBox1.Image = bmp;

            //foreach (Polyhedron polyhedron in newPolyhedrons)
            //{
            //    //foreach (Vertex v in polyhedron.vertices)
            //    //{

            //    //    var coords = AffineTransformations.Multiply(new double[1, 4] { { v.x, v.y, v.z, 1 } }, projectionMatrix);
            //    //    v.x = coords[0, 0] / coords[0, 3];
            //    //    v.y = coords[0, 1] / coords[0, 3];
            //    //    v.z = coords[0, 2] / coords[0, 3];
            //    //    //v.x = coords[0, 0];
            //    //    //v.y = coords[0, 1];
            //    //    //v.z = coords[0, 2];
            //    //}
            //    //Polyhedron polyhedron = p;
            //    //polyhedron.vertices = AffineTransformations.RecalculatedCoords(p, mvp);


            //    foreach (List<int> face in polyhedron.faces)
            //    {
            //        g.DrawLines(pen, face.Select(i => new PointF(
            //            (float)(polyhedron.vertices[i].x),
            //            (float)(polyhedron.vertices[i].y))).ToArray());
            //        g.DrawLine(pen, (float)polyhedron.vertices[face[0]].x,
            //                        (float)polyhedron.vertices[face[0]].y,
            //                        (float)polyhedron.vertices[face[face.Count - 1]].x,
            //                        (float)polyhedron.vertices[face[face.Count - 1]].y);
            //    }

            //    this.Text = polyhedron.vertices[0].x + " " + polyhedron.vertices[0].y + " " + polyhedron.vertices[0].z;
            //}


            // РИСОВАНИЕ ОСЕЙ КООРДИНАТ

            //Polyhedron ox = new Polyhedron();
            //ox.vertices.Add(new Vertex(100, 0, 0));
            //ox.vertices.Add(new Vertex(0, 0, 0));
            ////ox.vertices = AffineTransformations.RecalculatedCoords(ox, mvp);
            //ox = camera.GetPolyhedronsInCameraCoordinates(new List<Polyhedron> { ox })[0];
            //ox.vertices = AffineTransformations.RecalculatedCoords(ox, projectionMatrix);
            //g.DrawLine(new Pen(Color.Red), (float)ox.vertices[0].x, (float)ox.vertices[0].y, (float)ox.vertices[1].x, (float)ox.vertices[1].y);

            //Polyhedron oy = new Polyhedron();
            //oy.vertices.Add(new Vertex(0, 100, 0));
            //oy.vertices.Add(new Vertex(0, 0, 0));
            ////oy.vertices = AffineTransformations.RecalculatedCoords(oy, mvp);
            //oy = camera.GetPolyhedronsInCameraCoordinates(new List<Polyhedron> { oy })[0];
            //oy.vertices = AffineTransformations.RecalculatedCoords(oy, projectionMatrix);
            //g.DrawLine(new Pen(Color.Green), (float)oy.vertices[0].x, (float)oy.vertices[0].y, (float)oy.vertices[1].x, (float)oy.vertices[1].y);

            //Polyhedron oz = new Polyhedron();
            //oz.vertices.Add(new Vertex(0, 0, 100));
            //oz.vertices.Add(new Vertex(0, 0, 0));
            ////oz.vertices = AffineTransformations.RecalculatedCoords(oz, mvp);
            //oz = camera.GetPolyhedronsInCameraCoordinates(new List<Polyhedron> { oz })[0];
            //oz.vertices = AffineTransformations.RecalculatedCoords(oz, projectionMatrix);
            //g.DrawLine(new Pen(Color.Blue), (float)oz.vertices[0].x, (float)oz.vertices[0].y, (float)oz.vertices[1].x, (float)oz.vertices[1].y);

            label4.Text = $"Позиция камеры: ({camera.position.x}, {camera.position.y}, {camera.position.z})";
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            RedrawField();
        }

        private void RotateCamera(double[,] matrix)
        {
            double[,] coord = new double[,] { { camera.position.x, camera.position.y, camera.position.z, 1 } };
            coord = AffineTransformations.Multiply(coord, matrix);

            //camera.position = new Vector3(coord[0, 0] / coord[0, 3], coord[0, 1] / coord[0, 3], coord[0, 2] / coord[0, 3]);
            camera.position = new Vector3(coord[0, 0], coord[0, 1], coord[0, 2]);
        }

        private void RotateCameraAboutYAxis(object sender, EventArgs e)
        {
            double angle = AffineTransformations.DegreesToRadians(5);

            double[,] matrix = new double[4, 4] {
                { Math.Cos(angle), 0, -Math.Sin(angle), 0 },
                {      0,          1,       0,          0 },
                { Math.Sin(angle), 0,  Math.Cos(angle), 0 },
                {      0,          0,       0,          1 }
            };

            RotateCamera(matrix);

            if (sender != null)
                RedrawField();
            System.Threading.Thread.Sleep(sleeptime);
        }

        private void RotateCameraAboutZAxis(object sender, EventArgs e)
        {
            double angle = AffineTransformations.DegreesToRadians(5);

            double[,] matrix = new double[4, 4] {
                { Math.Cos(angle), Math.Sin(angle), 0, 0 },
                {-Math.Sin(angle), Math.Cos(angle), 0, 0 },
                {      0,               0,          1, 0 },
                {      0,               0,          0, 1 }
            };

            RotateCamera(matrix);

            if (sender != null)
                RedrawField();
            System.Threading.Thread.Sleep(sleeptime);
        }

        private void RotateCameraAboutXAxis(object sender, EventArgs e)
        {
            double angle = AffineTransformations.DegreesToRadians(5);

            double[,] matrix = new double[4, 4] {
                { 1,       0,               0,          0 },
                { 0,  Math.Cos(angle), Math.Sin(angle), 0 },
                { 0, -Math.Sin(angle), Math.Cos(angle), 0 },
                { 0,       0,               0,          1 }
            };

            RotateCamera(matrix);

            if (sender != null)
                RedrawField();
            System.Threading.Thread.Sleep(sleeptime);
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            if (g != null)
                g.Dispose();
            g = pictureBox1.CreateGraphics();
        }


        private async void AsyncRotateX()
        {
            while (radioButton2.Checked)
            {
                await Task.Run(() => RotateCameraAboutXAxis(null, null));
                RedrawField();
            }
        }

        private async void AsyncRotateY()
        {
            while (radioButton3.Checked)
            {
                await Task.Run(() => RotateCameraAboutYAxis(null, null));
                RedrawField();
            }
        }

        private async void AsyncRotateZ()
        {
            while (radioButton4.Checked)
            {
                await Task.Run(() => RotateCameraAboutZAxis(null, null));
                RedrawField();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            camera.Move((double)numericUpDown1.Value, (double)numericUpDown2.Value, (double)numericUpDown3.Value);
            RedrawField();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // do nothing
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            AsyncRotateX();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            AsyncRotateY();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            AsyncRotateZ();
        }
    }
}
