using System.Collections.Generic;
using System.Linq;
using Modules.Controls;
using UnityEngine;

namespace Modules.Level
{
    public class Snake : MonoBehaviour
    {
        [SerializeField] private float _interval = 0.2f;
        
        [SerializeField] private Transform _tailPrefab;
        private List<Transform> _tailParts = new();
        public List<Transform> TailParts => _tailParts;
        private Vector3 _lastTailPosition;
        
        private Vector2 _direction = Vector2.right;
        private Vector2 _nextDirection = Vector2.right;

        private ControlsManager _controlsManager = ControlsManager.Instance;
        private GameManager _gameManager = GameManager.Instance;

        private void Start()
        {
            _tailParts.Add(transform);
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
            _lastTailPosition = _tailParts.Last().position;
        
            for (int i = _tailParts.Count - 1; i > 0; i--)
            {
                Vector3 newPosition = _tailParts[i - 1].position;
                _tailParts[i].position = newPosition;
            }
            
            _direction = _nextDirection;
            transform.position += new Vector3(_direction.x, _direction.y, 0);
            
            if (transform.position.x > _gameManager.GridSize.x / 2f ||
                transform.position.x < -_gameManager.GridSize.x / 2f ||
                transform.position.y > _gameManager.GridSize.y / 2f ||
                transform.position.y < -_gameManager.GridSize.y /2f)
            {
                _gameManager.GameOver();
                return;
            }

            if (HasBiteItself())
            {
                _gameManager.GameOver();
                return;
            }
            
            Vector3 applePosition = _gameManager.ApplePosition;
            if (transform.position == applePosition)
            {
                Eat();
            }
        }

        private void Eat()
        {
            _gameManager.UpdateApplePosition();
            Transform newTail = Instantiate(_tailPrefab, _lastTailPosition, Quaternion.identity);
            _tailParts.Add(newTail);
        }
        
        private bool HasBiteItself()
        {
            for (int i = _tailParts.Count - 1; i > 0; i--)
            {
                if (_tailParts[i].position == transform.position)
                {
                    return true;
                }
            }

            return false;
        }
    }
}