﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinder : MonoBehaviour {

	void OnCollisionEnter2D( Collision2D other ) {
		if ( other.gameObject.tag != "Player" ) 
		{
			Destroy ( other.gameObject );
		}
	}

	void OnTriggerEnter2D( Collider2D other ) {
		Destroy ( other.gameObject );
	}
}
