using _Project.CodeBase.Gameplay.Services.AI;
using _Project.CodeBase.Gameplay.Services.TurnQueue;
using _Project.CodeBase.Gameplay.Services.Visualizers.ActiveCharacter;
using _Project.CodeBase.Gameplay.Services.Visualizers.Attackable;
using _Project.CodeBase.Gameplay.Services.Visualizers.Path;
using _Project.CodeBase.Gameplay.Services.Visualizers.Select;
using _Project.CodeBase.Gameplay.Services.Visualizers.Walkable;
using _Project.CodeBase.Gameplay.Tiles;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Infrastructure.Services.Providers.LevelSpawner;
using _Project.CodeBase.Infrastructure.StateMachines.Common.States;
using _Project.CodeBase.Infrastructure.StateMachines.Gameplay.FSM;

namespace _Project.CodeBase.Infrastructure.StateMachines.Gameplay.States
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
            
            await _levelSpawner.Spawn();
            _gameplayStateMachine.Enter<BattleState>();
        }

        public void Exit()
        {
        }
    }
}