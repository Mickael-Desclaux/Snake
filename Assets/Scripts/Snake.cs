using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private float _interval = 0.1f;
    private Vector3 _direction;

    private void Start()
    {
        _direction = Vector3.right;
        InvokeRepeating(nameof(Move), 0, _interval);
    }

    private void Move()
    {
        transform.position += _direction;
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && _direction != Vector3.down)
        {
            _direction = Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && _direction != Vector3.up)
        {
            _direction = Vector3.down;
        } 
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && _direction != Vector3.right)
        {
            _direction = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && _direction != Vector3.left)
        {
            _direction = Vector3.right;
        }
    }

    private void LateUpdate()
    {
        HandleInput();
    }
}
