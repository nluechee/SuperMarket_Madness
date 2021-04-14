using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class play_again : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
        FindObjectOfType<audio_manager>().Play("music");
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        FindObjectOfType<audio_manager>().Stop("ticking");
        FindObjectOfType<audio_manager>().Stop("music");
        SceneManager.LoadScene("Menu");
        FindObjectOfType<audio_manager>().Play("music");
    }
}
