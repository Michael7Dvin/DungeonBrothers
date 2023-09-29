using System.Collections.Generic;
using CodeBase.Gameplay.Animations.Move;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Characters.View;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Infrastructure.Services.AddressablesLoader.Loader;
using CodeBase.Infrastructure.Services.Factories.TurnQueue;
using CodeBase.Infrastructure.Services.Providers.CharactersProvider;
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

        public CharacterFactory(IAddressablesLoader addressablesLoader,
            IObjectResolver objectResolver,
            ICharactersProvider charactersProvider,
            ITurnQueueViewFactory turnQueueViewFactory)
        {
            _addressablesLoader = addressablesLoader;
            _objectResolver = objectResolver;
            _charactersProvider = charactersProvider;
            _turnQueueViewFactory = turnQueueViewFactory;
        }

        public async UniTask WarmUp(List<CharacterConfig> characterConfigs)
        {
            foreach (var character in characterConfigs) 
                await _addressablesLoader.LoadGameObject(character.Prefab);
        }

        public async UniTask<Character> Create(CharacterConfig config)
        {
            GameObject prefab = await _addressablesLoader.LoadGameObject(config.Prefab);
            GameObject gameObject = _objectResolver.Instantiate(prefab);

            CharacterStats characterStats = CreateCharacterStats(config);

            ICharacterLogic characterLogic = CreateCharacterLogic(config);

            Character character = gameObject.GetComponent<Character>();
            
            CharacterAnimation characterAnimation = CreateCharacterAnimation(gameObject);
            
            character.Construct(config.ID, config.Team ,characterStats, characterLogic, characterAnimation);

            CharacterInTurnQueueIcon icon = await _turnQueueViewFactory.CreateIcon(config.Image, config.ID);
            icon.gameObject.SetActive(false);
            
            _charactersProvider.Add(character, icon);

            return character;
        }

        private CharacterAnimation CreateCharacterAnimation(GameObject gameObject)
        {
            MoveAnimation moveAnimation = gameObject.GetComponent<MoveAnimation>();
            
            CharacterAnimation characterAnimation = new CharacterAnimation(moveAnimation);
            _objectResolver.Inject(characterAnimation);
            return characterAnimation;
        }

        private CharacterStats CreateCharacterStats(CharacterConfig config)
        {
            return new CharacterStats(config.Level, config.Intelligence, config.Strength, config.Dexterity,
                config.Initiative, config.MovePoints, config.IsMoveThroughObstacles);
        }

        private ICharacterLogic CreateCharacterLogic(CharacterConfig config)
        {
            CharacterLogic characterLogic = new CharacterLogic();
            
            _objectResolver.Inject(characterLogic);

            return characterLogic;
        }
    }
}