using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	private ScoreKeeper scorekeeper;

	void Start() {
		// Get a reference to the scorekeeper object
		scorekeeper = (ScoreKeeper) FindObjectOfType( typeof(ScoreKeeper) );
	}

	// Check for collisions with player
	void OnTriggerEnter2D( Collider2D other ) {
		if ( other.gameObject.tag == "Player" ) {
			// Destroy coin
			Destroy ( gameObject );

			// Add to score
			scorekeeper.AddToScore(100);
		}
	}

}
