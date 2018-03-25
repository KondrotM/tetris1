using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameOverManager : MonoBehaviour {
	public int vol = 50;
	public Text usertext;
	public Text[] personalScore;
	static public string usrstore;
	public InputField usrname;
	public Text confirmusr;
	public Button buttonOptions;

	void Awake() {
	}

	public void setUsername () {
		//shows the user's username upon hitting the submit button to act as confirmation of the name being set.
		usertext.text = usrstore;
	}
		

	public void displayEndScores () {
		//shows the user's statistics at the end of the game. I used a list because this made it easier to implement in the unity editor (click and drag one thing).
		personalScore [0].text = Game.Score.ToString();
		personalScore [1].text = Game.level.ToString ();
		personalScore [2].text = Game.linesCleared.ToString();
		personalScore [3].text = GameOverManager.usrstore;

	}

	public void PlayAgain () {
		//loads the game on play again
		Application.LoadLevel ("Tetris");
		//sets the scores to the default so they don't carry over from previous sessions
		Game.level = 1;
		Game.Score = 0;
		Game.linesCleared = 0;
	}

	public void minusVol () {
		vol = vol - 5;
		Debug.Log (vol);
	}

	public void plusVol () {
		vol = vol + 5;
		Debug.Log (vol);
	}

	public void submit () {
		Debug.Log (usrstore);
		//checks if the user is trying to break the database by entering banned characters, and stops them from doing so
		if (usrstore == "") {
			usrstore = "Guest";
		}
		if (usrstore.Contains ("*")) {
			usrstore = "Guest";
		}
		//adds the highscore to the datatable
		Highscores.AddNewHighscore (usrstore, Game.Score, Game.level, Game.linesCleared);
	}

	public void getusrname () {
		confirmusr.text = usrname.text;
		usrstore = usrname.text;
		Debug.Log (usrstore);
	}

	void Start () {
		//test for button functionality
		Button btn = buttonOptions.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnclick);
	}
	
	void TaskOnclick() {
		//test for button functionality
		Debug.Log ("You have clicked the button!");
	}

	public void loadMenu() {
		//return to menu button
		SceneManager.LoadScene ("Start_Screen");
//		Application.Loadlevel ("Start_Screen");
	}

	void Update () {

	}

	public void Options () {
		//controls visibility of different sections of the start menu
		gameObject.SetActive(false);

	}
}
