using _Project.CodeBase.Gameplay.Services.Random;
using _Project.CodeBase.Infrastructure.Audio;
using _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Loader;
using _Project.CodeBase.Infrastructure.Services.Audio;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Infrastructure.Services.Logger;
using _Project.CodeBase.Infrastructure.Services.Providers.CameraProvider;
using _Project.CodeBase.Infrastructure.Services.Providers.CharactersProvider;
using _Project.CodeBase.Infrastructure.Services.SceneLoader;
using _Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using _Project.CodeBase.Infrastructure.StateMachines.App.FSM;
using _Project.CodeBase.Infrastructure.StateMachines.App.States;
using _Project.CodeBase.UI.Services.UIProvider;
using UnityEngine;
using UnityEngine.Audio;
using VContainer;
using VContainer.Unity;

namespace _Project.CodeBase.Infrastructure.Installers
{
    public class ProjectInstaller : LifetimeScope
    {
        [SerializeField] private AllStaticData _allStaticData;
        
        [SerializeField] private SoundPlayer _soundPlayer;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterStateMachine(builder);
            RegisterServices(builder);
            RegisterProviders(builder);
            RegisterPrefab(builder);
        }

        private void RegisterPrefab(IContainerBuilder builder)
        {
            builder
                .RegisterComponentInNewPrefab(_soundPlayer, Lifetime.Singleton)
                .DontDestroyOnLoad()
                .As<ISoundPlayer>();
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