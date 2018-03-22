using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunOrbit : MonoBehaviour {
	public int orbitSpeed = 5;
	private Transform self;
	private Light sun;
	private float sunIntensity;

	public float minDimDeg = 160.0f;
	public float maxDimDeg = 220.0f;

	public float minIncDeg = 320.0f;
	public float maxIncDeg = 359.9f;
	// Use this for initialization
	void Start () {
		self = GetComponent<Transform>();
		sun  = self.Find("Sun").GetComponent<Light>();

		if (sun != null) {
			sunIntensity = sun.intensity;
		}
	}
	
	// Update is called once per frame
	void Update () {
		var rotationSpeed = orbitSpeed * Time.deltaTime;
		transform.Rotate(0,0,rotationSpeed);
	}

	void FixedUpdate() {
		if (self.rotation.eulerAngles.z > minDimDeg && self.rotation.eulerAngles.z < maxDimDeg) {
			sun.intensity = sunIntensity * (maxDimDeg - self.rotation.eulerAngles.z) / (maxDimDeg - minDimDeg);
		} else if (self.rotation.eulerAngles.z > minIncDeg && self.rotation.eulerAngles.z < maxIncDeg) {
			sun.intensity = sunIntensity * (self.rotation.eulerAngles.z - minIncDeg) / (maxIncDeg - minIncDeg);
		}
	}
}
