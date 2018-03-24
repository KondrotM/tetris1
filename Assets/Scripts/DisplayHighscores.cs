using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighscores : MonoBehaviour {
	public Text[] usernameText;
	public Text[] scoreText;
	public Text[] levelText;
	public Text[] linesText;

	//Reference to the highscore class
	Highscores highscoreManager;

	void Start () {
		//Goes through all the texts inside highscoreText that we have,
		for (int i = 0; i < usernameText.Length; i++) {
			usernameText [i].text = i + 1 + ". Fetching...";
			scoreText [i].text = i + 1 + ". Fetching...";
			levelText [i].text = i + 1 + ". Fetching...";
			linesText [i].text = i + 1 + ". Fetching...";
		}
		highscoreManager = GetComponent<Highscores> ();

		StartCoroutine ("RefreshHighscores");
	}
	//Appends the fetching text with current highscores,
	//checks that there are enough highscores to display and if there aren't displays a dot.
	public void OnHighscoresDownloaded(Highscore[] highscoreList) {
		for (int i = 0; i < usernameText.Length; i++) {
			usernameText [i].text = i + 1 + ". ";
			scoreText [i].text = "";
			levelText [i].text = "";
			linesText [i].text = "";
			if (highscoreList.Length > i) {
				usernameText [i].text += highscoreList [i].username; 
				scoreText [i].text += highscoreList [i].score; 
				levelText [i].text += highscoreList [i].level; 
				linesText [i].text += highscoreList [i].lines; 


			}
		}
	}
	//Every so often, refreshes highscores
	IEnumerator RefreshHighscores() {
		while (true) {
			highscoreManager.DownloadHighscores ();
			yield return new WaitForSeconds (30);
		}
	}
	// Update is called once per frame
	void Update () {


	}
}
