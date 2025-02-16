using System;
using Modules.Level;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    [SerializeField] private Vector2Int _gridSize = new Vector2Int(15, 8);

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
        Instantiate(_snakePrefab, Vector3.zero, Quaternion.identity);
    }
}
