using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Threading;
using CjClutter.ObjLoader.Viewer.test;
using ObjLoader.Loader.Loaders;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace CjClutter.ObjLoader.Viewer
{
    public partial class ShellView
    {
        private bool _glControlLoaded;
        private double _angle;
        private readonly Camera _camera;
        private readonly Random _random;
        private LoadResult _loadResult;
        
        public ShellView()
        {
            InitializeComponent();

            _camera = new Camera();
            _random = new Random();
        }

        private void OnGlControlPaint(object sender, PaintEventArgs e)
        {
            if (!_glControlLoaded)
            {
                return;
            }

            Render();
        }

        private void Render(Face face)
        {
            GL.Begin(BeginMode.Quads);
            GL.Color3(GetRandomColor());
            foreach (var vector3 in face.Vertices)
            {
                //vector3.Normalize();
                //GL.Vertex3(vector3);
                GL.Vertex3(MapCubeToSphere(vector3 * 2));
            }

            GL.End();
        }

        private Color GetRandomColor()
        {
            var r = _random.Next(256);
            var g = _random.Next(256);
            var b = _random.Next(256);

            return Color.FromArgb(r, g, b);
        }

        private Vector3 MapCubeToSphere(Vector3 coordinate)
        {
            var xSquared = coordinate.X * coordinate.X;
            var ySquared = coordinate.Y * coordinate.Y;
            var zSquared = coordinate.Z * coordinate.Z;

            var xBelowSqrt = 1 - ySquared / 2 - zSquared / 2 + (ySquared * zSquared) / 3;
            var x = coordinate.X * Math.Sqrt(xBelowSqrt);

            var yBelowSqrt = 1 - zSquared / 2 - xSquared / 2 + (zSquared * xSquared) / 3;
            var y = coordinate.Y * Math.Sqrt(yBelowSqrt);

            var zBelowSqrt = 1 - xSquared / 2 - ySquared / 2 + (xSquared * ySquared) / 3;
            var z = coordinate.Z * Math.Sqrt(zBelowSqrt);

            return new Vector3((float)x, (float)y, (float)z);
        }

        private void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            var cameraMatrix = _camera.GetCameraMatrix();
            GL.LoadMatrix(ref cameraMatrix);

            GL.Rotate(_angle, 0, 1, 0);
            GL.Disable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            GL.Begin(BeginMode.Triangles);
            foreach (var modelGroup in _loadResult.Groups)
            {
                foreach (var face in modelGroup.Faces)
                {
                    Render(face);
                }
            }
            GL.End();

            //var catmull = new Catmull();
            //var cubeMesh = catmull.CreateCubeMesh().ToList();

            //List<Face> newFaces = SubdivideQuadMesh(cubeMesh, 6);

            //foreach (var face in newFaces)
            //{
            //    Render(face);
            //}

            glControl.SwapBuffers();
        }

        private void Render(global::ObjLoader.Loader.Data.Elements.Face face)
        {
            for (int i = 0; i < face.Count; i++)
            {
                var faceVertex = face[i];

                var vertex = _loadResult.Vertices[faceVertex.VertexIndex - 1];
                GL.Vertex3(vertex.X, vertex.Y, vertex.Z);
            }
        }

        private List<Face> SubdivideQuadMesh(List<Face> mesh, int subdivisionLevel)
        {
            if (subdivisionLevel == 0)
            {
                return mesh;
            }

            var monkey = new Monkey();
            var faces = new List<Face>();
            foreach (var face in mesh)
            {
                var splitQuad = monkey.SplitQuad(face);
                faces.AddRange(splitQuad);
            }

            return SubdivideQuadMesh(faces, subdivisionLevel - 1);
        }

        private void Update()
        {
            _angle += 1.0;
            Render();
        }

        private void OnGlControlLoad(object sender, EventArgs e)
        {
            _glControlLoaded = true;
            GL.ClearColor(Color.Aqua);

            var dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += (o, args) =>
            {
                _angle += 1.0;
                Render();
            };

            dispatcherTimer.Interval = TimeSpan.FromSeconds(1 / 60.0);
            dispatcherTimer.Start();

            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create();

            _loadResult = objLoader.Load(File.OpenRead("buddha.obj"));

            Resize();
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
    }
}
