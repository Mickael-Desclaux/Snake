using System;
using Modules.Level;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    [SerializeField] private Vector2Int _gridSize = new Vector2Int(16, 8);

    [SerializeField] private Snake _snakePrefab;
    [SerializeField] private GameObject _applePrefab;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(_instance.gameObject);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        SpawnSnake();
        SpawnApple();
    }

    private void SpawnSnake()
    {
        Instantiate(_snakePrefab, Vector3.zero, Quaternion.identity);
    }

    private void SpawnApple()
    {
        Vector3 position = Vector3.zero;

        while (position == Vector3.zero)
        {
            int gridRandomX = Random.Range(-_gridSize.x / 2, _gridSize.x / 2);
            int gridRandomY = Random.Range(-_gridSize.y / 2, _gridSize.y / 2);
            position = new Vector3(gridRandomX, gridRandomY, 0);
        }
        
        Instantiate(_applePrefab, position, Quaternion.identity);
    }
}
