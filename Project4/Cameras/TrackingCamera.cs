using Project4.Objects;
using Project4.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project4.Cameras
{
    public class TrackingCamera : StaticCamera
    {
        private AbstractObject trackedObject;

        public TrackingCamera(AbstractObject trackedObject, Vector3 cameraPosition) : base(cameraPosition, trackedObject.Position) 
        {
            /*cameraPosition.X = -cameraPosition.X;
            cameraPosition.Y = -cameraPosition.Y;
            this.cameraPosition = cameraPosition;*/
            this.trackedObject = trackedObject;
        }

        public override Matrix GetViewMatrix()
        {
            cameraTarget = trackedObject.Position;
            return base.GetViewMatrix();
        }
    }
}
