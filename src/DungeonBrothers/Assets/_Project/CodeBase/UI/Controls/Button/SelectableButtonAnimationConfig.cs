using CodeBase.Gameplay.Animations.Scale;
using UnityEngine;

namespace CodeBase.UI.Controls.Button
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Buttons/Animation/SelectableButton", fileName = "SelectableButton")]
    public class SelectableButtonAnimationConfig : ScriptableObject
    {
        [field: SerializeField] public ScaleAnimationConfig ScaleAnimationOnEntered { get; private set; }
        [field: SerializeField] public ScaleAnimationConfig ScaleAnimationOnExited { get; private set; }
        [field: SerializeField] public ScaleAnimationConfig ScaleAnimationOnDowned { get; private set; }
        [field: SerializeField] public ScaleAnimationConfig ScaleAnimationOnUpped { get; private set; }
    }
}