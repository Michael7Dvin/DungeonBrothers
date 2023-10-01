using CodeBase.Gameplay.Animations.Scale;
using UnityEngine;

namespace CodeBase.UI.Button
{
    public class SelectableButtonAnimation : MonoBehaviour
    {
        [SerializeField] private ScaleAnimation _scaleAnimation;
        [SerializeField] private SelectableButtonAnimationConfig _config;
        
        public void ScaleOnPointerEntered() =>
            _scaleAnimation.Scale(_config.ScaleOnEntered);
        
        public void ScaleOnPointerDowned() =>
            _scaleAnimation.Scale(_config.ScaleOnDowned);
        
        public void ScaleOnPointerUpped() =>
            _scaleAnimation.Scale(_config.ScaleOnUpped);
        
        public void ScaleOnPointerExited() =>
            _scaleAnimation.Scale(_config.ScaleOnExited);
    }
}