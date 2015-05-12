using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class moleGen : MonoBehaviour {

	public GameObject moles;
	public GameObject moleHills;

	public float timer = 0.0f;

	public float minSpawnTime;
	public float maxSpawnTime;
	public float nextSpawnTime;
	public int spawnRange;


	private bool gameOverMole;




	public float gameTime = 60f;
	public static float gameScore = 0f;

	public Text ScoreBoard;
	public Text timeLeft;
	public Text OverGame;
	

	//variables to hold spot of spawn and check if spot is taken
	static public bool[] spawnPoint = new bool[9];
	public Vector3[] spawnSpot = new Vector3[9];

	float decreaseMaxSpawnTimer;


	// start the mole generator 
	void Start () {

		gameOverMole = false;
		OverGame.text = "";

		//
		nextSpawnTime = Random.Range (minSpawnTime, maxSpawnTime);

		GenerateSpawnPoint ();
		FixedMoleSpawn ();

		decreaseMaxSpawnTimer = 0f;
	}

	//initialize the points where moles can spawn
	void GenerateSpawnPoint(){

		int counter = 0;
		for (int i = 0; i < 3; i++) 
		{
			for(int j = 0; j < 3; j++)
			{
				spawnSpot[counter] = new Vector3(i*2 -2f, -0.6f, j*2 -2f);
				counter++;	
			}
		}

	}

	void FixedMoleSpawn(){

		foreach (Vector3 MoleVector in spawnSpot) {
			GameObject moleSpawn = (GameObject)Instantiate (moles);
			moleSpawn.transform.position = MoleVector;

			GameObject hillSpawn = (GameObject)Instantiate (moleHills);
			hillSpawn.transform.position = MoleVector+ new Vector3(0f, 0.6f,0f);
		
		}
	}


	// spawn moles randomly at at spawn points 
	void SpawnMoles(){

		//randomly pick spawn point
		spawnRange = Random.Range (0, 9);
		
		//if the spot is not taken
		if (spawnPoint[spawnRange]==false) {

			GameObject go = (GameObject)Instantiate (moles);
			go.transform.position = spawnSpot[spawnRange];

			spawnPoint[spawnRange]=true;
		}

		//else if the spot is taken call the function again

		else {
			SpawnMoles();
		}
		

		
	}

	// check to see if it's time to spawn a mole
	void CheckMoleSpawn(){
		timer += Time.deltaTime;
		if (timer > nextSpawnTime) {
			int spawnRate = Random.Range(1, 3);

			SpawnMoles ();
			timer =0.0f;
			nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
		}
	}

	void LowerMaxSpawn(){

		decreaseMaxSpawnTimer += Time.deltaTime;
		if (decreaseMaxSpawnTimer >= 2f) {
			if (maxSpawnTime >= minSpawnTime + .3)
			{
				decreaseMaxSpawnTimer = 0f;
				maxSpawnTime -= .03f;
			}
		}
	
	}

	void gameBoard(){

		gameTime -= Time.deltaTime;
		ScoreBoard.color = Color.yellow;

		if (gameTime > 10) {
			timeLeft.color = Color.green;
		
		} else if (gameTime < 10 && gameTime > 0) {
			timeLeft.color = Color.red;

		} else {
			StartCoroutine("GameOver");
		}
		ScoreBoard.text = "Score: "+ (int)gameScore;
		timeLeft.text = "Time: " + (int)gameTime;
	


	}

	IEnumerator GameOver() {

		OverGame.text = " Game over!";

		yield return new WaitForSeconds(3);

		gameOverMole = true;

		//EXIT THE GAME HERE:
		Application.LoadLevel ("Menu");
	}

	// Update is called once per frame
	void Update () {
		if (gameTime < 0) {
			Debug.Log("game over");
		}

		//LowerMaxSpawn ();
		//CheckMoleSpawn ();
		gameBoard ();
	
	}

}
