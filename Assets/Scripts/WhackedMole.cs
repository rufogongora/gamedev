using UnityEngine;
using System.Collections;
using Leap;

public class WhackedMole : MonoBehaviour {

	public float killMeTime;
	public float DownTimeMole;
	public float UpTimeMole;

	public AudioClip moleHit;
	AudioSource audio; 

	public int position{ set; get; }


	float timer;

	// start the timer for mole death
	void Start () {
		DownTimeMole = Random.Range (1, 5);
		timer = 0f;
	}

	void PopDownMole(){

		moleGen.spawnPoint [position] = false;
		while (transform.position.y>=-0.6f) {
			transform.position -= new Vector3 (0f, 0.05f, 0f);
		}

	}

	void KillMe(){
		moleGen.spawnPoint [position] = false;
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	timer += Time.deltaTime;
	if (timer >= DownTimeMole) {
			PopDownMole();
		
	}
	
	}

	void OnCollisionEnter(Collision collision){
		//If the robot collides with the hand

		audio.PlayOneShot (moleHit);

		if (collision.gameObject.name == "palm"||collision.gameObject.name  == 
		    "bone1"||collision.gameObject.name == "bone2" ||collision.gameObject.name == "bone3"){

			KillMe();
			
		}
		
	}
}
