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
	public bool[] spawnPoint = new bool[9];

	public Vector3[] spawnSpot = new Vector3[9];
	//spawnSpot = {{1, 1,1},{1, 1,1},{1, 1,1},{1, 1,1},{1, 1,1},{1, 1,1},{1, 1,1},{1, 1,1},{1, 1,1}};


	// start the mole generator 
	void Start () {
		nextSpawnTime = Random.Range (minSpawnTime, maxSpawnTime);
	}




	// spawn moles randomly at at spawn points 
	void SpawnMoles(){

		//randomly pick spawn point
		//spawnRange = Random.Range (0, 9);


		//if the spot is not taken
		//if (spawnPoint[spawnRange]==false) {

			float x = Random.Range (-3.25f, 3.25f);
			float z = Random.Range (-2.75f, 1.75f);
			GameObject go = (GameObject)Instantiate (moles);
			go.transform.position = new Vector3 (x, 2.5f, z);
			theList.InsertIntoList (go);

		//	spawnPoint[spawnRange]=true;
		//}

		//else if the spot is taken call the function again

		//else {
		//	SpawnMoles();
		//}


		

		
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
		CheckMoleSpawn ();	
	}

}
