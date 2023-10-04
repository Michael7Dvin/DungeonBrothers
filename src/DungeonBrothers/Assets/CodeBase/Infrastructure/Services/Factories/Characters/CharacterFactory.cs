using System.Collections.Generic;
using CodeBase.Gameplay.Animations.Move;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Characters.CharacterInfo;
using CodeBase.Gameplay.Characters.Logic;
using CodeBase.Gameplay.Characters.View;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.UI.Gameplay;
using CodeBase.Infrastructure.Services.AddressablesLoader.Loader;
using CodeBase.Infrastructure.Services.Factories.TurnQueue;
using CodeBase.Infrastructure.Services.Logger;
using CodeBase.Infrastructure.Services.Providers.CharactersProvider;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using CodeBase.UI.HealthBar;
using CodeBase.UI.TurnQueue;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Services.Factories.Characters
{
    public class CharacterFactory : ICharacterFactory
    {
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly IObjectResolver _objectResolver;
        private readonly ICharactersProvider _charactersProvider;
        private readonly ITurnQueue _turnQueue;
        private readonly ITurnQueueViewFactory _turnQueueViewFactory;
        private readonly ICustomLogger _customLogger;
        private readonly GameplayUIAddresses _gameplayUIAddresses;
        private readonly BonusDamageConfig _bonusDamageConfig;

        public CharacterFactory(IAddressablesLoader addressablesLoader,
            IObjectResolver objectResolver,
            ICharactersProvider charactersProvider,
            ITurnQueueViewFactory turnQueueViewFactory,
            ICustomLogger customLogger,
            IStaticDataProvider staticDataProvider)
        {
            _addressablesLoader = addressablesLoader;
            _objectResolver = objectResolver;
            _charactersProvider = charactersProvider;
            _turnQueueViewFactory = turnQueueViewFactory;
            _gameplayUIAddresses = staticDataProvider.AssetsAddresses.AllUIAddresses.GameplayUIAddresses;
            _bonusDamageConfig = staticDataProvider.GameBalanceConfig.BonusDamageConfig;
            _customLogger = customLogger;
        }

        public async UniTask WarmUp(List<CharacterConfig> characterConfigs)
        {
            foreach (var character in characterConfigs) 
                await _addressablesLoader.LoadGameObject(character.Prefab);

            await _addressablesLoader.LoadGameObject(_gameplayUIAddresses.HealthBar);
        }

        public async UniTask<Character> Create(CharacterConfig config)
        {
            GameObject prefab = await _addressablesLoader.LoadGameObject(config.Prefab);
            GameObject gameObject = _objectResolver.Instantiate(prefab);

            CharacterStats characterStats = CreateCharacterStats(config);
            MovementStats movementStats = CreateMovementStats(config);
            CharacterDamage characterDamage = CreateCharacterDamage(config, characterStats);

            ICharacterLogic characterLogic = CreateCharacterLogic(config, gameObject);

            Character character = gameObject.GetComponent<Character>();

            CharacterInTurnQueueIcon icon = await _turnQueueViewFactory.CreateIcon(config.Image, config.ID);
            icon.gameObject.SetActive(false);
            
            character.Construct(config.ID, config.Team, movementStats, characterStats, characterDamage, icon, characterLogic);
            await CreateHealthBar(character);
            
            _charactersProvider.Add(character, icon);

            return character;
        }

        private async UniTask CreateHealthBar(Character character)
        {
            GameObject prefab = await _addressablesLoader.LoadGameObject(_gameplayUIAddresses.HealthBar);
            GameObject gameObject = _objectResolver.Instantiate(prefab, character.Transform);

            HealthBarView healthBarView = gameObject.GetComponent<HealthBarView>();
            HealthBarPresenter healthBarPresenter = new HealthBarPresenter();
            
            healthBarPresenter.Construct(character.CharacterLogic.Health, healthBarView);

            gameObject.transform.position = character.Transform.position - new Vector3(0, 0.5f);
            healthBarPresenter.Initialize();
        }

        private CharacterDamage CreateCharacterDamage(CharacterConfig config, CharacterStats stats) =>
            new(_bonusDamageConfig.TotalBonusDamagePerMainStat, _bonusDamageConfig.TotalBonusDamagePerLevel,
                config.Damage, stats, config.characterAttackType, _customLogger);

        private MovementStats CreateMovementStats(CharacterConfig config) => 
            new (config.MovePoints, config.IsMoveThroughObstacles);

        private CharacterStats CreateCharacterStats(CharacterConfig config) =>
            new (config.Level, config.MainAttribute,config.Intelligence, config.Strength, config.Dexterity,
                config.Initiative);

        private ICharacterLogic CreateCharacterLogic(CharacterConfig config, GameObject gameObject)
        {
            Health health = gameObject.GetComponent<Health>();
            health.Construct(config.HealthPoints);
            _objectResolver.Inject(health);
            
            ICharacterLogic characterLogic = new CharacterLogic(health);
            
            _objectResolver.Inject(characterLogic);

            return characterLogic;
        }
    }
}