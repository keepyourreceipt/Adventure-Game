using UnityEngine;
using System.Collections;

public class PlayerCtrl : MonoBehaviour {

	private Rigidbody2D rb;
	private Animator anim;
	private SpriteRenderer sprite;

	public Collider2D grounded;
	private float horizAxis;
	private float vertVelocity;

	//private ScoreKeeper scorekeeper;

	public LayerMask mask;

	public float horizontalSpeed = 20f;
	public float jumpForce = 5f; 

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer>();
		// scorekeeper = (ScoreKeeper) FindObjectOfType( typeof(ScoreKeeper) );
	}

	// Update is called once per frame
	void Update () {
		// Get velocity to control jump animation tree
		vertVelocity = rb.velocity.y;
		anim.SetFloat("vertVelocity", vertVelocity);	
	}

	void FixedUpdate() {
		horizAxis = Input.GetAxis("Horizontal");

		// Ground detection
		grounded = Physics2D.OverlapCircle( new Vector2( transform.position.x, transform.position.y - 0.45f ), 0.25f, mask);

		// Jump
		if ( Input.GetKeyDown( KeyCode.Space ) ) {			

			if ( grounded ) {				
				rb.velocity = new Vector3( rb.velocity.x, jumpForce, 0f );
			}
		}

		if ( grounded ) {
			// Debug.Log ( grounded.gameObject.layer );
			anim.SetBool("grounded", true);
		} else {
			anim.SetBool("grounded", false);
		}

		// Move player horizontally
		if ( horizAxis != 0f ) {
			rb.AddForce( new Vector2( horizAxis * horizontalSpeed, 0.0f ) );

			// Flip sprite depending on direction moving
			if ( horizAxis > 0.1f ) {
				sprite.flipX = false;
			} else if ( horizAxis < -0.01f ) {
				sprite.flipX = true;
			}
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere( new Vector3(transform.position.x, transform.position.y - 0.45f, transform.position.z), 0.25f);
	}
}
