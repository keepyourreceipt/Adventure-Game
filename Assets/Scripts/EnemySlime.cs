using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour {

	private bool active;

	private Rigidbody2D rb;
	private PolygonCollider2D mainCollider;
	private PlayerCtrl player;
	private float movement;
	private SpriteRenderer sprite;

	public float speed = 1f;

	// Use this for initialization
	void Start () {
		active = true;

		rb = GetComponent<Rigidbody2D>();
		mainCollider = GetComponent<PolygonCollider2D>();
		sprite = GetComponent<SpriteRenderer>();

		// Get a reference to the player object
		player = (PlayerCtrl)FindObjectOfType ( typeof(PlayerCtrl) );
	}
		
	void Update () {
		// If enemy is active, move enemy
		if ( active ) {			
			rb.velocity = new Vector2( speed, rb.velocity.y );
		}
	}

	void OnTriggerEnter2D( Collider2D other ) {
		if( other.gameObject.tag == "Player" ) {
			// Invoke("KillEnemy", 0.05f);
		}	
	}
		
	void OnCollisionEnter2D( Collision2D other ) {

		if ( other.gameObject.tag == "Obstacles" ) {	

			// Flip the direction the enemy is moving
			speed = speed > 0f ? speed = -speed : speed = Mathf.Abs ( speed );

			// Flip the sprite rendered
			sprite.flipX = !sprite.flipX;		
		}
	}
		

	void KillEnemy() {		
		mainCollider.enabled = false;
		active = false;
	}
}
	
