using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnterName : MonoBehaviour {

	public Text nameText;
	public string changeName;
	public bool nameEntered;
	public bool newHighscore;

	// Use this for initialization
	void Start () {
		nameEntered = false;
		nameText = GetComponent<Text> ();
		Debug.Log (PlayerPrefs.GetInt ("highscore"));

		int score = 100;

		int temp = 0;

		for(int i=1; i<=5; i++) //for top 5 highscores
		{
			if(PlayerPrefs.GetInt("highscorePos"+i)<score)     //if cuurent score is in top 5
			{
				temp=PlayerPrefs.GetInt("highscorePos"+i);     //store the old highscore in temp varible to shift it down 
				PlayerPrefs.SetInt("highscorePos"+i,score);     //store the currentscore to highscores
				if(i<5)                                        //do this for shifting scores down
				{
					int j=i+1;
					score = PlayerPrefs.GetInt("highscorePos"+j);
					PlayerPrefs.SetInt("highscorePos"+j,temp); 
				}
			}
			Debug.Log (PlayerPrefs.GetString("nameScorePos"+i)+" "+PlayerPrefs.GetInt("highscorePos"+i));
		}




	}
	
	// Update is called once per frame
	void Update () {

		changeName = nameText.text;
	}

	public void OnGUI () {
		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return) {
			changeName = nameText.text;
			Debug.Log (changeName);
			nameEntered = true;
		}
	}

}
