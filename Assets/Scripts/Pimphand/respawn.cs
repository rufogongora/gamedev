using UnityEngine;
using System.Collections;

public class respawn : MonoBehaviour {
	public floorCollision fc;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		while (fc.landed == false) {
			fc.slap.land();
		}
	}

	void OnTriggerEnter(Collider other) {
		//fc.landed = true;
	}
}
