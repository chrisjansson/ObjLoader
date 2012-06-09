using System;
using OpenTK;

namespace CjClutter.ObjLoader.Viewer.Camera
{
    public interface ICamera
    {
        Vector3d Position { get; set; }
        Vector3d Target { get; set; }
        Vector3d Up { get; set; }
        Matrix4d GetCameraMatrix();
        event Action CameraChanged;
    }
}