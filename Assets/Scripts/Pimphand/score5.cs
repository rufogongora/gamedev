using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class score5 : MonoBehaviour {
	public string highScore;
	public Text hsText;
	// Use this for initialization
	void Start () {
		highScore = "5: "+PlayerPrefs.GetString ("nameScorePos5")+": " + PlayerPrefs.GetInt ("highscorePos5");
		hsText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		hsText.text = highScore;
	}
}
