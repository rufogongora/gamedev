using UnityEngine;
using System.Collections;

public class DudeSpawner : MonoBehaviour {

	public float minX = -14f;
	public float maxX = 17f;

	public GameObject dude;

	public AudioSource audioSource;
	public AudioSource booSound;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("spawnADude", 0f, 2f);
	}

 
	void spawnADude()
	{
		GameObject newDude = (GameObject)Instantiate (dude);
		newDude.transform.position = new Vector3 (Random.Range (minX, maxX), transform.position.y, transform.position.z); 
		newDude.transform.rotation = Random.rotation;
		newDude.GetComponent<fallingDude> ().spawner = this;
	}

	public void scoreUp()
	{
		audioSource.Play ();
	}

	public void Boo()
	{
		booSound.Play ();
	}
}
