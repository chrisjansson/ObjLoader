using System.Windows.Forms;

namespace CjClutter.ObjLoader.Viewer.InputProcessor
{
    public interface IMouseInputAdapter
    {
        Control Source { get; set; }
        IMouseInputTarget Target { get; set; }
    }
}