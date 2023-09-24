using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.MapGenerator;
using CodeBase.Infrastructure.Services.Factories.Buttons;
using CodeBase.Infrastructure.Services.Factories.Characters;
using CodeBase.Infrastructure.Services.Factories.TurnQueue;
using CodeBase.Infrastructure.Services.Factories.UI;
using CodeBase.Infrastructure.Services.Providers.SceneServicesProvider;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services.Providers.LevelSpawner
{
    public class LevelSpawner : ILevelSpawner
    {
        private readonly ISceneServicesProvider _sceneServicesProvider;
        private readonly IMapGenerator _mapGenerator;
        private readonly ICommonUIFactory _commonUIFactory;
        private readonly ICharacterFactory _characterFactory;
        private readonly ITurnQueueViewFactory _turnQueueViewFactory;
        private readonly IButtonsFactory _buttonsFactory;
        private readonly Dictionary<CharacterID, CharacterConfig> _charactersConfigs;

        public LevelSpawner(ISceneServicesProvider sceneServicesProvider,
            ICommonUIFactory commonUIFactory,
            ICharacterFactory characterFactory,
            ITurnQueueViewFactory turnQueueViewFactory,
            IMapGenerator mapGenerator,
            IButtonsFactory buttonsFactory,
            IStaticDataProvider staticDataProvider)
        {
            _sceneServicesProvider = sceneServicesProvider;
            _commonUIFactory = commonUIFactory;
            _characterFactory = characterFactory;
            _turnQueueViewFactory = turnQueueViewFactory;
            _mapGenerator = mapGenerator;
            _buttonsFactory = buttonsFactory;
            _charactersConfigs = staticDataProvider.AllCharactersConfigs.CharacterConfigs;
        }

        public void Initialize() => 
            _sceneServicesProvider.SetLevelDataProvider(this);

        public async UniTask WarmUp()
        {
            await _commonUIFactory.WarmUp();
            await _buttonsFactory.WarmUp();
            await _characterFactory.WarmUp(_charactersConfigs.Values.ToList());
        }

        public async UniTask Spawn()
        {
            await _mapGenerator.GenerateMap();
            
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
            await _characterFactory.Create(_charactersConfigs[CharacterID.Hero]);
            await _characterFactory.Create(_charactersConfigs[CharacterID.Enemy]);
            await _characterFactory.Create(_charactersConfigs[CharacterID.Enemy]);
            await _characterFactory.Create(_charactersConfigs[CharacterID.Enemy]);
            await _characterFactory.Create(_charactersConfigs[CharacterID.Enemy]);
        }
    }
}