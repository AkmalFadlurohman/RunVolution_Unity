﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, Time.deltaTime * 50, 0));
	}

	void OnTriggerEnter(Collider col) {
		Destroy(gameObject);
		PlayerPrefs.SetInt ("itemCount", PlayerPrefs.GetInt("itemCount")-1);
	}
}
