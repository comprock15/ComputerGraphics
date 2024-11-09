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

        public CameraForm(ListBox.ObjectCollection objects_list, Camera.Point3 position, Camera.Point3 direction)
        {
            InitializeComponent();
            objectCollection = objects_list;

            foreach (Polyhedron polyhedron in objects_list)
                polyhedrons.Add(polyhedron);

            camera = new Camera(position, direction);

            g = pictureBox1.CreateGraphics();
            RedrawField();
        }

        public void RedrawField()
        {
            g.Clear(Color.White);
            (List<Polyhedron> newPolyhedrons, string s) = camera.GetPolyhedronsInCameraCoordinates(polyhedrons);
            this.Text = $"cam: ({camera.position.x},{camera.position.y},{camera.position.z}) " + s;
            foreach (Polyhedron polyhedron in newPolyhedrons)
            {
                foreach (List<int> face in polyhedron.faces)
                {
                    g.DrawLines(pen, face.Select(i => new PointF((float)polyhedron.vertices[i].x, (float)polyhedron.vertices[i].y)).ToArray());
                    g.DrawLine(pen, (float)polyhedron.vertices[face[0]].x,
                                    (float)polyhedron.vertices[face[0]].y,
                                    (float)polyhedron.vertices[face[face.Count - 1]].x,
                                    (float)polyhedron.vertices[face[face.Count - 1]].y);
                }
                var center = AffineTransformations.CalculateCenterCoords(polyhedron);
                this.Text += $" p({center[0,0]},{center[0, 1]},{center[0, 2]})";
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            RedrawField();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Polyhedron cam = new Polyhedron(new List<Vertex>(), new List<List<int>>(), new List<List<int>>());
            cam.vertices.Add(new Vertex(camera.position.x, camera.position.y, camera.position.z));
            cam.vertices.Add(new Vertex(camera.direction.x, camera.direction.y, camera.direction.z));
            AffineTransformations.RotateAroundCenter(ref cam, 1, 0);
            camera.position = new Camera.Point3(cam.vertices[0].x, cam.vertices[0].y, cam.vertices[0].z);
            camera.direction = new Camera.Point3(cam.vertices[1].x, cam.vertices[1].y, cam.vertices[1].z);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Polyhedron cam = new Polyhedron(new List<Vertex>(), new List<List<int>>(), new List<List<int>>());
            cam.vertices.Add(new Vertex(camera.position.x, camera.position.y, camera.position.z));
            cam.vertices.Add(new Vertex(camera.direction.x, camera.direction.y, camera.direction.z));
            AffineTransformations.RotateAroundCenter(ref cam, 1, 1);
            camera.position = new Camera.Point3(cam.vertices[0].x, cam.vertices[0].y, cam.vertices[0].z);
            camera.direction = new Camera.Point3(cam.vertices[1].x, cam.vertices[1].y, cam.vertices[1].z);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Polyhedron cam = new Polyhedron(new List<Vertex>(), new List<List<int>>(), new List<List<int>>());
            cam.vertices.Add(new Vertex(camera.position.x, camera.position.y, camera.position.z));
            cam.vertices.Add(new Vertex(camera.direction.x, camera.direction.y, camera.direction.z));
            AffineTransformations.RotateAroundCenter(ref cam, 1, 2);
            camera.position = new Camera.Point3(cam.vertices[0].x, cam.vertices[0].y, cam.vertices[0].z);
            camera.direction = new Camera.Point3(cam.vertices[1].x, cam.vertices[1].y, cam.vertices[1].z);
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            if (g != null)
                g.Dispose();
            g = pictureBox1.CreateGraphics();
        }
    }
}
