using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class moleList : MonoBehaviour {

	//queue to hold the moles
	Queue<GameObject> moleQueue = new Queue<GameObject>();

	//
	void Start () {
		//function that will 
		InvokeRepeating ("removeFirst", 0f, 5f);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	//remove the first mole in FiFo
	public void removeFirst()
	{
		if (moleQueue.Count > 0) {
			GameObject obj = moleQueue.Dequeue();
			Destroy(obj);
		}
	}



	public bool CheckMoleCollisions(GameObject Mole){

		bool MoleCollision=false;
	
		foreach (GameObject moles in moleQueue) {
					 
			//convert the mole into a 2d object as you only care about the x and z position

			Vector2 mole1 = new Vector2 (Mole.transform.position.x, Mole.transform.position.z);
			Vector2 mole2 = new Vector2 (moles.transform.position.x, moles.transform.position.z);

						
			Vector2 dist = mole1 - mole2;
			float dist_mag = Mathf.Sqrt (Mathf.Pow (2, dist.x) + Mathf.Pow (2, dist.y));


			float overlap = 1 - dist_mag;
			//Debug.Log (overlap);

			//if there is an overalap set MoleCollision to true

			if (overlap > 0){
				Debug.Log ("colliding moles need to change position");
				MoleCollision = true;

			}

		}

		return MoleCollision;

	}

	public void InsertIntoList(GameObject mole)
	{
		// for every mole in the the molelist check that the mole you're 
		// trying to insert does not collide with other moles in the queue

		//call the function to check if there's a collision
		bool isCollision = CheckMoleCollisions (mole);
	

		//if there is a mole collision change the position and try to insert again
		if (isCollision) {
			//Debug.Log ("There's a collision");
				float x = Random.Range(-3.25f, 3.25f);
				float z = Random.Range(-2.75f, 1.75f);
				GameObject newMole = mole;
				newMole.transform.position = new Vector3(x, 2.5f,z);
				InsertIntoList(newMole);

		}
		//if there's no collision between the moles insert into the list
		else{

			Debug.Log("There were no collisions, inserting into list");
			moleQueue.Enqueue(mole);
		}

	}
}