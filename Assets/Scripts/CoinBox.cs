using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBox : MonoBehaviour {

	private ScoreKeeper scorekeeper;
	private Animator anim;

	// Use this for initialization
	void Start () {

		// Get reference to scorekeeper object
		scorekeeper = (ScoreKeeper) FindObjectOfType( typeof(ScoreKeeper) );

		// Get reference to the animator component
		anim = GetComponent<Animator>();

	}

	void OnTriggerEnter2D( Collider2D other ) {
		if (  other.gameObject.tag == "Player") {

			if ( anim.GetBool( "coinBoxDisabled" ) == false ) {
				// Set animation vaiable to trigger hit animation
				anim.SetBool( "coinBoxHit", true );

				// Set animation variable to transition to disabled
				anim.SetBool( "coinBoxDisabled", true );

				scorekeeper.AddToScore( 100 );
			}
		}
	}

}
