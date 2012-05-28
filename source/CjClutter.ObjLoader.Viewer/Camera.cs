using OpenTK;

namespace CjClutter.ObjLoader.Viewer
{
    public class Camera
    {
        public Camera()
        {
            Position = new Vector3d(0, 0, 20);
            Target = new Vector3d(0, 0, 0);
            Up = new Vector3d(0, 1, 0);
        }

        public Vector3d Position { get; set; }
        public Vector3d Target { get; set; }
        public Vector3d Up { get; set; }

        public Matrix4d GetCameraMatrix()
        {
            Position.Normalize();
            Target.Normalize();
            Up.Normalize();

            var look = Position - Target;
            look.Normalize();
            var right = Vector3d.Cross(Up, look);

            return Matrix4d.LookAt(Position, Target, Up);
        }
    }
}