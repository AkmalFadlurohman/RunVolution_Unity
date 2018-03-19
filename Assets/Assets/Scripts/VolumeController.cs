using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour {
	public GameObject homeCamera;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetFloat ("volume") != null) {
			float volumeValue = PlayerPrefs.GetFloat ("volume");
			AudioSource bgm = homeCamera.GetComponent<AudioSource> ();
			bgm.volume = volumeValue / 100;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
