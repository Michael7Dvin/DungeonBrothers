using System;
using UniRx;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Rooms
{
    public class Door : MonoBehaviour
    {
        private ReactiveCommand _entered;
        public IObservable<Unit> Entered => _entered;

        public bool IsReturnExit { get; private set; }

        public void SetIsReturnExit(bool condition) => 
            IsReturnExit = condition;

        public void Enter() => 
            _entered.Execute();
    }
}