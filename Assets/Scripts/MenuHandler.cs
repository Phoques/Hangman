using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    #region Scene and Panel control

    public GameObject pausePanel;
    private void Awake()
    {
        pausePanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    } 

   public void ExitGame()
    {
        Application.Quit();
    }

    public void PausePanel()
    {
        if (pausePanel != null)
        {// Set Panel as active
            pausePanel.SetActive(true);
        }
    }

    public void UnpausePanel()
    {
        if (pausePanel != null)
        {// Set Panel as active
            pausePanel.SetActive(false);
        }
    }
    #endregion
}
