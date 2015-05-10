using UnityEngine;
using System.Collections;
using Leap;

public class WhackedMoles : MonoBehaviour {
	
	public float killMeTime;
	
	public int position{ set; get; }
	
	
	public float timer;
	
	// start the timer for mole death
	void Start () {
		
		timer = 0f;
		
	}
	
	void KillMe(){
		GenerateMoles.spawnPoint [position] = false;
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= killMeTime) {
			KillMe();
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
