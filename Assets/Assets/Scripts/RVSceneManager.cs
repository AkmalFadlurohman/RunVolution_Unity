using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RVSceneManager : MonoBehaviour {

	public void PlayGame() {
		SceneManager.LoadScene("_Scenes/Home");
	}

	public void QuitGame() {
		Application.Quit();
	}
}
