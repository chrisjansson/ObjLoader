using System.Windows.Forms;

namespace CjClutter.ObjLoader.Viewer.Adapters
{
    public class WinFormsSizeAdapter : ISizeAdapter
    {
        public Control Source { get; set; }

        public double Width
        {
            get { return Source.Width; }
        }

        public double Height
        {
            get { return Source.Height; }
        }
    }
}