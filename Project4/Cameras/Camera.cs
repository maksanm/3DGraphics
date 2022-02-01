using Project4.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project4.Cameras
{
    public class Camera
    {
        private Vector3 cameraPosition;
        private Vector3 cameraTarget;
        private Vector3 upVersor = new Vector3(0, 0, 1);

        public Camera(Vector3 cameraPosition, Vector3 cameraTarget)
        {
            this.cameraPosition = cameraPosition / cameraPosition.Length();
            this.cameraTarget = cameraTarget / cameraTarget.Length();
        }

        public Matrix GetViewMatrix()
        {
            Vector3 zAxis = Vector3.Subtract(cameraPosition, cameraTarget);
            zAxis = zAxis / zAxis.Length();
            Vector3 xAxis = Vector3.Cross(upVersor, zAxis);
            Vector3 yAxis = Vector3.Cross(zAxis, xAxis);
            Matrix invertViewMatrix = new Matrix(4, 4);
            invertViewMatrix.matrix = new double[,] { { xAxis.X, yAxis.X, zAxis.X, cameraPosition.X }, { xAxis.Y, yAxis.Y, zAxis.Y, cameraPosition.Y },
                { xAxis.Z, yAxis.Z, zAxis.Z, cameraPosition.Z }, { 0, 0, 0, 1 } };
            return invertViewMatrix.Inverse();
        }
    }
}
