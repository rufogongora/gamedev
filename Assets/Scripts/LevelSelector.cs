using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

	public Text t; 

	IEnumerator OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<CapsuleScript>() != null)
		{
			if (other.GetComponent<CapsuleScript>().capsuleName != "Exit")
			{//Application.LoadLevel(other.GetComponent<CapsuleScript>().capsuleName);
				t.text = "Loading: " + other.GetComponent<CapsuleScript>().capsuleName + " ...";
				AsyncOperation async = Application.LoadLevelAsync(other.GetComponent<CapsuleScript>().capsuleName);
				yield return async;
				Debug.Log("Loading complete");
			}

				else
			{
				//Debug.l
				Application.Quit();
			}
		}
	}
}
