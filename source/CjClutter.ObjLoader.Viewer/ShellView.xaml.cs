using System.Windows;

namespace CjClutter.ObjLoader.Viewer
{
    public partial class ShellView : IShellView
    {
        public ShellView()
        {
            InitializeComponent();
        }

        public FrameworkElement GlControl
        {
            get { return openGlUserControl; }
        }
    }
}
