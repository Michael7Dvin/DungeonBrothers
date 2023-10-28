using UnityEngine;

namespace CodeBase.Gameplay.Characters.View.Outline
{
    [CreateAssetMenu(menuName = "StaticData/Configs/CharacterOutlineColors", fileName = "CharacterOutlineColors")]
    public class CharacterOutlineColors : ScriptableObject
    {
        [field: SerializeField] public Color Active { get; private set; }
        [field: SerializeField] public Color Attackable { get; private set; }
    }
}