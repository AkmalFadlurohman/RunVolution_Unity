using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Evolution : MonoBehaviour {
	public GameObject monster;
	public GameObject camera;
	public float rotation = 0;
	public float acc = 1;
	public float cameraSpeed = 0f;
	public float cameraAcc = 0.00001f;
	public float maxSpeed = 100.0f;
	private bool accelerating = true;
	private bool done = false;

	void Start() {
		AudioSource bgm = camera.GetComponent<AudioSource> ();
		bgm.Play ();
	}

	void Update() {
		if (!done) {
			if (accelerating) {
				rotation += acc;
				monster.transform.Rotate(0, rotation, 0);	

				cameraSpeed += cameraAcc;
				camera.transform.Translate(0, 0, cameraSpeed);

				if (rotation >= maxSpeed) {
					accelerating = false;
					evolveModel();
				}
			} else {
				rotation -= acc;
				monster.transform.Rotate(0, rotation, 0);

				cameraSpeed -= cameraAcc;
				camera.transform.Translate(0, 0, -cameraSpeed);

				if (rotation <= 0) {
					done = true;
					monster.transform.LookAt(camera.transform);
					AudioSource bgm = camera.GetComponent<AudioSource> ();
					bgm.Pause ();
					SceneManager.LoadScene ("_Scenes/Home");
				}
			}
		}
	}

	void evolveModel() {
		PetModelSelect petSelect = monster.GetComponent(typeof(PetModelSelect)) as PetModelSelect;
		if (petSelect != null) {
			petSelect.changeNextModel();
			PlayerPrefs.SetInt ("petType", petSelect.currentType);
			int petID = PlayerPrefs.GetInt ("petId");
			StartCoroutine (updatePetAppearance(petID,petSelect.currentType,petSelect.currentSkin));
		}
	}

	void evolveSkin() {
		PetModelSelect petSelect = monster.GetComponent(typeof(PetModelSelect)) as PetModelSelect;
		if (petSelect != null) {
			petSelect.changeNextSkin();
		}
	}

	public  IEnumerator updatePetAppearance(int petID,int type,int skin) {
		WWWForm data = new WWWForm();
		data.AddField("petid", petID);
		data.AddField ("type", type);
		data.AddField ("skin", skin);
		UnityWebRequest www = UnityWebRequest.Post("https://runvolution.herokuapp.com/updatepetappearance",data);
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log (www.error);
		}
		else {
			string msg = www.downloadHandler.text;
			if (msg != null) {
				Debug.Log (msg);
				if (msg.Equals ("OK")) {
					Debug.Log ("Updated pet appearance setting on server");
				}
			} else {
				Debug.Log ("Data not found");
			}
		}
	}
}
