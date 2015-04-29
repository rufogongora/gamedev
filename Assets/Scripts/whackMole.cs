using UnityEngine;
using System.Collections;

public class whackMole : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision){
		Debug.Log ("hitting mole");
		if (collision.gameObject.name == "palm" || collision.gameObject.name == 
			"bone1" || collision.gameObject.name == "bone2" || collision.gameObject.name == "bone3") {

		}
	}
}
