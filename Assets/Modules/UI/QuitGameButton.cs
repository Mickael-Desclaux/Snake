using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI
{
    [RequireComponent(typeof(Button))]
    public class QuitGameButton : MonoBehaviour
    {
        private Button _quitGameButton;

        private void Awake()
        {
            _quitGameButton = GetComponent<Button>();
            _quitGameButton.onClick.AddListener(QuitGame);
        }

        private void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void OnDestroy()
        {
            _quitGameButton.onClick.RemoveListener(QuitGame);
        }
    }
}
