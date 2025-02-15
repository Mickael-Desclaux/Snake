using System;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector3 _direction;

    private void Start()
    {
        _direction = Vector3.right;
    }

    private void Move()
    {
        transform.position += _direction * Time.deltaTime;
    }

    private void LateUpdate()
    {
        Move();
    }
}
