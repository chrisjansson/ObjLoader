using Caliburn.Micro;

namespace CjClutter.ObjLoader.Viewer
{
    public class ViewModelBase<TView> : Screen
    {
        protected TView View { get; set; }

        protected override void OnViewAttached(object view, object context)
        {
            View = (TView) view;
        }
    }
}