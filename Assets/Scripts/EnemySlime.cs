using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour {

	private bool active;
	private Rigidbody2D rb;
	private CircleCollider2D[] colliders;
	private float movement;

	public float speed = -1f;

	// Use this for initialization
	void Start () {
		active = true;
		rb = GetComponent<Rigidbody2D>();
		colliders = GetComponents<CircleCollider2D>();
	}

	// Update is called once per frame
	void Update () {
		if ( active ) {			
			movement = speed * Time.deltaTime;
			// transform.Translate( movement, 0, 0 );		
		}
	}

	void OnTriggerEnter2D( Collider2D other ) {
		if( other.gameObject.tag == "Player" ) {
			Invoke("KillEnemy", 0.1f);
		}
	}

	void KillEnemy() {
		foreach( CircleCollider2D collider in colliders ) {
			collider.enabled = false;
		}
	}
}
