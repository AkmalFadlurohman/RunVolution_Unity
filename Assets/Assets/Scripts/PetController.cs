﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour {
	public float rotateSpeed = 150.0f;
	public float verticalSpeed = 3.0f;
	public int jumpHeight = 5;
	private Rigidbody rb;

	bool isGrounded() {
		if (rb.velocity.y == 0) {
			return true;
		}

		return false;
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

		// Player Controller
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * verticalSpeed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
	}

	void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded()) {
            rb.velocity = new Vector3(0, jumpHeight, 0);
        }
    }
}
