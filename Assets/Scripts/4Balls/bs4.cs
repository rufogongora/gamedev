using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class bs4 : MonoBehaviour {
	public string highScore;
	public Text hsText;
	// Use this for initialization
	void Start () {
		highScore = "4: "+PlayerPrefs.GetString ("nameballPos4")+": " + PlayerPrefs.GetInt ("highballPos4");
		hsText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		hsText.text = highScore;
	}
}
