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
            resultVector = new Vector3(0, 0, 0);
            foreach (var lightSource in LightSources)
            {
                Vector3 L = lightSource.GetLVector(position);
                float cosNL = Vector3.Dot(L, normal);
                cosNL = cosNL < 0 ? 0 : cosNL;
                Vector3 R = Vector3.Reflect(L, normal);
                float cosVR = Vector3.Dot(V, R);
                cosVR = cosVR < 0 ? 0 : cosVR;
                float distance = (position - lightSource.position).Length();
                float attenuation = 1.0f / (lightSource.constant + lightSource.linear * distance + lightSource.quadratic * (distance * distance));

                if (lightSource.isReflector)
                {
                    float theta = Vector3.Dot(L, Vector3.Normalize(lightSource.direction));
                    if (theta > Math.Cos(lightSource.cutOff))
                    {
                        resultVector += attenuation * lightSource.Il * (kd * cosNL + ks * (float)Math.Pow(cosVR, this.n));
                        resultVector += attenuation * ka * Ia;
                    }
                }
                else
                {
                    resultVector += attenuation * lightSource.Il * (kd * cosNL + ks * (float)Math.Pow(cosVR, this.n));
                    resultVector += attenuation * ka * Ia;
                }
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

        public Vector3 GetColorVector3(Vector3 position, Vector3 normal, Vector3 cameraPosition)
        {
            var color = GetColor(position, normal, cameraPosition);
            return new Vector3(color.R, color.G, color.B);
        }
    }
}
