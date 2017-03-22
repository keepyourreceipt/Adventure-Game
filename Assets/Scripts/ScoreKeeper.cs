using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public static int score = 000;
	public static int lives = 0;

	public Text livesText;
	public Text scoreText;

	void Start() {		
		// Set score in the HUD
		scoreText.text = "Score: 000";	

		// Set lives in the HUD
		livesText.text = "Lives: " + lives.ToString();
	}

	// Add or remove points from player and update the HUD
	public void AddToScore( int points ) {
		score += points;
		scoreText.text = "Score: " + score.ToString();
	}

	// Add or remove lives from player
	public void UpdateLives( int life ) {
		lives += life;
	}

	   
}
