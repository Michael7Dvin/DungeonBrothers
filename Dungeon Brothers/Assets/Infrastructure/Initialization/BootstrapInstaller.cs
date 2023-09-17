using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Initialization
{
    public class BootstrapInstaller : LifetimeScope
    {
        [SerializeField] private Bootstrapper _bootstrapper;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_bootstrapper).AsImplementedInterfaces();
        }
    }
}