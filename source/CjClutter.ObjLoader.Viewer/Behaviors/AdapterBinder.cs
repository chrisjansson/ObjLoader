using System.Windows;
using CjClutter.ObjLoader.Viewer.Adapters.InputAdapters;

namespace CjClutter.ObjLoader.Viewer.Behaviors
{
    public static class AdapterBinder
    {
        public static DependencyProperty InputTargetProperty =
            DependencyProperty.RegisterAttached(
                "InputTarget",
                typeof (IMouseInputTarget),
                typeof (AdapterBinder),
                new PropertyMetadata(null, OnInputTargetPropertyChanged));

        public static void SetInputTarget(UIElement source, IMouseInputTarget target)
        {
            source.SetValue(InputTargetProperty, target);    
        }

        private static IMouseInputTarget GetInputTarget(UIElement source)
        {
            return (IMouseInputTarget) source.GetValue(InputTargetProperty);
        }

        private static void OnInputTargetPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var source = (UIElement) dependencyObject;

            var mouseInputAdapter = GetInputAdapter(source) ?? new MouseInputAdapter();

            mouseInputAdapter.Source = source;
            mouseInputAdapter.Target = (IMouseInputTarget) eventArgs.NewValue;

            SetInputAdapter(source, mouseInputAdapter);
        }

        public static DependencyProperty InputAdapterProperty =
            DependencyProperty.RegisterAttached(
                "InputAdapter",
                typeof (IMouseInputAdapter),
                typeof (AdapterBinder),
                new PropertyMetadata());

        private static void SetInputAdapter(UIElement source, IMouseInputAdapter adapter)
        {
            source.SetValue(InputAdapterProperty, adapter);
        }

        private static IMouseInputAdapter GetInputAdapter(UIElement source)
        {
            return (IMouseInputAdapter) source.GetValue(InputAdapterProperty);
        }
    }
}