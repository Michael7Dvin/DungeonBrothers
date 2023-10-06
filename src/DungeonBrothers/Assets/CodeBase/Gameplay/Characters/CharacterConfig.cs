using System;
using CodeBase.Gameplay.Characters.CharacterInfo;
using CodeBase.Gameplay.Characters.Logic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Gameplay.Characters
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Characters/Character", fileName = "CharacterConfig")]
    public class CharacterConfig : SerializedScriptableObject
    {
        [TitleGroup("Info")]
        public CharacterID ID;
        [TitleGroup("Info")]
        public CharacterTeam Team;
        
        [TitleGroup("Prefabs")]
        public AssetReferenceGameObject Image;
        [TitleGroup("Prefabs")]
        public AssetReferenceGameObject Prefab;

        [FoldoutGroup("StartHealth"), MinValue(0)] 
        public int HealthPoints;

        public MovementStats MovementStats;
        
        public CharacterDamage CharacterDamage;
        
        public CharacterStats CharacterStats;
    }
}