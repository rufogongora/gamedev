using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class highScores : MonoBehaviour {
	
	public Text nameText;
	public string changeName;
	public bool nameEntered;
	public bool newHighscore;
	bool updated;
	int score;
	
	// Use this for initialization
	void Start () {
		updated = false;
		nameEntered = false;
		nameText = GetComponent<Text> ();
		score = PlayerPrefs.GetInt ("highball");
	}
	
	// Update is called once per frame
	void Update () {
		if (nameEntered) {
			namepls ();
			Application.LoadLevel("Menu");
		}
	}
	
	void namepls(){
		if (!updated) {
			int tempint = 0;
			string tempstring = "";
			for (int i=1; i<=5; i++) {
				if (PlayerPrefs.GetInt ("highballPos" + i) < score) {
					tempint = PlayerPrefs.GetInt ("highballPos" + i); 	
					tempstring = PlayerPrefs.GetString ("nameballPos" + i); 
					PlayerPrefs.SetInt ("highballPos" + i, score); 
					PlayerPrefs.SetString ("nameballPos" + i, changeName);
					if (i < 5) {
						int j = i + 1;
						score = PlayerPrefs.GetInt ("highballPos" + j);
						changeName = PlayerPrefs.GetString ("nameballPos" + j);
						PlayerPrefs.SetInt ("highballPos" + j, tempint); 
						PlayerPrefs.SetString ("nameballPos" + j, tempstring); 
					}
				}
				Debug.Log ("after "+PlayerPrefs.GetString ("nameballPos" + i) + " " + PlayerPrefs.GetInt ("highballPos" + i));
			}
			updated=true;
		}
	}
	
	public void OnGUI () {
		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return) {
			changeName = nameText.text;
			nameEntered = true;
		}
	}
	
}

