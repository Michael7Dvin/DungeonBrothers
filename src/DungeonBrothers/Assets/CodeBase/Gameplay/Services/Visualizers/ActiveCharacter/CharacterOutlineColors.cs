using UnityEngine;

namespace CodeBase.Gameplay.Services.Visualizers.ActiveCharacter
{
    [CreateAssetMenu(menuName = "StaticData/Configs/CharacterOutlineColors", fileName = "CharacterOutlineColors")]
    public class CharacterOutlineColors : ScriptableObject
    {
        [field: SerializeField] public Color ActiveCharacter { get; private set; }
    }
}