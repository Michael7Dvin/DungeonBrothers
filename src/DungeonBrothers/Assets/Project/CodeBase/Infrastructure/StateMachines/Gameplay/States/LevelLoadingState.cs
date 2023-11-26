using Project.CodeBase.Gameplay.Services.AI;
using Project.CodeBase.Gameplay.Services.Dungeon;
using Project.CodeBase.Gameplay.Services.TurnQueue;
using Project.CodeBase.Gameplay.Services.Visualizers.ActiveCharacter;
using Project.CodeBase.Gameplay.Services.Visualizers.Attackable;
using Project.CodeBase.Gameplay.Services.Visualizers.Path;
using Project.CodeBase.Gameplay.Services.Visualizers.Select;
using Project.CodeBase.Gameplay.Services.Visualizers.Walkable;
using Project.CodeBase.Gameplay.Tiles;
using Project.CodeBase.Infrastructure.Services.InputService;
using Project.CodeBase.Infrastructure.Services.Providers.LevelSpawner;
using Project.CodeBase.Infrastructure.StateMachines.Common.States;
using Project.CodeBase.Infrastructure.StateMachines.Gameplay.FSM;

namespace Project.CodeBase.Infrastructure.StateMachines.Gameplay.States
{
    public class LevelLoadingState : IState
    {
        private readonly ILevelSpawner _levelSpawner;
        private readonly ITurnQueue _turnQueue;
        private readonly IGameplayStateMachine _gameplayStateMachine;
        private readonly IInputService _inputService;
        private readonly IActiveCharacterVisualizer _visualizer;
        private readonly IWalkableTilesVisualizer _walkableTilesVisualizer;
        private readonly ISelectedTileVisualizer _selectedTileVisualizer;
        private readonly ITileSelector _tileSelector;
        private readonly IPathVisualizer _pathVisualizer;
        private readonly IAttackableTilesVisualizer _visualizationAttack;
        private readonly IDungeonService _dungeonService;

        private readonly IAIService _aiService;
        
        public LevelLoadingState(ILevelSpawner levelSpawner, 
            ITurnQueue turnQueue, 
            IGameplayStateMachine gameplayStateMachine, 
            IInputService inputService,
            IActiveCharacterVisualizer activeCharacterTileVisualizer,
            IWalkableTilesVisualizer walkableTilesVisualizer,
            ISelectedTileVisualizer selectedTileVisualizer,
            ITileSelector tileSelector,
            IPathVisualizer pathVisualizer,
            IAttackableTilesVisualizer visualizationAttack,
            IDungeonService dungeonService,
            IAIService aiService)
        {
            _levelSpawner = levelSpawner;
            _turnQueue = turnQueue;
            _gameplayStateMachine = gameplayStateMachine;
            _inputService = inputService;
            _visualizer = activeCharacterTileVisualizer;
            _walkableTilesVisualizer = walkableTilesVisualizer;
            _selectedTileVisualizer = selectedTileVisualizer;
            _tileSelector = tileSelector;
            _pathVisualizer = pathVisualizer;
            _visualizationAttack = visualizationAttack;
            _dungeonService = dungeonService;

            _aiService = aiService;
        }

        public async void Enter()
        {
            _inputService.Initialization();
            _inputService.EnableInput();
            _turnQueue.Initialize();
            _tileSelector.Initialize();
            _pathVisualizer.Initialize();
            _selectedTileVisualizer.Initialize();
            _visualizer.Initialize();
            _walkableTilesVisualizer.Initialize();
            _visualizationAttack.Initialize();
            _aiService.Initialize();
            await _dungeonService.CreateDungeon();
            
            _gameplayStateMachine.Enter<IdleState>();
        }

        public void Exit()
        {
        }
    }
}