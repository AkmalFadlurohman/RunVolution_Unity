using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
	public GameObject menuObject;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
	}

	// Update is called once per frame
	void Update () {
	}

	public void pause() {
		if (Time.timeScale == 1) {
			Time.timeScale = 0;
			Debug.Log ("Game paused");
			menuObject.SetActive (true);
		}
	}

	public void resume() {
		if (Time.timeScale == 0) {
			Debug.Log ("Game resumed");
			Time.timeScale = 1;
			menuObject.SetActive (false);
		}
	}
	public void logOut() {
		PlayerPrefs.DeleteAll ();
		SceneManager.LoadScene ("_Scenes/MainMenu");
	}
	public void QuitGame() {
		PlayerPrefs.DeleteAll ();
		Application.Quit();
	}
}
