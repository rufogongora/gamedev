//Dreamlo private key: jYKDapJLaEiT-oKwuc284waGNjjBulq02yX5kvWjvPwQ
//Dreamlo public key: 5551854e6e51b61a1cf90915

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnterName : MonoBehaviour {

	public Text nameText;			//To access the text the user will be editing
	string changeName;				//A string to store the user's name
	bool nameEntered;				//A bool to check if they hit enter or not
	bool updated;					//A variable for debugging, so the highscore only shifts once
	int score;						//A variable to get the record score from the last round
	Highscore[] highscoresList;

	public struct Highscore {
		public string username;
		public int score;
		
		public Highscore(string _username, int _score) {
			this.username = _username;
			this.score = _score;
		}
		
	}

	const string privateCode = "jYKDapJLaEiT-oKwuc284waGNjjBulq02yX5kvWjvPwQ";
	const string publicCode = "5551854e6e51b61a1cf90915";
	const string webURL = "http://dreamlo.com/lb/";


	// Use this for initialization
	void Start () {
		updated = false;							//Score hasn't been shifted yet
		nameEntered = false;						//Name hasn't been entered yet
		nameText = GetComponent<Text> ();			//Get access to text component
		score = PlayerPrefs.GetInt ("highscore");	//Get the score from the last session

		DownloadHighscores();

		DeleteHighscore ("Mary");

	}


	// Update is called once per frame
	void Update () {
		//If the user entered their name they are ready to go back to playing, put their score in the table and send them back
		if (nameEntered) {
			AddNewHighscore(changeName, score);
			//namepls ();
			//Application.LoadLevel("Pimphand");
		}

		DeleteHighscore ("Mary");

		for (int i = 0; i<highscoresList.Length; i++) {
			print (highscoresList[i].username + ": " + highscoresList[i].score);
		}
	}

	void highScores(){
			
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

	public void AddNewHighscore(string username, int score) {
		StartCoroutine(UploadNewHighscore(username,score));
	}
	
	IEnumerator UploadNewHighscore(string username, int score3) {
		WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
		Debug.Log(www);
		yield return www;
		
		if (string.IsNullOrEmpty(www.error))
			print ("Upload Successful");
		else {
			print ("Error uploading: " + www.error);
		}
	}

	IEnumerator DeleteHighscore(string username) {
		WWW www = new WWW(webURL + privateCode + "/delete/"+WWW.EscapeURL(username));
		yield return www;
		
		if (string.IsNullOrEmpty(www.error))
			print ("Delete Successful");
		else {
			print ("Error uploading: " + www.error);
		}
	}
	
	public void DownloadHighscores() {
		StartCoroutine("DownloadHighscoresFromDatabase");
	}
	
	IEnumerator DownloadHighscoresFromDatabase() {
		WWW www = new WWW(webURL + publicCode + "/pipe/");
		yield return www;
		
		if (string.IsNullOrEmpty(www.error))
			FormatHighscores(www.text);
		else {
			print ("Error Downloading: " + www.error);
		}
	}
	
	void FormatHighscores(string textStream) {
		string[] entries = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
		this.highscoresList = new Highscore[entries.Length];
		
		for (int i = 0; i <entries.Length; i ++) {
			string[] entryInfo = entries[i].Split(new char[] {'|'});
			string username = entryInfo[0];
			int score = int.Parse(entryInfo[1]);
			this.highscoresList[i] = new Highscore(username,score);
		}
	}

}
