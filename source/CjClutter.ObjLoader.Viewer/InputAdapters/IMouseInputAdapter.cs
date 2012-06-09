using System.Windows.Forms;

namespace CjClutter.ObjLoader.Viewer.InputAdapters
{
    public interface IMouseInputAdapter
    {
        Control Source { get; set; }
        IMouseInputTarget Target { get; set; }
    }
}