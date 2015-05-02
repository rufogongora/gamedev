using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighscoreText : MonoBehaviour {
	public string highScore;
	public Text hsText;
	// Use this for initialization
	void Start () {
		highScore = "Highscore: "+PlayerPrefs.GetInt ("highscore").ToString();
		hsText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		hsText.text = highScore;
	}
}
