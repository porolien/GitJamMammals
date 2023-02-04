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

    // Start is called before the first frame update
    

    public void Play()
    {
        SceneManager.LoadScene("Alban_Test");
    }
    public void Quit()
    {
        SceneManager.LoadScene("MenuStart");
    }
}
