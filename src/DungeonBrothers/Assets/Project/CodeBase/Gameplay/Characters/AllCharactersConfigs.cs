using System.Collections.Generic;
using Project.CodeBase.Gameplay.Animations.Hit;
using Project.CodeBase.Gameplay.Animations.Movement;
using Project.CodeBase.Gameplay.Characters.CharacterInfo;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Characters
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Characters/All", fileName = "AllCharactersConfigs")]
    public class AllCharactersConfigs : SerializedScriptableObject
    {
        [OdinSerialize] public Dictionary<CharacterID, CharacterConfig> CharacterConfigs;

        public HitAnimationConfig HitAnimationConfig;
        public MovementAnimationConfig MovementAnimationConfig;
    }
}