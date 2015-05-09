using UnityEngine;
using System.Collections;

public class floorCollision : MonoBehaviour {
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
			PlayerPrefs.SetInt("highscore", (int)slap.deltaDistance.x*(-1));
			return true;
		}
		return false;
	}

	void OnTriggerEnter(Collider other) {
		bool newHighscore = false;
		if (!landed) {
			score = (int)slap.deltaDistance.x*(-1);
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
			slapText.wordScore = "Mayweather, is that you?: "+score;
		}
		if (score > 5000 && landed == false){
			slapText.wordScore = "You Rodney King'd him!: "+score;
		}		
		if (score > 6000 && landed == false){
			slapText.wordScore = "PLAYAAAAAA: "+score;
		}
		if (score > 7000 && landed == false){
			slapText.wordScore = "DAYUMMMMMMM: "+score;
		}
		if (score > 8000 && landed == false){
			slapText.wordScore = "The lord has blessed your pimphand: "+score;
		}
		if (score > 9000 && landed == false){
			slapText.wordScore = "The lord has blessed your pimphand: "+score;
		}
		if (score < 800 && landed == false){
			slapText.wordScore = "You suck, uninstall the game: "+score;
		}
		if (score < 0 && landed == false){
			slapText.wordScore = "HAHAHAHA SO BAD: "+score;
		}



		//Set the robot back to his original point of whatever and stuff
		slap.restart = true;
		landed = true;
		mcamera.enabled = true;
		rcamera.enabled = false;
		slap.explosion (landed);
		
		

		if (newHighscore){
			slapText.wordScore = "New Highscore!: "+score;
		}
	}
}
