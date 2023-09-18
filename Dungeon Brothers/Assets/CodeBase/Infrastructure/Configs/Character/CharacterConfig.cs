using System;
using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.UI.TurnQueue;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.Configs.Character
{
    [CreateAssetMenu(menuName = "Configs/Characters/CharacterConfig", fileName = "CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        public CharacterID CharacterID;
        public AssetReferenceGameObject Image;

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