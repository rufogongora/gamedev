using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class score3 : MonoBehaviour {
	public string highScore;
	public Text hsText;
	// Use this for initialization
	void Start () {
		highScore = PlayerPrefs.GetString ("nameScorePos3")+": " + PlayerPrefs.GetInt ("highscorePos3");
		hsText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		hsText.text = highScore;
	}
}
