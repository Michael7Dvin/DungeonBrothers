using CodeBase.Gameplay.Animations.Scale;
using UnityEngine;

namespace CodeBase.UI.Controls.Button
{
    public class SelectableButtonAnimation : MonoBehaviour
    {
        [SerializeField] private ScaleAnimation _scaleAnimation;
        [SerializeField] private SelectableButtonAnimationConfig _config;
        
        public void ScaleOnPointerEntered() =>
            _scaleAnimation.DoScale(_config.ScaleAnimationOnEntered);
        
        public void ScaleOnPointerDowned() =>
            _scaleAnimation.DoScale(_config.ScaleAnimationOnDowned);
        
        public void ScaleOnPointerUpped() =>
            _scaleAnimation.DoScale(_config.ScaleAnimationOnUpped);
        
        public void ScaleOnPointerExited() =>
            _scaleAnimation.DoScale(_config.ScaleAnimationOnExited);
    }
}