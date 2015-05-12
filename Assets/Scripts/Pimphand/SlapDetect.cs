using UnityEngine;
using System.Collections;
using Leap;


//Robot Position: 414.25, 153.93, -35.25
//Class for detecting slaps
public class SlapDetect : MonoBehaviour {

	//Different sounds to be used
	public AudioClip[] compliment;			//Store the voices that compliment the user
	public AudioClip[] insult;				//Store the voices that insult the user
	public AudioClip hit;					//Stores the sound for the homerun bat
	public AudioClip slowHit;				//Stores the sound for hitting the sandbag slowly
	public AudioClip newRecord;				//Stores the sound for a new highscore
	public AudioClip explode; 				//Stores the sound for when the sandbag lands
	public AudioClip momAudio;				//Stores the sound for setting the overall highscore
	public AudioClip kidAudio;				//Stores the sound for setting the overall highscore
	AudioSource audio;						//Variable to play audio in the game
	bool soundPlayed;						//Variable to avoid playing a sound multiple times on hand collision detection
	bool startGame; 						//Variable to do specific things only when the game starts
	public bool restart;					//Variable for respawning the sandbag



	//Class objects to access the text on screen
	public SlapText speedText;				//Access the text that shows how fast they slapped
	public floorCollision fc; 				//Access the floorCollision class
	public int score;						//Holds the score for the user
	
	//Position and speed variables for the hand
	public int speed;						//To store swipe speed
	public int[] speed10frames; 			//Store the speed average of the hand over the last 10 frames
	public int pos;							//To store the position of the hand
	Controller controller;					//Leap Motion controller	

	//position vectors
	public Vector3 initialDistance;			//Get the initial position of the sandbag
	public Vector3 finalDistance;			//Get the final distance of the sandbag
	public Vector3 deltaDistance;			//Get the delta distance of the sandbag
	public Vector3 spawnpoint;				//Store the location of the original spawnpoint

	//Cameras for robot and main
	public Camera mcamera;					//Access the camera for the starting position
	public Camera rcamera;					//Access the camera that follows the sandbag
		
	public float robotSpeed;				//Stores the speed of the sandbag
	public Rigidbody rb;					//Stores the rigidbody component of the sandbag

	//Run at the start
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		soundPlayed = false;
		restart = true;
		startGame = false;
		spawnpoint = new Vector3(414.25f, 153.93f, -35.25f);
		initialDistance = gameObject.GetComponent<Rigidbody> ().position;
		//Set the variables
		audio = GetComponent<AudioSource>();
		speed10frames = new int[10];
		//Leap Motion controller declaration
		controller = new Controller ();

		/* This section enables different gesture recognition for the leap motion. 
		 * these include swiping, a circular motion, and a screen tap motion. 
		 * Only screen tap is in use right now, the other motions were unfortunately 
		 * scrapped from the final version of the game but I am leaving this here as
		 * reference material for the team */

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


	//Run every frame
	void Update(){
		
		
		//Check if the hand is ready, if yes spawn a sandbag
		handReady();
		if (restart)
			spawnRobot ();
		
		//Get the velocity of the sandbag and prevent the sandbag from moving on the z axis
		Vector3 leftRight = rb.velocity;
		rb.velocity = new Vector3 (leftRight.x, leftRight.y, 0);
		
		//Get the delta distance of the sandbag to calculate the players score
		finalDistance = gameObject.GetComponent<Rigidbody> ().position;
		deltaDistance = finalDistance - initialDistance;
		robotSpeed = gameObject.GetComponent<Rigidbody> ().velocity.magnitude;
		
		
		//If the sandbag is in the air, let them know where the sandbag is at right now
		if (!fc.landed && startGame)
			speedText.wordScore = "Distance: " + (int)deltaDistance.x*(-1)+" Height: "+(int)rb.position.y;
		
		
		//Tells the second camera to keep following the robot
		rcamera.transform.position = new Vector3 (finalDistance.x , 150f, finalDistance.z -300f);
		rcamera.transform.rotation = Quaternion.Euler (Vector3.zero);
		
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
			
			//Checks if the hand exists in the scene
			//			if (hand != Hand.Invalid) {
			//				Debug.Log ("HAYYYYYYY");
			//			}
			
			//Get velocity and position of the hand in the current frame
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
					//load main menu here if they gestured to go back
					Application.LoadLevel("Menu");
					
				}
			}
		}
		
	}

	/* Plays a sound when the hand hits the sandbag. The speed variable is set based on
	 if their slap is deemed fast enough or not. The sounds are one of two arrays, 
	 one array contains compliments where the other one contains insults. The speed variable 
	 determines which hand collision sound to play (metal clank or homerun bat) */
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

	//Plays the explosion sound effect for when the sandbag lands
	public void explosion(bool landed){
		if (landed)
			audio.PlayOneShot (explode);
	}
	
	//Play smash bros audio clip when the user get a new highscore
	public void smashBros(bool landed){
		if (landed)
			audio.PlayOneShot (newRecord);
	}


	//Play smlg clip when the user gets the overall highscore
	public void momCamera(bool landed){
		if (landed) {
			audio.PlayOneShot (momAudio);
			audio.PlayOneShot (kidAudio);
		}
	}
	

	//Spawn a new sandbag when the user moves his hand far enough right
	public void spawnRobot(){
		if (handReady()) {
			redo ();
			restart = false;
		}
	}


	//Returns true if the hand is far enough to the right to spawn a sandbag, else returns false
	public bool handReady(){
		if (pos > 90)
			return true;
		else 
			return false;
	}

	//Moves the sandbag back to the original position and removes all velocities and forces upon it
	public void redo(){
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		transform.position = new Vector3 (414.25f, 153.93f, -35.25f);
		transform.localEulerAngles = new Vector3(0,0,0);
		soundPlayed = false;
	}


	/* Stores the speed of the hand from the last 10 frames into an array. This functions 
	 * takes those speeds and returns the average speed of said 10 frames when called */
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
	
	//On hand colliding with an object, do: 
	void OnCollisionEnter(Collision collision){
		//If the sandbag collides with the hand
		if (collision.gameObject.name == "palm"||collision.gameObject.name == 
		    "bone1"||collision.gameObject.name == "bone2" ||collision.gameObject.name == "bone3"){

			//If they hit the sandbag slow, play a slow sound and insult them
			if (speed < 800){
				PlaySound (insult, false);
			}
			//Otherwise they hit the sandbag fast and we compliment them
			else{
				PlaySound (compliment, true);
			}

			startGame = true;
			fc.landed = false;			//Lets us know the sandbag is in the air
			mcamera.enabled = false;	//Switch to the other camera if we hit the sandbag
			rcamera.enabled = true;		//Switch to the other camera if we hit the sandbag
			
		}

	}

	
	
}
