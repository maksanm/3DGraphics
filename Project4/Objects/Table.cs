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
    public class Table
    {
        private Cube tabletop;
        private Cube leg1;
        private Cube leg2;
        private Cube leg3;
        private Cube leg4;

        public Table(Color color, double[,] zBuffer)
        {
            tabletop = new Cube(new Vector3(200, 250, 450), 800, 10, 500, color, zBuffer);
            leg1 = new Cube(new Vector3(980, 730, 450), 20, 200, 20, color, zBuffer);
            leg2 = new Cube(new Vector3(200, 730, 450), 20, 200, 20, color, zBuffer);
            leg3 = new Cube(new Vector3(200, 250, 450), 20, 200, 20, color, zBuffer);
            leg4 = new Cube(new Vector3(980, 250, 450), 20, 200, 20, color, zBuffer);
        }

        public void Draw(DirectBitmap canvas, Matrix projMatrix, Matrix viewMatrix)
        {
            tabletop.Draw(canvas, projMatrix, viewMatrix);
            leg1.Draw(canvas, projMatrix, viewMatrix);
            leg2.Draw(canvas, projMatrix, viewMatrix);
            leg3.Draw(canvas, projMatrix, viewMatrix);
            leg4.Draw(canvas, projMatrix, viewMatrix);
        }
    }
}
