using System;
using System.Collections.Generic;
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
            _glControlLoaded = true;

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

        private void Resize()
        {
            var width = glControl.Width;
            var height = glControl.Height;

            GL.Viewport(0, 0, width, height);

            var aspect = (float)width / height;
            var fovy = (float)((Math.PI / 180.0) * 50);
            var perspectiveMatrix = Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, 1, 1000);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspectiveMatrix);

            GL.MatrixMode(MatrixMode.Modelview);
        }

        private void OnGlControlResize(object sender, EventArgs e)
        {
            Resize();
        }

        private void Render()
        {
            if (!_glControlLoaded)
            {
                return;
            }

            GL.ClearColor(Color.Aqua);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            var cameraMatrix = _camera.GetCameraMatrix();
            GL.LoadMatrix(ref cameraMatrix);

            GL.Rotate(_angle, 0f, 1f, 0f);
            GL.Disable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            GL.Begin(BeginMode.Triangles);

            if (Meshes != null)
            {
                foreach (var mesh in Meshes)
                {
                    Render(mesh);
                }
            }

            GL.End();
            //foreach (var modelGroup in _loadResult.Groups)
            //{
            //    foreach (var face in modelGroup.Faces)
            //    {
            //        Render(face);
            //    }
            //}

            //var catmull = new Catmull();
            //var cubeMesh = catmull.CreateCubeMesh().ToList();

            //List<Face> newFaces = SubdivideQuadMesh(cubeMesh, 6);

            //foreach (var face in newFaces)
            //{
            //    Render(face);
            //}

            glControl.SwapBuffers();
        }

        private void Render(Mesh mesh)
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
