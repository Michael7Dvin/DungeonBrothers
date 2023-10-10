using CodeBase.Common.FSM.States;
using CodeBase.Gameplay.Services.AI;
using CodeBase.Gameplay.Services.InteractionsService;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Tiles;
using CodeBase.Gameplay.Tiles.Visualisation;
using CodeBase.Gameplay.Tiles.Visualisation.ActiveCharacter;
using CodeBase.Gameplay.Tiles.Visualisation.Attack;
using CodeBase.Gameplay.Tiles.Visualisation.Path;
using CodeBase.Gameplay.Tiles.Visualisation.PathFinder;
using CodeBase.Gameplay.Tiles.Visualisation.Select;
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
        private readonly IVisualizationPathToTile _visualizationPathToTile;
        private readonly IVisualizationAttackedTiles _visualizationAttack;

        private readonly IAIService _aiService;
        
        public LevelLoadingState(ILevelSpawner levelSpawner, 
            ITurnQueue turnQueue, 
            IGameplayStateMachine
                gameplayStateMachine, 
            IInputService inputService,
            ITileVisualizationActiveCharacter tileVisualizationActiveCharacter,
            IPathFinderVisualization pathFinderVisualization,
            ISelectedTileVisualisation selectedTileVisualisation,
            ITileSelector tileSelector,
            IVisualizationPathToTile visualizationPathToTile,
            IVisualizationAttackedTiles visualizationAttack,
            IAIService aiService)
        {
            _levelSpawner = levelSpawner;
            _turnQueue = turnQueue;
            _gameplayStateMachine = gameplayStateMachine;
            _inputService = inputService;
            _visualizationActiveCharacter = tileVisualizationActiveCharacter;
            _pathFinderVisualization = pathFinderVisualization;
            _selectedTileVisualisation = selectedTileVisualisation;
            _tileSelector = tileSelector;
            _visualizationPathToTile = visualizationPathToTile;
            _visualizationAttack = visualizationAttack;

            _aiService = aiService;
        }

        public async void Enter()
        {
            _inputService.Initialization();
            _inputService.EnableInput();
            _turnQueue.Initialize();
            _tileSelector.Initialize();
            _visualizationPathToTile.Initialize();
            _selectedTileVisualisation.Initialize();
            _visualizationActiveCharacter.Initialize();
            _pathFinderVisualization.Initialize();
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