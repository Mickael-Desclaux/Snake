using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Modules.Controls
{
    public class ControlsManager : MonoBehaviour
    {
        public event Action<Vector2> OnMove;
        private static ControlsManager _instance;
        public static ControlsManager Instance => _instance;
        [SerializeField] private InputAction _move;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(_instance.gameObject);
                return;
            }

            _instance = this;
        }

        private void OnEnable()
        {
            _move.performed += Move;
            _move.Enable();
        }

        private void OnDisable()
        {
            _move.performed -= Move;
            _move.Disable();
        }

        private void Move(InputAction.CallbackContext context)
        {
            Vector2 value = context.ReadValue<Vector2>();
            OnMove?.Invoke(value);
        }
    }
}
