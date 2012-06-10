using System.Windows.Forms;

namespace CjClutter.ObjLoader.Viewer
{
    public partial class ShellView : IShellView
    {
        public ShellView()
        {
            InitializeComponent();
        }

        public Control GlControl
        {
            get { return openGlUserControl.glControl; }
        }
    }
}
