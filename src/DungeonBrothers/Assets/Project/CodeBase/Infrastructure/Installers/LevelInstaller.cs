using _Project.CodeBase.Gameplay.Services.AI;
using _Project.CodeBase.Gameplay.Services.AI.Behaviours;
using _Project.CodeBase.Gameplay.Services.Attack;
using _Project.CodeBase.Gameplay.Services.InteractionsService;
using _Project.CodeBase.Gameplay.Services.Map;
using _Project.CodeBase.Gameplay.Services.MapGenerator;
using _Project.CodeBase.Gameplay.Services.Move;
using _Project.CodeBase.Gameplay.Services.PathFinder;
using _Project.CodeBase.Gameplay.Services.Raycast;
using _Project.CodeBase.Gameplay.Services.TurnQueue;
using _Project.CodeBase.Gameplay.Services.Visualizers.ActiveCharacter;
using _Project.CodeBase.Gameplay.Services.Visualizers.Attackable;
using _Project.CodeBase.Gameplay.Services.Visualizers.Path;
using _Project.CodeBase.Gameplay.Services.Visualizers.Select;
using _Project.CodeBase.Gameplay.Services.Visualizers.Walkable;
using _Project.CodeBase.Gameplay.Spawner.CharacterSpawner;
using _Project.CodeBase.Gameplay.Tiles;
using _Project.CodeBase.Infrastructure.Services.Factories.Buttons;
using _Project.CodeBase.Infrastructure.Services.Factories.Cameras;
using _Project.CodeBase.Infrastructure.Services.Factories.Characters;
using _Project.CodeBase.Infrastructure.Services.Factories.TileFactory;
using _Project.CodeBase.Infrastructure.Services.Factories.TurnQueue;
using _Project.CodeBase.Infrastructure.Services.Factories.UI;
using _Project.CodeBase.Infrastructure.Services.Providers.LevelSpawner;
using _Project.CodeBase.Infrastructure.StateMachines.Gameplay;
using _Project.CodeBase.Infrastructure.StateMachines.Gameplay.FSM;
using _Project.CodeBase.Infrastructure.StateMachines.Gameplay.States;
using Project.CodeBase.Infrastructure.Services.Factories.Buttons;
using VContainer;
using VContainer.Unity;

namespace _Project.CodeBase.Infrastructure.Installers
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
            builder.Register<IWalkableTilesVisualizer, WalkableTilesVisualizer>(Lifetime.Singleton);
            builder.Register<IActiveCharacterVisualizer, ActiveCharacterVisualizer>(Lifetime.Singleton);
            builder.Register<ISelectedTileVisualizer, SelectedTileVisualizer>(Lifetime.Singleton);
            builder.Register<IPathVisualizer, PathVisualizer>(Lifetime.Singleton);
            builder.Register<IAttackableTilesVisualizer, AttackableTilesVisualizer>(Lifetime.Singleton);
        }
    }
}
