﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {
	public int vol = 50;
	public string usrstore;
	public InputField usrname;
	public Text confirmusr;
	//public Text volT = int.Parse(vol.text);
	public Button buttonOptions;

	public void PlayAgain () {
		Application.LoadLevel ("Tetris");
	}

	public void minusVol () {
		vol = vol - 5;
		Debug.Log (vol);
	}

	public void plusVol () {
		vol = vol + 5;
		Debug.Log (vol);
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

	void Update () {

	}

	public void Options () {
		gameObject.SetActive(false);

	}
}
