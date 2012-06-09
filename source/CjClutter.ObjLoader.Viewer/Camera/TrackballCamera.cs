using OpenTK;
using CjClutter.ObjLoader.Viewer.Extensions;

namespace CjClutter.ObjLoader.Viewer.Camera
{
    public class TrackballCamera : PerspectiveCamera, ITrackballCamera
    {
        private readonly ITrackballCameraRotationCalculator _cameraRotationCalculator;
        
        private Matrix4d _cameraOrientation = Matrix4d.Identity;
        private Matrix4d _tempCameraOrientation = Matrix4d.Identity;

        public TrackballCamera(ITrackballCameraRotationCalculator cameraRotationCalculator)
        {
            _cameraRotationCalculator = cameraRotationCalculator;
        }

        public override Matrix4d GetCameraMatrix()
        {
            var baseMatrix = base.GetCameraMatrix();
            var orientationMatrix = GetOrientationMatrix();

            return orientationMatrix.Multiply(baseMatrix);
        }

        private Matrix4d GetOrientationMatrix()
        {
            //return _cameraOrientation.Multiply(_tempCameraOrientation);
            return _tempCameraOrientation;
        }

        public void Rotate(Vector2d startPoint, Vector2d endPoint)
        {
            var rotation = CalculateRotation(startPoint, endPoint);
            var rotationMatrix = rotation.GetRotationMatrix();
            _tempCameraOrientation = _cameraOrientation.Multiply(rotationMatrix);

            FireCameraChanged();
        }

        public void CommitRotation(Vector2d startPoint, Vector2d endPoint)
        {
            var rotation = CalculateRotation(startPoint, endPoint);
            var rotationMatrix = rotation.GetRotationMatrix();

            _cameraOrientation = _cameraOrientation.Multiply(rotationMatrix);
            _tempCameraOrientation = _cameraOrientation;

            FireCameraChanged();
        }

        private Quaterniond CalculateRotation(Vector2d startPoint, Vector2d endPoint)
        {
            return _cameraRotationCalculator.Calculate(startPoint, endPoint);
        }
    }
}