using OpenTK;

namespace CjClutter.ObjLoader.Viewer
{
    public class Camera
    {
        public Camera()
        {
            Position = new Vector3d(0, 1, 20);
            Target = new Vector3d(0, 0, 0);
            Up = new Vector3d(0, 1, 0);
        }

        public Vector3d Position { get; set; }
        public Vector3d Target { get; set; }
        public Vector3d Up { get; set; }

        public Matrix4d GetCameraMatrix()
        {
            return Matrix4d.LookAt(Position, Target, Up);
        }
    }
}