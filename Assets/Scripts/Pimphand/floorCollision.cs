using UnityEngine;
using System.Collections;

public class floorCollision : MonoBehaviour {
	public HighscoreText scoreText;
	public SlapText slapText;
	public SlapDetect slap;
	public float robotSpeed;
	public int score;
	public int currentHighscore;
	public Camera mcamera;
	public Camera rcamera;
	public bool landed;

	//clone robot
	public Transform prefab;
	
	// Use this for initialization
	void Start () {
		landed = false;
		score = PlayerPrefs.GetInt ("highscore");
	}
	
	// Update is called once per frame
	void Update () {
		currentHighscore = PlayerPrefs.GetInt ("highscore");
		robotSpeed = slap.robotSpeed;
	}


	bool HighScore(){
		if (score > PlayerPrefs.GetInt ("highscore")) {
			PlayerPrefs.SetInt("highscore", (int)slap.deltaDistance.magnitude);
			scoreText.highScore = "Highscore: "+PlayerPrefs.GetInt ("highscore").ToString();
			return true;
		}
		return false;
	}

	void OnTriggerEnter(Collider other) {

		if (!landed) {
			bool newHighscore = false;
			score = (int)slap.deltaDistance.magnitude;
			newHighscore = HighScore ();
		}


		if (score > 800 && landed == false){
			slapText.wordScore = "You hit like David Garza: "+score;
		}
		if (score > 1000 && landed == false){
			slapText.wordScore = "Take off your diaper and try again: "+score;
		}
		if (score > 1300 && landed == false){
			slapText.wordScore = "Are you even trying?: "+score;
		}
		if (score > 1600 && landed == false){
			slapText.wordScore = "Yawn...: "+score;
		}
		if (score > 1900 && landed == false){
			slapText.wordScore = "Its ok I guess: "+score;
		}
		if (score > 2200 && landed == false){
			slapText.wordScore = "Not the worst I've seen: "+score;
		}
		if (score > 2500 && landed == false){
			slapText.wordScore = "Pretty good pimp: "+score;
		}
		if (score > 3000 && landed == false){
			slapText.wordScore = "Chris Brown would be proud: "+score;
		}
		if (score > 4000 && landed == false){
			slapText.wordScore = "The lord has blessed your pimphand: "+score;
		}
		if (score < 800 && landed == false){
			slapText.wordScore = "You suck, uninstall the game: "+score;
		}

		//if (newHighscore){
		//	slapText.wordScore = "New Highscore!: "+score;
		//}
		
		//Set the robot back to his original point of whatever and stuff
		slap.restart = true;

		landed = true;





		//Instantiate (prefab, new Vector3 (164f, 95f, -62f), Quaternion.identity);
		mcamera.enabled = true;
		rcamera.enabled = false;
	}
}
