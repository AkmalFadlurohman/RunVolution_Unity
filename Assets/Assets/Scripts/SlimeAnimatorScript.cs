using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimatorScript : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow)) {
			anim.SetBool("isMoving", true);
		} else {
			anim.SetBool("isMoving", false);
		}
	}
}
