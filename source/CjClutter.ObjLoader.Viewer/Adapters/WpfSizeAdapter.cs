using System.Windows;

namespace CjClutter.ObjLoader.Viewer.Adapters
{
    public class WpfSizeAdapter : ISizeAdapter
    {
        public FrameworkElement Source { get; set; }

        public double Width
        {
            get { return Source.ActualWidth; }
        }

        public double Height
        {
            get { return Source.ActualHeight; }
        }
    }
}