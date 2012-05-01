using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Color = System.Drawing.Color;

namespace CjClutter.ObjLoader.Viewer
{
    public partial class OpenGlUserControl
    {
        private bool _glControlLoaded;
        private double _angle;

        private readonly Camera _camera;

        public OpenGlUserControl()
        {
            InitializeComponent();

            _camera = new Camera();
        }

        private void OnGlControlLoad(object sender, EventArgs e)
        {
            _glControlLoaded = !DesignerProperties.GetIsInDesignMode(this);

            var stopwatch = new Stopwatch();

            var dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += (o, args) =>
            {
                _angle += stopwatch.Elapsed.TotalSeconds * 10;
                stopwatch.Restart();
                Render();
            };

            dispatcherTimer.Interval = TimeSpan.FromSeconds(1 / 60.0);
            stopwatch.Start();
            dispatcherTimer.Start();

            Resize();
        }

        private void OnGlControlPaint(object sender, PaintEventArgs e)
        {
            Render();
        }

        private void OnGlControlResize(object sender, EventArgs e)
        {
            Resize();
        }

        private void Resize()
        {
            if(!_glControlLoaded)
            {
                return;
            }

            ResizeViewport();

            SetPerspectiveMatrix();
        }

        private void ResizeViewport()
        {
            var width = glControl.Width;
            var height = glControl.Height;

            GL.Viewport(0, 0, width, height);
        }

        private void SetPerspectiveMatrix()
        {
            var perspectiveMatrix = CreatePerspectiveMatrix();

            GL.MatrixMode(MatrixMode.Projection);

            GL.LoadMatrix(ref perspectiveMatrix);

            GL.MatrixMode(MatrixMode.Modelview);
        }

        private Matrix4 CreatePerspectiveMatrix()
        {
            var width = glControl.Width;
            var height = glControl.Height;

            var aspect = (float)width / height;
            const float fovy = (float)((Math.PI / 180.0) * 50);
            var perspectiveMatrix = Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, 1, 1000);

            return perspectiveMatrix;
        }

        private void Render()
        {
            if (!_glControlLoaded)
            {
                return;
            }

            SetupScene();

            GL.Rotate(_angle, 0f, 1f, 0f);
            
            GL.Begin(BeginMode.Triangles);

            if (Meshes != null)
            {
                foreach (var mesh in Meshes)
                {
                    RenderMesh(mesh);
                }
            }

            GL.End();

            glControl.SwapBuffers();
        }

        private void SetupScene()
        {
            GL.ClearColor(Color.Aqua);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);

            var cameraMatrix = _camera.GetCameraMatrix();
            GL.LoadMatrix(ref cameraMatrix);
        }

        private void RenderMesh(Mesh mesh)
        {
            for (int index = 0; index < mesh.Triangles.Count; index++)
            {
                var vertex = mesh.Triangles[index];
                var normal = mesh.Normals[index];

                GL.Normal3(normal);
                GL.Vertex3(vertex);
            }
        }

        public static readonly DependencyProperty MeshesProperty =
            DependencyProperty.Register("Meshes", typeof (List<Mesh>), typeof (OpenGlUserControl), new PropertyMetadata(default(List<Mesh>)));

        public List<Mesh> Meshes
        {
            get { return (List<Mesh>) GetValue(MeshesProperty); }
            set
            {
                SetValue(MeshesProperty, value);
                Render();
            }
        }
    }
}
