using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class highscoreText : MonoBehaviour
{
	public Text highscoreTextArea;
	int highscore;

    // Start is called before the first frame update
    void Start()
    {
		// Set the highscore text
		highscore = PlayerPrefs.GetInt("highscore", 0);
		highscoreTextArea.text = ("High score: " + highscore);
    }
}
