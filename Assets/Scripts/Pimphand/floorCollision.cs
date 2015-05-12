using UnityEngine;
using System.Collections;

public class floorCollision : MonoBehaviour {
	public SlapText slapText;			//Access the class object for the main text
	public SlapDetect slap;				//Access the class object for the slap detection
	public float robotSpeed;			//Stores the speed of the sandbag
	public int score;					//Stores the distance the sandbag has flown
	public Camera mcamera;				//Access the camera for hitting the sandbag
	public Camera rcamera;				//Access the camera that follows the sandbag
	public bool landed;					//Stores the variable that says if the sandbag has landed or not
		
	
	// Use this for initialization
	void Start () {
		//The object hasn't landed yet
		landed = false;
		//Get the highscore from the last session
		score = PlayerPrefs.GetInt ("highscore");
	}
	
	// Update is called once per frame
	void Update () {
		//Update the speed of the sandbag
		robotSpeed = slap.robotSpeed;
	}


	//To let the player bask in their glory
	IEnumerator StartLoader () 
	{
		yield return new WaitForSeconds(3);
		Application.LoadLevel("pimpScore");
	}

	//Give a longer delay and celebration for this achievement
	IEnumerator numberOneLoader () 
	{
		slapText.speedText.color = Color.cyan;
		for (int i=0; i<12; i++) {
			if (i%2==0){
				yield return new WaitForSeconds(1);
				slapText.speedText.color = Color.cyan;
			}
			else{
				yield return new WaitForSeconds(1);
				slapText.speedText.color = Color.green;	
			}
		}
		Application.LoadLevel("pimpScore");
	}

	//Returns if they made the highscorelist or not
	bool HighScore(){
		if (score > PlayerPrefs.GetInt ("highscorePos5")) {
			PlayerPrefs.SetInt("highscore", (int)slap.deltaDistance.x*(-1));
			return true;
		}
		return false;
	}

	//If the sandbag landed on the floor, set the score and text accordingly
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
			slapText.wordScore = "Mayweather, that's not your girlfriend: "+score;
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



		//Set the sandbag back to his original point of whatever and stuff
		slap.restart = true;
		landed = true;
		mcamera.enabled = true;
		rcamera.enabled = false;
		slap.explosion (landed);
		slap.pos = 0;
		
		
		//If a highscore was set, check the following
		if (newHighscore){
			//If its the overall highscore, go crazy
			if (score > PlayerPrefs.GetInt ("highscorePos5")){
				slap.restart = false;
				slapText.wordScore = "OVERALL HIGHSCORE!!!!!!!: "+score;
				slap.momCamera(landed);
				StartCoroutine(numberOneLoader());
			}
			//Otherwise, go only slightly crazy
			else{
				slap.restart = false;
				slapText.wordScore = "New Highscore!: "+score;
				slap.smashBros(landed);
				StartCoroutine(StartLoader());
			}
		}
	}
}
