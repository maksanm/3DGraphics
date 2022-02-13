using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project4.ColorFilling
{
    public class ColorGenerator
    {
        private Vector3 Ia;
        private float kd;
        private float ks;
        private float ka;
        private int n;
        public List<LightSource> LightSources { get; set; }

        public ColorGenerator(Color ambientColor, float ka, float kd, float ks, int n, List<LightSource> lightSources)
        {
            this.Ia = Vector3.Normalize(new Vector3(ambientColor.R, ambientColor.G, ambientColor.B));
            this.ka = ka;
            this.kd = kd;
            this.ks = ks;
            this.n = n;
            this.LightSources = lightSources;
        }

        public Color GetColor(Vector3 position, Vector3 normal, Vector3 cameraPosition)
        {
            Vector3 resultVector;
            var V = Vector3.Normalize(position - cameraPosition);
            resultVector = Ia * ka;
            foreach(var lightSource in LightSources)
            {
                Vector3 L = lightSource.GetLVector(position);
                float cosNL = normal.X * L.X + normal.Y * L.Y + normal.Z * L.Z;
                cosNL = cosNL < 0 ? 0 : cosNL;
                Vector3 R = 2 * cosNL * normal - L;
                float cosVR = V.X * R.X + V.Y * R.Y + V.Z * R.Z;
                cosVR = cosVR < 0 ? 0 : cosVR;

                float distance = (position - lightSource.position).Length();
                //float attenuation = 1.0f / (lightSource.constant + lightSource.linear * distance + lightSource.quadratic * (distance * distance));

                resultVector +=  lightSource.Il * (kd * cosNL + ks * (float)Math.Pow(cosVR, this.n));
            }
            resultVector *= 255;
            resultVector.X = resultVector.X <= 255 ? resultVector.X : 255;
            resultVector.Y = resultVector.Y <= 255 ? resultVector.Y : 255;
            resultVector.Z = resultVector.Z <= 255 ? resultVector.Z : 255;
            resultVector.X = resultVector.X >= 0 ? resultVector.X : 0;
            resultVector.Y = resultVector.Y >= 0 ? resultVector.Y : 0;
            resultVector.Z = resultVector.Z >= 0 ? resultVector.Z : 0;
            return Color.FromArgb((int)(resultVector.X), (int)(resultVector.Y), (int)(resultVector.Z));
        }
    }
}
