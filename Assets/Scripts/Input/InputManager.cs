using System;
using TG.Attributes;
using TG.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input {
    /// <summary>
    /// you can either use this class to manage input or use your own version of an input manager
    /// and input actions depending on your project requirements
    /// </summary>
    public class InputManager : Singleton<InputManager> {
        [SerializeField] private UIInputReader uiInputReader;
        [SerializeField] private PlayerInputReader playerInputReader;

        private InputSystem_Actions m_input;
        private PlayerInput m_playerInput;
        
        private EInputActionMap m_currentActionMap;
        [SerializeField,ReadOnly] private EDeviceType m_currentDeviceType;
        private InputDevice m_currentDevice;
        private string m_currentControlScheme;

        public static event Action OnControlTypeChanged;
        public static event Action OnDeviceDisconnected;
        public static event Action OnDeviceReconnected;
        
        public bool IsUsingController => m_currentDeviceType == EDeviceType.GAMEPAD;
        
        public EDeviceType CurrentDeviceType => m_currentDeviceType;
        
        private void OnEnable() {
            if (m_input == null) {
                m_input = new InputSystem_Actions();
                m_input.Player.SetCallbacks(playerInputReader);
                m_input.UI.SetCallbacks(uiInputReader);
            }
            
            SetInputActionMap(EInputActionMap.PLAYER);
            
            m_playerInput.onControlsChanged += ControlTypeChanged;
            m_playerInput.onDeviceLost += DeviceDisconnected;
            m_playerInput.onDeviceRegained += DeviceReconnected;
        }

        private void OnDisable() {
            m_playerInput.onControlsChanged -= ControlTypeChanged;
            m_playerInput.onDeviceLost -= DeviceDisconnected;
            m_playerInput.onDeviceRegained -= DeviceReconnected;
        }

        protected override void Awake() {
            base.Awake();
            m_playerInput = GetComponent<PlayerInput>();
            
            ControlTypeChanged(m_playerInput);
        }

        private void ControlTypeChanged(PlayerInput input) {
            if (input.currentControlScheme == m_currentControlScheme) return;
            m_currentControlScheme = input.currentControlScheme;
            m_currentDevice = input.devices[0];
            
            Debug.Log($"Control Scheme Changed to {m_currentControlScheme}");
            m_currentDeviceType = m_currentDevice switch {
                Gamepad => EDeviceType.GAMEPAD,
                Keyboard => EDeviceType.KEYBOARD,
                Joystick => EDeviceType.JOYSTICK,
                _ => m_currentDeviceType
            };
            OnControlTypeChanged?.Invoke();
        }

        private void DeviceDisconnected(PlayerInput playerInput) {
            OnDeviceDisconnected?.Invoke();
        }

        private void DeviceReconnected(PlayerInput playerInput) {
            OnDeviceReconnected?.Invoke();
        }

        public void SetInputActionMap(EInputActionMap inputActionMap) {
            if (m_currentActionMap == inputActionMap) return;
            m_currentActionMap = inputActionMap;

            switch (inputActionMap) {
                case EInputActionMap.DEFAULT:
                    UpdateCursorState(false);
                    break;
                case EInputActionMap.UI:
                    UpdateCursorState(true);
                    m_input.Player.Disable();
                    m_input.UI.Enable();
                    break;
                case EInputActionMap.PLAYER:
                    UpdateCursorState(true);
                    m_input.UI.Disable();
                    m_input.Player.Enable();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(inputActionMap), inputActionMap, null);
            }
        }

        public void UpdateCursorState(bool state) {
            Cursor.visible = state;
            Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}