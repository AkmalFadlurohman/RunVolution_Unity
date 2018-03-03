using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour, IPointerDownHandler {

	public float rotateSpeed;
	public int dir;
	public GameObject target;

	void Awake() {
		if (dir < 0) {
			dir = -1;
		} else if (dir > 0) {
			dir = 1;
		}
	}
	
	void rotateY() {
		var y = rotateSpeed * dir;
		target.transform.Rotate(0,y,0);
	}

	public void OnPointerDown(PointerEventData eventData) {
		if (Input.GetMouseButton(0)) {
			rotateY();
		}
	}
}
