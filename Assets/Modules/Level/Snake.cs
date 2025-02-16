using System;
using Modules.Controls;
using UnityEngine;

namespace Modules.Level
{
    public class Snake : MonoBehaviour
    {
        [SerializeField] private float _interval = 0.2f;
        private Vector2 _direction = Vector2.right;
        private Vector2 _nextDirection = Vector2.right;

        private ControlsManager _controlsManager = ControlsManager.Instance;

        private void Start()
        {
            InvokeRepeating(nameof(Move), 0, _interval);
        }

        private void OnEnable()
        {
            _controlsManager.OnMove += HandleInput;
        }

        private void OnDisable()
        {
            _controlsManager.OnMove -= HandleInput;
        }

        private void HandleInput(Vector2 input)
        {
            if (input == -_direction)
            {
                return;
            }
            _nextDirection = input;
            CancelInvoke(nameof(Move));
            InvokeRepeating(nameof(Move), 0, _interval);
        }

        private void Move()
        {
            _direction = _nextDirection;
            transform.position += new Vector3(_direction.x, _direction.y, 0);
        }
    }
}