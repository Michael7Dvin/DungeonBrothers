using System;
using CodeBase.UI.Controls.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Controls.Button
{
    [RequireComponent(typeof(IControlEvents))]
    public class BaseButton : MonoBehaviour
    {
        private void Awake() => 
            Events = GetComponent<IControlEvents>();

        protected virtual void OnEnable() => 
            Events.PointerClicked += OnClicked;

        protected virtual void OnDisable() => 
            Events.PointerClicked -= OnClicked;

        protected IControlEvents Events { get; private set; }
        
        public event Action Cliked;

        private void OnClicked(PointerEventData eventData) => 
            Cliked?.Invoke();
    }
}