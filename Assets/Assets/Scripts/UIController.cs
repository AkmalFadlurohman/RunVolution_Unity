using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public Text petNameLevel;
	public Slider hungerSlider;
	public Slider xpSlider;

	public GameObject menuObject;
	public GameObject noticeWindow;

	void OnGUI() {
		string petName = PlayerPrefs.GetString ("petName");
		int petLevel = PlayerPrefs.GetInt ("petLevel");
		int petXP = PlayerPrefs.GetInt ("petXP");
		petNameLevel.text = petName + " Lv." + petLevel;
		xpSlider.value = petXP;
	}

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		float previousRecord = PlayerPrefs.GetFloat ("previousRecord");
		float currentRecord = PlayerPrefs.GetFloat ("currentRecord");
		float delta = currentRecord - previousRecord;
		if (delta > 100) {
			showNoticeWindow ();
		}
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
		SceneManager.LoadScene ("_Scenes/MainMenu");
	}
	public void showNoticeWindow() {
		noticeWindow.SetActive(true);
	}
	public void hideNoticeWindow() {
		noticeWindow.SetActive(false);
	}
	public void QuitGame() {
		Application.Quit();
	}
}
