using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

[System.Serializable]
public class petModel : System.Object
{
    public GameObject[] skins;

    public int getSkinCount() {
    	return skins.Length;
    }
}

public class PetModelSelect : MonoBehaviour {

	public petModel[] petPrefabs;
	public int currentType;	
	public int currentSkin;
	private Transform self;
	public GameObject modelObject;
	public bool debug;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("petType") != -1 && PlayerPrefs.GetInt ("petSkin") != -1) {
			currentType = PlayerPrefs.GetInt ("petType");
			currentSkin = PlayerPrefs.GetInt ("petSkin");
		}
		self = GetComponent<Transform>();
		modelObject = Instantiate(petPrefabs[currentType].skins[currentSkin], self.position, self.rotation);
		modelObject.transform.parent = self;
	}
	
	// Update is called once per frame
	void Update () {
		if (debug) {
			if (Input.GetKeyDown(KeyCode.F2)) {
				changeNextModel();
			} else if (Input.GetKeyDown(KeyCode.F1)) {
				changePrevModel();
			}

			if (Input.GetKeyDown(KeyCode.F4)) {
				changeNextSkin();
			} else if (Input.GetKeyDown(KeyCode.F3)) {
				changePrevSkin();
			}
		}
	}

	public void changeModel(int index) {
		if (index >= 0 && index < petPrefabs.Length) {
			currentType = index;
			Destroy(modelObject);
			currentSkin = 0;
			modelObject = Instantiate(petPrefabs[currentType].skins[currentSkin], self.position, self.rotation);
			modelObject.transform.parent = self;
		}
	}
	
	public void changeNextModel() {
		currentType++;
		if (currentType >= petPrefabs.Length) {
			currentType = 0;
		}
		Destroy(modelObject);
		currentSkin = 0;
		modelObject = Instantiate(petPrefabs[currentType].skins[currentSkin], self.position, self.rotation);
		modelObject.transform.parent = self;
		GetComponent<AudioSource>().Play ();
	}

	public void changePrevModel() {
		currentType--;
		if (currentType < 0) {
			currentType = petPrefabs.Length - 1;
		}

		Destroy(modelObject);
		currentSkin = 0;
		modelObject = Instantiate(petPrefabs[currentType].skins[currentSkin], self.position, self.rotation);
		modelObject.transform.parent = self;
		GetComponent<AudioSource>().Play ();
	}

	public void changeSkin(int index) {
		if (index >= 0 && index < petPrefabs[currentType].getSkinCount()) {
			currentSkin = index;
			Destroy(modelObject);
			modelObject = Instantiate(petPrefabs[currentType].skins[currentSkin], self.position, self.rotation);
			modelObject.transform.parent = self;
		}
	}
	
	public void changeNextSkin () {
		currentSkin++;
		if (currentSkin >= petPrefabs[currentType].getSkinCount()) {
			currentSkin = 0;
		}
		Destroy(modelObject);
		modelObject = Instantiate(petPrefabs[currentType].skins[currentSkin], self.position, self.rotation);
		modelObject.transform.parent = self;
	}

	public void changePrevSkin() {
		currentSkin--;
		if (currentSkin < 0) {
			currentSkin = petPrefabs[currentType].getSkinCount() - 1;
		}

		Destroy(modelObject);
		modelObject = Instantiate(petPrefabs[currentType].skins[currentSkin], self.position, self.rotation);
		modelObject.transform.parent = self;
	}

	public void saveAppearances() {
		PlayerPrefs.SetInt ("petType", currentType);
		PlayerPrefs.SetInt ("petSkin", currentSkin);
		Debug.Log ("Saved pet settings with type : " + currentType + " & skin : " + currentSkin);
		int petID = PlayerPrefs.GetInt ("petId");
		StartCoroutine (updatePetAppearance(petID,currentType,currentSkin));
		SceneManager.LoadScene ("_Scenes/Home");
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
