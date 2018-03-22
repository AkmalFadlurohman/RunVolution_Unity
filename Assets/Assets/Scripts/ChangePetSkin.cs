using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePetSkin : MonoBehaviour {
	public List<Material> skins;
	public int currentIndex;
	private Renderer ownRenderer;

	void Start() {
		ownRenderer = GetComponent<Renderer>();
		ownRenderer.material = skins[currentIndex];
	}

	public void changeSkinIndex(int index) {
		if (index >= 0 && index < skins.Count) {
			currentIndex = index;
			ownRenderer.material = skins[index];
		}
	}

	public void changeNextSkin() {
		currentIndex++;
		if (currentIndex >= skins.Count) {
			currentIndex = 0;
		}
		ownRenderer.material = skins[currentIndex];
	}

	public void changePrevSkin() {
		currentIndex--;
		if (currentIndex < 0) {
			currentIndex = skins.Count;
		}
		ownRenderer.material = skins[currentIndex];
	}
}
