using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using CjClutter.ObjLoader.Viewer.Camera;
using CjClutter.ObjLoader.Viewer.CoordinateSystems;
using CjClutter.ObjLoader.Viewer.InputProcessor;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Color = System.Drawing.Color;

namespace CjClutter.ObjLoader.Viewer
{
    public partial class OpenGlUserControl : IMouseInputTarget
    {
        private float _fovy = 50;
        private bool _glControlLoaded;

        private MouseInputAdapter _mouseInputAdapter;
        private readonly GuiToRelativeCoordinateTransformer _guiToRelativeCoordinateTransformer;
        private readonly ITrackballCamera _camera;

        public OpenGlUserControl()
        {
            InitializeComponent();

            _mouseInputAdapter = new MouseInputAdapter
                                     {
                                         Source = glControl,
                                         Target = this
                                     };

            _camera = new TrackballCamera(new TrackballCameraRotationCalculator());
            _camera.CameraChanged += Render;
            _guiToRelativeCoordinateTransformer = new GuiToRelativeCoordinateTransformer {Control = glControl};
        }

        private void OnGlControlLoad(object sender, EventArgs e)
        {
            _glControlLoaded = !DesignerProperties.GetIsInDesignMode(this);

            Resize();
            Render();
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
            if (!_glControlLoaded)
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
            var fovyRadians = (Math.PI / 180) * _fovy;
            var perspectiveMatrix = Matrix4.CreatePerspectiveFieldOfView((float)fovyRadians, aspect, 1, 1000);

            return perspectiveMatrix;
        }

        private void Render()
        {
            if (!_glControlLoaded)
            {
                return;
            }

            SetupScene();

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
            DependencyProperty.Register("Meshes", typeof(List<Mesh>), typeof(OpenGlUserControl), new PropertyMetadata(default(List<Mesh>), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var openGlUserControl = (OpenGlUserControl)dependencyObject;
            openGlUserControl.Render();
        }

        public List<Mesh> Meshes
        {
            get { return (List<Mesh>)GetValue(MeshesProperty); }
            set
            {
                SetValue(MeshesProperty, value);
            }
        }

        private void OnGlControlMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0)
            {
                _fovy += -(float)(e.Delta / 100.0);
                SetPerspectiveMatrix();
                Render();
            }
        }

        public void OnMouseMove(Vector2d position)
        {
        }

        public void OnLeftMouseButtonDown(Vector2d position)
        {
        }

        public void OnLeftMouseButtonUp(Vector2d position)
        {
        }

        public void OnMouseDrag(MouseDragEventArgs mouseDragEventArgs)
        {
            var startPoint = _guiToRelativeCoordinateTransformer.TransformToRelative(mouseDragEventArgs.StartPosition);
            var endPoint = _guiToRelativeCoordinateTransformer.TransformToRelative(mouseDragEventArgs.EndPosition);

            _camera.Rotate(startPoint, endPoint);
        }

        public void OnMouseDragEnd(MouseDragEventArgs mouseDragEventArgs)
        {
            var startPoint = _guiToRelativeCoordinateTransformer.TransformToRelative(mouseDragEventArgs.StartPosition);
            var endPoint = _guiToRelativeCoordinateTransformer.TransformToRelative(mouseDragEventArgs.EndPosition);

            _camera.CommitRotation(startPoint, endPoint);
        }
    }
}
