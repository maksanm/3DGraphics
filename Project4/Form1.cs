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

        private double[,] zBuffer;

        private Matrix projMatrix;
        private Matrix viewMatrix;

        private int phi = 0;

        private bool goBack = true;

        public Form1()
        {
            timer.Tick += new EventHandler(timer_tick);
            timer.Interval = 75;
            timer.Enabled = true;

            InitializeComponent();
        }

        private void timer_tick(object sender, EventArgs e)
        {
            canvas.Dispose();
            canvas = new DirectBitmap(bitmap.Width, bitmap.Height);
            zBufferReset();
            sphere2.RotateZ(3*phi);
            if (!goBack)
            {
                if (sphere1.Position.Y > 730)
                    goBack = true;
                sphere1.Translate(new Vector3(0, 10, 0));
                sphere1.RotateX(5 * phi);
            }
            else if (goBack)
            {
                if (sphere1.Position.Y < 270)
                    goBack = false;
                sphere1.Translate(new Vector3(0, -5, 0));
                sphere1.RotateX(-5 * phi);
            }
            if (staticCameraButton.Checked)
                viewMatrix = staticCamera.GetViewMatrix();
            else if (trackingCameraButton.Checked)
                viewMatrix = trackingCamera.GetViewMatrix();
            else if (attachedCameraButton.Checked)
                viewMatrix = attachedCamera.GetViewMatrix();
            Draw();
            phi++;
            bitmap.Image = canvas.Bitmap;
        }

        private void bitmap_Paint(object sender, PaintEventArgs e)
        {
            /*canvas = new DirectBitmap(bitmap.Width, bitmap.Height);
            using (Graphics g = Graphics.FromImage(canvas.Bitmap))
            {
                g.Clear(Color.White);
                DrawCube(g, cube);
            }
            e.Graphics.DrawImage(canvas.Bitmap, 0, 0);*/
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            canvas = new DirectBitmap(bitmap.Width, bitmap.Height);
            zBuffer = new double[bitmap.Width, bitmap.Height];
            zBufferReset();
            table = new Table(Color.SaddleBrown, zBuffer);
            sphere1 = new Sphere(new Vector3(600, 400, 430), 20, Color.White, zBuffer);
            sphere2 = new Sphere(new Vector3(590, 500, 450), 20, Color.Red, zBuffer);

            staticCamera = new StaticCamera(new Vector3(1000, 1000, 1000), new Vector3(-500, -500, -500));
            trackingCamera = new TrackingCamera(sphere1, new Vector3(0, 500, 0));
            attachedCamera = new AttachedCamera(sphere1, new Vector3(1000, 1000, 1000));

            projMatrix = GetProjectionMatrix();
        }
    }
}
