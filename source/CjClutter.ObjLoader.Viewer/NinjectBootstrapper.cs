using Caliburn.Micro;
using Ninject;
using Ninject.Extensions.Conventions;
using ObjLoader.Loader.Loaders;

namespace CjClutter.ObjLoader.Viewer
{
    public class NinjectBootstrapper : Bootstrapper<IShell>
    {
        private StandardKernel _kernel;

        protected override void Configure()
        {
            _kernel = new StandardKernel();

            _kernel.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            _kernel.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();

            _kernel.Bind(x => x
                .FromThisAssembly()
                .SelectAllClasses()
                .BindAllInterfaces());

            _kernel.Bind(x => x
                .FromAssemblyContaining<IObjLoaderFactory>()
                .SelectAllClasses()
                .BindAllInterfaces());
        }

        protected override object GetInstance(System.Type service, string key)
        {
            return _kernel.Get(service);
        }

        protected override System.Collections.Generic.IEnumerable<object> GetAllInstances(System.Type service)
        {
            return _kernel.GetAll(service);
        }

        protected override void BuildUp(object instance)
        {
            _kernel.Inject(instance);
        }
    }
}