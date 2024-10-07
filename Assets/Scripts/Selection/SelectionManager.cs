using System;
using Input;
using TG.Attributes;
using TG.Utilities;
using UnityEngine;



namespace DefaultNamespace {
    public class SelectionManager : Singleton<SelectionManager> {
        [SerializeField, ReadOnly] private GameObject currentSelectedObject;
        [SerializeField,ReadOnly] private GameObject currentHoveredObject;
        
        [SerializeField] private PlayerInputReader playerInputReader;

        private Camera m_mainCamera;

        protected override void Awake() {
            m_mainCamera = Camera.main;
        }

        public void Update() {
            if(InputManager.Instance == null) return;
            switch (InputManager.Instance.CurrentDeviceType) {
                case EDeviceType.GAMEPAD:
                    break;
                case EDeviceType.KEYBOARD:
                    HandleMouseHover(playerInputReader.MoveSelectionInput);
                    break;
                case EDeviceType.JOYSTICK:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void HandleMouseHover(Vector2 mousePosition) {
            Vector2 worldPosition = m_mainCamera.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit.collider != null) {
                var hitSelectable = hit.collider.GetComponent<ISelectable>();
                if (hitSelectable != null) {
                    if (currentHoveredObject == hit.collider.gameObject) return;
                    if (currentHoveredObject != null) {
                        currentHoveredObject.GetComponent<ISelectable>()?.OnUnhover();
                    }
                    currentHoveredObject = hit.collider.gameObject;
                    hitSelectable.OnHover();
                } else {
                    if (currentHoveredObject != null) {
                        currentHoveredObject.GetComponent<ISelectable>()?.OnUnhover();
                    }
                    currentHoveredObject = null;
                }
            } else {
                if (currentHoveredObject != null) {
                    currentHoveredObject.GetComponent<ISelectable>()?.OnUnhover();
                }
                currentHoveredObject = null;
            }
        }
        
        private void HandleGamepadSelection() {
            // get the current hovered object
        }
        
        private void SelectObject(ISelectable selectable) {
        }
    }
}