using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour {

	public Scene gameStartScene;

	// Load the first scene
	void GameOver() {
		SceneManager.LoadScene( gameStartScene.name );
	}
}
