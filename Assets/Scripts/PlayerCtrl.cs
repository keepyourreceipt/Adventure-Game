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
	}

	void Update() {
		// Player jump
		if ( Input.GetKeyDown( KeyCode.Space ) ) {			
			if ( grounded ) {				
				rb.velocity = new Vector3( rb.velocity.x, jumpForce, 0f );
			}
		}

		// Add a little kick to get the player moving
		if (  Input.GetKeyDown( KeyCode.RightArrow )) {
			if ( rb.velocity.x < 0.25f ) {
				rb.AddForce( new Vector2( 32f, 0f ), ForceMode2D.Impulse );
			}	
		}

		if (  Input.GetKeyDown( KeyCode.LeftArrow )) {			
			if ( rb.velocity.x > -0.25f ) {
				rb.AddForce( new Vector2( -32f, 0f ), ForceMode2D.Impulse );
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

	void OnCollisionEnter2D( Collision2D other  ) {
		if ( other.gameObject.tag == "Enemy" ) {
			rb.AddForce( new Vector2( -25f, 50f ), ForceMode2D.Impulse );
			// Debug.Log( other.gameObject.tag );
		}

		if ( other.gameObject.tag == "Platform" ) 
		{	
			// If the player lands on a moving platform, set the player
			// as a child of the platform to solve movement issues
			gameObject.transform.parent = other.gameObject.transform;
		}
	}

	void OnCollisionExit2D( Collision2D other )
	{			
		if ( other.gameObject.tag == "Platform" )
		{
			// When the player leaves a platform, reset parent to null			
			transform.parent = null;
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		// Gizmos.DrawWireSphere( new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z), 0.25f);
	}
}
