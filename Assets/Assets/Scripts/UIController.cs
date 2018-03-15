using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
	public GameObject menuObject;
	public bool clickedAudio;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		clickedAudio = false;
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
	public void setAudioSettings() {
		if (clickedAudio) {
			hideAudioSlider();
			clickedAudio = false;
		} else {
			showAudioSlider();
			clickedAudio = true;
		}
	}
	public void showAudioSlider() {
		GameObject logoutButton = menuObject.transform.Find ("LogOut").gameObject;
		Vector3 relativeLocation = new Vector3(0, -30, 0);
		Vector3 targetLocation = logoutButton.transform.position + relativeLocation;
		Rigidbody2D rb = logoutButton.GetComponent<Rigidbody2D>();
		rb.position = targetLocation;
		GameObject volumeSlider = menuObject.transform.Find ("Volume Slider").gameObject;
		volumeSlider.SetActive (true);
	}

	public void hideAudioSlider() {
		GameObject volumeSlider = menuObject.transform.Find ("Volume Slider").gameObject;
		Debug.Log("Volume value : " + volumeSlider.GetComponent<Slider>().value);
		volumeSlider.SetActive (false);
		GameObject logoutButton = menuObject.transform.Find ("LogOut").gameObject;
		Vector3 relativeLocation = new Vector3(0, 30, 0);
		Vector3 targetLocation = logoutButton.transform.position + relativeLocation;
		Rigidbody2D rb = logoutButton.GetComponent<Rigidbody2D>();
		rb.position = targetLocation;
	}
			
	public void logOut() {
		PlayerPrefs.DeleteAll ();
		SceneManager.LoadScene ("_Scenes/MainMenu");
	}

	IEnumerator SmoothMove(GameObject targetObject,Vector3 targetPosition, float delta) {
		float closeEnough = 0.2f;
		float distance = (targetObject.transform.position - targetPosition).magnitude;

		WaitForEndOfFrame wait = new WaitForEndOfFrame();

		while(distance >= closeEnough)
		{
			Debug.Log("Executing Movement");

			transform.position = Vector3.Slerp(targetObject.transform.position, targetPosition, delta);
			yield return wait;

			// Check if we should repeat
			distance = (targetObject.transform.position - targetPosition).magnitude;
		}

		// Complete the motion to prevent negligible sliding
		targetObject.transform.position = targetPosition;

		Debug.Log ("Movement Complete");
	}

	public void QuitGame() {
		Application.Quit();
	}
}
