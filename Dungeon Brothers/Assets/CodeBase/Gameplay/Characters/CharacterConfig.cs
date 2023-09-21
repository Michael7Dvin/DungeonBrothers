using CodeBase.Gameplay.Characters;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Configs.Character
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Characters/Character", fileName = "CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        public CharacterID CharacterID;
        public AssetReferenceGameObject Image;
        public AssetReferenceGameObject CharacterPrefab;

        [FoldoutGroup("Stats")]
        public int Level;
        [FoldoutGroup("Stats")]
        public int Initiative;
        [FoldoutGroup("Stats"), GUIColor(0, 0, 1)]
        public int Intelligence;
        [FoldoutGroup("Stats"), GUIColor(1, 0, 0)]
        public int Strength;
        [FoldoutGroup("Stats"), GUIColor(0, 1, 0)]
        public int Dexterity;
    }
}