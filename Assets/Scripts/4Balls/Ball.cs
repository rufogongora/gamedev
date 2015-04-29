using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	

	// Update is called once per frame
	void Update () {
		if (transform.position.y < -8f) {
			Destroy(gameObject);
		}
	}
}
