﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (FindObjectOfType<Game>().tetLoop == true) {
			Destroy(gameObject);
			FindObjectOfType<Game>().tetLoop = false;
			Debug.Log (FindObjectOfType<Game>().tetLoop);
		}
		//SpawnHold ();
	}}
			


		//exp = GetComponent<Tetrimino>();
		//Destroy (exp);
