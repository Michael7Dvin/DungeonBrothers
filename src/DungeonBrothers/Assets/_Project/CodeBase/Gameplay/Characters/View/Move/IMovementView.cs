using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Characters.View.Move
{
    public interface IMovementView
    {
        public UniTask Move(Vector3[] path);
    }
}