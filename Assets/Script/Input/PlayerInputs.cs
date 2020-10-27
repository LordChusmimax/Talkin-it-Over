// GENERATED AUTOMATICALLY FROM 'Assets/Script/Input/PlayerInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""5e774777-7bac-4cf1-b86d-9431a5ed7ff1"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""185834f4-1886-4939-b9c7-c9f3abd468d2"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""b1f22dbc-40cc-49ec-9bfd-04d6d9fa393a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""6d6b37d2-7222-4b24-8b52-75747eb3cc0e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pick"",
                    ""type"": ""Button"",
                    ""id"": ""787dc771-1999-4b13-bc3a-6faf0d899e09"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Special"",
                    ""type"": ""Button"",
                    ""id"": ""8b1932bc-fa75-45dc-bdfe-8182428e8c88"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""9cb86dd4-6bc7-4cb7-a09a-af997bf16d26"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Slow"",
                    ""type"": ""Button"",
                    ""id"": ""ccc4cfa6-7eb8-48ec-addd-004ada5b8b4d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""64288cea-4a17-4465-9d53-372944906de9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Button"",
                    ""id"": ""f0acbe5d-30d2-4204-beee-ff8fe9ffde0d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Asignar"",
                    ""type"": ""Button"",
                    ""id"": ""dc1e8a26-af05-4536-a059-0c60f2c66a50"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Desasignar"",
                    ""type"": ""Button"",
                    ""id"": ""2a027b78-a40d-4e4a-88a7-2c8541595639"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Empezar"",
                    ""type"": ""Button"",
                    ""id"": ""7625f852-8fcc-4df2-ab76-56ef14a527e3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""AD"",
                    ""id"": ""75a70167-a7fa-44b5-9285-f364129bc1ca"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""ccbaedd0-fab4-4522-8649-1abf4742d3ee"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""aa4e8fef-e0a3-4fb9-8cdc-001cea4bde81"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""791559b8-c89b-4aab-8840-315b05351629"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4595221-1350-4a42-8fe4-763f9d0ede8d"",
                    ""path"": ""<Gamepad>/dpad/x"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone(min=0.3)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26384c03-fe41-4247-ae41-655412fb08d9"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7076825c-5766-4541-94b7-31fe20cc2d0a"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""499a327d-7331-4149-9617-42ebb2ca9aaa"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82ed96e4-4d23-4be2-ae12-dd66e336c3af"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5013f73d-92b1-4faf-944c-ac47a73d4432"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Pick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2015905a-169e-48a8-b4dc-2dd5e533ff67"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6a90c82-8338-4faf-8f20-2cdee2ed72ba"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""474f7755-b351-42b7-aad3-406127d059ef"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Special"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9257af76-c27a-4749-a63f-df97328c6885"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Special"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c66cad14-00ee-49bc-80e7-dcf4e0af3c79"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4906351-5e43-4545-b2cf-153a011e7851"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""250d1e4d-5390-4b86-a3b0-0d2b4a9c9326"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Slow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1aaa87d1-46cb-4547-8081-947acdd657fa"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d748cc87-bbbf-444b-9e7d-b01f913c52e8"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1712bc6-912e-4159-8cd4-d55683f960d7"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Asignar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ade30cd-4608-4e8e-8fd1-2e0f70f64896"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Asignar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""feb5cb7f-97c0-4402-a1c6-aa58959801a8"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Desasignar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""896e2ec5-909b-41e3-a52a-25ddacc357ce"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Desasignar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f43c4f3-3078-4d7c-90f4-a12c3269e796"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Empezar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""799431ef-adad-4dd0-9807-efb5cb1856ed"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Empezar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""All Control Schemes"",
            ""bindingGroup"": ""All Control Schemes"",
            ""devices"": []
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Shoot = m_Player.FindAction("Shoot", throwIfNotFound: true);
        m_Player_Pick = m_Player.FindAction("Pick", throwIfNotFound: true);
        m_Player_Special = m_Player.FindAction("Special", throwIfNotFound: true);
        m_Player_Menu = m_Player.FindAction("Menu", throwIfNotFound: true);
        m_Player_Slow = m_Player.FindAction("Slow", throwIfNotFound: true);
        m_Player_Aim = m_Player.FindAction("Aim", throwIfNotFound: true);
        m_Player_MousePosition = m_Player.FindAction("MousePosition", throwIfNotFound: true);
        m_Player_Asignar = m_Player.FindAction("Asignar", throwIfNotFound: true);
        m_Player_Desasignar = m_Player.FindAction("Desasignar", throwIfNotFound: true);
        m_Player_Empezar = m_Player.FindAction("Empezar", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Shoot;
    private readonly InputAction m_Player_Pick;
    private readonly InputAction m_Player_Special;
    private readonly InputAction m_Player_Menu;
    private readonly InputAction m_Player_Slow;
    private readonly InputAction m_Player_Aim;
    private readonly InputAction m_Player_MousePosition;
    private readonly InputAction m_Player_Asignar;
    private readonly InputAction m_Player_Desasignar;
    private readonly InputAction m_Player_Empezar;
    public struct PlayerActions
    {
        private @PlayerInputs m_Wrapper;
        public PlayerActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Shoot => m_Wrapper.m_Player_Shoot;
        public InputAction @Pick => m_Wrapper.m_Player_Pick;
        public InputAction @Special => m_Wrapper.m_Player_Special;
        public InputAction @Menu => m_Wrapper.m_Player_Menu;
        public InputAction @Slow => m_Wrapper.m_Player_Slow;
        public InputAction @Aim => m_Wrapper.m_Player_Aim;
        public InputAction @MousePosition => m_Wrapper.m_Player_MousePosition;
        public InputAction @Asignar => m_Wrapper.m_Player_Asignar;
        public InputAction @Desasignar => m_Wrapper.m_Player_Desasignar;
        public InputAction @Empezar => m_Wrapper.m_Player_Empezar;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Shoot.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Pick.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPick;
                @Pick.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPick;
                @Pick.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPick;
                @Special.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecial;
                @Special.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecial;
                @Special.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecial;
                @Menu.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenu;
                @Slow.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlow;
                @Slow.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlow;
                @Slow.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlow;
                @Aim.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @MousePosition.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @Asignar.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAsignar;
                @Asignar.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAsignar;
                @Asignar.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAsignar;
                @Desasignar.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDesasignar;
                @Desasignar.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDesasignar;
                @Desasignar.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDesasignar;
                @Empezar.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEmpezar;
                @Empezar.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEmpezar;
                @Empezar.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEmpezar;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Pick.started += instance.OnPick;
                @Pick.performed += instance.OnPick;
                @Pick.canceled += instance.OnPick;
                @Special.started += instance.OnSpecial;
                @Special.performed += instance.OnSpecial;
                @Special.canceled += instance.OnSpecial;
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
                @Slow.started += instance.OnSlow;
                @Slow.performed += instance.OnSlow;
                @Slow.canceled += instance.OnSlow;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @Asignar.started += instance.OnAsignar;
                @Asignar.performed += instance.OnAsignar;
                @Asignar.canceled += instance.OnAsignar;
                @Desasignar.started += instance.OnDesasignar;
                @Desasignar.performed += instance.OnDesasignar;
                @Desasignar.canceled += instance.OnDesasignar;
                @Empezar.started += instance.OnEmpezar;
                @Empezar.performed += instance.OnEmpezar;
                @Empezar.canceled += instance.OnEmpezar;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_AllControlSchemesSchemeIndex = -1;
    public InputControlScheme AllControlSchemesScheme
    {
        get
        {
            if (m_AllControlSchemesSchemeIndex == -1) m_AllControlSchemesSchemeIndex = asset.FindControlSchemeIndex("All Control Schemes");
            return asset.controlSchemes[m_AllControlSchemesSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnPick(InputAction.CallbackContext context);
        void OnSpecial(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
        void OnSlow(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnAsignar(InputAction.CallbackContext context);
        void OnDesasignar(InputAction.CallbackContext context);
        void OnEmpezar(InputAction.CallbackContext context);
    }
}
