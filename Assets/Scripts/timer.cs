using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    private float maxTime = 30.0f;
    private float timeRemaining;
    private bool timerRunning = true;
    public score scoreScript;
    public Image timerBar;
    private float timeToGive;
    bool ticking;


    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = 30.0f;
        timerRunning = true;
        ticking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining = scoreScript.getTimeLeft();
                timeRemaining -= Time.deltaTime;
                scoreScript.setTimeLeft(timeRemaining);
                timerBar.fillAmount = timeRemaining / maxTime;
                ticking = FindObjectOfType<audio_manager>().isPlaying("ticking");
                if (timeRemaining < 7.5f && ticking == false && Time.timeScale!=0)
                {
                    FindObjectOfType<audio_manager>().Play("ticking");
                }
            }
            else
            {
                scoreScript.gameOver();
                timerRunning = false;
            }
        }
    }

	public void setTimer(int itemsCashed){
        FindObjectOfType<audio_manager>().Stop("ticking");
        switch (itemsCashed)
        {
            case -1:
                timeToGive = timeRemaining + 15f;
                if (timeToGive > maxTime)
                {
                    timeToGive = maxTime;
                }
                scoreScript.setTimeLeft(timeToGive);
                break;
            case 1:
                timeToGive = timeRemaining + 1f;
                if (timeToGive > maxTime)
                {
                    timeToGive = maxTime;
                }
                scoreScript.setTimeLeft(timeToGive);
                break;
            case 2:
                timeToGive = timeRemaining + 3f;
                if (timeToGive > maxTime)
                {
                    timeToGive = maxTime;
                }
                scoreScript.setTimeLeft(timeToGive);
                break;
            case 3:
                timeToGive = timeRemaining + 7f;
                if (timeToGive > maxTime)
                {
                    timeToGive = maxTime;
                }
                scoreScript.setTimeLeft(timeToGive);
                break;
            case 4:
                timeToGive = timeRemaining + 10f;
                if (timeToGive > maxTime)
                {
                    timeToGive = maxTime;
                }
                scoreScript.setTimeLeft(timeToGive);
                break;
            case 5:
                timeToGive = timeRemaining + 15f;
                if (timeToGive > maxTime)
                {
                    timeToGive = maxTime;
                }
                scoreScript.setTimeLeft(timeToGive);
                break;
        }	
	}

}
