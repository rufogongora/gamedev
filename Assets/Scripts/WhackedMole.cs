using UnityEngine;
using System.Collections;
using Leap;

public class WhackedMole : MonoBehaviour {

	public float killMeTime;

	public int position{ set; get; }


	float timer;

	// start the timer for mole death
	void Start () {

		timer = 0f;

	}

	void KillMe(){

		//GetComponent<moleGen> ().spawnPoint [(int)position] = false;
		//spawnPoint [position] = false;
		//moleGen moleBool = GetComponent<moleGen> ();
		moleGen.spawnPoint [position] = false;
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	timer += Time.deltaTime;
	if (timer >= killMeTime) {
		//KillMe();
	}
	
	}

	void OnCollisionEnter(Collision collision){
		//If the robot collides with the hand
		if (collision.gameObject.name == "palm"||collision.gameObject.name  == 
		    "bone1"||collision.gameObject.name == "bone2" ||collision.gameObject.name == "bone3"){

			KillMe();
			
		}
		
	}
}
