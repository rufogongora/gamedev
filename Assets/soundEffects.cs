using UnityEngine;
using System.Collections;

public class soundEffects : MonoBehaviour {

	public AudioClip moleHit;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playBonk(){
		audio.PlayOneShot (moleHit);
	}
}
