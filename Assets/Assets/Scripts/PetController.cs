using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour {
	public GameObject itemContainer;
	public float rotateSpeed = 150.0f;
	public float verticalSpeed = 3.0f;
	public int jumpHeight = 5;
	private Rigidbody rb;

	private AudioSource jumpSound;
	private bool jumpSound_toggle;
	private bool jumpSound_play;

	bool isGrounded() {
		if (rb.velocity.y == 0) {
			return true;
		}

		return false;
	}

	// void playJumpSound() {
	// 	if (jumpSound_play == true && jumpSound_toggle == true) {
	// 		jumpSound.Play();
	// 		jumpSound_toggle = false;
	// 	}

	// 	if (jumpSound_play == false && jumpSound_toggle == true) {
	// 		jumpSound.Stop();
	// 		jumpSound_toggle = false;
	// 	}
	// }
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		jumpSound = GetComponent<AudioSource>();
		jumpSound.Stop();
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
            jumpSound.Play();
        }
    }
	/*void OnCollisionEnter(Collision col) {
		Debug.Log (col.gameObject.name);
		if (gameObject.name == "Food") {
			Debug.Log ("Found foods");
			Destroy (col.rigidbody);
			Destroy (col.gameObject);
		}
	}*/
	/*void OnTriggerEnter(Collider col) {
		Debug.Log (col.gameObject.name);
		if (gameObject.name == "Food") {
			Debug.Log ("Found foods");
			col.gameObject.SetActive (false);
			Destroy (col.gameObject.GetComponent<Item>());
			Destroy (col.gameObject);
		}
	}*/

	void OnApplicationQuit()
	{
		Debug.Log ("Game has quit");
		PlayerPrefs.DeleteAll ();
	}
}
