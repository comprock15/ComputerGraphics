using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB9
{ 
    public partial class Form1 : Form
    {
        
        internal class Cam
        {
            // положение камеры в мировой систем е координат
            public Vertex c;
            // вертикальная ориентация камеры (вектор туда где у камеры верх)
            public Vertex u;
            // горизонтальная ориентация камеры
            public Vertex v;
            // куда смотрит камера 
            public Vertex n;


            int W; int H;

            // расстояние до передней плоскости отсечения
            int zn; 
            // расстояние до задней плоскости отсечения
            int zf;

            public Cam(Vertex c, Vertex n, int pbW, int pbH)
            {
                this.c = c;

                this.n = n;
                v = RotateToAngleByX(n, 90);
                u = RotateToAngleByY(n, 90);

                W = pbW; H = pbH;

                //zn = Math.Max(W / 2, H / 2);
                //zf = zn + 1000;

                zn = 1;
                zf = 10;
            }

            public double[,] GetViewMatrix()
            {
                var uc = Vertex.Dot(u, c);
                var vc = Vertex.Dot(v, c);
                var nc = Vertex.Dot(n, c);
                return new double[,] {
                    { u.x, v.x, n.x, 0 },
                    { u.y, v.y, n.y, 0 },
                    { u.z, v.z, n.z, 0 },
                    { -uc, -vc, -nc, 1 }
                };
            }


            public double[,] GetProjectionMatrix()
            {
                var w = (W / 2.0) / zn;
                var h = (H / 2.0) / zn;
                //return new double[,] {
                //    { w, 0, 0,                0 },
                //    { 0, h, 0,                0 },
                //    { 0, 0, zf/(zf-zn),       1 },
                //    { 0, 0, (-zf*zn)/(zf-zn), 0 }
                //};

                //return new double[,] {
                //    { 2.0/W, 0, 0, 0 },
                //    { 0, 2.0/H, 0,0 },
                //    { 0, 0, 1.0/ (zf - zn), 0},
                //    { 0, 0, (double)zn / (zf - zn), 1}
                //};
                //return new double[,] {
                //    { W/2.0 , 0, 0, 0 },
                //    { 0, H/2.0, 0,0 },
                //    { 0, 0, 1.0/ (zf - zn), 0},
                //    { 0, 0, (double)zn / (zf - zn), 1}
                //};

                return new double[,] {
                    { w, 0, 0,                0 },
                    { 0, h, 0,                0 },
                    { 0, 0, zf/(zf-zn),       1 },
                    { 0, 0, (-zf*zn)/(zf-zn), 0 }
                };
            }

            public static Vertex RotateToAngleByX(Vertex v, double angle)
            {
                angle = AffineTransformations.DegreesToRadians(angle);

                // Задаём матрицу преобразования
                double[,] matrix = new double[4, 4] {
                    { 1,       0,               0,          0 },
                    { 0,  Math.Cos(angle), Math.Sin(angle), 0 },
                    { 0, -Math.Sin(angle), Math.Cos(angle), 0 },
                    { 0,       0,               0,          1 }
                };

                double[,] oldCoords = new double[,] { { v.x, v.y, v.z, 1 } };
                double[,] newCoords = AffineTransformations.Multiply(oldCoords, matrix);
                return new Vertex(newCoords[0, 0] / newCoords[0, 3], newCoords[0, 1] / newCoords[0, 3], newCoords[0, 2] / newCoords[0, 3]);

            }

            public static Vertex RotateToAngleByY(Vertex v, double angle) 
            {
                angle = AffineTransformations.DegreesToRadians(angle);
                // Задаём матрицу преобразования
                double[,] matrix = new double[4, 4] {
                    { Math.Cos(angle), 0, -Math.Sin(angle), 0 },
                    {      0,          1,       0,          0 },
                    { Math.Sin(angle), 0,  Math.Cos(angle), 0 },
                    {      0,          0,       0,          1 }
                };
                double[,] oldCoords = new double[,] { { v.x, v.y, v.z, 1 } };
                double[,] newCoords = AffineTransformations.Multiply(oldCoords, matrix);
                return new Vertex(newCoords[0, 0] / newCoords[0, 3], newCoords[0, 1] / newCoords[0, 3], newCoords[0, 2] / newCoords[0, 3]);

            }




        }

        Cam camr = new Cam(new Vertex(10, 10, 100), new Vertex(-1, -1, 0), 591, 387);

        public void CameraDraw()
        {
            var p = camr.GetProjectionMatrix();
            var v = camr.GetViewMatrix();
            if (pictureBox1.Image != null)
                pictureBox1.Image.Dispose();
            //var matrix = AffineTransformations.Multiply(v, p);
            var matrix = AffineTransformations.Multiply(p, v);
            CamDrawEdges(matrix);
        }


        private void CameraUpButton_Click(object sender, EventArgs e)
        {
            //camr.c = camr.c + camr.u;
            //CameraDraw();

            camry.move(updown: 15);
            RedrawCamryField();
        }

        private void CameraDownButton_Click(object sender, EventArgs e)
        {
            //camr.c = camr.c - camr.u;
            //CameraDraw();

            camry.move(updown: -15);
            RedrawCamryField();
        }

        private void CameraLeftButton_Click(object sender, EventArgs e)
        {
            //camr.c = camr.c - camr.v;
            //CameraDraw();

            camry.move(leftright: 15);
            RedrawCamryField();
        }

        private void CameraRightButton_Click(object sender, EventArgs e)
        {
            //camr.c = camr.c + camr.v;
            //CameraDraw();
            camry.move(leftright: -15);
            RedrawCamryField();
        }

        private void CameraLeftRotateButton_Click(object sender, EventArgs e)
        {
            //camr.c = Cam.RotateToAngleByX(camr.c, 10); 
            //camr.n = Cam.RotateToAngleByX(camr.n, 10);
            //camr.v = Cam.RotateToAngleByX(camr.n, 90);
            //camr.u = Cam.RotateToAngleByY(camr.n, 90);
            //CameraDraw();
            camry.changeView(shiftY: 10);
            RedrawCamryField();
        }

        private void CameraRightRotateButton_Click(object sender, EventArgs e)
        {
            //camr.c = Cam.RotateToAngleByX(camr.c, -10);
            //camr.n = Cam.RotateToAngleByX(camr.n, -10);
            //camr.v = Cam.RotateToAngleByX(camr.n, 90);
            //camr.u = Cam.RotateToAngleByY(camr.n, 90);
            //CameraDraw();

            camry.changeView(shiftX: 10);
            RedrawCamryField();
        }

        private void CameraPlusButton_Click(object sender, EventArgs e)
        {
            //camr.c = camr.c + camr.n;
            //CameraDraw(); 

            camry.move(forwardbackward: 15);
            RedrawCamryField();
        }

        private void CameraMinusButton_Click(object sender, EventArgs e)
        {
            //camr.c = camr.c - camr.n;
            //CameraDraw();

            camry.move(forwardbackward: -15);
            RedrawCamryField();
        }

        internal void CamDrawEdges(double[,] matrix)
        {
            double[,] cur_m;
            var pm = camr.GetProjectionMatrix();
            var vm = camr.GetViewMatrix();
            Vertex line_start;
            Vertex line_end;
            //g2.Dispose();
            g2.Clear(Color.White);
            foreach (var obj in objects_list.Items)
            {
                var cur_poly = obj as Polyhedron;
                for (int i = 0; i < cur_poly.vertices.Count; i++)
                {
                    //cur_m = AffineTransformations.Multiply(new double[,] {{ cur_poly.vertices[i].x,
                    //                                                                cur_poly.vertices[i].y,
                    //                                                                cur_poly.vertices[i].z,
                    //                                                                1 }}, matrix);
                    cur_m = AffineTransformations.Multiply(new double[,] {{ cur_poly.vertices[i].x,
                                                                                    cur_poly.vertices[i].y,
                                                                                    cur_poly.vertices[i].z,
                                                                                    1 }}, vm);
                    cur_m = AffineTransformations.Multiply(new double[,] {{ cur_m[0,0],
                                                                                    cur_m[0,1],
                                                                                    cur_m[0,2],
                                                                                    cur_m[0,3] }}, pm);
                    line_start = new Vertex(cur_m[0,0]/ cur_m[0,3], cur_m[0, 1] / cur_m[0, 3], 0);
                    //line_start = new Vertex(cur_m[0, 0], cur_m[0, 1], 0);

                    //пробегает по всем граничным точкам и рисует линию
                    for (int j = 0; j < cur_poly.edges[i].Count; j++)
                    {
                        var ind = cur_poly.edges[i][j];
                        //cur_m = AffineTransformations.Multiply(new double[,] {{ cur_poly.vertices[ind].x,
                        //                                                        cur_poly.vertices[ind].y,
                        //                                                        cur_poly.vertices[ind].z,
                        //                                                        1 }}, matrix);


                        cur_m = AffineTransformations.Multiply(new double[,] {{ cur_poly.vertices[ind].x,
                                                                                    cur_poly.vertices[ind].y,
                                                                                    cur_poly.vertices[ind].z,
                                                                                    1 }}, vm);
                        cur_m = AffineTransformations.Multiply(new double[,] {{ cur_m[0,0],
                                                                                    cur_m[0,1],
                                                                                    cur_m[0,2],
                                                                                    cur_m[0,3] }}, pm);
                        line_end = new Vertex(cur_m[0, 0] / cur_m[0, 3], cur_m[0, 1] / cur_m[0, 3], 0);
                        //line_end = new Vertex(cur_m[0, 0], cur_m[0, 1], 0);
                        
                            g2.DrawLine(p, (float)line_start.x, (float)line_start.y, (float)line_end.x, (float)line_end.y);
                    }
                }
            }
        }
    }
}
