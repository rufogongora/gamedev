using UnityEngine;
using System.Collections;

public class openMenu : MonoBehaviour {

	public GameObject[] objects;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		foreach (GameObject go in objects){
			bool active = GUILayout.Toggle(go.activeSelf, go.name);
			if (active != go.activeSelf)
				go.SetActive(active);

		}
	}
}
