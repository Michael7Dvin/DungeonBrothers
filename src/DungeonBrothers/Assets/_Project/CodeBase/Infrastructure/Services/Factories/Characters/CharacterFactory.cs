﻿using System.Collections.Generic;
using _Project.CodeBase.Gameplay.Animations.Colors;
using _Project.CodeBase.Gameplay.Animations.Hit;
using _Project.CodeBase.Gameplay.Animations.Movement;
using _Project.CodeBase.Gameplay.Animations.Scale;
using _Project.CodeBase.Gameplay.Characters;
using _Project.CodeBase.Gameplay.Characters.CharacterInfo;
using _Project.CodeBase.Gameplay.Characters.Logic;
using _Project.CodeBase.Gameplay.Characters.View;
using _Project.CodeBase.Gameplay.Characters.View.Hit;
using _Project.CodeBase.Gameplay.Characters.View.Move;
using _Project.CodeBase.Gameplay.Characters.View.Sounds;
using _Project.CodeBase.Gameplay.Services.TurnQueue;
using _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.UI.Gameplay;
using _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Loader;
using _Project.CodeBase.Infrastructure.Services.Factories.TurnQueue;
using _Project.CodeBase.Infrastructure.Services.Logger;
using _Project.CodeBase.Infrastructure.Services.Providers.CharactersProvider;
using _Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using _Project.CodeBase.UI.HealthBar;
using _Project.CodeBase.UI.TurnQueue;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.CodeBase.Infrastructure.Services.Factories.Characters
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

        private readonly Vector3 _offset = new(0, 0.5f);
        
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

            CharacterStats stats = config.CharacterStats;
            CharacterDamage damage = CreateCharacterDamage(config, stats);

            ICharacterLogic logic = CreateCharacterLogic(gameObject, config);
            ICharacterView view = await CreateCharacterView(gameObject, config);
            
            Character character = gameObject.GetComponent<Character>();
            character.Construct(config.ID, config.Team, stats, damage, logic, view);
            
            await CreateHealthBar(character);
            
            _charactersProvider.Add(character, character.View.Icon);

            return character;
        }

        private async UniTask<ICharacterView> CreateCharacterView(GameObject gameObject,
            CharacterConfig config)
        {
            CharacterInTurnQueueIcon icon = await _turnQueueViewFactory.CreateIcon(config.Image, config.ID);
            icon.gameObject.SetActive(false);
            
            CharacterSounds characterSounds = SetupCharacterSounds(gameObject);

            IMovementView movementView = CreateMovementView(gameObject, characterSounds);
            IHitView hitView = CreateHitView(gameObject, characterSounds);
            
            CharacterView characterView = new CharacterView();
            characterView.Construct(icon, movementView, hitView);
            return characterView;
        }

        private IMovementView CreateMovementView(GameObject gameObject, 
            CharacterSounds characterSounds)
        {
            MovementAnimation movementAnimation = new MovementAnimation(gameObject.transform);

            IMovementView movementView = new MovementView(movementAnimation, characterSounds);
            _objectResolver.Inject(movementView);
            return movementView;
        }

        private CharacterSounds SetupCharacterSounds(GameObject gameObject)
        {
            CharacterSounds characterSounds = gameObject.GetComponent<CharacterSounds>();
            _objectResolver.Inject(characterSounds);
            return characterSounds;
        }

        private IHitView CreateHitView(GameObject gameObject,
            CharacterSounds characterSounds)
        {;
            ScaleAnimation scaleAnimation = new ScaleAnimation(gameObject.transform);

            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            ColorAnimation colorAnimation = new ColorAnimation(spriteRenderer);
            
            HitAnimation hitAnimation = new(scaleAnimation, colorAnimation);

            HitView hitView = new HitView(hitAnimation, characterSounds);
            _objectResolver.Inject(hitView);
            return hitView;
        }

        private async UniTask CreateHealthBar(Character character)
        {
            GameObject prefab = await _addressablesLoader.LoadGameObject(_gameplayUIAddresses.HealthBar);
            GameObject gameObject = _objectResolver.Instantiate(prefab, character.Transform);

            HealthBarView healthBarView = gameObject.GetComponent<HealthBarView>();
            HealthBarPresenter healthBarPresenter = new HealthBarPresenter();
            
            healthBarPresenter.Construct(character.Logic.Health, healthBarView);

            gameObject.transform.position = character.Transform.position - _offset;
            healthBarPresenter.Initialize();
        }

        private CharacterDamage CreateCharacterDamage(CharacterConfig config,
            CharacterStats stats)
        {
            CharacterDamage characterDamage = config.CharacterDamage;

            characterDamage.Construct(_bonusDamageConfig.TotalBonusDamagePerMainStat,
                _bonusDamageConfig.TotalBonusDamagePerLevel, stats, _customLogger); 
        
            return characterDamage;
        }
        
        private ICharacterLogic CreateCharacterLogic(GameObject gameObject, 
            CharacterConfig config)
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