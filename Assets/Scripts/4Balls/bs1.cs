﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class bs1 : MonoBehaviour {
	public string highScore;
	public Text hsText;
	// Use this for initialization
	void Start () {
		highScore = "1: "+PlayerPrefs.GetString ("nameballPos1")+": " + PlayerPrefs.GetInt ("highballPos1");
		hsText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		hsText.text = highScore;
	}
}
