using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class score1 : MonoBehaviour {
	public string highScore;
	public Text hsText;
	// Use this for initialization
	void Start () {
		highScore = "1: "+PlayerPrefs.GetString ("nameScorePos1")+": " + PlayerPrefs.GetInt ("highscorePos1");
		hsText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		hsText.text = highScore;
	}
}
