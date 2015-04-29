using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FourBallsGameMaster : MonoBehaviour {

	public Transform [] balls;
	public Text scoreText;
	public Text timeText;
	public Text gameOver;
	int score;
	public float time;
	bool gOver;
	// Use this for initialization
	void Start () {
		score = 0;
		InvokeRepeating ("generateBalls", 0f, 5f);
		gOver = false;
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

		if (time <= 0) {
			//game over
			if (!gameOver.enabled)
			{
				gameOver.enabled = true;
				gameOver.text += "\n Balls: " + score;
				StartCoroutine("GameOver");
				gOver = true;
			}
			
		}

	}
}
