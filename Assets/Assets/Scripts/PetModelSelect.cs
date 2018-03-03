using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetModelSelect : MonoBehaviour {

	public GameObject [] slimesPrefab;
	public int currentIndex;
	private Transform self;
	public GameObject modelObject;

	// Use this for initialization
	void Start () {
		self = GetComponent<Transform>();
		modelObject = Instantiate(slimesPrefab[currentIndex], self.position, self.rotation);
		modelObject.transform.parent = self;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F2)) {
			changeNextModel();
		} else if (Input.GetKeyDown(KeyCode.F1)) {
			changePrevModel();
		}
	}

	void changeModel(int index) {
		if (index >= 0 && index < slimesPrefab.Length) {
			currentIndex = index;
			Destroy(modelObject);
			modelObject = Instantiate(slimesPrefab[currentIndex], self.position, self.rotation);
			modelObject.transform.parent = self;
		}
	}
	
	void changeNextModel() {
		currentIndex++;
		if (currentIndex >= slimesPrefab.Length) {
			currentIndex = 0;
		}
		Destroy(modelObject);
		modelObject = Instantiate(slimesPrefab[currentIndex], self.position, self.rotation);
		modelObject.transform.parent = self;
	}

	void changePrevModel() {
		currentIndex--;
		if (currentIndex < 0) {
			currentIndex = slimesPrefab.Length - 1;
		}

		Destroy(modelObject);
		modelObject = Instantiate(slimesPrefab[currentIndex], self.position, self.rotation);
		modelObject.transform.parent = self;
	}
}
