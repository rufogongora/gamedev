using UnityEngine;
using System.Collections;
using Leap;



//Class for detecting slaps
public class SlapDetect : MonoBehaviour {

	//Store the different robot voices to play
	public AudioClip[] compliment;			//Store the voices that compliment the user
	public AudioClip[] insult;				//Store the voices that insult the user
	public AudioClip hit;
	AudioSource audio;
	public bool soundPlayed;

	//Class objects to access the text on screen
	public HighscoreText scoreText;			//Access the text that shows the highscore
	public SlapText speedText;				//Access the text that shows how fast they slapped
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

	//Cameras for robot and main
	public Camera mcamera;
	public Camera rcamera;

	public float robotSpeed;
	public Rigidbody rb;
	public Rigidbody srb;

	public Transform tf;

	//Run at the start
	void Start () {
		srb = gameObject.GetComponent<Rigidbody> ();
		soundPlayed = false;
		initialDistance = gameObject.GetComponent<Rigidbody> ().position;
		//Set the variables
		audio = GetComponent<AudioSource>();
		//scoreText.highScore = "Highscore: "+PlayerPrefs.GetInt ("highscore").ToString();
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
		speed = 0;
	}


	void PlaySound(AudioClip[] sounds){
		if (!soundPlayed){
			soundPlayed = true;
			audio.PlayOneShot(hit);
			audio.PlayOneShot (sounds [Random.Range (0, sounds.Length)]);
		}
	}	              

	bool HighScore(){
		if (score > PlayerPrefs.GetInt ("highscore")) {
			PlayerPrefs.SetInt("highscore", score);
			//scoreText.highScore = "Highscore: "+PlayerPrefs.GetInt ("highscore").ToString();
			return true;
		}
		return false;
	}

	//Hit the robot again after it lands
	public void redo(){
		rb = gameObject.GetComponent<Rigidbody> ();
//		rb.velocity = new Vector3 (0, 0, 0);
		rb.velocity = Vector3.zero;
		transform.position = tf.position;
		transform.rotation = tf.rotation;
		soundPlayed = false;
	}

	public void land(){
		transform.rotation = tf.rotation;	
		Debug.Log (transform.rotation);
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

		finalDistance = gameObject.GetComponent<Rigidbody> ().position;
		deltaDistance = finalDistance - initialDistance;
		robotSpeed = gameObject.GetComponent<Rigidbody> ().velocity.magnitude;

		//Follow the robot with the camera when hit
		rcamera.transform.position = new Vector3 (finalDistance.x, finalDistance.y, finalDistance.z - 10f);
		rcamera.transform.rotation = Quaternion.Euler (Vector3.zero);

		//Get all actions with leap motion done in the last frame
		Frame frame = controller.Frame ();
		GestureList gestures = frame.Gestures ();


		//For each gesture in gesture list
		for (int i = 0; i < gestures.Count; i++)
		{
			//Hand declaration
			Hand hand = frame.Hands.Rightmost;


			if (hand != Hand.Invalid) {
				Debug.Log ("HAYYYYYYY");
			}
			//Get velocity and position of the hand in the current frame
			Vector velocity = hand.PalmVelocity;
			Vector position = hand.PalmPosition;

			//Get the speed of the hand and it's position
			speed = (int)velocity.x;
			speed = System.Math.Abs(speed);
			speed = shift(speed10frames, speed);
			pos = (int)position.x;

			//Debug.Log(speed);
			//speedText.wordScore = pos.ToString();
			//Get the gesture to detect for circle
			//Gesture gesture = gestures[i];
//			if(gesture.Type == Gesture.GestureType.TYPE_CIRCLE){
//				if (pos > 245){
//					//Application.LoadLevel ("slap");
//					Debug.Log ("detected");
//					transform.position = new Vector3(-2f, 2f, -4f);
//				}
//				SwipeGesture Swipe = new SwipeGesture(gesture);
//				//Get the swipe speed and direction (Purpose TBD)
//				float SwipeSpeed = Swipe.Speed;
//				Vector swipeDirection = Swipe.Direction;
//
//			}
			
		}

		//Reload the level to play again
		if (Input.GetKeyDown (KeyCode.Return)) {  
			Application.LoadLevel ("slap");  
		} 
		
	}
	
	void OnCollisionEnter(Collision collision){
		//If the robot collides with the hand
		if (collision.gameObject.name == "palm"||collision.gameObject.name == 
		    "bone1"||collision.gameObject.name == "bone2" ||collision.gameObject.name == "bone3"){

			if (speed < 1000){
				PlaySound (insult);
			}
			else{
				PlaySound (compliment);
			}

			mcamera.enabled = false;
			rcamera.enabled = true;
			
		}

	}

	
	
}
