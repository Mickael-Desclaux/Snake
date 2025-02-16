using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        Debug.Log(value);
    }
}
