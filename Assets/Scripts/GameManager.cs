using System.Linq;
using Modules.Level;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    [SerializeField] private Vector2Int _gridSize = new Vector2Int(16, 8);
    public Vector2Int GridSize => _gridSize;
    
    [SerializeField] private GameObject _borderPrefab;

    [SerializeField] private Snake _snakePrefab;
    private Snake _snake;
    
    [SerializeField] private GameObject _applePrefab;
    private GameObject _apple;

    [SerializeField] private GameObject _gameOverCanvasPrefab;

    public Vector2 ApplePosition { get; private set; }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(_instance.gameObject);
            return;
        }

        _instance = this;
    }

    public void NewGame()
    {
        CreateBorder();
        SpawnSnake();
        SpawnApple();
    }

    private void CreateBorder()
    {
        for (int i = -_gridSize.x / 2 - 1; i <= _gridSize.x / 2 + 1; i++)
        {
            Vector3 bottomPosition = new Vector3(i, -_gridSize.y / 2f - 1, 0);
            Vector3 topPosition = new Vector3(i, _gridSize.y / 2f + 1, 0);

            Instantiate(_borderPrefab, bottomPosition, Quaternion.identity);
            Instantiate(_borderPrefab, topPosition, Quaternion.identity);
        }
        
        for (int j = -_gridSize.y / 2; j <= _gridSize.y / 2; j++)
        {
            Vector3 leftPosition = new Vector3(-_gridSize.x / 2f - 1, j, 0);
            Vector3 rightPosition = new Vector3(_gridSize.x / 2f + 1, j, 0);

            Instantiate(_borderPrefab, leftPosition, Quaternion.identity);
            Instantiate(_borderPrefab, rightPosition, Quaternion.identity);     
        }
    }

    private void SpawnSnake()
    {
        _snake = Instantiate(_snakePrefab, Vector3.zero, Quaternion.identity);
    }

    private void SpawnApple()
    {
        Vector3 position = Vector3.zero;
        bool isValidPosition = true;
        
        while (isValidPosition)
        {
            int gridRandomX = Random.Range(-_gridSize.x / 2, _gridSize.x / 2);
            int gridRandomY = Random.Range(-_gridSize.y / 2, _gridSize.y / 2);
            position = new Vector3(gridRandomX, gridRandomY, 0);
            
            if (IsValidPosition(position))
            {
                isValidPosition = false;
            }
        }
        
        ApplePosition = new Vector2(position.x, position.y);
        _apple = Instantiate(_applePrefab, position, Quaternion.identity);
    }

    private bool IsValidPosition(Vector3 position)
    {
        return _snake.TailParts.All(t => t.position != position);
    }

    public void UpdateApplePosition()
    {
        Destroy(_apple);
        SpawnApple();
    }

    public void GameOver()
    {
        Instantiate(_gameOverCanvasPrefab);
    }
}
