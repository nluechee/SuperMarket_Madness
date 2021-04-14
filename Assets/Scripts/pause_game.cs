using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause_game : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseUI;
    public GameObject optionsUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("pause"))
        {
            if (gamePaused)
            {
                // resume
                resume();
            }
            else
            {
                // pause
                pause();
            }
        }
    }

    public void resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    void pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        FindObjectOfType<audio_manager>().Stop("ticking");
        gamePaused = true;
    }

    public void quitToMenu()
    {
        Time.timeScale = 1f;
        FindObjectOfType<audio_manager>().Stop("ticking");
        FindObjectOfType<audio_manager>().Stop("music");
        SceneManager.LoadScene("Menu");
        FindObjectOfType<audio_manager>().Play("music");
    }

    public void options()
    {
        optionsUI.SetActive(true);
    }

    public void closeOptions()
    {
        optionsUI.SetActive(false);
    }
}
