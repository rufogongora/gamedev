using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public Transform[] Targets;
	public float time = 10f;
	public Transform arrow;
	public float spawnTime = 2f;
	public Spawner[] spawns;

	public int score;
	public int lives;

	Spawner spawnerScript;
	Transform target;
	float lastTime = 0f;
	

	//GUI STUFF
	public Text scoreBox;
	public Text livesText;


	// Use this for initialization
	void Start () {
		//This is amount of time it takes to spawn a monster again

		InvokeRepeating ("changeSpawn", 0f, time);
		InvokeRepeating ("IncreaseDifficulty", 10f, 10);

	}


	void IncreaseDifficulty()
	{
		if (spawnTime > 0) {
			Debug.Log ("harder!");
			spawnTime -= .1f;
		}
	}

	// Update is called once per frame
	void Update () {


		if (lives == 0)
			Application.LoadLevel ("Menu");

		//take care of the spawn
		if (Time.time - lastTime > spawnTime) {
			int rndSpawnIndex = Random.Range(0, spawns.Length);
			spawns[rndSpawnIndex].Spawn ();
			GameObject [] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
			foreach (GameObject enemy in enemies) {
				UnityStandardAssets.Characters.ThirdPerson.AICharacterControl controller = enemy.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
				if (controller != null)
					controller.SetTarget(target);
				
			}
			lastTime = Time.time;
			
		}


		//UPDATE THE GUI
		scoreBox.text = "Score: " + score;
		livesText.text = "Lives: " + lives;
	}

	void changeSpawn()
	{		
		if (target != null)
			target.GetComponent<Goal> ().isGoal = false;
		//select a random target from the pre-defined array
		int targetIndex = Random.Range (0, Targets.Length);
		target = Targets [targetIndex];
		target.GetComponent<Goal> ().isGoal = true;
		arrow.position = new Vector3(target.position.x, -1, target.position.z);

		GameObject [] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies) {
			UnityStandardAssets.Characters.ThirdPerson.AICharacterControl controller = enemy.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
			if (controller != null)
				controller.SetTarget(target);
			
		}
	}

	public void Score()
	{
		score += 1;
	}

	public void ScoreDown()
	{
		lives -= 1;
	}
}
