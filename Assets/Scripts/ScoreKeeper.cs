using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public static int score = 000;

	public Text scoreText;

	void Start() {		
		// Set score in the HUD
		scoreText.text = "Score: 000";
	}

	// Add or remove points from player and update the HUD
	public void AddToScore( int points ) {
		score += points;
		scoreText.text = "Score: " + score.ToString();
	} 
}
