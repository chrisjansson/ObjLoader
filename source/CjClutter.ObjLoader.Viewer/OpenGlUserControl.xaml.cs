using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using CjClutter.ObjLoader.Viewer.Camera;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Color = System.Drawing.Color;

namespace CjClutter.ObjLoader.Viewer
{
    public partial class OpenGlUserControl
    {
        private const float FovY = 50;

        private bool _glControlLoaded;

        public OpenGlUserControl()
        {
            InitializeComponent();
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
            const double fovyRadians = (Math.PI / 180) * FovY;
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

            var cameraMatrix = Camera.GetCameraMatrix();
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
            DependencyProperty.Register("Meshes", typeof(List<Mesh>), typeof(OpenGlUserControl), new PropertyMetadata(default(List<Mesh>), OnMeshesPropertyChanged));

        private static void OnMeshesPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var openGlUserControl = (OpenGlUserControl)dependencyObject;
            openGlUserControl.Render();
        }

        public List<Mesh> Meshes
        {
            get { return (List<Mesh>)GetValue(MeshesProperty); }
            set { SetValue(MeshesProperty, value); }
        }

        public static readonly DependencyProperty CameraProperty =
            DependencyProperty.Register("Camera", typeof (ICamera), typeof (OpenGlUserControl), new PropertyMetadata(default(ICamera), OnCameraChanged));

        private static void OnCameraChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var openGlUserControl = (OpenGlUserControl)dependencyObject;

            var oldCamera = (ICamera)e.OldValue;
            if(oldCamera != null)
            {
                oldCamera.CameraChanged -= openGlUserControl.Render;
            }

            var newCamera = (ICamera) e.NewValue;
            if(newCamera != null)
            {
                newCamera.CameraChanged += openGlUserControl.Render;
            }

            openGlUserControl.Render();
        }

        public ICamera Camera
        {
            get { return (ICamera) GetValue(CameraProperty); }
            set { SetValue(CameraProperty, value); }
        }
    }
}
