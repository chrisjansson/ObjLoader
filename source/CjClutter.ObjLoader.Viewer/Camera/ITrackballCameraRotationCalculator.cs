using OpenTK;

namespace CjClutter.ObjLoader.Viewer.Camera
{
    public interface ITrackballCameraRotationCalculator
    {
        Quaterniond Calculate(Vector2d startPoint, Vector2d endPoint);
    }
}