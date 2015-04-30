using UnityEngine;
using System.Collections;

public class moleGen : MonoBehaviour {

	public GameObject moles;

	public float moleLife = 2.0f;

	public float moleSpawn = 10.0f;
	private float moleDeath = 0.0f;

	public float timer = 0.0f;

	public float minSpawnTime;
	public float maxSpawnTime;
	public float nextSpawnTime;

	public int spawnRange;


	public moleList theList;


	//check to see if the spawn point is in use
	static public bool[] spawnPoint = new bool[9];

	public Vector3[] spawnSpot = new Vector3[9];
	//spawnSpot = {{1, 1,1},{1, 1,1},{1, 1,1},{1, 1,1},{1, 1,1},{1, 1,1},{1, 1,1},{1, 1,1},{1, 1,1}};


	float decreaseMaxSpawnTimer;


	// start the mole generator 
	void Start () {
		nextSpawnTime = Random.Range (minSpawnTime, maxSpawnTime);
		int counter = 0;
		for (int i = 0; i < 3; i++) 
		{
			for(int j = 0; j < 3; j++)
			{
				spawnSpot[counter] = new Vector3(i*2 -2f, 2.5f, j*2 -2f);
				counter++;	
			}
		}

		decreaseMaxSpawnTimer = 0f;

	}




	// spawn moles randomly at at spawn points 
	void SpawnMoles(){

		GameObject go = (GameObject)Instantiate (moles);

		int randomPosition = Random.Range (0, spawnSpot.Length - 1);

		if (!spawnPoint [randomPosition]) {
			go.GetComponent<WhackedMole>().position = randomPosition;
			go.transform.position = spawnSpot [randomPosition];
			spawnPoint [randomPosition] = true;
		} else {
			Destroy(go);
			SpawnMoles();
		}


	
	}

	void CheckMoleDeath(){

	}

	// check to see if it's time to spawn a mole
	void CheckMoleSpawn(){
		timer += Time.deltaTime;
		if (timer > nextSpawnTime) {
			SpawnMoles ();
			timer =0.0f;
			nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
		}
	}


	// Update is called once per frame
	void Update () {

		decreaseMaxSpawnTimer += Time.deltaTime;
		if (decreaseMaxSpawnTimer >= 2f) {
			if (maxSpawnTime >= minSpawnTime + .3)
			{
				decreaseMaxSpawnTimer = 0f;
				maxSpawnTime -= .03f;
			}
		}
		CheckMoleSpawn ();	
	}

}
