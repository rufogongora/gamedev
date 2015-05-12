using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class bs3 : MonoBehaviour {
	public string highScore;
	public Text hsText;
	// Use this for initialization
	void Start () {
		highScore = "3: "+PlayerPrefs.GetString ("nameballPos3")+": " + PlayerPrefs.GetInt ("highballPos3");
		hsText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		hsText.text = highScore;
	}
}
