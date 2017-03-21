using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	// Check for collisions with player
	void OnTriggerEnter2D( Collider2D other ) {
		if ( other.gameObject.tag == "Player" ) {
			Destroy ( gameObject );

			// TODO: add points to score
		}
	}

}
