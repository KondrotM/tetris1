using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour {
	
	public Text scoreText;

	public float scoreCount;

	public float pointsPerSecond;

	public bool scoreIncreaing;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "Score: " + FindObjectOfType<Game>().Score;

	}
}
