//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Project/Input/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Interactions"",
            ""id"": ""d5a5a998-1fe4-4f4e-a52f-7d34038501e5"",
            ""actions"": [
                {
                    ""name"": ""PrimaryFingerTouch"",
                    ""type"": ""Button"",
                    ""id"": ""5718b6b2-d1c8-4f68-a3df-b658bb94e923"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PrimaryFingerPosition"",
                    ""type"": ""Value"",
                    ""id"": ""fa0a1a5c-9113-4597-b688-41d2f82ccf0c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5f936750-4e48-41f5-9584-1eb75d2add11"",
                    ""path"": ""<Touchscreen>/touch0/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""PrimaryFingerTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8208643-f434-4e01-a295-9d52981d51a7"",
                    ""path"": ""<Touchscreen>/touch0/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""PrimaryFingerPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mobile"",
            ""bindingGroup"": ""Mobile"",
            ""devices"": []
        }
    ]
}");
        // Interactions
        m_Interactions = asset.FindActionMap("Interactions", throwIfNotFound: true);
        m_Interactions_PrimaryFingerTouch = m_Interactions.FindAction("PrimaryFingerTouch", throwIfNotFound: true);
        m_Interactions_PrimaryFingerPosition = m_Interactions.FindAction("PrimaryFingerPosition", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Interactions
    private readonly InputActionMap m_Interactions;
    private List<IInteractionsActions> m_InteractionsActionsCallbackInterfaces = new List<IInteractionsActions>();
    private readonly InputAction m_Interactions_PrimaryFingerTouch;
    private readonly InputAction m_Interactions_PrimaryFingerPosition;
    public struct InteractionsActions
    {
        private @PlayerInputActions m_Wrapper;
        public InteractionsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @PrimaryFingerTouch => m_Wrapper.m_Interactions_PrimaryFingerTouch;
        public InputAction @PrimaryFingerPosition => m_Wrapper.m_Interactions_PrimaryFingerPosition;
        public InputActionMap Get() { return m_Wrapper.m_Interactions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InteractionsActions set) { return set.Get(); }
        public void AddCallbacks(IInteractionsActions instance)
        {
            if (instance == null || m_Wrapper.m_InteractionsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_InteractionsActionsCallbackInterfaces.Add(instance);
            @PrimaryFingerTouch.started += instance.OnPrimaryFingerTouch;
            @PrimaryFingerTouch.performed += instance.OnPrimaryFingerTouch;
            @PrimaryFingerTouch.canceled += instance.OnPrimaryFingerTouch;
            @PrimaryFingerPosition.started += instance.OnPrimaryFingerPosition;
            @PrimaryFingerPosition.performed += instance.OnPrimaryFingerPosition;
            @PrimaryFingerPosition.canceled += instance.OnPrimaryFingerPosition;
        }

        private void UnregisterCallbacks(IInteractionsActions instance)
        {
            @PrimaryFingerTouch.started -= instance.OnPrimaryFingerTouch;
            @PrimaryFingerTouch.performed -= instance.OnPrimaryFingerTouch;
            @PrimaryFingerTouch.canceled -= instance.OnPrimaryFingerTouch;
            @PrimaryFingerPosition.started -= instance.OnPrimaryFingerPosition;
            @PrimaryFingerPosition.performed -= instance.OnPrimaryFingerPosition;
            @PrimaryFingerPosition.canceled -= instance.OnPrimaryFingerPosition;
        }

        public void RemoveCallbacks(IInteractionsActions instance)
        {
            if (m_Wrapper.m_InteractionsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IInteractionsActions instance)
        {
            foreach (var item in m_Wrapper.m_InteractionsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_InteractionsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public InteractionsActions @Interactions => new InteractionsActions(this);
    private int m_MobileSchemeIndex = -1;
    public InputControlScheme MobileScheme
    {
        get
        {
            if (m_MobileSchemeIndex == -1) m_MobileSchemeIndex = asset.FindControlSchemeIndex("Mobile");
            return asset.controlSchemes[m_MobileSchemeIndex];
        }
    }
    public interface IInteractionsActions
    {
        void OnPrimaryFingerTouch(InputAction.CallbackContext context);
        void OnPrimaryFingerPosition(InputAction.CallbackContext context);
    }
}
