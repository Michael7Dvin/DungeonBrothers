using CodeBase.Common.FSM.States;
using CodeBase.Gameplay.Services.AI;
using CodeBase.Gameplay.Services.InteractionsService;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Services.Visualizers.ActiveCharacter;
using CodeBase.Gameplay.Services.Visualizers.Attack;
using CodeBase.Gameplay.Services.Visualizers.Path;
using CodeBase.Gameplay.Services.Visualizers.Select;
using CodeBase.Gameplay.Services.Visualizers.Walkable;
using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.Services.InputService;
using CodeBase.Infrastructure.Services.Providers.LevelSpawner;
using CodeBase.Infrastructure.StateMachines.Gameplay.FSM;

namespace CodeBase.Infrastructure.StateMachines.Gameplay.States
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
            IActiveCharacterVisualizer activeCharacterVisualizer,
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
            _visualizer = activeCharacterVisualizer;
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