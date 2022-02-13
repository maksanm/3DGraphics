using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project4.Tools;
using Project4.Cameras;
using System.Numerics;
using Project4.Objects;
using Project4.ColorFilling;

namespace Project4
{
    /*TODO
    3 КАМЕРЫ
    ДИНАМИЧЕСКИЙ ОБЪЕКТ
    */
    public partial class Form1 : Form
    {
        private DirectBitmap canvas;
        private Timer timer = new Timer();

        private int n = 1;
        private int f = 100;
        private int fov = 90;
        private StaticCamera staticCamera;
        private TrackingCamera trackingCamera;
        private AttachedCamera attachedCamera;

        private Table table;
        private Sphere sphere1;
        private Sphere sphere2;

        private List<LightSource> lightSources;
        private ShadingModels currentModel = ShadingModels.Constant;

        private double[,] zBuffer;

        private Matrix projMatrix;
        private Matrix viewMatrix;

        private int phi = 360;

        private bool spiralIncrease = false;

        public Form1()
        {
            timer.Tick += new EventHandler(timer_tick);
            timer.Interval = 75;
            timer.Enabled = true;

            lightSources = new List<LightSource>();

            InitializeComponent();
        }

        private void timer_tick(object sender, EventArgs e)
        {
            canvas.Dispose();
            canvas = new DirectBitmap(bitmap.Width, bitmap.Height);
            zBufferReset();

            SpiralIterate();

            if (staticCameraButton.Checked)
                viewMatrix = staticCamera.GetViewMatrix();
            else if (trackingCameraButton.Checked)
                viewMatrix = trackingCamera.GetViewMatrix();
            else if (attachedCameraButton.Checked)
                viewMatrix = attachedCamera.GetViewMatrix();
            Draw();
            bitmap.Image = canvas.Bitmap;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            canvas = new DirectBitmap(bitmap.Width, bitmap.Height);
            zBuffer = new double[bitmap.Width, bitmap.Height];
            zBufferReset();
            table = new Table(new Vector3(0, 0, 0), Color.SaddleBrown, zBuffer);
            sphere1 = new Sphere(new Vector3(500, 400, 485), 20, Color.Red, zBuffer);
            sphere2 = new Sphere(new Vector3(590, 500, 465), 30, Color.White, zBuffer);

            staticCamera = new StaticCamera(new Vector3(1000, 1000, 1000), new Vector3(590, 500, 450));
            trackingCamera = new TrackingCamera(sphere1, new Vector3(1000, 1000, 1000));
            attachedCamera = new AttachedCamera(sphere1, new Vector3(1000, 1000, 1000));

            lightSources.Add(new LightSource(new Vector3(590, 500, 500), Color.White, 1.0f, 0.022f, 0.019f));
            sphere1.AddLights(1f, 0.5f, 0.5f, 4, lightSources);
            sphere2.AddLights(1f, 0.5f, 0.5f, 4, lightSources);
            table.AddLights(0.5f, 0.3f, 0.3f, 4, lightSources);

            projMatrix = GetProjectionMatrix();
        }

        private void constantShading_CheckedChanged(object sender, EventArgs e)
        {
            currentModel = constantShading.Checked ? ShadingModels.Constant : currentModel;
        }

        private void gouraudShading_CheckedChanged(object sender, EventArgs e)
        {
            currentModel = gouraudShading.Checked ? ShadingModels.Gouraud : currentModel;
        }

        private void phongShading_CheckedChanged(object sender, EventArgs e)
        {
            currentModel = phongShading.Checked ? ShadingModels.Phong : currentModel;
        }
    }
}
