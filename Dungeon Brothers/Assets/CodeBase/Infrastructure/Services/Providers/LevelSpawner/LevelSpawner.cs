using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.MapGenerator;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Spawner.CharacterSpawner;
using CodeBase.Infrastructure.Services.Factories.Buttons;
using CodeBase.Infrastructure.Services.Factories.Characters;
using CodeBase.Infrastructure.Services.Factories.TurnQueue;
using CodeBase.Infrastructure.Services.Factories.UI;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Providers.LevelSpawner
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
        private readonly Dictionary<CharacterID, CharacterConfig> _charactersConfigs;

        public LevelSpawner(ICommonUIFactory commonUIFactory,
            ICharacterFactory characterFactory,
            ITurnQueueViewFactory turnQueueViewFactory,
            IMapGenerator mapGenerator,
            IButtonsFactory buttonsFactory,
            ICharactersSpawner charactersSpawner,
            IMapService mapService,
            IStaticDataProvider staticDataProvider)
        {
            _commonUIFactory = commonUIFactory;
            _characterFactory = characterFactory;
            _turnQueueViewFactory = turnQueueViewFactory;
            _mapGenerator = mapGenerator;
            _buttonsFactory = buttonsFactory;
            _charactersSpawner = charactersSpawner;
            _mapService = mapService;
            _charactersConfigs = staticDataProvider.AllCharactersConfigs.CharacterConfigs;
        }
        
        public async UniTask WarmUp()
        {
            await _commonUIFactory.WarmUp();
            await _buttonsFactory.WarmUp();
            await _characterFactory.WarmUp(_charactersConfigs.Values.ToList());
        }

        public async UniTask Spawn()
        {
            _mapService.ResetMap(await _mapGenerator.GenerateMap());
            
            await CreateUI();
            await CreateCharacters();
        }

        private async Task CreateUI()
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
                { new Vector2Int(1, 0), _charactersConfigs[CharacterID.Enemy] },
                { new Vector2Int(2,0), _charactersConfigs[CharacterID.Enemy] },
                { new Vector2Int(3,0), _charactersConfigs[CharacterID.Enemy] },
                { new Vector2Int(4,0), _charactersConfigs[CharacterID.Enemy] }
            };
            
            Debug.Log("2");

            await _charactersSpawner.Spawn(charactersToSpawn);
        }
    }
}