using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Indiv2
{
    public enum SelectedItem
    {
        BackWall,
        RightWall,
        LeftWall,
        Floor,
        Ceiling,
        Cube,
        SphereOnCube,
        SphereOnGround
    }

    public partial class Form1 : Form
    {
        RayTracer ray_tracer;

        //SelectedItem currentItemType;
        Shape currentItem;

        Cube cube;
        Sphere sphere_small;
        Sphere sphere_big;
        Face leftWall;
        Face rightWall;
        Face backWall;
        Face cameraWall;
        Face ceiling;
        Face floor;

        LightSource mainLight = new LightSource(new Point(0, 9, 9), 0.9);
        LightSource additionalLight = new LightSource(new Point(-7, 7, 0), 0.8);

        public Form1()
        {
            InitializeComponent();

            frame.Image = new Bitmap(frame.Width, frame.Height);


            var center = new Point(0, 0, 9);
            double roomSide = 20;

            initShapes(center, roomSide);

            ray_tracer = new RayTracer();


            currentItemType = SelectedItem.BackWall;
            currentItem = backWall;

        }

        private void renderButton_Click(object sender, EventArgs e)
        {
            // Добавляем все объекты сцены в рейтрейсер
            ray_tracer.sceneObjects = new List<Shape> { 
                sphere_small, cube, sphere_big,
                cameraWall, backWall, ceiling, floor, rightWall, leftWall
            };

            ray_tracer.lightSources = new List<LightSource> { mainLight };
            if (checkBoxAdditionalLight.Checked)
                ray_tracer.lightSources.Add(additionalLight);

            
            var bitmap = ray_tracer.Render(frame.Size); 

            if (frame.Image != null)
                frame.Image.Dispose();
            frame.Image = bitmap;

        }

        void initShapes(Point center, double roomSide)
        {
            sphere_big = new Sphere(new Point(-5, -7, 15), 
                4, Color.DodgerBlue,
                new Material(40, 0.25, 0.9, 0.05));

            cube = new Cube(new Point(5, -7, 16), 
                6, Color.Orange,
                new Material(40, 0.25, 0.9, 0.05));

            sphere_small = new Sphere(new Point(6, -1, 15), 
                2, Color.Green,
                new Material(40, 0.25, 0.9, 0.05));

            leftWall = new Face(
                new Point(center.x - roomSide / 2, center.y, center.z),
                new Vector(1, 0, 0),
                new Vector(0, 1, 0),
                roomSide, roomSide,
                Color.IndianRed,
                new Material(0, 0, 0.9, 0.1)
            );
            rightWall = new Face(
                new Point(center.x + roomSide / 2, center.y, center.z),
                new Vector(-1, 0, 0),
                new Vector(0, 1, 0),
                roomSide, roomSide, Color.LightSkyBlue,
                new Material(0, 0, 0.9, 0.1)
            );
            backWall = new Face(
                new Point(center.x, center.y, center.z + roomSide / 2),
                new Vector(0, 0, -1),
                new Vector(0, 1, 0),
                roomSide, roomSide, Color.Gray,
                new Material(0, 0, 0.9, 0.1)
            );
            cameraWall = new Face(
                new Point(center.x, center.y, center.z - roomSide / 2), 
                new Vector(0, 0, 1), new Vector(0, 1, 0), 
                roomSide, roomSide, Color.Gray, 
                new Material(0, 0, 0.9, 0.1)
            );
            ceiling = new Face(
                new Point(center.x, center.y + roomSide / 2, center.z), 
                new Vector(0, -1, 0), 
                new Vector(0, 0, -1), 
                roomSide, roomSide, Color.Gray, 
                new Material(0, 0, 0.9, 0.1)
            );
            floor = new Face(
                new Point(center.x, center.y - roomSide / 2, center.z), 
                new Vector(0, 1, 0), 
                new Vector(0, 0, 1), 
                roomSide, roomSide, Color.Gray, 
                new Material(0, 0, 0.9, 0.1)
            );
        }


        private void buttonChangeColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = currentItem.color;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                currentItem.color = colorDialog1.Color;
            }
        }

        private void lightX_ValueChanged(object sender, EventArgs e)
        {
            mainLight.location.x = (double)lightX.Value;
        }

        private void lightY_ValueChanged(object sender, EventArgs e)
        {
            mainLight.location.y = (double)lightY.Value;
        }

        private void lightZ_ValueChanged(object sender, EventArgs e)
        {
            mainLight.location.z = (double)lightZ.Value;
        }

        private void lightValue_ValueChanged(object sender, EventArgs e)
        {
            mainLight.intensity = (double)lightValue.Value;
        }


        private void additLightX_ValueChanged(object sender, EventArgs e)
        {
            additionalLight.location.x = (double)additLightX.Value;
        }

        private void additLightY_ValueChanged(object sender, EventArgs e)
        {
            additionalLight.location.y = (double)additLightY.Value;
        }

        private void additLightZ_ValueChanged(object sender, EventArgs e)
        {
            additionalLight.location.z = (double)additLightZ.Value;
        }

        private void additLightValue_ValueChanged(object sender, EventArgs e)
        {
            additionalLight.intensity = (double)additLightValue.Value;
        }




    }
}
