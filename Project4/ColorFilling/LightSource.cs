using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project4.ColorFilling
{
    public class LightSource
    {
        public Vector3 position { get; }
        public Vector3 Il { get; }
        public float constant { get; }
        public float linear { get; }
        public float quadratic { get; }

        public LightSource(Vector3 position, Color lightColor, float constant, float linear, float quadratic)
        {
            this.constant = constant;
            this.linear = linear;
            this.quadratic = quadratic;
            this.position = position;
            this.Il = Vector3.Normalize(new Vector3(lightColor.R, lightColor.G, lightColor.B));
        }

        public Vector3 GetLVector(Vector3 point)
        {
            return Vector3.Normalize(point - this.position);
        }
    }
}
