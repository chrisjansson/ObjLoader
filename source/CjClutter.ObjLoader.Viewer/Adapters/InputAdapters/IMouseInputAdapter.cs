using System.Windows;

namespace CjClutter.ObjLoader.Viewer.Adapters.InputAdapters
{
    public interface IMouseInputAdapter
    {
        UIElement Source { get; set; }
        IMouseInputTarget Target { get; set; }
    }
}