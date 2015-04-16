using UnityEngine;
using System.Collections;

public class Thrash : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.transform.tag == "Enemy") {
			Destroy(other.gameObject);
		}

	}
}
