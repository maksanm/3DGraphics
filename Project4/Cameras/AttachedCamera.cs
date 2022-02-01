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
    public class AttachedCamera : StaticCamera
    {
        private AbstractObject attachedObject;

        public AttachedCamera(AbstractObject attachedObject, Vector3 cameraPosition) : base(cameraPosition, attachedObject.Position) 
        {
            this.attachedObject = attachedObject;
        }

        public override Matrix GetViewMatrix()
        {
            cameraPosition = cameraPosition + attachedObject.Position - cameraTarget;
            cameraTarget = attachedObject.Position;
            return base.GetViewMatrix();
        }
    }
}
