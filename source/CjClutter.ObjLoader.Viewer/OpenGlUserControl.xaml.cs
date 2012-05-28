using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Color = System.Drawing.Color;
using Point = System.Drawing.Point;

namespace CjClutter.ObjLoader.Viewer
{
    public partial class OpenGlUserControl
    {
        private float _fovy = 50;
        private bool _glControlLoaded;

        private readonly Camera _camera;

        public OpenGlUserControl()
        {
            InitializeComponent();

            _camera = new Camera();
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
            var fovyRadians = (Math.PI/180)*_fovy;
            var perspectiveMatrix = Matrix4.CreatePerspectiveFieldOfView((float) fovyRadians, aspect, 1, 1000);

            return perspectiveMatrix;
        }

        private void Render()
        {
            if (!_glControlLoaded)
            {
                return;
            }

            SetupScene();

            GL.Rotate(_angle, _axis);

            //GL.Rotate(_horizontalAngle, 0f, 1f, 0f);
            
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
            DependencyProperty.Register("Meshes", typeof (List<Mesh>), typeof (OpenGlUserControl), new PropertyMetadata(default(List<Mesh>), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var openGlUserControl = (OpenGlUserControl) dependencyObject;
            openGlUserControl.Render();
        }

        private Vector2 _oldMousePosition;
        private bool _isPressed;
        private Point _mouseDownPosition;
        private Vector3 _axis;
        private float _angle;

        public List<Mesh> Meshes
        {
            get { return (List<Mesh>) GetValue(MeshesProperty); }
            set { SetValue(MeshesProperty, value);
            }
        }

        private void OnGlControlMouseMove(object sender, MouseEventArgs e)
        {
            var mouseDelta = GetMouseDelta(e);

            if(e.Button == MouseButtons.Left)
            {
                Transform(e.Location);
                Render();
            }


            if(_isPressed)
            {
                var v1 = TransformToSphere(_mouseDownPosition);
                var v2 = TransformToSphere(e.Location);

                _axis = Vector3.Cross(v1, v2);
                _angle = (float) (Vector3.CalculateAngle(v1, v2) * (180/Math.PI));

                //var delta = new Quaternion(axis, angle);
                //var q = new Quaternion(_axis, (float)_angle);

                Render();
            }
            else
            {
                _axis = new Vector3(0, 0, 1);
                _angle = 0;
                Render();
            }
        }

        private Vector3 TransformToSphere(Point location)
        {
            var x = location.X / (glControl.Width / 2.0);
            var y = location.Y / (glControl.Height / 2.0);

            x = x - 1;
            y = 1 - y;

            double z2 = 1 - x * x - y * y;
            double z = z2 > 0 ? Math.Sqrt(z2) : 0;

            var vector3 = new Vector3((float)x, (float)y, (float)z);
            vector3.Normalize();

            return vector3;
        }

        private void Transform(Point location)
        {


            //var axis = Vector3.Cross(_oldSpherePosition, vector3);
            //var angle = Vector3.CalculateAngle(_oldSpherePosition, vector3);

            //var delta = new Quaternion(axis, angle);
            //var q = new Quaternion(_axis, (float) _angle);

            //q *= delta;

            //_axis = q.Xyz;
            //_angle = q.W;

            //_oldSpherePosition = vector3;
            ////Vector3D p = new Vector3D(x, y, z);
            ////p.Normalize();
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

        private Vector2 GetMouseDelta(MouseEventArgs e)
        {
            var newMousePosition = new Vector2(e.X, e.Y);
            var mouseDelta = newMousePosition - _oldMousePosition;

            _oldMousePosition = newMousePosition;
            return mouseDelta;
        }

        private void OnGlControlMouseDown(object sender, MouseEventArgs e)
        {
            _isPressed = e.Button == MouseButtons.Left;
            _mouseDownPosition = e.Location;
        }

        private void OnGlControlMouseUp(object sender, MouseEventArgs e)
        {
            _isPressed = !(_isPressed && e.Button == MouseButtons.Left);
        }
    }
}
