using OpenTK;

namespace CjClutter.ObjLoader.Viewer.Extensions
{
    public static class Matrix4DExtensions
    {
        public static Matrix4d Multiply(this Matrix4d left, Matrix4d right)
        {
            Matrix4d result;
            Matrix4d.Mult(ref left, ref right, out result);

            return result;
        }

        public static Matrix4d GetRotationMatrix(this Quaterniond quaternion)
        {
            return Matrix4d.Rotate(quaternion);
        }
    }
}