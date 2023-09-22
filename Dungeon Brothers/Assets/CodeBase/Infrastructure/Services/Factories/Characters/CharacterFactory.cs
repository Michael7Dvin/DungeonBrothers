using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using CodeBase.Infrastructure.Addressable.Loader;
using CodeBase.Infrastructure.Configs.Character;
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

        public CharacterFactory(IAddressablesLoader addressablesLoader,
            IObjectResolver objectResolver)
        {
            _addressablesLoader = addressablesLoader;
            _objectResolver = objectResolver;
        }

        public async UniTask WarmUp(List<CharacterConfig> characterConfigs)
        {
            foreach (var character in characterConfigs)
            {
                await _addressablesLoader.LoadGameObject(character.CharacterPrefab);
            }
        }

        public async UniTask<ICharacter> Create(CharacterConfig config)
        {
            GameObject prefabLoaded = await _addressablesLoader.LoadGameObject(config.CharacterPrefab);
            GameObject characterPrefab = _objectResolver.Instantiate(prefabLoaded);
            
            CharacterStats characterStats = CreateCharacterStats(config);

            ICharacterLogic characterLogic = CreateCharacterLogic(config);
            
            Character character = new Character();
            
            character.Construct(config.CharacterID, characterStats, characterLogic);

            return character;
        }

        private CharacterStats CreateCharacterStats(CharacterConfig config)
        {
            return new CharacterStats(config.Level, config.Intelligence, config.Strength, config.Dexterity,
                config.Initiative);
        }

        private ICharacterLogic CreateCharacterLogic(CharacterConfig config)
        {
            CharacterLogic characterLogic = new CharacterLogic();
            
            _objectResolver.Inject(characterLogic);

            return characterLogic;
        }
    }
}