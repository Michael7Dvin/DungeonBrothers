using System.Collections.Generic;
using System.Linq;
using _Project.CodeBase.Gameplay.Characters;
using _Project.CodeBase.Gameplay.Characters.CharacterInfo;
using _Project.CodeBase.Gameplay.Services.InteractionsService;
using _Project.CodeBase.Gameplay.Services.Map;
using _Project.CodeBase.Gameplay.Services.MapGenerator;
using _Project.CodeBase.Gameplay.Spawner.CharacterSpawner;
using _Project.CodeBase.Infrastructure.Services.Factories.Buttons;
using _Project.CodeBase.Infrastructure.Services.Factories.Cameras;
using _Project.CodeBase.Infrastructure.Services.Factories.Characters;
using _Project.CodeBase.Infrastructure.Services.Factories.TurnQueue;
using _Project.CodeBase.Infrastructure.Services.Factories.UI;
using _Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.Providers.LevelSpawner
{
    public class LevelSpawner : ILevelSpawner
    {
        private readonly IMapGenerator _mapGenerator;
        private readonly IMapService _mapService;
        private readonly ICommonUIFactory _commonUIFactory;
        private readonly ICharacterFactory _characterFactory;
        private readonly ITurnQueueViewFactory _turnQueueViewFactory;
        private readonly IButtonsFactory _buttonsFactory;
        private readonly ICharactersSpawner _charactersSpawner;
        private readonly IInteractionService _interactionService;
        private readonly ICameraFactory _cameraFactory;
        private readonly Dictionary<CharacterID, CharacterConfig> _charactersConfigs;

        public LevelSpawner(ICommonUIFactory commonUIFactory,
            ICharacterFactory characterFactory,
            ITurnQueueViewFactory turnQueueViewFactory,
            IMapGenerator mapGenerator,
            IButtonsFactory buttonsFactory,
            ICharactersSpawner charactersSpawner,
            IMapService mapService,
            IInteractionService interactionService,
            ICameraFactory cameraFactory,
            IStaticDataProvider staticDataProvider)
        {
            _commonUIFactory = commonUIFactory;
            _characterFactory = characterFactory;
            _turnQueueViewFactory = turnQueueViewFactory;
            _mapGenerator = mapGenerator;
            _buttonsFactory = buttonsFactory;
            _charactersSpawner = charactersSpawner;
            _mapService = mapService;
            _interactionService = interactionService;
            _cameraFactory = cameraFactory;
            _charactersConfigs = staticDataProvider.AllCharactersConfigs.CharacterConfigs;
        }
        
        public async UniTask WarmUp()
        {
            await _commonUIFactory.WarmUp();
            await _buttonsFactory.WarmUp();
            await _cameraFactory.WarmUp();
            await _characterFactory.WarmUp(_charactersConfigs.Values.ToList());
        }

        public async UniTask Spawn()
        {
            _mapService.ResetMap(await _mapGenerator.GenerateMap());
            
            await CreateUI();
            await CreateCharacters();
            await CreateCamera();
            
            _interactionService.Initialize();
        }

        private async UniTask CreateCamera() =>
            await _cameraFactory.Create();
        
        private async UniTask CreateUI()
        {
            await _commonUIFactory.Create();
            await _turnQueueViewFactory.CreateTurnQueueView();
            await _buttonsFactory.CreateSkipTurnButton();
        }

        private async UniTask CreateCharacters()
        {
            Dictionary<Vector2Int, CharacterConfig> charactersToSpawn = new()
            {
                { new Vector2Int(0, 0), _charactersConfigs[CharacterID.Hero] },
                { new Vector2Int(2,0), _charactersConfigs[CharacterID.Skeleton] },
                { new Vector2Int(3,0), _charactersConfigs[CharacterID.Skeleton] },
                { new Vector2Int(4,0), _charactersConfigs[CharacterID.Skeleton] }
            };
            
            await _charactersSpawner.Spawn(charactersToSpawn);
        }
    }
}