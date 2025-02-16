using UnityEngine;
using UnityEngine.InputSystem;

namespace Modules.Controls
{
    public class ControlsManager : MonoBehaviour
    {
        [SerializeField] private InputAction _move;

        private void OnEnable()
        {
            _move.started += OnMove;
            _move.Enable();
        }

        private void OnDisable()
        {
            _move.started -= OnMove;
            _move.Disable();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            Vector2 value = context.ReadValue<Vector2>();
            Debug.Log(value);
        }
    }
}
