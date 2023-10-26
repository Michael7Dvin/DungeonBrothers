using CodeBase.Infrastructure.StateMachines.App;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Installers
{
    public class BootstrapInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder) => 
            builder.Register<AppBootstrapper>(Lifetime.Singleton).AsImplementedInterfaces();
    }
}