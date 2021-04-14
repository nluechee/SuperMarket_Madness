
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class score : MonoBehaviour
{
    public Text getText;
    public Text scoreText;
    public GameObject gameOverText;
    public GameObject playAgain;
    private int playerScore = 0;
    public List<(string item, bool acquired)> getThis = new List<(string item, bool acquired)>();
    public List<string> items = new List<string>();
    int numItems;
    private int difficulty = 1;
    public int levelsCompleted;
    private float timeLeft;
	int highscore;
	public Text highscoreText;

	// Enemy shoppers (so that they may be toggled active)
	public GameObject enemyShopper1;
	public GameObject enemyShopper2;

    private void Start()
    {
        levelsCompleted = 0;
        numItems = items.Count;
        itemRequest();
        timeLeft = 30.0f;

		// Set the highscore text
		highscore = PlayerPrefs.GetInt("highscore", 0);
		highscoreText.text = ("High score: " + highscore);
    }

    // Update is called once per frame
    void Update()
    {
        //Check if we are moving to next level and the player has gone to the checkout
        scoreText.text = playerScore.ToString();
    }

    public void itemRequest()
    {
        increaseDifficulty();
        int randInt;
        for (int i=0; i<difficulty; i++)
        {
            randInt = Random.Range(0, numItems);
            getThis.Add((items[randInt], false));
        }
        displayGetText();
    }

    public void addPoint()
    {
		int numItemsCollected = 0;
        foreach ((string item, bool acquired) tup in getThis)
        {
            if (tup.acquired == true)
            {
                playerScore += 10;
				numItemsCollected++;
            }
        }
        // point bonus for checking out more items at once
        switch (numItemsCollected)
        {
            case 1:
                playerScore += 0;
                break;
            case 2:
                playerScore += 5;
                break;
            case 3:
                playerScore += 15;
                break;
            case 4:
                playerScore += 25;
                break;
            case 5:
                playerScore += 50;
                break;
            default:
                break;
        }
    }

    public void displayGetText()
    {
        int numItemsCollected = 0;
		string s = "Shopping List: \n ";
		for (int i = 0; i < getThis.Count; i++) {
            if (getThis[i].acquired == false)
            {
               s += "[  ] ";
               s += getThis[i].item;
               s += " \n ";
            }
            else
            {
                numItemsCollected++;
               s += "[x] ";
               s += getThis[i].item;
               s += " \n ";
            }
        }
        if (numItemsCollected == 5)
        {
            s += "Cart full!";
        }
	    getText.text = s;
    }

    public void gameOver()
    {
        if (timeLeft > 0)
        {
            timeLeft -= difficulty;
        }
        else
        {
            gameOverText.SetActive(true);
            playAgain.SetActive(true);
            Time.timeScale = 0;

			// If a new high score, record it
			highscore = PlayerPrefs.GetInt("highscore", 0);
			if(playerScore > highscore){
				highscore = playerScore;
				PlayerPrefs.SetInt ("highscore", highscore);
				PlayerPrefs.Save();
			}

			// Set the highscore text
			highscoreText.text = ("High score: " + highscore);
        }
    }

    private void increaseDifficulty()
    {
        if (levelsCompleted % 3 == 0 && levelsCompleted != 0 && difficulty<11)
        {
            difficulty++;
			if(difficulty >= 4 && !enemyShopper1.activeSelf){
				enemyShopper1.SetActive(true);
			}
			if(difficulty >= 6 && !enemyShopper2.activeSelf){
				enemyShopper2.SetActive(true);
			}
        }
    }

    public int getScore()
    {
        return playerScore;
    }

    public float getTimeLeft()
    {
        return timeLeft;
    }

    public void setTimeLeft(float t)
    {
        timeLeft = t;
    }
}
