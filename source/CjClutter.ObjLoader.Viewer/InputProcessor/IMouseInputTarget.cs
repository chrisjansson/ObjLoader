using OpenTK;

namespace CjClutter.ObjLoader.Viewer.InputProcessor
{
    public interface IMouseInputTarget
    {
        void OnMouseMove(Vector2d position);
        void OnLeftMouseButtonDown(Vector2d position);
        void OnLeftMouseButtonUp(Vector2d position);
    }
}