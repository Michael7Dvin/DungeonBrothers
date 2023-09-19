using System.Collections.Generic;
using CodeBase.Gameplay.UI.TurnQueue;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Infrastructure.Configs.Character
{
    [CreateAssetMenu(menuName = "Configs/Characters/AllCharacterConfigs", fileName = "AllCharactersConfigs")]
    public class AllCharactersConfigs : SerializedScriptableObject
    {
        public Dictionary<CharacterID, CharacterConfig> CharacterConfigs;
    }
}