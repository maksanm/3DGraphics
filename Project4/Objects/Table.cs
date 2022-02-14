using Project4.ColorFilling;
using Project4.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project4.Objects
{
    public class Table : AbstractObject
    {
        private List<Cube> tabletop;
        private List<Cube> legs;

        public Table(Vector3 position, Color color, double[,] zBuffer) : base(position, color, zBuffer)
        {
            tabletop = new List<Cube>();
            legs = new List<Cube>();
            for (int i = 200; i < 1000; i += 100)
                for (int j = 250; j < 750; j += 125)
                    tabletop.Add(new Cube(position + new Vector3(i, j, 450), 100, 10, 125, color, zBuffer));
            legs.AddRange(new List<Cube>(){
                new Cube(position + new Vector3(980, 730, 250), 20, 200, 20, color, zBuffer),
                new Cube(position + new Vector3(200, 730, 250), 20, 200, 20, color, zBuffer),
                new Cube(position + new Vector3(200, 250, 250), 20, 200, 20, color, zBuffer),
                new Cube(position + new Vector3(980, 250, 250), 20, 200, 20, color, zBuffer)
                });
        }

        public override void Draw(DirectBitmap canvas, Matrix projMatrix, Matrix viewMatrix, ShadingModels shadingModel)
        {
            foreach (var cube in tabletop)
                cube.Draw(canvas, projMatrix, viewMatrix, shadingModel);
            foreach (var leg in legs)
                leg.Draw(canvas, projMatrix, viewMatrix, shadingModel);
        }

        public override void AddLights(float ka, float kd, float ks, int n, List<LightSource> lightSources)
        {
            foreach (var cube in tabletop)
                cube.AddLights(ka, kd, ks, n, lightSources);
            foreach (var leg in legs)
                leg.AddLights(ka, kd, ks, n, lightSources);
        }
    }
}
