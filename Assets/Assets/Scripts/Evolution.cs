using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evolution : MonoBehaviour {
	public GameObject monster;
	public GameObject camera;
	public float rotation = 0;
	public float acc = 5;
	public float cameraSpeed = 0f;
	public float cameraAcc = 0.001f;
	public float maxSpeed = 100.0f;
	private bool accelerating = true;

	void Start() {
		
	}

	void Update() {
		if (accelerating) {
			rotation += acc;
			monster.transform.Rotate(0, rotation, 0);	

			cameraSpeed += cameraAcc;
			camera.transform.Translate(0, 0, cameraSpeed);

			if (rotation >= maxSpeed) {
				accelerating = false;
			}
		} else {
			rotation -= acc;
			monster.transform.Rotate(0, rotation, 0);

			cameraSpeed -= cameraAcc;
			camera.transform.Translate(0, 0, -cameraSpeed);

			if (rotation <= 0) {
				accelerating = true;
			}
		}
	}
}
