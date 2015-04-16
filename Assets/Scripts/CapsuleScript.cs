using UnityEngine;
using System.Collections;

public class CapsuleScript: MonoBehaviour {

	public string capsuleName;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3 (0, Random.Range(-100,100), 0));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
