using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemController : MonoBehaviour {
	public GameObject modelObject;
	public GameObject noticeWindow;

	void Start () {
		float previousRecord = PlayerPrefs.GetFloat ("previousRecord");
		float currentRecord = PlayerPrefs.GetFloat ("currentRecord");
		float divisor = 10;
		float bound = 10;
		if (!PlayerPrefs.HasKey ("itemCount")) {
			if (previousRecord != -1) {
				float diffRecord = currentRecord - previousRecord;
				if (diffRecord > divisor) {

					int itemCount = (int)(diffRecord / divisor);
					if (itemCount > bound) {
						itemCount = 10;
					}
					PlayerPrefs.SetInt ("itemCount", itemCount);
					Debug.Log ("Generating " + itemCount + " food items");

					Vector3 position = new Vector3 (-6, 1, 0);
					for (int i = 0; i < itemCount; i++) {
						GameObject itemObject = Instantiate (modelObject, position, modelObject.transform.rotation);
						itemObject.SetActive (true);
						position.x += 2;
						if (position.x == 0 && position.z == 0) {
							position.x += 2;
						}
						if (position.x == 4) {
							position.x = -6;
							position.z += 2;
						}
					}
					showNoticeWindow ();
				} else {
					Debug.Log ("User has not run far enough");
				}
			} else {
				Debug.Log ("Record has been consumed previously");
			}
		} else {
			if (PlayerPrefs.GetInt ("itemCount") > 0) {
				int itemCount = PlayerPrefs.GetInt ("itemCount");
				Vector3 position = new Vector3 (-6, 1, 0);
				for (int i = 0; i < itemCount; i++) {
					GameObject itemObject = Instantiate (modelObject, position, modelObject.transform.rotation);
					itemObject.SetActive (true);
					position.x += 2;
					if (position.x == 0 && position.z == 0) {
						position.x += 2;
					}
					if (position.x == 4) {
						position.x = -6;
						position.z += 2;
					}
				}
			}
		}
	}
	
	public  IEnumerator SetUserRecordAsConsumed(string email) {
		WWWForm data = new WWWForm();
		data.AddField("email", email);
		UnityWebRequest www = UnityWebRequest.Post("https://runvolution.herokuapp.com/setconsumed",data);
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log (www.error);
		}
		else {
			string msg = www.downloadHandler.text;
			if (msg != null) {
				Debug.Log (msg);
				if (msg.Equals ("OK")) {
					Debug.Log ("Set user's running record as consumed");
				}
			} else {
				Debug.Log ("Data not found");
			}
		}
	}

	public void showNoticeWindow() {
		noticeWindow.SetActive(true);
	}
	public void hideNoticeWindow() {
		string email = PlayerPrefs.GetString ("email");
		PlayerPrefs.SetFloat ("previousRecord", -1);
		StartCoroutine (SetUserRecordAsConsumed (email));
		noticeWindow.SetActive(false);
	}
}
