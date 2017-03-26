using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour {

	private Rigidbody2D rb;
	private Animator anim;
	private SpriteRenderer sprite;

	public Collider2D grounded;
	private float horizAxis;
	private float vertVelocity;

	private LevelManager levelManager;
	private int playerHealth = 100;

	private int lives = 3;

	public Text playerHealthUI;
	public Text playerLivesUI;

	public LayerMask mask;

	public float horizontalSpeed = 12f;
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
			if ( rb.velocity.x < 1f ) {						
				rb.AddForce( new Vector2( 50f, 0f ), ForceMode2D.Impulse );
			}	
		}

		if (  Input.GetKeyDown( KeyCode.LeftArrow )) {			
			if ( rb.velocity.x > -1f ) {
				rb.AddForce( new Vector2( -50f, 0f ), ForceMode2D.Impulse );
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

	void OnTriggerEnter2D( Collider2D other ) {
		if ( other.gameObject.tag == "Hazard" ) {
			TakeDamage(100);
		}
	}

	void OnCollisionEnter2D( Collision2D other  ) {
		// Check to see if the player is touching a platform
		if ( other.gameObject.tag == "Platform" ) 
		{				
			gameObject.transform.parent = other.gameObject.transform;
		}
	}

	public void TakeDamage( int damage )
	{
		playerHealth = playerHealth - damage;

		if ( playerHealth > 0 ) 
		{
			playerHealthUI.text = "HEALTH :" + playerHealth.ToString() + "%";
		} else if ( playerHealth <= 0 ) {
			playerHealthUI.text = "HEALTH :0%";
			UpdateLives( -1 );
			if ( lives == 0 ) {
				GameOver();
			} else if ( lives > 0 )  {
				ResetPlayer();
			}
		}

	}

	void ResetPlayer() 
	{
		rb.velocity = new Vector3( 0,0,0 );
		transform.position = new Vector3( 0, 1, 0 );
		playerHealth = 100;
		playerHealthUI.text = "HEALTH :" + playerHealth.ToString() + "%";
		sprite.flipX = false;
		// Remove player life
	}

	void OnCollisionExit2D( Collision2D other )
	{			
		if ( other.gameObject.tag == "Platform" )
		{
			// When the player leaves a platform, reset parent to null			
			transform.parent = null;
		}
	}

	void UpdateLives( int life ) {
		lives += life;
		playerLivesUI.text = "X " + lives.ToString();
	}

	void GameOver() {
		levelManager.LoadScene("GameOver");
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		// Gizmos.DrawWireSphere( new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z), 0.25f);
	}
}
