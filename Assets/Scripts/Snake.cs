using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    [SerializeField] private Transform _tailPrefab;
    [SerializeField] private float _interval = 0.1f;
    [SerializeField] private LayerMask _layer;
    private Vector3 _direction;
    private Vector3 _nextDirection;
    private List<Transform> _tailParts = new();
    private Vector3 _lastTailPosition;

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
        Apple apple = FindFirstObjectByType<Apple>();
        apple.UpdatePosition();
        
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
}
