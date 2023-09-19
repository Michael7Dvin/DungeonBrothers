using System.Collections.Generic;
using CodeBase.Gameplay.Characters;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.Infrastructure.Configs.Character
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Characters/All", fileName = "AllCharactersConfigs")]
    public class AllCharactersConfigs : SerializedScriptableObject
    {
        [OdinSerialize] public Dictionary<CharacterID, CharacterConfig> CharacterConfigs;
    }
}