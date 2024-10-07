using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Input {
    /// <summary>
    /// Input Reader for UI Actions
    /// </summary>
    [CreateAssetMenu(fileName = "UIInputReader", menuName = "Data/Input/PlayerInputReader")]
    public class PlayerInputReader : ScriptableObject, InputSystem_Actions.IPlayerActions{
        public event UnityAction OnMoveSelectionEvent = delegate { };
        public event UnityAction OnMoveCameraEvent = delegate { };
        public event UnityAction OnConfirmEvent = delegate { };
        
        public Vector2 MoveSelectionInput { get; private set; }
        public Vector2 MoveCameraInput { get; private set; }
        
        public void OnMoveSelection(InputAction.CallbackContext context) {
            MoveSelectionInput = context.ReadValue<Vector2>();
            OnMoveSelectionEvent.Invoke();
        }

        public void OnMoveCamera(InputAction.CallbackContext context) {
            MoveCameraInput = context.ReadValue<Vector2>();
            OnMoveCameraEvent.Invoke();
        }

        public void OnConfirm(InputAction.CallbackContext context) {
            OnConfirmEvent.Invoke();
        }
    }
}