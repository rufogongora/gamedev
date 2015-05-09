using UnityEngine;
using System.Collections;
using Leap;


//Robot Position: 414.25, 153.93, -35.25
//Class for detecting slaps
public class SlapDetect : MonoBehaviour {

	//Store the different robot voices to play
	public AudioClip[] compliment;			//Store the voices that compliment the user
	public AudioClip[] insult;				//Store the voices that insult the user
	public AudioClip hit;
	public AudioClip slowHit;
	public AudioClip explode; 
	AudioSource audio;
	bool soundPlayed;
	bool startGame; 
	public bool restart;



	//Class objects to access the text on screen
	public SlapText speedText;				//Access the text that shows how fast they slapped
	public floorCollision fc; 				//Access the floorCollision class
	public int score;						//Holds the score for the user
	
	//Position and speed variables for the hand
	//Rigidbody rb = GetComponent<Rigidbody>();
	public int speed;						//To store swipe speed
	public int[] speed10frames; 			//Speed from the last 10 frames
	public int pos;							//To store the position of the hand
	Controller controller;					//Leap Motion controller	

	//position vectors
	public Vector3 initialDistance;
	public Vector3 finalDistance;
	public Vector3 deltaDistance;
	public Vector3 spawnpoint;

	//Cameras for robot and main
	public Camera mcamera;
	public Camera rcamera;

	public float robotSpeed;
	public Rigidbody rb;

	//Run at the start
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		soundPlayed = false;
		restart = true;
		startGame = false;
		//PlayerPrefs.DeleteAll ();
		spawnpoint = new Vector3(414.25f, 153.93f, -35.25f);
		initialDistance = gameObject.GetComponent<Rigidbody> ().position;
		//Set the variables
		audio = GetComponent<AudioSource>();
		speed10frames = new int[10];
		//Leap Motion controller declaration
		controller = new Controller ();

		//Configuring Leap Motion to check for a circle
		controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
		controller.Config.SetFloat("Gesture.Circle.MinRadius", 80.0f);
		controller.Config.SetFloat("Gesture.Circle.MinArc", 1f);

		//Configuring Leap Motion to check for all swipes in any speed
		controller.EnableGesture (Gesture.GestureType.TYPESWIPE);
		controller.Config.SetFloat ("Gesture.Swipe.MinLength", 1.0f);
		controller.Config.SetFloat ("Gesture.Swipe.MinVelocity", 1.0f);
		controller.Config.Save ();

		controller.EnableGesture (Gesture.GestureType.TYPESCREENTAP);
		controller.Config.SetFloat("Gesture.ScreenTap.MinForwardVelocity", 30.0f);
		controller.Config.SetFloat("Gesture.ScreenTap.HistorySeconds", 2.0f);
		controller.Config.SetFloat("Gesture.ScreenTap.MinDistance", 1.0f);
		controller.Config.Save();

		speed = 0;
	}


	//Play sound effect, onHit sound dependant upon velocity
	void PlaySound(AudioClip[] sounds, bool speed){
		if (!soundPlayed){
			soundPlayed = true;
			if (speed)
				audio.PlayOneShot(hit);
			else
				audio.PlayOneShot(slowHit);
			audio.PlayOneShot (sounds [Random.Range (0, sounds.Length)]);
		}
	}	

	//Play explosion
	public void explosion(bool landed){
		if (landed)
			audio.PlayOneShot (explode);
	}

	//Update the user highscore on screen
	bool HighScore(){
		if (score > PlayerPrefs.GetInt ("highscore")) {
			PlayerPrefs.SetInt("highscore", score);
			return true;
		}
		return false;
	}

	//Spawn a robot upon promping with a gesture
	public void spawnRobot(){
		if (handReady()) {
			//speedText.wordScore =  "Hit the robot as far as you can";
			redo ();
			restart = false;
		}
	}


	//Return if the hand is in place to spawn or not
	public bool handReady(){
		if (pos > 90)
			return true;
		else 
			return false;
	}

	//Move the robot back to starting position the robot again after it lands
	public void redo(){
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		transform.position = new Vector3 (414.25f, 153.93f, -35.25f);
		transform.localEulerAngles = new Vector3(0,0,0);
		soundPlayed = false;
	}


	//Store the speed of the last 10 frames and return the average
	int shift(int[] arr, int lastSpeed){

		//Queue for frame speed, add the last frame, remove the 10th frame
		for (int i = 0; i < 9; i++) {
			arr[i] = arr[i+1];
		}
		arr [9] = lastSpeed;

		//Take the average speed from the 10 frames stored
		int avgspeed = 0;
		for (int i = 0; i < 10; i++) {
			avgspeed += arr[i];
		}
		avgspeed /= 10;
		return avgspeed;
	}

	//Run every frame
	void Update(){

		if (restart)
			spawnRobot ();


		Vector3 leftRight = rb.velocity;


		rb.velocity = new Vector3 (leftRight.x, leftRight.y, 0);
		//rb.position.z = 37f;


		finalDistance = gameObject.GetComponent<Rigidbody> ().position;
		deltaDistance = finalDistance - initialDistance;
		robotSpeed = gameObject.GetComponent<Rigidbody> ().velocity.magnitude;

		if (!fc.landed && startGame)
			speedText.wordScore = "Distance: " + (int)deltaDistance.x*(-1)+" Height: "+(int)rb.position.y;

		//Follow the robot with the camera when hit
		rcamera.transform.position = new Vector3 (finalDistance.x , 150f, finalDistance.z -300f);
		rcamera.transform.rotation = Quaternion.Euler (Vector3.zero);

		handReady();


		//Get all actions with leap motion done in the last frame
		Frame frame = controller.Frame ();
		GestureList gestures = frame.Gestures ();


		//For each gesture in gesture list
		for (int i = 0; i < gestures.Count; i++)
		{
			//Get the gestures
			Gesture gesture = gestures[i];
			//Hand declaration
			Hand hand = frame.Hands.Rightmost;

			//See if the hand exists in the scene
//			if (hand != Hand.Invalid) {
//				Debug.Log ("HAYYYYYYY");
//			}
			//Get velocit y and position of the hand in the current frame
			Vector velocity = hand.PalmVelocity;
			Vector position = hand.PalmPosition;


			//Get the speed of the hand and it's position
			speed = (int)velocity.x;
			speed = System.Math.Abs(speed);
			speed = shift(speed10frames, speed);
			pos = (int)position.x;


			//If the hand touches the main menu, go back to the main menu
			if (position.y > 220 && position.x > 160){
				if(gesture.Type == Gesture.GestureType.TYPESCREENTAP){
						//load main menu here
						Debug.Log ("detected");

				}
			}
		}
		
	}

	//On hand colliding with an object, do: 
	void OnCollisionEnter(Collision collision){
		//If the robot collides with the hand
		if (collision.gameObject.name == "palm"||collision.gameObject.name == 
		    "bone1"||collision.gameObject.name == "bone2" ||collision.gameObject.name == "bone3"){

			if (speed < 800){
				PlaySound (insult, false);
			}
			else{
				PlaySound (compliment, true);
			}

			startGame = true;
			fc.landed = false;
			mcamera.enabled = false;
			rcamera.enabled = true;
			
		}

	}

	
	
}
