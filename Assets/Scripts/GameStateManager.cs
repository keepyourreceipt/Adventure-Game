using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour {

	public string menuSceneName;

	// Load the first scene
	void LoadMainMenu() {
		SceneManager.LoadScene( menuSceneName );
	}

	void PauseGame() {
		// TODO: pause game
	}
}
