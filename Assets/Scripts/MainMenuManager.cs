using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject controlsPanel;
    
    public void HandlePlayButton()
    {
        HideStartButton();
        ShowControls();
    }

    public void HandleStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void HideStartButton()
    {
        startButton.SetActive(false);
    }

    private void ShowControls()
    {
        controlsPanel.SetActive(true);
    }
}
