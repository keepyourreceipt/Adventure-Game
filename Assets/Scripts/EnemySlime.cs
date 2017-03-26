using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour {

	private bool active;
	private Rigidbody2D rb;
	private CircleCollider2D[] colliders;
	private float movement;
	private SpriteRenderer sprite;

	public float speed = 1f;

	// Use this for initialization
	void Start () {
		active = true;
		rb = GetComponent<Rigidbody2D>();
		colliders = GetComponents<CircleCollider2D>();
		sprite = GetComponent<SpriteRenderer>();
	}

	void OnTriggerEnter2D( Collider2D other ) {
		if( other.gameObject.tag == "Player" ) {
			Invoke("KillEnemy", 0.1f);
		}	
	}

	// When the enemy hits a wall...
	void OnCollisionEnter2D( Collision2D other ) {
		if ( other.gameObject.tag == "Obstacles" ) {	

			// Flip the direction the enemy is moving
			if (  speed > 0f) {				
				speed = -speed;
			} else if ( speed < 0f ) {
				speed = Mathf.Abs( speed );
			}
			// Flip the sprite rendered
			sprite.flipX = !sprite.flipX;		
		}
	}

	// Update is called once per frame
	void Update () {
		if ( active ) {			
			rb.velocity = new Vector2( speed, rb.velocity.y );
		}
	}

	void OnCollisionExit2D( Collision2D other ) {
		if ( other.gameObject.tag == "Ground" ) 
		{
			active = false;
		}
	}

	void KillEnemy() {
		foreach( CircleCollider2D collider in colliders ) {
			collider.enabled = false;
		}

		active = false;
	}
}
