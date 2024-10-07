using UnityEngine;
using UnityEngine.InputSystem;

namespace Input {
    /// <summary>
    /// Input Reader for UI Actions
    /// </summary>
    [CreateAssetMenu(fileName = "UIInputReader", menuName = "Data/Input/UIInputReader")]
    public class UIInputReader : ScriptableObject, InputSystem_Actions.IUIActions{
        public event System.Action<Vector2> OnNavigateEvent;
        public event System.Action OnSubmitEvent;
        public event System.Action OnCancelEvent;
        
        public Vector2 NavigationInput { get; private set; }
        
        public void OnNavigate(InputAction.CallbackContext context) {
            NavigationInput = context.ReadValue<Vector2>();
            OnNavigateEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnSubmit(InputAction.CallbackContext context) {
            if (context.started) OnSubmitEvent?.Invoke();
        }

        public void OnCancel(InputAction.CallbackContext context) {
            if (context.started) OnCancelEvent?.Invoke();
        }
    }
}