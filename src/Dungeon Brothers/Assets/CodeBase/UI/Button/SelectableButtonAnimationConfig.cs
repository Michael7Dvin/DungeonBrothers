using CodeBase.Gameplay.Animations.Scale;
using UnityEngine;

namespace CodeBase.UI.Button
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Buttons/Animation/SelectableButton", fileName = "SelectableButton")]
    public class SelectableButtonAnimationConfig : ScriptableObject
    {
        [field: SerializeField] public ScaleConfig ScaleOnEntered { get; private set; }
        [field: SerializeField] public ScaleConfig ScaleOnExited { get; private set; }
        [field: SerializeField] public ScaleConfig ScaleOnDowned { get; private set; }
        [field: SerializeField] public ScaleConfig ScaleOnUpped { get; private set; }
    }
}