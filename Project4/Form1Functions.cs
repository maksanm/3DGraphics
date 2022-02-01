using Project4.Objects;
using Project4.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project4
{
    public partial class Form1 : Form
    {
        private Matrix GetProjectionMatrix()
        {
            double a = -(double)(f + n) / (double)(f - n);
            double b = -(double)(2 * f * n) / (double)(f - n);
            double e = 1.0 / Math.Tan((double)fov / 2 * Math.PI / 180);
            Matrix result = new Matrix(4, 4);
            result.matrix = new double[,] { { e, 0, 0, 0 }, { 0, e, 0, 0 }, { 0, 0, a, b }, { 0, 0, -1, 0 } };
            return result;
        }

        private void Draw()
        {
            table.Draw(canvas, projMatrix, viewMatrix);
            sphere2.Draw(canvas, projMatrix, viewMatrix);
            sphere1.Draw(canvas, projMatrix, viewMatrix);
        }

        private void zBufferReset()
        {
            for (int i = 0; i < bitmap.Width; i++)
                for (int j = 0; j < bitmap.Height; j++)
                    zBuffer[i, j] = double.MinValue;
        }
    }
}
