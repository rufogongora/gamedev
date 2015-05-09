using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnterName : MonoBehaviour {

	public Text nameText;
	public string changeName;
	public bool nameEntered;
	public bool newHighscore;

	// Use this for initialization
	void Start () {
		int [] testArr = new int [5];
		int [] testArr2;
		nameEntered = false;
		nameText = GetComponent<Text> ();
		Debug.Log (PlayerPrefs.GetInt ("highscore"));

		testArr2 = PlayerPrefsX.GetIntArray ("testInts");

		for (int i = 0; i < 5; i++) {
			Debug.Log(testArr2[i]);
		}

	}
	
	// Update is called once per frame
	void Update () {

		changeName = nameText.text;
	}

	public void OnGUI () {
		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return) {
			changeName = nameText.text;
			Debug.Log (changeName);
			nameEntered = true;
		}
	}

}
