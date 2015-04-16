using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	//Time
	public Transform enemy;
	public Transform target;
	public bool enabled;
	public GameMaster gameMaster;

	UnityStandardAssets.Characters.ThirdPerson.AICharacterControl controller;
	// Use this for initialization
	void Start () {
		 controller = enemy.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl> ();
		enemy.GetComponent<GrabbedGuy> ().gameMaster = gameMaster;
		if (!enabled)
			return;
	}

	public void Spawn()
	{
		controller.SetTarget (target);
		Transform e = (Transform)Instantiate (enemy, transform.position, Quaternion.identity);
		if (Random.Range (0, 100) > 80) {
			e.GetComponent<GrabbedGuy> ().type = 1;
		}

		e.GetComponent<GrabbedGuy> ().updateColor ();

	}
	// Update is called once per frame
	void Update () {

		
	}
}
