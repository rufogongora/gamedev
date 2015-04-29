using UnityEngine;
using System.Collections;

public class fallingDude : MonoBehaviour {

	//
	float dieTime = 5f;
	float timer = 0f;
	float startTime;

	Renderer rend; 
	public bool savable;

	public DudeSpawner spawner;

	//GameObject child;

	bool fading;
	// Use this for initialization
	void Start () {
		fading = false;
		rend = GetComponent<Renderer>();
		rend = GetComponentInChildren<Renderer> ();
		savable = true;

		Transform [] children = gameObject.GetComponentsInChildren<Transform> ();

		foreach (Transform child in children) {
			if (child.GetComponent<Rigidbody>() != null)
			{
				Dude_CollisionDetection x = child.gameObject.AddComponent<Dude_CollisionDetection>();
				x.theDude = this;
			}
		
		}
	}


	public void scoreUp()
	{
		spawner.scoreUp ();
		Destroy (gameObject);
	}
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= dieTime) {
			fading = true;
			startTime = Time.time;
		}
		if (fading) {
			Color c = rend.material.color;
			float x = rend.material.color.a;
			if (x>0)
			{
				x = x - Time.deltaTime;
			}
			else
			{
				x = 0;
				Destroy(gameObject);
			}
			//a = Mathf.Lerp(1.0f, 0.0f, x);
			c.a = x;
			rend.material.color = c;
		}
	}

	public void die()
	{
		spawner.Boo ();
	}
}
