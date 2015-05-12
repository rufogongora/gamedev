using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnterName : MonoBehaviour {

	public Text nameText;			//To access the text the user will be editing
	string changeName;				//A string to store the user's name
	bool nameEntered;				//A bool to check if they hit enter or not
	bool updated;					//A variable for debugging, so the highscore only shifts once
	int score;						//A variable to get the record score from the last round

	// Use this for initialization
	void Start () {
		updated = false;							//Score hasn't been shifted yet
		nameEntered = false;						//Name hasn't been entered yet
		nameText = GetComponent<Text> ();			//Get access to text component
		score = PlayerPrefs.GetInt ("highscore");	//Get the score from the last session
	}


	// Update is called once per frame
	void Update () {
		//If the user entered their name they are ready to go back to playing, put their score in the table and send them back
		if (nameEntered) {
			namepls ();
			Application.LoadLevel("Pimphand");
		}
	}

	/*Takes the user's name and their record setting score from last session and puts them in the table. 
	 It puts them in the correct section and kicks out the lowest guy (Sorry #5). Names and scores 
	 are stored in two different session keys. After its done, it sets the updated variable to true so the score doesn't 
	 keep shifting down */
	void namepls(){
		if (!updated) {
			int tempint = 0;
			string tempstring = "";
			for (int i=1; i<=5; i++) {
				if (PlayerPrefs.GetInt ("highscorePos" + i) < score) {
					tempint = PlayerPrefs.GetInt ("highscorePos" + i); 	
					Debug.Log (i+" "+tempint);
					tempstring = PlayerPrefs.GetString ("nameScorePos" + i); 
					Debug.Log (i+" "+tempstring);
					PlayerPrefs.SetInt ("highscorePos" + i, score); 
					PlayerPrefs.SetString ("nameScorePos" + i, changeName);
					if (i < 5) {
						int j = i + 1;
						score = PlayerPrefs.GetInt ("highscorePos" + j);
						changeName = PlayerPrefs.GetString ("nameScorePos" + j);
						PlayerPrefs.SetInt ("highscorePos" + j, tempint); 
						PlayerPrefs.SetString ("nameScorePos" + j, tempstring); 
					}
				}
			}
			updated=true;
		}
	}

	/*When the user hits enter, they are done typing their name in
	 Set the nameEnetered bool to true and change the value of the string to whatever
	 is in the text box*/
	public void OnGUI () {
		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return) {
			changeName = nameText.text;
			nameEntered = true;
		}
	}

}
