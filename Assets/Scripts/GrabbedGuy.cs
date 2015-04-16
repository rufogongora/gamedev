using UnityEngine;
using System.Collections;
//using UnityStandardAssets.Characters.ThirdPerson

public class GrabbedGuy : MonoBehaviour {

	public GrabbableObject g;
	public NavMeshAgent n;
 	public UnityStandardAssets.Characters.ThirdPerson.AICharacterControl c;
	public UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter t;
	Animator anim;
	public GameMaster gameMaster;
	public AudioSource audio;

	public int type;
	public ParticleSystem pS;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (g.IsGrabbed ()) {
			c.enabled = false;
			n.enabled = false;
			t.enabled = false;
			anim.CrossFade ("Airborne", 0.5f);
			if (!audio.isPlaying)
				audio.Play ();

		} else {
			audio.Stop();
		}

		//this means he's falling below the limits, therefore he's dead
		if (transform.position.y < -4 ) {
			gameMaster.Score();
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		//get crushed
		if (type == 0) {
			if (g.IsGrabbed())
			{
				Die ();
			}
		} else {

			if (!g.IsGrabbed () && other.transform.name == "Floor") {
				transform.rotation = Quaternion.identity;
				c.enabled = true;
				n.enabled = true;
				t.enabled = true;
			}
		}
	}

	public void updateColor()
	{
		if (type == 1) {
		
			Debug.Log ("masizo");
			pS.enableEmission = true;
		}
	}
	

	void Die()
	{
		gameMaster.Score ();
		Destroy (gameObject);
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.GetComponent<Goal>() != null)
		{
			if (other.GetComponent<Goal>().isGoal) 
			{
				gameMaster.ScoreDown();
				Destroy(gameObject);
			}
		}
		else if (other.name == "limit"){
			Die();
		}
	}
}
