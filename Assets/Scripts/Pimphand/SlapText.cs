using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlapText : MonoBehaviour {
	public string wordScore;
	public Text speedText;

	// Use this for initialization
	void Start () {
		wordScore = "Lift your hand over the card to spawn the robot";
		speedText = GetComponent <Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		speedText.text = wordScore;
	}
}
