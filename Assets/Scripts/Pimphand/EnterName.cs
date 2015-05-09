using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnterName : MonoBehaviour {

	public Text nameText;
	public string changeName;
	public Transform test;

	// Use this for initialization
	void Start () {
		test = GetComponent<InputField> ();
		nameText = GetComponent<Text> ();
		test.position = new Vector3 (100, 100, 100);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (nameText.text);
		//nameText.text = highScore;
	}
}
