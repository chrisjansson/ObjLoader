using System;
using OpenTK;

namespace CjClutter.ObjLoader.Viewer.Camera
{
    public class CameraBase
    {
        public event Action CameraChanged;

        protected void FireCameraChanged()
        {
            if (CameraChanged != null)
            {
                CameraChanged();
            }
        }

        public Vector3d Position { get; set; }
        public Vector3d Target { get; set; }
        public Vector3d Up { get; set; }
    }
}