using CodeBase.Common.FSM.States;
using CodeBase.Gameplay.Services.InteractionsService;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Tiles;
using CodeBase.Gameplay.Tiles.Visualisation;
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
        private readonly ITileVisualizationActiveCharacter _visualizationActiveCharacter;
        private readonly IPathFinderVisualization _pathFinderVisualization;
        private readonly ISelectedTileVisualisation _selectedTileVisualisation;
        private readonly ITileSelector _tileSelector;
        
        public LevelLoadingState(ILevelSpawner levelSpawner, 
            ITurnQueue turnQueue, 
            IGameplayStateMachine
                gameplayStateMachine, 
            IInputService inputService,
            ITileVisualizationActiveCharacter tileVisualizationActiveCharacter,
            IPathFinderVisualization pathFinderVisualization,
            ISelectedTileVisualisation selectedTileVisualisation,
            ITileSelector tileSelector)
        {
            _levelSpawner = levelSpawner;
            _turnQueue = turnQueue;
            _gameplayStateMachine = gameplayStateMachine;
            _inputService = inputService;
            _visualizationActiveCharacter = tileVisualizationActiveCharacter;
            _pathFinderVisualization = pathFinderVisualization;
            _selectedTileVisualisation = selectedTileVisualisation;
            _tileSelector = tileSelector;
        }

        public async void Enter()
        {
            _inputService.Initialization();
            _inputService.EnableInput();
            _turnQueue.Initialize();
            _tileSelector.Initialize();
            _selectedTileVisualisation.Initialize();
            _visualizationActiveCharacter.Initialize();
            _pathFinderVisualization.Initialize();
            
            await _levelSpawner.Spawn();
            _gameplayStateMachine.Enter<BattleState>();
        }

        public void Exit()
        {
        }
    }
}