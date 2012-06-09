using OpenTK;

namespace CjClutter.ObjLoader.Viewer.InputAdapters
{
    public struct MouseDragEventArgs
    {
        public MouseDragEventArgs(Vector2d startPosition, Vector2d endPosition) : this()
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
        }

        public Vector2d StartPosition { get; private set; }
        public Vector2d EndPosition { get; private set; }
    }
}