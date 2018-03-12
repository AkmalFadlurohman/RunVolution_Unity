using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetStatusController : MonoBehaviour {
	public Text petNameLevel;
	public Slider hungerSlider;
	public Slider xpSlider;

	void OnGUI() {
		string petName = PlayerPrefs.GetString ("petName");
		int petLevel = PlayerPrefs.GetInt ("petLevel");
		int petXP = PlayerPrefs.GetInt ("petXP");
		petNameLevel.text = petName + " Lv." + petLevel;
		xpSlider.value = petXP;
	}
}
