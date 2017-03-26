using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {

	public GameObject enemy;
	private BoxCollider2D trigger;

	void Start() {
		trigger = GetComponent<BoxCollider2D>();
	}

	void OnTriggerEnter2D( Collider2D other ) {
		if( other.gameObject.tag == "Player" ) 
		{	
			trigger.enabled = false;
			Instantiate(enemy, transform.position,  Quaternion.identity);
		}
	}
}
