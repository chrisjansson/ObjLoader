using OpenTK;

namespace CjClutter.ObjLoader.Viewer.InputAdapters
{
    public struct MouseMoveInputEventArguments
    {
        public MouseMoveInputEventArguments(Vector2d position)
        {
            Position = position;
        }

        public readonly Vector2d Position;
    }
}