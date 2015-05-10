using UnityEngine;
using System.Collections;
using Leap;

public class WhackedMole : MonoBehaviour {

	public float killMeTime;

	public AudioClip moleHit;
	AudioSource audio;

	public int position{ set; get; }


	float timer;

	// start the timer for mole death
	void Start () {
		audio = GetComponent<AudioSource>();
		timer = 0f;

	}

	void KillMe(){
		moleGen.spawnPoint [position] = false;
		Destroy (gameObject);
		audio.PlayOneShot (moleHit);

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
