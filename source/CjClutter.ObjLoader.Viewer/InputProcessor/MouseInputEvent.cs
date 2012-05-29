using OpenTK;

namespace CjClutter.ObjLoader.Viewer.InputProcessor
{
    public struct MouseInputEvent
    {
        public MouseInputEvent(Vector2d position)
        {
            Position = position;
        }

        public readonly Vector2d Position;
    }
}