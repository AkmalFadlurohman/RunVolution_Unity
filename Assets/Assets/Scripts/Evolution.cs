using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evolution : MonoBehaviour {
	public GameObject self;
	public float rotation = 0;
	public float acc = 5;
	public float maxSpeed = 300.0f;

	void Start() {
		
	}

	void Update() {
		if (rotation < maxSpeed); {
			rotation = (30 + acc) * Time.deltaTime;
			self.transform.Rotate(0, rotation, 0);
			acc += acc;
		}
	}
}
