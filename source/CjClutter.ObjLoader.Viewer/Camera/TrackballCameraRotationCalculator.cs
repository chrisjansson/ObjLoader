using System;
using OpenTK;

namespace CjClutter.ObjLoader.Viewer.Camera
{
    public class TrackballCameraRotationCalculator : ITrackballCameraRotationCalculator
    {
        private const double Radius = 1.0;

        public Quaterniond Calculate(Vector2d startPoint, Vector2d endPoint)
        {
            if(startPoint == endPoint)
            {
                return Quaterniond.Identity;
            }

            var projectedStartPoint = ProjectToSphere(startPoint);
            var projectedEndPoint = ProjectToSphere(endPoint);

            var axis = Vector3d.Cross(projectedStartPoint, projectedEndPoint);
            var theta = Vector3d.Dot(projectedStartPoint, projectedEndPoint);

            return new Quaterniond(axis, theta);
       }

        private Vector3d ProjectToSphere(Vector2d point)
        {
            var x = point.X;
            var y = point.Y;
            var z = CalculateZCoordinate(point);

            var projectedPoint = new Vector3d(x, y, z);
            projectedPoint.Normalize();

            return projectedPoint;
        }

        private double CalculateZCoordinate(Vector2d point)
        {
            //If the point is further than away from the center than a constant we calculate the z value with a hyperbola based function instead
            //the distance is chosen for niceity
            double distance = Math.Sqrt(point.X * point.X + point.Y * point.Y);

            //Inside sphere
            if (distance < Radius * 0.70710678118654752440)
            {
                return Math.Sqrt(Radius * Radius - distance * distance);
            }

            //On hyperbola
            const double t = Radius / 1.41421356237309504880;
            return t * t / distance;
        }
    }
}