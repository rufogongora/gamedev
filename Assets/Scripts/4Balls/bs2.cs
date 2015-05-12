using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class bs2 : MonoBehaviour {
	public string highScore;
	public Text hsText;
	// Use this for initialization
	void Start () {
		highScore = "2: "+PlayerPrefs.GetString ("nameballPos2")+": " + PlayerPrefs.GetInt ("highballPos2");
		hsText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		hsText.text = highScore;
	}
}
