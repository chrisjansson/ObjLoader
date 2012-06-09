using OpenTK;

namespace CjClutter.ObjLoader.Viewer.InputAdapters
{
    public interface IMouseInputTarget
    {
        void OnMouseMove(Vector2d position);
        void OnLeftMouseButtonDown(Vector2d position);
        void OnLeftMouseButtonUp(Vector2d position);
        void OnMouseDrag(MouseDragEventArgs mouseDragEventArgs);
        void OnMouseDragEnd(MouseDragEventArgs mouseDragEventArgs);
    }
}