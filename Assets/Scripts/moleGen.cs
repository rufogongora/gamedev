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




	public float gameTime = 60f;

	public Text ScoreBoard;
	public Text timeLeft;
	

	//variables to hold spot of spawn and check if spot is taken
	static public bool[] spawnPoint = new bool[9];
	public Vector3[] spawnSpot = new Vector3[9];

	float decreaseMaxSpawnTimer;


	// start the mole generator 
	void Start () {

		//
		nextSpawnTime = Random.Range (minSpawnTime, maxSpawnTime);

		spawnPointGeneration ();
		FixedMoleSpawn ();

		decreaseMaxSpawnTimer = 0f;
	}

	//initialize the points where moles can spawn
	void spawnPointGeneration(){

		int counter = 0;
		for (int i = 0; i < 3; i++) 
		{
			for(int j = 0; j < 3; j++)
			{
				spawnSpot[counter] = new Vector3(i*2 -2f, 0f, j*2 -2f);
				counter++;	
			}
		}

	}

	void FixedMoleSpawn(){
		foreach (Vector3 MoleVector in spawnSpot) {
			GameObject go = (GameObject)Instantiate (moles);
			go.transform.position = MoleVector;

			GameObject goH = (GameObject)Instantiate (moleHills);
			goH.transform.position = MoleVector;
		
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

		if (gameTime > 10) {
			timeLeft.color = Color.green;
		
		} else {
			timeLeft.color = Color.red;

		}
		ScoreBoard.text = "Score: ";
		timeLeft.text = "Time: " + (int)gameTime;
	


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
