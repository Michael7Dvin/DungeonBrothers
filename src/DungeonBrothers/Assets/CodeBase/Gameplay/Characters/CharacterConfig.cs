using CodeBase.Gameplay.Characters.CharacterInfo;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Gameplay.Characters
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Characters/Character", fileName = "CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        [TitleGroup("Info")]
        public CharacterID ID;
        [TitleGroup("Info")]
        public CharacterTeam Team;
        
        [TitleGroup("Prefabs")]
        public AssetReferenceGameObject Image;
        [TitleGroup("Prefabs")]
        public AssetReferenceGameObject Prefab;

        [TitleGroup("Movement")]
        public int MovePoints;
        [TitleGroup("Movement")]
        public bool IsMoveThroughObstacles;
        
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