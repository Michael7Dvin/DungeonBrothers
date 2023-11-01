using System;
using Project.CodeBase.UI.Controls.Events;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.CodeBase.UI.Controls.Button
{
    [RequireComponent(typeof(IControlEvents))]
    public class BaseButton : MonoBehaviour
    {
        protected readonly CompositeDisposable Disposable = new();
        private readonly ReactiveCommand _clicked = new();
        
        private void Awake() => 
            Events = GetComponent<IControlEvents>();

        protected virtual void OnEnable() =>
            Events.PointerClicked
                .Subscribe(OnClicked)
                .AddTo(Disposable);

        protected virtual void OnDisable() =>
            Disposable.Clear();

        protected IControlEvents Events { get; private set; }

        public IObservable<Unit> Clicked => _clicked;

        private void OnClicked(PointerEventData eventData) =>
            _clicked.Execute();
    }
}