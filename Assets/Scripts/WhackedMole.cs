using UnityEngine;
using System.Collections;
using Leap;

public class WhackedMole : MonoBehaviour {

	public float DownTimeMole;
	public float UpTimeMole;

	public moleGen mG;

	public AudioClip moleHit;
	AudioSource audio; 

	public int position{ set; get; }


	public float DownTimer;
	public float UpTimer;

	// start the timer for mole death
	void Start () {
		audio = GetComponent<AudioSource>();
		UpTimeMole = Random.Range (3, 8);
		DownTimeMole = Random.Range (5, 15);
		UpTimer = 0f;
	}

	//pop the mole up and down 
	void PopMole(){

		bool IsMoleUp = moleGen.spawnPoint [position];

		//if the mole is down 
		if (IsMoleUp== false) {
			Debug.Log("mole is  down moving up");
			moleGen.spawnPoint [position] = true;

			//move the mole up to max height of 0f
			if(transform.position.y < 0){
				transform.position = transform.position + new Vector3 (0f, 0.6f, 0f);

			}

		}
		//else if the mole is up move it down
		else {
			Debug.Log("mole is up moving down");
			moleGen.spawnPoint [position] = false;
			if(transform.position.y > -0.6f){
				transform.position = transform.position - new Vector3 (0f, 0.6f, 0f);
			}	
		}

	}

	// Update is called once per frame
	void Update () {
	UpTimer += Time.deltaTime;
	if (UpTimer >= UpTimeMole) {
			PopMole();
			UpTimer=0f;
		
	}
	
	}

	void UpScore(){
		//if (moleGen.spawnPoint [position] == true) {
			moleGen.gameScore += 1;
		//}
	}

	void OnCollisionEnter(Collision collision){

		//check to see if the hand hits the mole 
		if (collision.gameObject.name == "palm"||collision.gameObject.name  == 
		    "bone1"||collision.gameObject.name == "bone2" ||collision.gameObject.name == "bone3"){
			//if the mole is up, you can hit the mole 
			if(moleGen.spawnPoint[position]==true){

				//you have hit the mole and now want to move it down
				//set the condition to false as you want it to move down

				int pos = position;

				audio.PlayOneShot(moleHit);

				Debug.Log ("hit mole at position: "+ pos);

				moleGen.spawnPoint[position] = false;

				UpScore();	
				PopMole();
			}

			// else the mole is down and you can't hit it 
			else {
				Debug.Log("mole is down, can't hit it");

			}

			
		}
		
	}
}
