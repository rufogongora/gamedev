using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Test
public class SlapText : MonoBehaviour {
	public string wordScore;
	public Text speedText;

	// Use this for initialization
	void Start () {
		wordScore = "Read instructions to the right";
		speedText = GetComponent <Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		speedText.text = wordScore;
	}
}
