using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Characters;
using Project.CodeBase.Gameplay.Characters.CharacterInfo;
using Project.CodeBase.Gameplay.Characters.Logic;
using Project.CodeBase.Gameplay.Characters.Logic.Deaths;
using Project.CodeBase.Gameplay.Characters.Logic.Healths;
using Project.CodeBase.Gameplay.Characters.Logic.Movement;
using Project.CodeBase.Gameplay.Characters.View;
using Project.CodeBase.Gameplay.Characters.View.Animators;
using Project.CodeBase.Gameplay.Characters.View.Hit;
using Project.CodeBase.Gameplay.Characters.View.Move;
using Project.CodeBase.Gameplay.Characters.View.Outline;
using Project.CodeBase.Gameplay.Characters.View.Sounds;
using Project.CodeBase.Gameplay.Characters.View.SpriteFlip;
using Project.CodeBase.Gameplay.Services.TurnQueue;
using Project.CodeBase.Gameplay.Tweeners.Color;
using Project.CodeBase.Gameplay.Tweeners.Hit;
using Project.CodeBase.Gameplay.Tweeners.Move;
using Project.CodeBase.Gameplay.Tweeners.Scale;
using Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.UI.Gameplay;
using Project.CodeBase.Infrastructure.Services.AddressablesLoader.Loader;
using Project.CodeBase.Infrastructure.Services.Factories.TurnQueue;
using Project.CodeBase.Infrastructure.Services.Logger;
using Project.CodeBase.Infrastructure.Services.Providers.CharactersProvider;
using Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using Project.CodeBase.UI.HealthBar;
using Project.CodeBase.UI.TurnQueue;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.CodeBase.Infrastructure.Services.Factories.Characters
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

            ICharacterView view = await CreateCharacterView(gameObject, config);

            Character character = gameObject.GetComponent<Character>();
            ICharacterLogic logic = CreateCharacterLogic(character, view.MovementView, config);
            character.Construct(config.ID, config.Team, stats, damage, logic, view);
            
            await CreateHealthBar(character);
            
            _charactersProvider.Add(character, character.View.Icon);

            return character;
        }

        private async UniTask<ICharacterView> CreateCharacterView(GameObject gameObject, CharacterConfig config)
        {
            CharacterTurnQueueIcon icon = await _turnQueueViewFactory.CreateIcon(config.Image, config.ID);
            icon.gameObject.SetActive(false);
            
            CharacterSounds characterSounds = SetupCharacterSounds(gameObject);

            IHitView hitView = CreateHitView(gameObject, characterSounds);
            ISpriteFlip spriteFlip = CreateSpriteFlip(gameObject.transform);
            ICharacterOutline characterOutline = CreateOutline(gameObject);
            ICharacterAnimator characterAnimator = CreateCharacterAnimator(gameObject);
            IMovementView movementView = CreateMovementView(gameObject, characterSounds, spriteFlip, characterAnimator);

            CharacterView characterView = new();
            characterView.Construct(icon, characterAnimator, movementView, hitView, spriteFlip, characterOutline);
            return characterView;
        }

        private CharacterOutline CreateOutline(GameObject gameObject)
        {
            Material material = gameObject.GetComponent<Renderer>().material;
            return new CharacterOutline(material);
        }

        private ISpriteFlip CreateSpriteFlip(Transform characterTransform) => 
            new SpriteFlip(characterTransform);

        private IMovementView CreateMovementView(GameObject gameObject,
            CharacterSounds characterSounds,
            ISpriteFlip spriteFlip,
            ICharacterAnimator characterAnimator)
        {
            MoveTweener moveTweener = new(gameObject.transform);

            MovementView movementView = new(moveTweener, characterSounds, spriteFlip, characterAnimator);
            _objectResolver.Inject(movementView);
            return movementView;
        }

        private CharacterSounds SetupCharacterSounds(GameObject gameObject)
        {
            CharacterSounds characterSounds = gameObject.GetComponent<CharacterSounds>();
            _objectResolver.Inject(characterSounds);
            return characterSounds;
        }

        private IHitView CreateHitView(GameObject gameObject, CharacterSounds characterSounds)
        {
            ScaleTweener scaleTweener = new(gameObject.transform);

            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            ColorTweener colorTweener = new(spriteRenderer);
            
            HitTweener hitTweener = new(scaleTweener, colorTweener);

            HitView hitView = new(hitTweener, characterSounds);
            _objectResolver.Inject(hitView);
            return hitView;
        }

        private ICharacterAnimator CreateCharacterAnimator(GameObject gameObject)
        {
            Animator animator = gameObject.GetComponent<Animator>();
            return new CharacterAnimator(animator);
        }

        private async UniTask CreateHealthBar(ICharacter character)
        {
            GameObject prefab = await _addressablesLoader.LoadGameObject(_gameplayUIAddresses.HealthBar);
            GameObject gameObject = _objectResolver.Instantiate(prefab, character.GameObject.transform);

            HealthBarView healthBarView = gameObject.GetComponent<HealthBarView>();
            HealthBarPresenter healthBarPresenter = new HealthBarPresenter();
            
            healthBarPresenter.Construct(character.Logic.Health, character.Logic.Death, healthBarView);

            gameObject.transform.position = character.GameObject.transform.position - _offset;
            healthBarPresenter.Initialize();
        }

        private CharacterDamage CreateCharacterDamage(CharacterConfig config, CharacterStats stats)
        {
            CharacterDamage characterDamage = config.CharacterDamage;

            characterDamage.Construct(_bonusDamageConfig.TotalBonusDamagePerMainStat,
                _bonusDamageConfig.TotalBonusDamagePerLevel, stats, _customLogger); 
        
            return characterDamage;
        }
        
        private ICharacterLogic CreateCharacterLogic(ICharacter character,
            IMovementView movementView,
            CharacterConfig config)
        {
            Death death = new(character.GameObject);
            
            Health health = new(config.HealthPoints, death);
            _objectResolver.Inject(health);

            Movement movement = new(character, movementView, config.IsMoveThroughObstacles, config.MovePoints);
            _objectResolver.Inject(movement);
            
            ICharacterLogic characterLogic = new CharacterLogic(health, death, movement);
            _objectResolver.Inject(characterLogic);

            return characterLogic;
        }
    }
}