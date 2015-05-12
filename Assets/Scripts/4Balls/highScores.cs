using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class high : MonoBehaviour {
	
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
		score = PlayerPrefs.GetInt ("highscore");
		
		
		for (int i=1; i<=5; i++) {
			Debug.Log ("before "+PlayerPrefs.GetString ("nameScorePos" + i) + " " + PlayerPrefs.GetInt ("highscorePos" + i));
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if (nameEntered) {
			namepls ();
			Application.LoadLevel("Pimphand");
		}
	}
	
	void namepls(){
		if (!updated) {
			int tempint = 0;
			string tempstring = "";
			for (int i=1; i<=5; i++) {
				if (PlayerPrefs.GetInt ("highscorePos" + i) < score) {
					tempint = PlayerPrefs.GetInt ("highscorePos" + i); 	
					Debug.Log (i+" "+tempint);
					tempstring = PlayerPrefs.GetString ("nameScorePos" + i); 
					Debug.Log (i+" "+tempstring);
					PlayerPrefs.SetInt ("highscorePos" + i, score); 
					PlayerPrefs.SetString ("nameScorePos" + i, changeName);
					if (i < 5) {
						int j = i + 1;
						score = PlayerPrefs.GetInt ("highscorePos" + j);
						changeName = PlayerPrefs.GetString ("nameScorePos" + j);
						PlayerPrefs.SetInt ("highscorePos" + j, tempint); 
						PlayerPrefs.SetString ("nameScorePos" + j, tempstring); 
					}
				}
				Debug.Log ("after "+PlayerPrefs.GetString ("nameScorePos" + i) + " " + PlayerPrefs.GetInt ("highscorePos" + i));
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
