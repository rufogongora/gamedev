﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlapText : MonoBehaviour {
	public string wordScore;
	public Text speedText;

	// Use this for initialization
	void Start () {
		wordScore = "Hit the robot as far as you can";
		speedText = GetComponent <Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		speedText.text = wordScore;
	}
}
