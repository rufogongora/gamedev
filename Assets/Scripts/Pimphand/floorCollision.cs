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
		landed = true;
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
		bool newHighscore = false;
		score = (int)slap.deltaDistance.magnitude;
		newHighscore = HighScore();

		if (newHighscore){
			slapText.wordScore = "New Highscore!: "+score;
		}
		if (score < 100){
			slapText.wordScore = "what a joke: "+score;
		}
		else{
			slapText.wordScore = "The pimp hand is strong in you: "+score;
		}

		slap.redo ();
		landed = false;


		//Instantiate (prefab, new Vector3 (164f, 95f, -62f), Quaternion.identity);
		mcamera.enabled = true;
		rcamera.enabled = false;
	}
}
