using UnityEngine;
using System.Collections;
using Leap;

public class WhackedMole : MonoBehaviour {

	Controller controller;



	// Use this for initialization
	void Start () {

//		controller = new Controller ();
//		controller.EnableGesture (Gesture.GestureType.TYPESWIPE);
//		controller.Config
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision){
		//If the robot collides with the hand
		if (collision.gameObject.name == "palm"||collision.gameObject.name  == 
		    "bone1"||collision.gameObject.name == "bone2" ||collision.gameObject.name == "bone3"){

			Destroy(gameObject);


			//Debug.Log("hitting mole");
			
		}
		
	}
}
