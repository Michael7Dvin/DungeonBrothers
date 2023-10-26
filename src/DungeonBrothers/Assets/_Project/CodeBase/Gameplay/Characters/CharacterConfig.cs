using CodeBase.Gameplay.Characters.CharacterInfo;
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

        [TitleGroup("Stats")]
        public int HealthPoints;

        [TitleGroup("Stats")]
        public CharacterDamage CharacterDamage;
        
        [TitleGroup("Stats")]
        public CharacterStats CharacterStats;
    }
}