using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Indiv2
{
    class RayTracer
    {
        public List<Shape> sceneObjects;
        public List<LightSource> lightSources;

        // field of view 
        const double fov = 80;
        Point cameraPosition = new Point(0, 0, 0);
        
        public RayTracer()
        {
            sceneObjects = new List<Shape>();
            lightSources = new List<LightSource>();
        }

        public Bitmap Render(Size frameSize)
        {
            var bmp = new Bitmap(frameSize.Width, frameSize.Height);

            for (int x = 0; x < frameSize.Width; x++)
                for (int y = 0; y < frameSize.Height; y++)
                {
                    var fov_tan = Math.Tan(Geometry.degreesToRadians(fov / 2));
                    Vector ray = new Vector(
                        (2 * (x + 0.5) / frameSize.Width  - 1) * fov_tan * frameSize.Width / frameSize.Height,
                       -(2 * (y + 0.5) / frameSize.Height - 1) * fov_tan,
                        1, true);

                    var color = shootRay(ray, cameraPosition);
                    bmp.SetPixel(x, y, color);
                }

            return bmp;
        }

        Color shootRay(Vector viewRay, Point origin, int depth = 0)
        {
            if (depth > 4)
                return Color.Gray;
            double nearestPoint = double.MaxValue;
            Color res = Color.Black;

            foreach (var shape in sceneObjects)  //для каждого объекта сцены
            {
                // выясняем пересекает ли луч объект и является ли этот объект ближайшим
                Tuple<Point, Vector> intersectionAndNormale = shape.getIntersect(viewRay, origin);
                if (intersectionAndNormale != null && intersectionAndNormale.Item1.z < nearestPoint)
                {
                    nearestPoint = intersectionAndNormale.Item1.z;
                    res = changeColorIntensity(shape.color, computeLightness(shape, intersectionAndNormale, viewRay));
                    if (shape.material.reflectivity > 0)
                    {
                        // вычисляем вектор отражения от поверхности который зависит от вектора обзора и выпускам луч в этом направлении из точки пересечени с фигурой
                        var reflectedColor = shootRay(getViewReflectionRay(viewRay, intersectionAndNormale.Item2), intersectionAndNormale.Item1, depth + 1);
                        res = mixColors(res, reflectedColor, shape.material.reflectivity);
                    }
                    
                }
            }
            return res;
        }


        static Color changeColorIntensity(Color color, double intensity)
        {
            return Color.FromArgb((byte)Geometry.Clamp(Math.Round(color.R * intensity), 0, 255),
                                  (byte)Geometry.Clamp(Math.Round(color.G * intensity), 0, 255),
                                  (byte)Geometry.Clamp(Math.Round(color.B * intensity), 0, 255));
        }

        // проверяет, пересекает ли вектор какой нибудь объект на сцене и если да возвращает пересечение 
        bool doesRayIntersectSomething(Vector direction, Point origin, out Tuple<Point,Vector> intersection)
        {
            intersection = null;
            foreach (var shape in sceneObjects)
            {
                if (shape is Face)
                {
                    continue;
                }
                intersection = shape.getIntersect(direction, origin);
                if ( intersection != null)
                {
                    return true;
                }
            }

            return false;
        }

        

        double computeLightness(Shape shape, Tuple<Point, Vector> intersectionAndNormale, Vector viewRay)
        {
            double diffuseLightness = 0;
            double specularLightness = 0;
            Tuple<Point, Vector> intersection;
            foreach (var lightSource in lightSources)
            {
                // луч выходящий из точки пересечения в точку источника света
                var shadowRay = new Vector(intersectionAndNormale.Item1, lightSource.location, true);
                // луч отражения света
                var reflectionRay = getLightReflectionRay(shadowRay, intersectionAndNormale.Item2);


                if (doesRayIntersectSomething(shadowRay, intersectionAndNormale.Item1, out intersection) )
                {
                    // проверка что пересечение с источником тени находится не дольше чем источник света от точки которую сейчас рассматриваем 
                    if (new Vector(intersectionAndNormale.Item1, intersection.Item1).Length() < new Vector(intersectionAndNormale.Item1,lightSource.location).Length())
                        continue;
                }


                diffuseLightness += lightSource.intensity * Geometry.Clamp(Vector.Dot(shadowRay, intersectionAndNormale.Item2), 0.0, double.MaxValue);
                specularLightness += lightSource.intensity *
                                     Math.Pow(Geometry.Clamp(Vector.Dot(reflectionRay, (-1 * viewRay)), 0.0, double.MaxValue), shape.material.shininess);
            }

            return shape.material.ambient + diffuseLightness * shape.material.diffuse + specularLightness * shape.material.specular;
        }

        // по лучу и нормали поверхности возращает его луч отражения от поверхности 
        Vector getLightReflectionRay(Vector shadowRay, Vector normale)
        {
            return (2 * Vector.Dot(shadowRay, normale) * normale - shadowRay).Normalize();
            
        }

        // находдит луч отражения взгляда камеры от поверхности по нормали поверхности 
        Vector getViewReflectionRay(Vector viewRay, Vector normale)
        {
            return (2 * Vector.Dot((-1 * viewRay) , normale) * normale - (-1 * viewRay)).Normalize();
        }

        Color mixColors(Color first, Color second, double secondToFirstRatio)
        {
            return Color.FromArgb((byte)((second.R * secondToFirstRatio) + first.R * (1 - secondToFirstRatio)), (byte)((second.G * secondToFirstRatio) + first.G * (1 - secondToFirstRatio)), (byte)((second.B * secondToFirstRatio) + first.B * (1 - secondToFirstRatio)));
        }




    }
}
