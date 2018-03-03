using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampRadiates : MonoBehaviour {
	public float duration = 3.0F;
	public float onDegreeTrig = 170.0f;
	public float offDegreeTrig = 330.0f;
    public float originalRange;
    public float originalIntensity;
    public Light lt;
    public GameObject orbit;

	// Use this for initialization
	void Start () {
		lt = GetComponent<Light>();
        originalRange = lt.range;
        originalIntensity = lt.intensity;
		
		if (orbit.transform.eulerAngles.z > onDegreeTrig && orbit.transform.eulerAngles.z < offDegreeTrig) {
			lt.intensity = originalIntensity;
		} else {
			lt.intensity = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (orbit.transform.eulerAngles.z > onDegreeTrig && orbit.transform.eulerAngles.z < offDegreeTrig) {
			lt.intensity = originalIntensity;
		} else {
			lt.intensity = 0;
		}
		float amplitude = Mathf.PingPong(Time.time, duration);
        amplitude = amplitude / duration * 0.5F + 0.5F;
        lt.range = originalRange * amplitude;
    }
}
