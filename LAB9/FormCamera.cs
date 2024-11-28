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

        internal class Camera
        {
            public Vector3 cameraPos;

            public Vector3 cameraDir;
            public Vector3 cameraUp;
            public Vector3 cameraRight;

            const double cameraRotationSpeed = 1;
            double yaw = 0.0, pitch = 0.0;
            double zScreenNear = 1;
            double zScreenFar = 100;
            double fov = 45;
            PointF worldCenter;
            int screenWidth, screenHeight;
            double[,] parallelProjectionMatrix, perspectiveProjectionMatrix;

            public Camera(int screenWidth, int screenHeight)
            {
                cameraPos = new Vector3(-200, 0, 0);
                cameraDir = new Vector3(1, 0, 0);
                cameraUp = new Vector3(0, 0, 1);
                cameraRight = (cameraDir * cameraUp).Normalize();
                this.screenHeight = screenHeight;
                this.screenWidth = screenWidth;
                worldCenter = new PointF(screenWidth / 2, screenHeight / 2);
                UpdateProjMatrix();
            }

            public void UpdateProjMatrix()
            {
                parallelProjectionMatrix = new double[,] {
                    { 1.0 / screenWidth, 0,                       0,                                 0},
                    { 0,                      1.0 / screenHeight, 0,                                 0},
                    { 0,                      0,                       -2.0 / (zScreenFar - zScreenNear), -(zScreenFar + zScreenNear) / (zScreenFar - zScreenNear)},
                    { 0,                      0,                        0,                                 1}
                };

                perspectiveProjectionMatrix = new double[,] {
                    { screenHeight / (Math.Tan(AffineTransformations.DegreesToRadians(fov / 2)) * screenWidth), 0, 0, 0},
                    { 0, 1.0 / Math.Tan(AffineTransformations.DegreesToRadians(fov / 2)), 0, 0},
                    { 0, 0, -(zScreenFar + zScreenNear) / (zScreenFar - zScreenNear), -2 * (zScreenFar * zScreenNear) / (zScreenFar - zScreenNear)},
                    { 0, 0, -1, 0}
                };
            }

            public void cameraMove(double leftright = 0, double forwardbackward = 0, double updown = 0)
            {
                cameraPos.x += leftright * cameraRight.x + forwardbackward * cameraDir.x + updown * cameraUp.x;
                cameraPos.y += leftright * cameraRight.y + forwardbackward * cameraDir.y + updown * cameraUp.y;
                cameraPos.z += leftright * cameraRight.z + forwardbackward * cameraDir.z + updown * cameraUp.z;
            }
            public void cameraRotate(double shiftX = 0, double shiftY = 0)
            {
                var newPitch = Clamp(pitch + shiftY * cameraRotationSpeed, -89.0, 89.0);
                var newYaw = (yaw + shiftX) % 360;
                if (newPitch != pitch)
                {
                    AffineTransformations.rotateVectors(ref cameraDir, ref cameraUp, (newPitch - pitch), cameraRight);
                    pitch = newPitch;
                }
                if (newYaw != yaw)
                {
                    AffineTransformations.rotateVectors(ref cameraDir, ref cameraRight, (newYaw - yaw), cameraUp);
                    yaw = newYaw;
                }
            }

            public Vertex toCameraView(Vertex v)
            {
                return new Vertex(cameraRight.x * (v.x - cameraPos.x) + cameraRight.y * (v.y - cameraPos.y) + cameraRight.z * (v.z - cameraPos.z),
                                  cameraUp.x * (v.x - cameraPos.x) + cameraUp.y * (v.y - cameraPos.y) + cameraUp.z * (v.z - cameraPos.z),
                                  cameraDir.x * (v.x - cameraPos.x) + cameraDir.y * (v.y - cameraPos.y) + cameraDir.z * (v.z - cameraPos.z)
                                  );
            }

            
            internal Vertex to2D(Vertex v)
            {
                var viewCoord = this.toCameraView(v);
                if (viewCoord.z < 0)
                {
                    return null;
                }

                var res = AffineTransformations.Multiply(new double[,] { { viewCoord.x, viewCoord.y, viewCoord.z, 1 } }, perspectiveProjectionMatrix);
                if (res[0, 3] == 0)
                {
                    return null;

                }

                var elem = 1.0 / res[0, 3];
                for (int i = 0; i < res.GetLength(0); i++)
                {
                    for (int j = 0; j < res.GetLength(1); j++)
                    {
                        res[i, j] *= elem;
                    }
                }

                res[0, 0] = Camera.Clamp(res[0, 0], -1, 1);
                res[0, 1] = Camera.Clamp(res[0, 1], -1, 1);

                if (res[0, 2] < 0)
                {
                    return null;
                }
                return new Vertex(worldCenter.X + res[0, 0] * worldCenter.X, worldCenter.Y + res[0, 1] * worldCenter.Y, (float)v.z);

            }

            //x = (x < a) ? a : ((x > b) ? b : x);
            public static double Clamp(double val, double min, double max) 
            {
                if (val.CompareTo(min) < 0) return min;
                else if (val.CompareTo(max) > 0) return max;
                else return val;
            }
        }

        public void RedrawCamryField()
        {
            g2.Clear(Color.White);
            Vertex line_start, line_end;
            foreach (var obj in objects_list.Items)
            {
                var cur_poly = obj as Polyhedron;
                for (int i = 0; i < cur_poly.vertices.Count; i++)
                {
                    
                    line_start = camera.to2D(cur_poly.vertices[i]);
                    //line_start = new Vertex(cur_m[0, 0], cur_m[0, 1], 0);

                    //пробегает по всем граничным точкам и рисует линию
                    for (int j = 0; j < cur_poly.edges[i].Count; j++)
                    {
                        var ind = cur_poly.edges[i][j];
                        
                        line_end = camera.to2D(cur_poly.vertices[ind]);
                        //if (null != line_start && null != line_end)
                        if (!(line_start is null || line_end is null))
                            g2.DrawLine(pen, (float)line_start.x, (float)line_start.y, (float)line_end.x, (float)line_end.y);
                    }
                }
            }
        }

        


        private void CameraUpButton_Click(object sender, EventArgs e)
        {
            camera.cameraMove(updown: 15);
            RedrawCamryField();
        }

        private void CameraDownButton_Click(object sender, EventArgs e)
        {
            camera.cameraMove(updown: -15);
            RedrawCamryField();
        }

        private void CameraLeftButton_Click(object sender, EventArgs e)
        {
            camera.cameraMove(leftright: 15);
            RedrawCamryField();
        }

        private void CameraRightButton_Click(object sender, EventArgs e)
        {
            camera.cameraMove(leftright: -15);
            RedrawCamryField();
        }

        private void CameraLeftRotateButton_Click(object sender, EventArgs e)
        {
            camera.cameraRotate(shiftX: 10);
            RedrawCamryField();
        }

        private void CameraRightRotateButton_Click(object sender, EventArgs e)
        {
            camera.cameraRotate(shiftX: 10);
            RedrawCamryField();
        }

        private void CameraUpRotateButton_Click(object sender, EventArgs e)
        {
            camera.cameraRotate(shiftY: -10);
            RedrawCamryField();
        }

        private void CameraDownRotateButton_Click(object sender, EventArgs e)
        {
            camera.cameraRotate(shiftY: -10);
            RedrawCamryField();
        }

        private void CameraPlusButton_Click(object sender, EventArgs e)
        {
            camera.cameraMove(forwardbackward: 15);
            RedrawCamryField();
        }

        private void CameraMinusButton_Click(object sender, EventArgs e)
        {
            camera.cameraMove(forwardbackward: -15);
            RedrawCamryField();
        }

    }


    
}
