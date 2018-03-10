using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using SimpleJSON;

public class Login : MonoBehaviour {

	public InputField emailField;
	public InputField passwordField;
	public GameObject gameObject;
	public GameObject targetObject;

	public void login() {
		Debug.Log ("Clicked login");
		string email = emailField.text;
		string password = passwordField.text;
		Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
		Match match = regex.Match(email);
		if (email.Length == 0 || password.Length == 0) {
			EditorUtility.DisplayDialog ("Login Error", "Email or Password can not be empty", "OK");
		} else if (!match.Success) {
			EditorUtility.DisplayDialog ("Login Error", "Please enter a valid email address", "OK");
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
				EditorUtility.DisplayDialog ("Login Error", "Invalid email or password", "OK");
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
				Debug.Log (petId);
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
				gameObject.SetActive (false);
				targetObject.SetActive (true);
			} else {
				Debug.Log ("Data not found");
			}
		}
	}
}
