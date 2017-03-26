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

	public LayerMask mask;
	private Collider2D playerCheck;

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

		// Check to see if the player lands on top of the enemy
		playerCheck = Physics2D.OverlapCircle( new Vector2( transform.position.x - 0.065f, transform.position.y - 0.025f ), 0.25f, mask);

		// If the player fits the slime form the top, kill slime
		if ( playerCheck ) {
			Invoke ("KillEnemy", 0.2f);
			active = false;
		}
	}
		
	void OnCollisionEnter2D( Collision2D other ) {

		if ( other.gameObject.tag == "Obstacles" ) {	

			// Flip the direction the enemy is moving
			speed = speed > 0f ? speed = -speed : speed = Mathf.Abs ( speed );

			// Flip the sprite rendered
			sprite.flipX = !sprite.flipX;		
		}

		if (  other.gameObject.tag == "Player" ) {
			if ( active ) {
				player.TakeDamage ( 20 );
			}
		}
	}
		

	void KillEnemy() {		
		mainCollider.enabled = false;
		active = false;
	}

	void OnDrawGizmos() {
	
		// Enable to see where the overlap circle is being drawn
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (new Vector3 ( transform.position.x - 0.065f, transform.position.y - 0.05f, transform.position.z ), 0.25f);

	}
}
	
