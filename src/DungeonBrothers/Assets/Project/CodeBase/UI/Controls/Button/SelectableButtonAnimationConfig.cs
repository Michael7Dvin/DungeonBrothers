using Project.CodeBase.Gameplay.Tweeners.Scale;
using UnityEngine;

namespace Project.CodeBase.UI.Controls.Button
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Buttons/Animation/SelectableButton", fileName = "SelectableButton")]
    public class SelectableButtonAnimationConfig : ScriptableObject
    {
        [field: SerializeField] public ScaleTweenerConfig ScaleTweenerOnEntered { get; private set; }
        [field: SerializeField] public ScaleTweenerConfig ScaleTweenerOnExited { get; private set; }
        [field: SerializeField] public ScaleTweenerConfig ScaleTweenerOnDowned { get; private set; }
        [field: SerializeField] public ScaleTweenerConfig ScaleTweenerOnUpped { get; private set; }
    }
}