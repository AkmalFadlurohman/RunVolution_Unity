using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
