using UnityEngine;
using System.Collections;

public class Dude_CollisionDetection : MonoBehaviour {

	public fallingDude theDude;

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name == "Floor") {
			theDude.savable = false;
			theDude.die();
		}
		if (other.gameObject.name == "palm" && theDude.savable) {
			theDude.savable = false;
			theDude.scoreUp();
		}
	}

}
