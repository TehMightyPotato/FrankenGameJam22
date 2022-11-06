using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menu
{
    public class LostGame : MonoBehaviour
    {
        public void HandleRetryButton()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void HandleQuitButton()
        {
            Application.Quit();
        }
    }
}
