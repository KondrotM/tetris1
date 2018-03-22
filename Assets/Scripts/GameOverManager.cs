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
	//public Text volT = int.Parse(vol.text);
	public Button buttonOptions;

	void Awake() {
//		DontDestroyOnLoad(this);
	}

	public void setUsername () {
		usertext.text = usrstore;
	}
		

	public void displayEndScores () {
		personalScore [0].text = Game.Score.ToString();
		personalScore [1].text = Game.level.ToString ();
		personalScore [2].text = Game.linesCleared.ToString();
		personalScore [3].text = GameOverManager.usrstore;

	}

	public void PlayAgain () {
//		Highscores.AddNewHighscore("Guest", 11);
		Application.LoadLevel ("Tetris");
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
		if (usrstore == "") {
			usrstore = "Guest";
		}
		if (usrstore.Contains ("*")) {
			usrstore = "Guest";
		}
		Highscores.AddNewHighscore (usrstore, Game.Score, Game.level, Game.linesCleared);
//		Highscores.AddNewHighscore (usrstore, FindObjectOfType<Game> ().Score);
	}

	public void getusrname () {
		confirmusr.text = usrname.text;
		usrstore = usrname.text;
		Debug.Log (usrstore);
	}

	void Start () {
		Button btn = buttonOptions.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnclick);
	}
	
	void TaskOnclick() {
		Debug.Log ("You have clicked the button!");
	}

	public void loadMenu() {
		SceneManager.LoadScene ("Start_Screen");
//		Application.Loadlevel ("Start_Screen");
	}

	void Update () {

	}

	public void Options () {
		gameObject.SetActive(false);

	}
}
