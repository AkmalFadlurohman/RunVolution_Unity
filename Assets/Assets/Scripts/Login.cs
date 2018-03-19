using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using SimpleJSON;

public class Login : MonoBehaviour {

	public InputField emailField;
	public InputField passwordField;
	public GameObject menuObject;
	public GameObject promptWindow;


	public void login() {
		Debug.Log ("Clicked login");
		string email = emailField.text;
		string password = passwordField.text;
		Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
		Match match = regex.Match(email);
		if (email.Length == 0 || password.Length == 0) {
			showPromptWindow ();
		} else if (!match.Success) {
			showPromptWindow ();
		} else {
			Debug.Log ("User with email " + email + " has logged");
			StartCoroutine(attemptLogin(email,password));
		}
	}
	public  IEnumerator attemptLogin(string email,string password) {
		WWWForm formData = new WWWForm();
		formData.AddField("email", email);
		formData.AddField ("password", password);

		UnityWebRequest www = UnityWebRequest.Post("https://runvolution.herokuapp.com/login", formData);
		www.uploadHandler.contentType = "application/x-www-form-urlencoded";
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log (www.error);
		}
		else {
			string msg = www.downloadHandler.text;
			Debug.Log(msg);
			emailField.text = "";
			passwordField.text = "";
			if (msg.Equals ("OK")) {
				StartCoroutine (getUserData (email));
			} else {
				//EditorUtility.DisplayDialog ("Login Error", "Invalid email or password", "OK");
				showPromptWindow();
			}
			
		}
	}

	public  IEnumerator getUserData(string email) {
		string param = "?email=" + email;
		UnityWebRequest www = UnityWebRequest.Get("https://runvolution.herokuapp.com/fetchuser"+param);
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log (www.error);
		}
		else {
			string msg = www.downloadHandler.text;
			if (msg != null) {
				Debug.Log (msg);
				var data = JSON.Parse (msg);
				int petId = data ["pet_id"].AsInt;
				PlayerPrefs.SetString ("name", data ["name"].Value);
				PlayerPrefs.SetString ("email", data ["email"].Value);
				PlayerPrefs.SetFloat ("previousRecord", data ["previous_record"].AsFloat);
				PlayerPrefs.SetFloat ("currentRecord", data ["current_record"].AsFloat);
				StartCoroutine (getPetData (petId));
			} else {
				Debug.Log ("Data not found");
			}
			
		}
	}

	public  IEnumerator getPetData(int petID) {
		string param = "?petid=" + petID;
		UnityWebRequest www = UnityWebRequest.Get("https://runvolution.herokuapp.com/fetchpet"+param);
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log (www.error);
		}
		else {
			string msg = www.downloadHandler.text;
			if (msg != null) {
				Debug.Log (msg);
				var data = JSON.Parse (msg);
				PlayerPrefs.SetInt ("petId", data ["id"].AsInt);
				PlayerPrefs.SetString ("petName", data ["name"].Value);
				PlayerPrefs.SetInt ("petLevel", data ["level"].AsInt);
				PlayerPrefs.SetInt ("petXP", data ["name"].AsInt);
				int petType = data ["type"].AsInt;
				int petSkin = data ["skin"].AsInt;
				menuObject.SetActive (false);
				if (petType == -1 && petSkin == -1) {
					SceneManager.LoadScene ("_Scenes/Wardrobe");
				} else {
					PlayerPrefs.SetInt ("petType", petType);
					PlayerPrefs.SetInt ("petSkin", petSkin);
					SceneManager.LoadScene ("_Scenes/Home");
				}
			} else {
				Debug.Log ("Data not found");
			}
		}
	}

	public void showPromptWindow() {
		promptWindow.SetActive(true);
	}

	public void hidePromptWindow() {
		promptWindow.SetActive(false);
	}
	public void quitGame() {
		Application.Quit();
	}
}
