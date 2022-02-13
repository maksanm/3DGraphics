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
            table.Draw(canvas, projMatrix, viewMatrix, currentModel);
            sphere2.Draw(canvas, projMatrix, viewMatrix, currentModel);
            sphere1.Draw(canvas, projMatrix, viewMatrix, currentModel);
        }

        private void zBufferReset()
        {
            for (int i = 0; i < bitmap.Width; i++)
                for (int j = 0; j < bitmap.Height; j++)
                    zBuffer[i, j] = double.MaxValue;
        }

        private void SpiralIterate()
        {
            sphere1.Translate(new Vector3(1f / (4f * (float)Math.PI) * (float)phi * (float)Math.Cos((float)phi / 4f), 1f / (4f * (float)Math.PI) * (float)phi * (float)Math.Sin((float)phi / 4f), 0));
            if (spiralIncrease)
            {
                sphere1.RotateX(8 * phi);
                phi++;
                if (phi == 360)
                    spiralIncrease = false;
            }
            else
            {
                sphere1.RotateY(8 * phi);
                phi--;
                if (phi == 0)
                    spiralIncrease = true;
            }
        }
    }
}
