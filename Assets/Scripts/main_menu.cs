using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main_menu : MonoBehaviour
{

    public GameObject optionsUI, howToPlayUI;

    private void Awake()
    {
        bool isTicking = FindObjectOfType<audio_manager>().isPlaying("ticking");
        if (isTicking)
        {
            FindObjectOfType<audio_manager>().Stop("ticking");
        }   
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Options()
    {
        optionsUI.SetActive(true);
    }

    public void HowToPlay()
    {
        howToPlayUI.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
