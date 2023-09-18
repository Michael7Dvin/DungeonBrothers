using UnityEngine;

namespace CodeBase.Infrastructure.Configs.Character
{
    [CreateAssetMenu(menuName = "Configs/Characters/AllCharacterConfigs", fileName = "AllCharactersConfigs")]
    public class AllCharactersConfigs : ScriptableObject
    {
        public CharacterConfig[] CharacterConfigs;
    }
}