using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public Transform[] Targets;
	public Text gameOver;
	public cs1 hs;
	public float time = 10f;
	public Transform arrow;
	public float spawnTime = 2f;
	public Spawner[] spawns;
	float timeAlive;

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
		//audio = GetComponent<AudioSource>();
		InvokeRepeating ("changeSpawn", 0f, time);
		InvokeRepeating ("IncreaseDifficulty", 10f, 10);

	}

	IEnumerator GameOver() {
		yield return new WaitForSeconds(3);
		//EXIT THE GAME HERE:
		Application.LoadLevel ("Menu");
	}
	
	IEnumerator newScore() {
		yield return new WaitForSeconds(3);
		//EXIT THE GAME HERE:
		Application.LoadLevel ("CrushHighscore");
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
		Application.LoadLevel ("CrushHighscore");
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
		if (lives > 0)
			timeAlive = timeAlive + Time.deltaTime;

		if (lives == 0) {
			PlayerPrefs.SetInt("highcrush", score);
			Application.LoadLevel("CrushHighscore");
		}

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
