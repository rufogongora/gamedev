//Dreamlo private key: 3sq5NJ4XDEGz6aSyjRKqVAQx4D2dFq4kioLxF3-y4JiA
//Dreamlo public key: 5551a3c46e51b61a1cf919aa

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class highScores : MonoBehaviour {
	
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
	
	const string privateCode = "3sq5NJ4XDEGz6aSyjRKqVAQx4D2dFq4kioLxF3-y4JiA";
	const string publicCode = "5551a3c46e51b61a1cf919aa";
	const string webURL = "http://dreamlo.com/lb/";
	
	
	// Use this for initialization
	void Start () {
		updated = false;							//Score hasn't been shifted yet
		nameEntered = false;						//Name hasn't been entered yet
		nameText = GetComponent<Text> ();			//Get access to text component
		score = PlayerPrefs.GetInt ("highball");
		DownloadHighscores();
		StartCoroutine(delExtras ());
	}
	
	
	// Update is called once per frame
	void Update () {
		//If the user entered their name they are ready to go back to playing, put their score in the table and send them back
		if (nameEntered) {
			AddNewHighscore(changeName, score);
			Application.LoadLevel("Menu");
		}
	}

	
	//Only the top 5 make it, rest is wasted space
	void excess(){
		for (int i = 5; i<highscoresList.Length; i++) {
			Debug.Log (highscoresList[i].username);
			DelHighscore(highscoresList[i].username);
		}
	}
	
	//To let the player bask in their glory
	IEnumerator delExtras () 
	{
		yield return new WaitForSeconds(3);
		excess ();
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
	
	public void DelHighscore(string username) {
		StartCoroutine(DeleteHighscore(username));
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
