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
        private readonly GameplayUIAddresses _gameplayUIAddresses;

        public CharacterFactory(IAddressablesLoader addressablesLoader,
            IObjectResolver objectResolver,
            ICharactersProvider charactersProvider,
            ITurnQueueViewFactory turnQueueViewFactory,
            IStaticDataProvider staticDataProvider)
        {
            _addressablesLoader = addressablesLoader;
            _objectResolver = objectResolver;
            _charactersProvider = charactersProvider;
            _turnQueueViewFactory = turnQueueViewFactory;
            _gameplayUIAddresses = staticDataProvider.AssetsAddresses.AllUIAddresses.GameplayUIAddresses;
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

            ICharacterLogic characterLogic = CreateCharacterLogic(config);

            Character character = gameObject.GetComponent<Character>();

            character.Construct(config.ID, config.Team, movementStats, characterStats, characterDamage,characterLogic);
            CreateHealthBar(character);

            CharacterInTurnQueueIcon icon = await _turnQueueViewFactory.CreateIcon(config.Image, config.ID);
            icon.gameObject.SetActive(false);
            
            _charactersProvider.Add(character, icon);

            return character;
        }

        private async UniTask CreateHealthBar(Character character)
        {
            GameObject prefab = await _addressablesLoader.LoadGameObject(_gameplayUIAddresses.HealthBar);
            GameObject gameObject = _objectResolver.Instantiate(prefab, character.Transform);

            HealthBarPresenter healthBarPresenter = gameObject.GetComponent<HealthBarPresenter>();
            healthBarPresenter.Construct(character.CharacterLogic.Health);

            gameObject.transform.position = character.Transform.position - new Vector3(0, 0.5f);
            healthBarPresenter.Initialize();
        }

        private CharacterDamage CreateCharacterDamage(CharacterConfig config, CharacterStats stats) =>
            new (config.Damage, stats);

        private MovementStats CreateMovementStats(CharacterConfig config) => 
            new (config.MovePoints, config.IsMoveThroughObstacles);

        private CharacterStats CreateCharacterStats(CharacterConfig config) =>
            new (config.Level, config.MainAttribute,config.Intelligence, config.Strength, config.Dexterity,
                config.Initiative);

        private ICharacterLogic CreateCharacterLogic(CharacterConfig config)
        {
            Health health = CreateHealth(config);
            
            ICharacterLogic characterLogic = new CharacterLogic(health);
            
            _objectResolver.Inject(characterLogic);

            return characterLogic;
        }

        private Health CreateHealth(CharacterConfig config) => 
            new (config.HealthPoints);
    }
}