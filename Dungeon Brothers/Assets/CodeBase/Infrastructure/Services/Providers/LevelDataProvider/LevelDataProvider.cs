using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.MapGenerator;
using CodeBase.Gameplay.Services.MapService;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.Factories.Characters;
using CodeBase.Infrastructure.Services.Factories.TurnQueue;
using CodeBase.Infrastructure.Services.Factories.UI;
using CodeBase.Infrastructure.Services.Providers.CharactersProvider;
using CodeBase.Infrastructure.Services.Providers.LevelDataProvider;
using CodeBase.Infrastructure.Services.Providers.ServiceProvider;
using CodeBase.Infrastructure.Services.StaticDataProviding;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services.Providers.LevelData
{
    public class LevelDataProvider : ILevelDataProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapService _mapService;
        private readonly IMapGenerator _mapGenerator;
        private readonly ICommonUIFactory _commonUIFactory;
        private readonly ICharacterFactory _characterFactory;
        private readonly ITurnQueueViewFactory _turnQueueViewFactory;
        private readonly ITurnQueue _turnQueue;
        private readonly ICharactersProvider _charactersProvider;
        private readonly IStaticDataProvider _staticDataProvider;

        public LevelDataProvider(IServiceProvider serviceProvider,
            ICommonUIFactory commonUIFactory,
            ICharacterFactory characterFactory,
            ITurnQueueViewFactory turnQueueViewFactory,
            ITurnQueue turnQueue,
            IStaticDataProvider staticDataProvider,
            ICharactersProvider charactersProvider,
            IMapService mapService,
            IMapGenerator mapGenerator)
        {
            _serviceProvider = serviceProvider;
            _commonUIFactory = commonUIFactory;
            _characterFactory = characterFactory;
            _turnQueueViewFactory = turnQueueViewFactory;
            _turnQueue = turnQueue;
            _staticDataProvider = staticDataProvider;
            _charactersProvider = charactersProvider;
            _mapService = mapService;
            _mapGenerator = mapGenerator;
        }

        public async UniTask WarmUp()
        {
            await _commonUIFactory.WarmUp();
        }

        public async UniTask CreateLevel()
        {
            await _commonUIFactory.Create();
            _turnQueue.Initialize();
            
            _charactersProvider.Add(await _characterFactory.Create(_staticDataProvider.AllCharactersConfigs.CharacterConfigs[CharacterID.Hero]),
                _staticDataProvider.AllCharactersConfigs.CharacterConfigs[CharacterID.Hero]);

            await _mapGenerator.GenerateMap();
        }

        public void Initialize() => _serviceProvider.SetLevelDataProvider(this);
    }
}