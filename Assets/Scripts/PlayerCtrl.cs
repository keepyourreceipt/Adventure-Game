using UnityEngine;
using System.Collections;

public class PlayerCtrl : MonoBehaviour {

	private Rigidbody2D rb;
	private Animator anim;
	private SpriteRenderer sprite;

	public Collider2D grounded;
	private float horizAxis;
	private float vertVelocity;

	private LevelManager levelManager;

	//private ScoreKeeper scorekeeper;

	public LayerMask mask;

	public float horizontalSpeed = 20f;
	public float jumpForce = 5f; 

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer>();
		levelManager = (LevelManager) FindObjectOfType( typeof( LevelManager ) );
		// scorekeeper = (ScoreKeeper) FindObjectOfType( typeof(ScoreKeeper) );
	}

	void Update() {
		// Player jump
		if ( Input.GetKeyDown( KeyCode.Space ) ) {			
			if ( grounded ) {				
				rb.velocity = new Vector3( rb.velocity.x, jumpForce, 0f );
			}
		}
	}

	void FixedUpdate() {
		horizAxis = Input.GetAxis("Horizontal");

		// Check if player is grounded
		grounded = Physics2D.OverlapCircle( new Vector2( transform.position.x, transform.position.y - 0.3f ), 0.25f, mask);

		// Set grounded animation variable
		if ( grounded ) {			
			anim.SetBool("grounded", true);
		} else {
			anim.SetBool("grounded", false);
		}
	

		// Move player horizontally
		if ( horizAxis != 0f ) {

			// Calculate movement speed							
			float movementSpeed = horizAxis * horizontalSpeed;

			if ( grounded ) {
				// If moving and on the ground, set animation to running
				anim.SetBool( "running", true );
			} else if ( ! grounded ) {
				// If player is in the air, half horizontal movement control
				movementSpeed = (horizAxis * horizontalSpeed) / 2;
				// If moving and not on the ground, set running animation to false
				anim.SetBool( "running", false );
			}

			// Move the player
			rb.AddForce( new Vector2( movementSpeed, 0.0f ) );

			// Flip sprite depending on direction moving
			if ( horizAxis > 0.1f ) {
				sprite.flipX = false;
			} else if ( horizAxis < -0.01f ) {
				sprite.flipX = true;
			}

		} else if ( horizAxis == 0f ) {
			// If not moving horizontally set running animation to false
			anim.SetBool( "running", false );
		}
	}

	// Kill player and load menu scene
	void OnTriggerEnter2D( Collider2D other ) {
		if ( other.gameObject.tag == "Hazard" ) {
			levelManager.LoadScene( "Main Menu" );
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere( new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z), 0.25f);
	}
}
