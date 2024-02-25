//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Input/CustomInput.inputactions
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

public partial class @CustomInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @CustomInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CustomInput"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""9fe98959-d098-4c55-8d2b-c0407994741f"",
            ""actions"": [
                {
                    ""name"": ""Turning"",
                    ""type"": ""Value"",
                    ""id"": ""dca15290-5aab-441d-b2fa-2f5ebf417938"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CameraRevolve"",
                    ""type"": ""Value"",
                    ""id"": ""65b40fbb-7d32-45e3-8f85-f6372881cc24"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Flap"",
                    ""type"": ""Button"",
                    ""id"": ""962bfb05-60c6-4490-94e5-5e1c34c945e8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dive"",
                    ""type"": ""Button"",
                    ""id"": ""339fa402-1b03-40df-9740-5471956067bb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""c86d7767-3430-49d5-aa49-0826319c5aab"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turning"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""c2109d3b-f6fd-4629-a22a-ca3fb576190b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turning"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d3288938-a40c-4a04-8495-38c5bbcf04b9"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turning"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e819db4d-105f-476e-bb91-a2830c2db8d3"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRevolve"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ca06c1b-f5cc-44c2-a63a-53b5d145e41e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Flap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d12f12b0-834e-4b84-9d55-574d81dbc055"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d901e7fa-8dac-4657-82b5-969415b01aea"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Turning = m_Gameplay.FindAction("Turning", throwIfNotFound: true);
        m_Gameplay_CameraRevolve = m_Gameplay.FindAction("CameraRevolve", throwIfNotFound: true);
        m_Gameplay_Flap = m_Gameplay.FindAction("Flap", throwIfNotFound: true);
        m_Gameplay_Dive = m_Gameplay.FindAction("Dive", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private List<IGameplayActions> m_GameplayActionsCallbackInterfaces = new List<IGameplayActions>();
    private readonly InputAction m_Gameplay_Turning;
    private readonly InputAction m_Gameplay_CameraRevolve;
    private readonly InputAction m_Gameplay_Flap;
    private readonly InputAction m_Gameplay_Dive;
    public struct GameplayActions
    {
        private @CustomInput m_Wrapper;
        public GameplayActions(@CustomInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Turning => m_Wrapper.m_Gameplay_Turning;
        public InputAction @CameraRevolve => m_Wrapper.m_Gameplay_CameraRevolve;
        public InputAction @Flap => m_Wrapper.m_Gameplay_Flap;
        public InputAction @Dive => m_Wrapper.m_Gameplay_Dive;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void AddCallbacks(IGameplayActions instance)
        {
            if (instance == null || m_Wrapper.m_GameplayActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameplayActionsCallbackInterfaces.Add(instance);
            @Turning.started += instance.OnTurning;
            @Turning.performed += instance.OnTurning;
            @Turning.canceled += instance.OnTurning;
            @CameraRevolve.started += instance.OnCameraRevolve;
            @CameraRevolve.performed += instance.OnCameraRevolve;
            @CameraRevolve.canceled += instance.OnCameraRevolve;
            @Flap.started += instance.OnFlap;
            @Flap.performed += instance.OnFlap;
            @Flap.canceled += instance.OnFlap;
            @Dive.started += instance.OnDive;
            @Dive.performed += instance.OnDive;
            @Dive.canceled += instance.OnDive;
        }

        private void UnregisterCallbacks(IGameplayActions instance)
        {
            @Turning.started -= instance.OnTurning;
            @Turning.performed -= instance.OnTurning;
            @Turning.canceled -= instance.OnTurning;
            @CameraRevolve.started -= instance.OnCameraRevolve;
            @CameraRevolve.performed -= instance.OnCameraRevolve;
            @CameraRevolve.canceled -= instance.OnCameraRevolve;
            @Flap.started -= instance.OnFlap;
            @Flap.performed -= instance.OnFlap;
            @Flap.canceled -= instance.OnFlap;
            @Dive.started -= instance.OnDive;
            @Dive.performed -= instance.OnDive;
            @Dive.canceled -= instance.OnDive;
        }

        public void RemoveCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameplayActions instance)
        {
            foreach (var item in m_Wrapper.m_GameplayActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameplayActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnTurning(InputAction.CallbackContext context);
        void OnCameraRevolve(InputAction.CallbackContext context);
        void OnFlap(InputAction.CallbackContext context);
        void OnDive(InputAction.CallbackContext context);
    }
}
