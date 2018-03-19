using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
	public GameObject menuObject;
	public GameObject homeCamera;
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
		logoutButton.transform.Translate (0,-30,0);
		GameObject volumeSlider = menuObject.transform.Find ("Volume Slider").gameObject;
		volumeSlider.SetActive (true);
		if (PlayerPrefs.HasKey("volume")) {
			float volumeValue = PlayerPrefs.GetFloat ("volume");
			volumeSlider.GetComponent<Slider> ().value = volumeValue;
		}
	}

	public void hideAudioSlider() {
		GameObject volumeSlider = menuObject.transform.Find ("Volume Slider").gameObject;
		Debug.Log("Volume value : " + volumeSlider.GetComponent<Slider>().value);
		float volumeValue = volumeSlider.GetComponent<Slider> ().value;
		AudioSource bgm = homeCamera.GetComponent<AudioSource> ();
		bgm.volume = volumeValue / 100;
		PlayerPrefs.SetFloat ("volume", volumeValue);
		volumeSlider.SetActive (false);
		GameObject logoutButton = menuObject.transform.Find ("LogOut").gameObject;
		logoutButton.transform.Translate (0,30,0);
	}
			
	public void logOut() {
		float volumeValue = PlayerPrefs.GetFloat ("volume");
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.SetString ("lastLogin", System.DateTime.Now.ToString ("yyyyMMddHHmmss"));
		PlayerPrefs.SetFloat ("volume", volumeValue);
		SceneManager.LoadScene ("_Scenes/MainMenu");
	}
}
