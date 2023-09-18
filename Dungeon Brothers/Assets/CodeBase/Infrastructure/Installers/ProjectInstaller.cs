using CodeBase.Gameplay.Services.MapGenerator;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Infrastructure.Addressable;
using CodeBase.Infrastructure.GameFSM.FSM;
using CodeBase.Infrastructure.GameFSM.States;
using CodeBase.Infrastructure.Services.Factories.TileFactory;
using CodeBase.Infrastructure.Services.Logging;
using CodeBase.Infrastructure.Services.ResourcesLoading;
using CodeBase.Infrastructure.Services.SceneLoading;
using CodeBase.Infrastructure.Services.StaticDataProviding;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Installers
{
    public class ProjectInstaller : LifetimeScope
    {
        [SerializeField] private AllAssetsAddresses _allAssetsAddresses;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterStateMachine(builder);
            RegisterServices(builder);
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
            builder
                .Register<IStaticDataProvider, StaticDataProvider>(Lifetime.Singleton)
                .WithParameter(_allAssetsAddresses);
            
            builder.Register<ICustomLogger, CustomLogger>(Lifetime.Singleton);
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
            builder.Register<IAddressablesLoader, AddressablesLoader>(Lifetime.Singleton);
            builder.Register<ILogWriter, LogWriter>(Lifetime.Singleton);
            builder.Register<ITileFactory, TileFactory>(Lifetime.Singleton);
            builder.Register<IMapGenerator, MapGenerator>(Lifetime.Singleton);
            builder.Register<IMapService, MapService>(Lifetime.Singleton);
        }
    }
}