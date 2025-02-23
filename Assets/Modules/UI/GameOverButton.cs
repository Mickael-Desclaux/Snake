using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GameOverButton : MonoBehaviour
{
    private Button _gameOverButton;

    private void Awake()
    {
        _gameOverButton = GetComponent<Button>();
        _gameOverButton.onClick.AddListener(GameOver);
    }

    private void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        _gameOverButton.onClick.RemoveListener(GameOver);
    }
}
