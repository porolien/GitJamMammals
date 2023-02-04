using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Button PlayButton;
    public Button QuitButton;
    public Button SettingsButton;
    public GameObject pauseMenuUI;
    // Start is called before the first frame update


    public void Play()
    {
        SceneManager.LoadScene("Aure");
    }
    public void Quit()
    {
        SceneManager.LoadScene("MenuStart");
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
