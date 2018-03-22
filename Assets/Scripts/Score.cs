using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour {
	
	public Text scoreText;

	public Text levelText;

	public Text linesText;

	public Text usrText;

	public Text goalText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "Score: " + Game.Score;

		levelText.text = "Level: " + Game.level;

		linesText.text = "Lines: " + Game.linesCleared;

		usrText.text = GameOverManager.usrstore;

		goalText.text = "Goal: " + FindObjectOfType<Game> ().levelGoal;
	}
}
