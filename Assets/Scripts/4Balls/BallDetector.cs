using UnityEngine;
using System.Collections;

public class BallDetector : MonoBehaviour {

	public string colorBall;
	public FourBallsGameMaster gameMaster;
	// Use this for initialization
	void Start () {
		
	}


	void OnCollisionEnter(Collision other)
	{

		//correct color
		if (other.transform.CompareTag (colorBall)) {
			gameMaster.ScoreUp();
		}
		else {
			gameMaster.ScoreDown();
		}

		Destroy (other.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
