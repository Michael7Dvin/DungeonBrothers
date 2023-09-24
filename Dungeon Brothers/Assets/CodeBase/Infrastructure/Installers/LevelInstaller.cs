using CodeBase.Gameplay.Services.MapGenerator;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.Factories.Buttons;
using CodeBase.Infrastructure.Services.Factories.Characters;
using CodeBase.Infrastructure.Services.Factories.TileFactory;
using CodeBase.Infrastructure.Services.Factories.TurnQueue;
using CodeBase.Infrastructure.Services.Factories.UI;
using CodeBase.Infrastructure.Services.Providers.LevelSpawner;
using CodeBase.Infrastructure.StateMachines.Gameplay;
using CodeBase.Infrastructure.StateMachines.Gameplay.FSM;
using CodeBase.Infrastructure.StateMachines.Gameplay.States;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Installers
{
    public class LevelInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterStateMachine(builder);
            RegisterServices(builder);
            RegisterFactories(builder);
        }

        private void RegisterStateMachine(IContainerBuilder builder)
        {
            builder.Register<GameplayBootstrapper>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<IGameplayStateMachine, GameplayStateMachine>(Lifetime.Singleton);

            builder.Register<LevelLoadingState>(Lifetime.Singleton);
            builder.Register<BattleState>(Lifetime.Singleton);
        }

        private void RegisterFactories(IContainerBuilder builder)
        {
            builder.Register<ITileFactory, TileFactory>(Lifetime.Singleton);
            builder.Register<ICommonUIFactory, CommonUIFactory>(Lifetime.Singleton);
            builder.Register<ITurnQueueViewFactory, TurnQueueViewFactory>(Lifetime.Singleton);
            builder.Register<ICharacterFactory, CharacterFactory>(Lifetime.Singleton);
            builder.Register<IButtonsFactory, ButtonsFactory>(Lifetime.Singleton);
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<ITurnQueue, TurnQueue>(Lifetime.Singleton);
            builder.Register<IMapGenerator, MapGenerator>(Lifetime.Singleton);
            builder.Register<IMapService, MapService>(Lifetime.Singleton);
            builder.Register<ILevelSpawner, LevelSpawner>(Lifetime.Singleton);
        }
    }
}
