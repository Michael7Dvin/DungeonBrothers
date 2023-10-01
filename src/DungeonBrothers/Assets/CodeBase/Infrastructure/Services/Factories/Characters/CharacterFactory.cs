using System.Collections.Generic;
using CodeBase.Gameplay.Animations.Move;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Characters.CharacterInfo;
using CodeBase.Gameplay.Characters.Logic;
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
            MovementStats movementStats = CreateMovementStats(config);

            ICharacterLogic characterLogic = CreateCharacterLogic(config);

            Character character = gameObject.GetComponent<Character>();

            character.Construct(config.ID, config.Team, movementStats, characterStats, characterLogic);

            CharacterInTurnQueueIcon icon = await _turnQueueViewFactory.CreateIcon(config.Image, config.ID);
            icon.gameObject.SetActive(false);
            
            _charactersProvider.Add(character, icon);

            return character;
        }

        public MovementStats CreateMovementStats(CharacterConfig config) => 
            new MovementStats(config.MovePoints, config.IsMoveThroughObstacles);

        private CharacterStats CreateCharacterStats(CharacterConfig config) =>
            new CharacterStats(config.Level, config.Intelligence, config.Strength, config.Dexterity,
                config.Initiative);

        private ICharacterLogic CreateCharacterLogic(CharacterConfig config)
        {
            CharacterLogic characterLogic = new CharacterLogic();
            
            _objectResolver.Inject(characterLogic);

            return characterLogic;
        }
    }
}