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

        internal class Camry
        {
            public Vector3 cameraPosition;
            public Vector3 cameraDirection;
            public Vector3 cameraUp;
            public Vector3 cameraRight;
            const double cameraRotationSpeed = 1;
            double yaw = 0.0, pitch = 0.0;

            public Camry()
            {
                cameraPosition = new Vector3(-200, 0, 0);
                cameraDirection = new Vector3(1, 0, 0);
                cameraUp = new Vector3(0, 0, 1);
                cameraRight = (cameraDirection * cameraUp).Normalize();
            }

            public void move(double leftright = 0, double forwardbackward = 0, double updown = 0)
            {
                cameraPosition.x += leftright * cameraRight.x + forwardbackward * cameraDirection.x + updown * cameraUp.x;
                cameraPosition.y += leftright * cameraRight.y + forwardbackward * cameraDirection.y + updown * cameraUp.y;
                cameraPosition.z += leftright * cameraRight.z + forwardbackward * cameraDirection.z + updown * cameraUp.z;
            }

            public Vertex toCameraView(Vertex v)
            {
                return new Vertex(cameraRight.x * (v.x - cameraPosition.x) + cameraRight.y * (v.y - cameraPosition.y) + cameraRight.z * (v.z - cameraPosition.z),
                                 cameraUp.x * (v.x - cameraPosition.x) + cameraUp.y * (v.y - cameraPosition.y) + cameraUp.z * (v.z - cameraPosition.z),
                                 cameraDirection.x * (v.x - cameraPosition.x) + cameraDirection.y * (v.y - cameraPosition.y) + cameraDirection.z * (v.z - cameraPosition.z));
            }

            public void changeView(double shiftX = 0, double shiftY = 0)
            {
                var newPitch =  Clamp(pitch + shiftY * cameraRotationSpeed, -89.0, 89.0);
                var newYaw = (yaw + shiftX) % 360;
                if (newPitch != pitch)
                {
                    AffineTransformations.rotateVectors(ref cameraDirection, ref cameraUp, (newPitch - pitch), cameraRight);
                    pitch = newPitch;
                }
                if (newYaw != yaw)
                {
                    AffineTransformations.rotateVectors(ref cameraDirection, ref cameraRight, (newYaw - yaw), cameraUp);
                    yaw = newYaw;
                }
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
                    
                    line_start = to2D(camry, cur_poly.vertices[i]);
                    //line_start = new Vertex(cur_m[0, 0], cur_m[0, 1], 0);

                    //пробегает по всем граничным точкам и рисует линию
                    for (int j = 0; j < cur_poly.edges[i].Count; j++)
                    {
                        var ind = cur_poly.edges[i][j];
                        
                        line_end = to2D(camry, cur_poly.vertices[ind]);
                        //if (null != line_start && null != line_end)
                        if (!(line_start is null || line_end is null))
                            g2.DrawLine(p, (float)line_start.x, (float)line_start.y, (float)line_end.x, (float)line_end.y);
                    }
                }
            }
        }

        internal Vertex to2D(Camry cam, Vertex v)
        {
            var viewCoord = cam.toCameraView(v);
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
            for (int i = 0; i < res.GetLength(0); i++) {
                for (int j = 0; j < res.GetLength(1); j++)
                {
                    res[i, j] *= elem;
                }
            }
            
            res[0, 0] = Camry.Clamp(res[0, 0], -1, 1);
            res[0, 1] = Camry.Clamp(res[0, 1], -1, 1);
            //res[0, 2] = Math.Clamp(res[0, 2], -1, 1);
            if (res[0, 2] < 0)
            {
                return null;
            }
            return new Vertex(worldCenter.X + res[0, 0] * worldCenter.X, worldCenter.Y + res[0, 1] * worldCenter.Y, (float)v.z);
            
        }

    }


    
}
