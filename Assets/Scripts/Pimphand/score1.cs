//Dreamlo private key: jYKDapJLaEiT-oKwuc284waGNjjBulq02yX5kvWjvPwQ
//Dreamlo public key: 5551854e6e51b61a1cf90915
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class score1 : MonoBehaviour {
	public string highScore;
	public Text hsText;
	public Highscore[] highscoresList;

	const string privateCode = "jYKDapJLaEiT-oKwuc284waGNjjBulq02yX5kvWjvPwQ";
	const string publicCode = "5551854e6e51b61a1cf90915";
	const string webURL = "http://dreamlo.com/lb/";




	// Use this for initialization
	void Start () {
		DownloadHighscores();
		StartCoroutine(delExtras ());
	}
	
	// Update is called once per frame
	void Update () {
		highScore = "1: "	+highscoresList[0].username+" "+highscoresList[0].score;
		highScore +="\n\n 2: "+highscoresList[1].username+" "+highscoresList[1].score;
		highScore +="\n\n 3: "+highscoresList[2].username+" "+highscoresList[2].score;
		highScore +="\n\n 4: "+highscoresList[3].username+" "+highscoresList[3].score;
		highScore +="\n\n 5: "+highscoresList[4].username+" "+highscoresList[4].score;

		DownloadHighscores();

		hsText = GetComponent<Text> ();
		hsText.text = highScore;
	}
	
	public struct Highscore {
		public string username;
		public int score;
		
		public Highscore(string _username, int _score) {
			this.username = _username;
			this.score = _score;
		}
		
	}

	//Only the top 5 make it, rest is wasted space
	void excess(){
		for (int i = 5; i<highscoresList.Length; i++) {
			DelHighscore(highscoresList[i].username);
		}
	}
	
	//Wait for the db to finish without halting the game
	IEnumerator delExtras () 
	{
		yield return new WaitForSeconds(3);
		excess ();
	}
	
	public void AddNewHighscore(string username, int score) {
		StartCoroutine(UploadNewHighscore(username,score));
	}
	
	public void DelHighscore(string username) {
		StartCoroutine(DeleteHighscore(username));
	}
	
	IEnumerator UploadNewHighscore(string username, int score) {
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
			username = username.Replace("+", " ");
			int score = int.Parse(entryInfo[1]);
			this.highscoresList[i] = new Highscore(username,score);
		}
	}

}
