using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Old
{
    public class OldSnake : MonoBehaviour
    {
        [SerializeField] private Transform _tailPrefab;
        [SerializeField] private float _interval = 0.1f;
        [SerializeField] private LayerMask _layer;
        private Vector3 _direction;
        private Vector3 _nextDirection;
        private List<Transform> _tailParts = new();
        private Vector3 _lastTailPosition;
        private Vector2 _gridSize;

        private void Awake()
        {
            _gridSize = new Vector2(15, 8);
        }

        private void Start()
        {
            _tailParts.Add(transform);
            _nextDirection = Vector3.right;
            InvokeRepeating(nameof(Move), 0, _interval);
        }
    
        private void LateUpdate()
        {
            HandleInput();
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & _layer.value) != 0)
            {
                GameOver();
                return;
            }
        
            Eat();
        }

        private void Move()
        {
            if (_direction == -_nextDirection)
            {
                GameOver();
                return;
            }

            if (transform.position.x > _gridSize.x ||
                transform.position.x < -_gridSize.x ||
                transform.position.y > _gridSize.y ||
                transform.position.y < -_gridSize.y)
            {
                GameOver();
                return;
            }

            _direction = _nextDirection;
        
            _lastTailPosition = _tailParts.Last().position;
        
            for (int i = _tailParts.Count - 1; i > 0; i--)
            {
                Vector3 newPosition = _tailParts[i - 1].position;
                _tailParts[i].position = newPosition;
            }

            transform.position += _direction;
        }

        private void Eat()
        {
            OldApple oldApple = FindFirstObjectByType<OldApple>();
            oldApple.UpdatePosition();
        
            Transform newTail = Instantiate(_tailPrefab, _lastTailPosition, Quaternion.identity);
            _tailParts.Add(newTail);
        }

        private void GameOver()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && _nextDirection != Vector3.down)
            {
                _nextDirection = Vector3.up;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && _nextDirection != Vector3.up)
            {
                _nextDirection = Vector3.down;
            } 
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && _nextDirection != Vector3.right)
            {
                _nextDirection = Vector3.left;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && _nextDirection != Vector3.left)
            {
                _nextDirection = Vector3.right;
            }
        }
    
        private void OnDrawGizmos()
        {
            // Define the bounds of the grid
            float halfWidth = _gridSize.x;
            float halfHeight = _gridSize.y;

            // Define the corners of the rectangle
            Vector3 topLeft = new Vector3(-halfWidth, halfHeight, 0);
            Vector3 topRight = new Vector3(halfWidth, halfHeight, 0);
            Vector3 bottomLeft = new Vector3(-halfWidth, -halfHeight, 0);
            Vector3 bottomRight = new Vector3(halfWidth, -halfHeight, 0);

            // Set the color to red
            Gizmos.color = Color.red;

            // Draw the wireframe rectangle
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);
        }
    }
}
