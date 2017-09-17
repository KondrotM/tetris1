using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour {
	
	public Text scoreText;

	public Text levelText;

	public Text goalText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "Score: " + FindObjectOfType<Game>().Score;

		levelText.text = "Level: " + FindObjectOfType<Game>().level;

		goalText.text = "Goal: " + FindObjectOfType<Game> ().levelGoal;
	}
}
