using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public static int score = 0;
	public static int lives = 0;

	[HideInInspector]
	public int livesValue;
	public Text livesText;

	[HideInInspector]
	public int scoreValue;
	public Text scoreText;

	// Add or remove points from player
	public void AddToScore( int points ) {
		score += points;
	}

	// Add or remove lives from player
	public void UpdateLives( int life ) {
		lives += life;
	}

	void Start() {
		scoreValue = 0;
		scoreText.text = "Score: " + scoreValue.ToString();	

		livesValue = 3;				
		livesText.text = "Lives: " + livesValue.ToString();
	}

	   
}
