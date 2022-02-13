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
        // TODO: private List<Cube> tabletop;
        private Cube tabletop1;
        private Cube tabletop2;
        private Cube tabletop3;
        private Cube tabletop4;
        private Cube tabletop5;
        private Cube tabletop6;
        private Cube tabletop7;
        private Cube tabletop8;
        private Cube leg1;
        private Cube leg2;
        private Cube leg3;
        private Cube leg4;

        public Table(Vector3 position, Color color, double[,] zBuffer) : base(position, color, zBuffer)
        {
            tabletop1 = new Cube(position + new Vector3(200, 250, 450), 200, 10, 250, color, zBuffer);
            tabletop2 = new Cube(position + new Vector3(200, 500, 450), 200, 10, 250, color, zBuffer);
            tabletop3 = new Cube(position + new Vector3(400, 250, 450), 200, 10, 250, color, zBuffer);
            tabletop4 = new Cube(position + new Vector3(400, 500, 450), 200, 10, 250, color, zBuffer);
            tabletop5 = new Cube(position + new Vector3(600, 250, 450), 200, 10, 250, color, zBuffer);
            tabletop6 = new Cube(position + new Vector3(600, 500, 450), 200, 10, 250, color, zBuffer);
            tabletop7 = new Cube(position + new Vector3(800, 250, 450), 200, 10, 250, color, zBuffer);
            tabletop8 = new Cube(position + new Vector3(800, 500, 450), 200, 10, 250, color, zBuffer);
            leg1 = new Cube(position + new Vector3(980, 730, 250), 20, 200, 20, color, zBuffer);
            leg2 = new Cube(position + new Vector3(200, 730, 250), 20, 200, 20, color, zBuffer);
            leg3 = new Cube(position + new Vector3(200, 250, 250), 20, 200, 20, color, zBuffer);
            leg4 = new Cube(position + new Vector3(980, 250, 250), 20, 200, 20, color, zBuffer);
        }

        public override void Draw(DirectBitmap canvas, Matrix projMatrix, Matrix viewMatrix, ShadingModels shadingModel)
        {
            tabletop1.Draw(canvas, projMatrix, viewMatrix, shadingModel);
            tabletop2.Draw(canvas, projMatrix, viewMatrix, shadingModel);
            tabletop3.Draw(canvas, projMatrix, viewMatrix, shadingModel);
            tabletop4.Draw(canvas, projMatrix, viewMatrix, shadingModel);
            tabletop5.Draw(canvas, projMatrix, viewMatrix, shadingModel);
            tabletop6.Draw(canvas, projMatrix, viewMatrix, shadingModel);
            tabletop7.Draw(canvas, projMatrix, viewMatrix, shadingModel);
            tabletop8.Draw(canvas, projMatrix, viewMatrix, shadingModel);
            leg1.Draw(canvas, projMatrix, viewMatrix, shadingModel);
            leg2.Draw(canvas, projMatrix, viewMatrix, shadingModel);
            leg3.Draw(canvas, projMatrix, viewMatrix, shadingModel);
            leg4.Draw(canvas, projMatrix, viewMatrix, shadingModel);
        }

        public override void AddLights(float ka, float kd, float ks, int n, List<LightSource> lightSources)
        {
            tabletop1.AddLights(ka, kd, ks, n, lightSources);
            tabletop2.AddLights(ka, kd, ks, n, lightSources);
            tabletop3.AddLights(ka, kd, ks, n, lightSources);
            tabletop4.AddLights(ka, kd, ks, n, lightSources);
            tabletop5.AddLights(ka, kd, ks, n, lightSources);
            tabletop6.AddLights(ka, kd, ks, n, lightSources);
            tabletop7.AddLights(ka, kd, ks, n, lightSources);
            tabletop8.AddLights(ka, kd, ks, n, lightSources);
            leg1.AddLights(ka, kd, ks, n, lightSources);
            leg2.AddLights(ka, kd, ks, n, lightSources);
            leg3.AddLights(ka, kd, ks, n, lightSources);
            leg4.AddLights(ka, kd, ks, n, lightSources);
        }
    }
}
