using System.Collections.Generic;
using System.Linq;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.MapGenerator;
using CodeBase.Gameplay.Services.TurnQueue;
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
        private readonly ITurnQueue _turnQueue;
        private readonly IButtonsFactory _buttonsFactory;
        private readonly Dictionary<CharacterID, CharacterConfig> _characterConfigs;

        public LevelSpawner(ISceneServicesProvider sceneServicesProvider,
            ICommonUIFactory commonUIFactory,
            ICharacterFactory characterFactory,
            ITurnQueueViewFactory turnQueueViewFactory,
            ITurnQueue turnQueue,
            IMapGenerator mapGenerator,
            IButtonsFactory buttonsFactory,
            IStaticDataProvider staticDataProvider)
        {
            _sceneServicesProvider = sceneServicesProvider;
            _commonUIFactory = commonUIFactory;
            _characterFactory = characterFactory;
            _turnQueueViewFactory = turnQueueViewFactory;
            _turnQueue = turnQueue;
            _mapGenerator = mapGenerator;
            _buttonsFactory = buttonsFactory;
            _characterConfigs = staticDataProvider.AllCharactersConfigs.CharacterConfigs;
        }

        public async UniTask WarmUp()
        {
            await _commonUIFactory.WarmUp();
            await _buttonsFactory.WarmUp();
        }

        public async UniTask CreateLevel()
        {
            await _commonUIFactory.Create();
            _turnQueue.Initialize();
            
            await _mapGenerator.GenerateMap();
            await _turnQueueViewFactory.CreateTurnQueueView();

            await _characterFactory.WarmUp(_characterConfigs.Values.ToList());
            
            await _characterFactory.Create(_characterConfigs[CharacterID.Hero]);
            await _characterFactory.Create(_characterConfigs[CharacterID.Enemy]);
            await _characterFactory.Create(_characterConfigs[CharacterID.Enemy]); 
            await _characterFactory.Create(_characterConfigs[CharacterID.Enemy]);
            await _characterFactory.Create(_characterConfigs[CharacterID.Enemy]);

            _turnQueue.SetFirstTurn();
            await _buttonsFactory.CreateSkipTurnButton();
        }

        public void Initialize() => 
            _sceneServicesProvider.SetLevelDataProvider(this);
    }
}