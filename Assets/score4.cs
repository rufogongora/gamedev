using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class score4 : MonoBehaviour {
	public string highScore;
	public Text hsText;
	// Use this for initialization
	void Start () {
		highScore = PlayerPrefs.GetString ("nameScorePos4")+": " + PlayerPrefs.GetInt ("highscorePos4");
		hsText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		hsText.text = highScore;
	}
}
