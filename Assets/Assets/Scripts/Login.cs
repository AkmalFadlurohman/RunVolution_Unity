using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {

	public InputField emailField;
	public InputField passwordField;
	public GameObject gameObject;
	public GameObject targetObject;

	public void login() {
		
		Debug.Log ("Clicked login");
		string email = emailField.text;
		string password = passwordField.text;
		Debug.Log ("User with email " + email + " has logged");
		gameObject.SetActive(false);
		targetObject.SetActive (true);
	}
}
