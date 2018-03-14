using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {
	public GameObject modelObject;

	void Start () {
		modelObject.name = "Food";
		int count = 3;
		Vector3 position = new Vector3(-6, 1, 0);
		for (int i=0;i<count;i++) {
			GameObject itemObject = Instantiate(modelObject, position, modelObject.transform.rotation);
			itemObject.SetActive (true);
			position.x += 2;
			if (position.x == 4) {
				position.x = -6;
				position.z += 2;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
