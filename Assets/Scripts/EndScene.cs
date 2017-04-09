using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour {

	//public Text gameOver;
	public Text score;
	public Text highScore;

	private int scoreNum;
	private int highScoreNum;

	public Button menuButton;







	// Use this for initialization
	void Start () {
		scoreNum = PlayerPrefs.GetInt ("score");
		highScoreNum = PlayerPrefs.GetInt ("highScore");
	}
	
	// Update is called once per frame
	void Update () {
		//set on text score for last run
		score.text = "Score: " + scoreNum;

		//if it is a new highscore say so, else don't
		if (scoreNum > highScoreNum) {
			highScore.text = "NEW HIGHSCORE!!!";
			PlayerPrefs.SetInt ("highScore", scoreNum);
		}
		else
			highScore.text = "";
	}

	public void goToMenu() {
		SceneManager.LoadScene ("main");
	}

}
