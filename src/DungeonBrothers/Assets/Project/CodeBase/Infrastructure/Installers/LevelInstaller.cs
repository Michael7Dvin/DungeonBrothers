using Project.CodeBase.Gameplay.Rooms.Doors;
using Project.CodeBase.Gameplay.Services.AI;
using Project.CodeBase.Gameplay.Services.AI.Behaviours;
using Project.CodeBase.Gameplay.Services.Attack;
using Project.CodeBase.Gameplay.Services.Dungeon;
using Project.CodeBase.Gameplay.Services.InteractionsService;
using Project.CodeBase.Gameplay.Services.Map;
using Project.CodeBase.Gameplay.Services.MapGenerator;
using Project.CodeBase.Gameplay.Services.Move;
using Project.CodeBase.Gameplay.Services.PathFinder;
using Project.CodeBase.Gameplay.Services.Raycast;
using Project.CodeBase.Gameplay.Services.TurnQueue;
using Project.CodeBase.Gameplay.Services.Visualizers.ActiveCharacter;
using Project.CodeBase.Gameplay.Services.Visualizers.Attackable;
using Project.CodeBase.Gameplay.Services.Visualizers.Path;
using Project.CodeBase.Gameplay.Services.Visualizers.Select;
using Project.CodeBase.Gameplay.Services.Visualizers.Walkable;
using Project.CodeBase.Gameplay.Spawner.Character;
using Project.CodeBase.Gameplay.Spawner.Dungeon;
using Project.CodeBase.Gameplay.Spawner.Rooms;
using Project.CodeBase.Gameplay.Tiles;
using Project.CodeBase.Infrastructure.Services.Factories.Buttons;
using Project.CodeBase.Infrastructure.Services.Factories.Cameras;
using Project.CodeBase.Infrastructure.Services.Factories.Characters;
using Project.CodeBase.Infrastructure.Services.Factories.Rooms;
using Project.CodeBase.Infrastructure.Services.Factories.TileFactory;
using Project.CodeBase.Infrastructure.Services.Factories.TurnQueue;
using Project.CodeBase.Infrastructure.Services.Factories.UI;
using Project.CodeBase.Infrastructure.Services.Providers.LevelSpawner;
using Project.CodeBase.Infrastructure.StateMachines.Gameplay;
using Project.CodeBase.Infrastructure.StateMachines.Gameplay.FSM;
using Project.CodeBase.Infrastructure.StateMachines.Gameplay.States;
using VContainer;
using VContainer.Unity;

namespace Project.CodeBase.Infrastructure.Installers
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
            builder.Register<IRoomFactory, RoomFactory>(Lifetime.Singleton);
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
            builder.Register<IRoomSpawner, RoomSpawner>(Lifetime.Singleton);
            builder.Register<IDungeonSpawner, DungeonSpawner>(Lifetime.Singleton);
            builder.Register<IDungeonService, DungeonService>(Lifetime.Singleton);
            builder.Register<IDoorSelector, DoorSelector>(Lifetime.Singleton);
            
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
