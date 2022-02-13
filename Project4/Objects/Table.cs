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
        private Cube tabletop;
        private Cube leg1;
        private Cube leg2;
        private Cube leg3;
        private Cube leg4;

        public Table(Vector3 position, Color color, double[,] zBuffer) : base(position, color, zBuffer)
        {
            tabletop = new Cube(position + new Vector3(200, 250, 450), 800, 10, 500, color, zBuffer);
            leg1 = new Cube(position + new Vector3(980, 730, 250), 20, 200, 20, color, zBuffer);
            leg2 = new Cube(position + new Vector3(200, 730, 250), 20, 200, 20, color, zBuffer);
            leg3 = new Cube(position + new Vector3(200, 250, 250), 20, 200, 20, color, zBuffer);
            leg4 = new Cube(position + new Vector3(980, 250, 250), 20, 200, 20, color, zBuffer);
        }

        public override void Draw(DirectBitmap canvas, Matrix projMatrix, Matrix viewMatrix, ShadingModels shadingModel)
        {
            tabletop.Draw(canvas, projMatrix, viewMatrix, shadingModel);
            leg1.Draw(canvas, projMatrix, viewMatrix, shadingModel);
            leg2.Draw(canvas, projMatrix, viewMatrix, shadingModel);
            leg3.Draw(canvas, projMatrix, viewMatrix, shadingModel);
            leg4.Draw(canvas, projMatrix, viewMatrix, shadingModel);
        }

        public override void AddLights(float ka, float kd, float ks, int n, List<LightSource> lightSources)
        {
            tabletop.AddLights(ka, kd, ks, n, lightSources);
            leg1.AddLights(ka, kd, ks, n, lightSources);
            leg2.AddLights(ka, kd, ks, n, lightSources);
            leg3.AddLights(ka, kd, ks, n, lightSources);
            leg4.AddLights(ka, kd, ks, n, lightSources);
        }
    }
}
