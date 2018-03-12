using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RVSceneManager : MonoBehaviour {

	public void PlayGame() {
		SceneManager.LoadScene("_Scenes/Home");
	}

	public void OpenWardrobe() {
		SceneManager.LoadScene("_Scenes/Wardrobe");
	}

	public void ReturnToMenu() {
		SceneManager.LoadScene("_Scenes/MainMenu");
	}
}
