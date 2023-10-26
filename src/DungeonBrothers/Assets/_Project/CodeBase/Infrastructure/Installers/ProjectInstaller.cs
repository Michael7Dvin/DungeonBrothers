using CodeBase.Gameplay.Services.Random;
using CodeBase.Infrastructure.Services.AddressablesLoader.Loader;
using CodeBase.Infrastructure.Services.InputService;
using CodeBase.Infrastructure.Services.Logger;
using CodeBase.Infrastructure.Services.Providers.CameraProvider;
using CodeBase.Infrastructure.Services.Providers.CharactersProvider;
using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using CodeBase.Infrastructure.StateMachines.App.FSM;
using CodeBase.Infrastructure.StateMachines.App.States;
using CodeBase.UI.Services.UIProvider;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Installers
{
    public class ProjectInstaller : LifetimeScope
    {
        [SerializeField] private AllStaticData _allStaticData;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterStateMachine(builder);
            RegisterServices(builder);
            RegisterProviders(builder);
        }

        private void RegisterStateMachine(IContainerBuilder builder)
        {
            builder.Register<IAppStateMachine, AppStateMachine>(Lifetime.Singleton);
            
            builder.Register<InitializationState>(Lifetime.Singleton);
            builder.Register<GameplayState>(Lifetime.Singleton);
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<ICustomLogger, CustomLogger>(Lifetime.Singleton);
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
            builder.Register<IAddressablesLoader, AddressablesLoader>(Lifetime.Singleton);
            builder.Register<ILogWriter, LogWriter>(Lifetime.Singleton);
            builder.Register<IRandomService, RandomService>(Lifetime.Singleton);
            builder.Register<IInputService, InputService>(Lifetime.Singleton);
        }

        private void RegisterProviders(IContainerBuilder builder)
        {
            builder.Register<IUIProvider, UIProvider>(Lifetime.Singleton);
            builder.Register<ICharactersProvider, CharactersProvider>(Lifetime.Singleton);
            builder.Register<ICameraProvider, CameraProvider>(Lifetime.Singleton);
            
            builder
                .Register<IStaticDataProvider, StaticDataProvider>(Lifetime.Singleton)
                .WithParameter(_allStaticData);
        }
    }
}