using System;
using System.Linq.Expressions;
using Caliburn.Micro;

namespace CjClutter.ObjLoader.Viewer
{
    public class Screen<TView> : Screen
    {
        protected TView View { get; set; }

        protected override void OnViewAttached(object view, object context)
        {
            View = (TView) view;
        }

        protected void Set<T>(Action<T> setter, Expression<Func<T>> getter, T value)
        {
            setter(value);
            NotifyOfPropertyChange(getter);
        }
    }
}