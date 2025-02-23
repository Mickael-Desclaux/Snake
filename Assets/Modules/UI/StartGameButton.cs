using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI
{
    [RequireComponent(typeof(Button))]
    public class StartGameButton : MonoBehaviour
    {
        private Button _startGameButton;

        private void Awake()
        {
            _startGameButton = GetComponent<Button>();
            _startGameButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            GameManager.Instance.NewGame();
            _startGameButton.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveListener(StartGame);
        }
    }
}
