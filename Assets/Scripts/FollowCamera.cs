using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

	public Transform gameObjectToFollow;
	// private Vector3 cameraStartingPos;

	// Use this for initialization
	void Start () {
		// Position the camera on start
		// cameraStartingPos = new Vector3( gameObjectToFollow.position.x, gameObjectToFollow.position.y + 4f, -10f );
	}

	// Update is called once per frame
	void LateUpdate () {
		// Move the camera on player movement
		if ( gameObjectToFollow.position.x >= 0f ) {
			transform.position = new Vector3( gameObjectToFollow.position.x, transform.position.y, -10f );
		} else if ( gameObjectToFollow.position.x < 0f ) {
			transform.position = new Vector3( 0f, transform.position.y, -10f );
		}
	}
}
