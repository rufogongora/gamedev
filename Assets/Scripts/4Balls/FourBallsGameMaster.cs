using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FourBallsGameMaster : MonoBehaviour {

	public Transform [] balls;
	public Text scoreText;
	public Text timeText;
	public Text gameOver;
	public AudioClip hit; 
	public AudioClip topScore; 
	public AudioClip highscore; 
	AudioSource audio;
	int score;
	public float time;
	bool gOver;
	// Use this for initialization
	void Start () {
		score = 0;
		InvokeRepeating ("generateBalls", 0f, 5f);
		gOver = false;
		audio = GetComponent<AudioSource>();
	}

	public void soundScore(){
		audio.PlayOneShot(hit);
	}

	public void numberOne(){
		audio.PlayOneShot(topScore);
	}

	public void highScored(){
		audio.PlayOneShot(highscore);
	}
	
	public void ScoreUp()
	{
		//if game over then do nothing
		if (gOver)
			return;

		score += 1;
		time += 2f;
	}
	public void ScoreDown()
	{
		//if game over then do nothing
		if (gOver)
			return;
		time -= 1f;
	}

	void generateBalls()
	{
		//area
		float x = Random.Range (-2f, 2f);
		float y = 3.8f;
		float z = Random.Range (1.2f, -2.4f);

		//hwo many balls
		int howMany = 4;

		for (int i =0 ; i<howMany ; i++)
		{
			Instantiate(balls[Random.Range(0,4)], new Vector3(x,y,z), Quaternion.identity);
		}



	}


	IEnumerator GameOver() {
		yield return new WaitForSeconds(3);
		//EXIT THE GAME HERE:
		Application.LoadLevel ("Menu");
	}

	IEnumerator newScore() {
		yield return new WaitForSeconds(3);
		//EXIT THE GAME HERE:
		Application.LoadLevel ("4BallsHighscore");
	}

	IEnumerator overallScore() {
		yield return new WaitForSeconds(3);
		//EXIT THE GAME HERE:
		gameOver.color = Color.cyan;
		for (int i = 0; i<12; i++) {
			if (i%2==0){
				gameOver.color = Color.cyan;
				yield return new WaitForSeconds(1);
			}
			else{
				gameOver.color = Color.green;
				yield return new WaitForSeconds(1);
			}
		}
		Application.LoadLevel ("4BallsHighscore");
	}

	// Update is called once per frame
	void Update () {
		//decrease time
		if (!gOver)
			time = time - Time.deltaTime;

		if (time > 10) {
			timeText.color = Color.green;
		} else {
			timeText.color = Color.red;
		}

		scoreText.text = "Balls: " + score.ToString ();
		timeText.text = "Time Left: " + (int)time;
		PlayerPrefs.SetInt("highball", score);

		if (time <= 0) {
			//game over
			if (!gameOver.enabled)
			{
				gameOver.enabled = true;
				if (score > PlayerPrefs.GetInt("scoreballPos5")){
					if (score > PlayerPrefs.GetInt("scoreballPos1")){
						gameOver.color = Color.cyan;
						numberOne();
						gameOver.text = "Overall Highscore!!!";
						gameOver.text += "\n Balls: " + score;
						StartCoroutine("overallScore");
					}
					else{
						highScored();
						gameOver.text = "New Highscore!";
						gameOver.text += "\n Balls: " + score;
						StartCoroutine("newScore");
					}
				}
				else{
					gameOver.text += "\n Balls: " + score;
					//StartCoroutine("GameOver");
					gOver = true;
				}
			}
			
		}

	}
}
