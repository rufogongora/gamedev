using UnityEngine;
using System.Collections;
using Leap;

public class WhackedMole : MonoBehaviour {

	Controller controller;


	public float killMeTime;

	public float position { set; get; }
	

	float timer;

	// Use this for initialization
	void Start () {

//		controller = new Controller ();
//		controller.EnableGesture (Gesture.GestureType.TYPESWIPE);
//		controller.Config
		timer = 0f;
	

	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= killMeTime) {
			KillMe();
		}
	}

	void KillMe()
	{
		moleGen.spawnPoint [(int)position] = false;
		Destroy (gameObject);
	}

	void OnCollisionEnter(Collision collision){
		//If the robot collides with the hand
		if (collision.gameObject.name == "palm"||collision.gameObject.name  == 
		    "bone1"||collision.gameObject.name == "bone2" ||collision.gameObject.name == "bone3"){

			KillMe();


			//Debug.Log("hitting mole");
			
		}
		
	}
}
