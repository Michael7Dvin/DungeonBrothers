using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Animations.Move
{
    public class MoveAnimation : MonoBehaviour
    {
        private Tweener _currentTweener;
        
        public void Move(Vector3 endCoordinate, float duration, bool isSnapping, Ease ease)
        {
            _currentTweener = GetMove(endCoordinate, duration, isSnapping, ease).Play();
        }

        public async UniTask MoveTask(Vector3 endCoordinate, float duration, bool isSnapping, Ease ease)
        {
            _currentTweener = GetMove(endCoordinate, duration, isSnapping, ease);
            
            await _currentTweener.Play().ToUniTask();
        }

        private Tweener GetMove(Vector3 endCoordinate, float duration, bool isSnapping, Ease ease)
        {
            if (_currentTweener.IsActive())
                _currentTweener.Kill();
            
            return transform.DOMove(endCoordinate, duration, isSnapping).SetEase(ease);
        }
    }
}