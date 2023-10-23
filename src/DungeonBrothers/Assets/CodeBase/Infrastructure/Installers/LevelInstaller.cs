using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Services.AI;
using CodeBase.Gameplay.Services.AI.Behaviours;
using CodeBase.Gameplay.Services.Attack;
using CodeBase.Gameplay.Services.InteractionsService;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Services.MapGenerator;
using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Services.Raycast;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Spawner.CharacterSpawner;
using CodeBase.Gameplay.Tiles;
using CodeBase.Gameplay.Tiles.Visualisation.ActiveCharacter;
using CodeBase.Gameplay.Tiles.Visualisation.Attack;
using CodeBase.Gameplay.Tiles.Visualisation.Path;
using CodeBase.Gameplay.Tiles.Visualisation.PathFinder;
using CodeBase.Gameplay.Tiles.Visualisation.Select;
using CodeBase.Infrastructure.Services.Factories.Buttons;
using CodeBase.Infrastructure.Services.Factories.Cameras;
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
            RegisterVisualizers(builder);
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
            builder.Register<ICameraFactory, CameraFactory>(Lifetime.Singleton);
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<ITurnQueue, TurnQueue>(Lifetime.Singleton);
            builder.Register<IMapGenerator, MapGenerator>(Lifetime.Singleton);
            builder.Register<IMapService, MapService>(Lifetime.Singleton);
            builder.Register<ILevelSpawner, LevelSpawner>(Lifetime.Singleton);
            builder.Register<ICharactersSpawner, CharactersSpawner>(Lifetime.Singleton);
            builder.Register<IPathFinder, PathFinder>(Lifetime.Singleton);
            builder.Register<IMoverService, MoverService>(Lifetime.Singleton);
            builder.Register<IInteractionService, InteractionService>(Lifetime.Singleton);
            builder.Register<IRaycastService, RaycastService>(Lifetime.Singleton);
            builder.Register<ITileSelector, TileSelector>(Lifetime.Singleton);
            builder.Register<IAttackService, AttackService>(Lifetime.Singleton);
            
            builder.Register<IAIService, AIService>(Lifetime.Singleton);
            builder.Register<ISelectTargetBehaviour, SelectTargetBehaviour>(Lifetime.Singleton);
            builder.Register<IMoveBehaviour, MoveBehaviour>(Lifetime.Singleton);
            builder.Register<IAttackBehaviour, AttackBehaviour>(Lifetime.Singleton);
        }

        private void RegisterVisualizers(IContainerBuilder builder)
        {
            builder.Register<IPathFinderVisualizer, PathFinderVisualizer>(Lifetime.Singleton);
            builder.Register<IActiveCharacterTileVisualizer, ActiveCharacterTileVisualizer>(Lifetime.Singleton);
            builder.Register<ISelectedTileVisualizer, SelectedTileVisualizer>(Lifetime.Singleton);
            builder.Register<IPathVisualizer, PathVisualizer>(Lifetime.Singleton);
            builder.Register<IAttackableTilesVisualizer, AttackableTilesVisualizer>(Lifetime.Singleton);
        }
    }
}
