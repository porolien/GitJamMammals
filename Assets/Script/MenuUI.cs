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
    public Road TheRoad;
    public GameObject[] AllBlock;
    public Timer Timer;

    public void Play()
    {
        Timer.TheTiming();
        AllBlock = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in AllBlock)
        {
            block.GetComponent<Road>().speed = 1;
        }
        AllBlock = GameObject.FindGameObjectsWithTag("Parallax");
        foreach (GameObject block in AllBlock)
        {
            block.GetComponent<Road>().speed = 1;
        }
        Empty.GetComponent<StartBeginingCoroutine>().HelpingFunction();
    }

    public void Quit()
    {
        Quit();
    }
    public void Retry()
    { 

        SceneManager.UnloadSceneAsync("Alban_test");
        SceneManager.LoadScene("Alban_test");
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
