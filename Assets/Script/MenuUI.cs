using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Button PlayButton;
    public Button QuitButton;
    public Button SettingsButton;
    public GameObject pauseMenuUI;
    public GameObject Empty;
    public void Play()
    {
        Empty.GetComponent<StartBeginingCoroutine>().HelpingFunction();
    }
    
    public void Quit()
    {
        Quit();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

}
