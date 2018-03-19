using System;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PetController : MonoBehaviour {
	public Text petNameLevel;
	public Slider hungerSlider;
	public Slider xpSlider;

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
		if (PlayerPrefs.GetString ("lastLogin") != null) {
			Debug.Log (PlayerPrefs.GetString ("lastLogin"));
			DateTime lastLoginDate = DateTime.ParseExact(PlayerPrefs.GetString ("lastLogin"),"yyyyMMddHHmmss",CultureInfo.InvariantCulture);//DateTime.Parse(PlayerPrefs.GetString ("lastLogin"));
			DateTime currentDate = System.DateTime.Now;
			TimeSpan iddleTime = currentDate - lastLoginDate;
			Debug.Log ("Interval from las login : " + iddleTime.TotalSeconds);
			float hungerLevel = hungerSlider.maxValue - (((float)iddleTime.TotalSeconds / (3600 * 6)) * hungerSlider.maxValue);
			hungerSlider.value = hungerLevel;
		} else {
			hungerSlider.value = 0;
		}
		string petName = PlayerPrefs.GetString ("petName");
		int petLevel = PlayerPrefs.GetInt ("petLevel");
		int petXP = PlayerPrefs.GetInt ("petXP");
		petNameLevel.text = petName + " Lv." + petLevel;
		xpSlider.value = petXP;
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

	void OnTriggerStay(Collider col) {
		Debug.Log (col.gameObject.name);
		Debug.Log ("Found foods");
		UpdateHungerLevel ();
		UpdatePetXp ();
	}

	void OnApplicationQuit()
	{
		Debug.Log ("Game has quit");
	}
	void UpdateHungerLevel() {
		hungerSlider.value += 20;
	}
	void UpdatePetXp() {
		Debug.Log ("Updating pet xp");
		int xpValue = (int) xpSlider.value;
		xpValue += 40;
		if (xpValue >= 100) {
			xpValue %= 100;
			UpdatePetLevel ();
		}
		int petID = PlayerPrefs.GetInt ("petId");
		StartCoroutine(UploadPetXP(petID,xpValue));
		xpSlider.value = xpValue;
	}

	void UpdatePetLevel() {
		Debug.Log ("Updating pet level");
		string petName = PlayerPrefs.GetString ("petName");
		int petLevel = PlayerPrefs.GetInt ("petLevel");
		petLevel++;
		if ((petLevel % 5) == 0) {
			evolvePet ();
		}
		int petID = PlayerPrefs.GetInt ("petId");
		StartCoroutine (UploadPetLevel (petID, petLevel));
		petNameLevel.text = petName + " Lv." + petLevel;
		PlayerPrefs.SetInt ("petLevel", petLevel);
	}

	void evolvePet() {
		Debug.Log ("Pet evolution event handler");
	}

	public  IEnumerator UploadPetXP(int petID,int xp) {
		WWWForm data = new WWWForm();
		data.AddField("petid", petID);
		data.AddField ("xp", xp);
		UnityWebRequest www = UnityWebRequest.Post("https://runvolution.herokuapp.com/updatepetxp",data);
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log (www.error);
		}
		else {
			string msg = www.downloadHandler.text;
			if (msg != null) {
				Debug.Log (msg);
				if (msg.Equals ("OK")) {
					Debug.Log ("Updated pet experiences on server");
				}
			} else {
				Debug.Log ("Data not found");
			}
		}
	}

	public  IEnumerator UploadPetLevel(int petID,int level) {
		WWWForm data = new WWWForm();
		data.AddField("petid", petID);
		data.AddField ("level", level);
		UnityWebRequest www = UnityWebRequest.Post("https://runvolution.herokuapp.com/updatepetlevel",data);
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log (www.error);
		}
		else {
			string msg = www.downloadHandler.text;
			if (msg != null) {
				Debug.Log (msg);
				if (msg.Equals ("OK")) {
					Debug.Log ("Updated pet level on server");
				}
			} else {
				Debug.Log ("Data not found");
			}
		}
	}


}
