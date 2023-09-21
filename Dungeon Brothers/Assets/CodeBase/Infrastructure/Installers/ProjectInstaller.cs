using CodeBase.Gameplay.Services.MapGenerator;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Services.Random;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.UI.TurnQueue;
using CodeBase.Infrastructure.Addressable.Loader;
using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States;
using CodeBase.Infrastructure.Services.Factories.TileFactory;
using CodeBase.Infrastructure.Services.Factories.TurnQueue;
using CodeBase.Infrastructure.Services.Factories.UI;
using CodeBase.Infrastructure.Services.Logging;
using CodeBase.Infrastructure.Services.Providers.CharactersProvider;
using CodeBase.Infrastructure.Services.Providers.ServiceProvider;
using CodeBase.Infrastructure.Services.Providers.UIProvider;
using CodeBase.Infrastructure.Services.SceneLoading;
using CodeBase.Infrastructure.Services.StaticDataProviding;
using CodeBase.UI.TurnQueue;
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
            RegisterStaticDataProvider(builder);
        }

        private void RegisterStateMachine(IContainerBuilder builder)
        {
            builder.Register<Bootstrapper>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<IGameStateMachine, GameStateMachine>(Lifetime.Singleton);
            builder.Register<InitializationState>(Lifetime.Singleton);
            builder.Register<GameplayState>(Lifetime.Singleton);
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<ICustomLogger, CustomLogger>(Lifetime.Singleton);
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
            builder.Register<IAddressablesLoader, AddressablesLoader>(Lifetime.Singleton);
            builder.Register<ILogWriter, LogWriter>(Lifetime.Singleton);
            builder.Register<IUIProvider, UIProvider>(Lifetime.Singleton);
            builder.Register<IRandomService, RandomService>(Lifetime.Singleton);
            builder.Register<ICharactersProvider, CharactersProvider>(Lifetime.Singleton);
            builder.Register<IServiceProvider, ServiceProvider>(Lifetime.Singleton);
        }
        
        private void RegisterStaticDataProvider(IContainerBuilder builder)
        {
            builder
                .Register<IStaticDataProvider, StaticDataProvider>(Lifetime.Singleton)
                .WithParameter(_allStaticData);
        }
    }
}