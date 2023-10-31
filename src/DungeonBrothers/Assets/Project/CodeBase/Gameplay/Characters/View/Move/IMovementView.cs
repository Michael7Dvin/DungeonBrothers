using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Characters.View.Move
{
    public interface IMovementView
    {
        public UniTask Move(Vector3[] path);
    }
}