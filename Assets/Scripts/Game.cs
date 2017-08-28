﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
	public static int gridWidth = 10;
	public static int gridHeight = 20;
	public int linesCleared = 0;
	public int tempLines = 0;
	public int Score = 0;
	public static Transform[,] grid = new Transform[gridWidth, gridHeight];
	//	public static Transform[,] grid = new Transform[gridWidth, gridHeight];
	// Use this for initialization
	void Start () {
		SpawnNextTetrimino ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddScore() {
		if (linesCleared == (tempLines + 4)) { 
			Score = (Score + 1200);
			tempLines = linesCleared;
		}
		if (linesCleared == (tempLines + 3)) {
			Score = (Score + 300);
			tempLines = linesCleared;
		}
		if (linesCleared == (tempLines + 2)) {
			Score = (Score + 100);
			tempLines = linesCleared;
		}
		if (linesCleared == (tempLines + 1)) {
			Score = (Score + 40);
			tempLines = linesCleared;
		}
			
		tempLines = linesCleared;
		Debug.Log (Score);
	}

	public bool IsFullRowAt (int y) {
		for (int x = 0; x < gridWidth; ++x) {
			if (grid[x,y] == null) {
				return false;
			}
		}
			linesCleared++;
			Debug.Log (linesCleared);
			return true;
	}

	public void DeleteMinoAt (int y){
		for (int x = 0; x < gridWidth; ++x) {
			Destroy (grid [x, y].gameObject);
			grid [x, y] = null;
		}
	}

	public void MoveRowDown (int y) {
		for (int x = 0; x < gridWidth; ++x) {
			if (grid [x, y] != null) {
				grid [x, y - 1] = grid [x, y];
				grid [x, y] = null;
				grid [x, y - 1].position += new Vector3 (0, -1, 0);

			}
		}
	}

	public void MoveAllRowDown (int y) {
		for (int i = y; i < gridHeight; ++i) {
			MoveRowDown(i);
		
		}
	}
		

	public void DeleteRow () {
		for (int y = 0; y < gridHeight; ++y) {
			if (IsFullRowAt(y)) {
				DeleteMinoAt(y);
				MoveAllRowDown(y+1);

				--y;
			}
		}
	}

	
	
//	public void UpdateGrid (Tetrimino tetrimino) {
//		for (int y = 0; y < gridHeight; ++y) {
//			for (int x = 0; x <gridWidth; ++x) {
//				if (grid[x,y] != null) {
//					if (grid[x,y].parent == tetrimino.transform) {
//						grid[x,y] = null;
//					}
//				}
//			}
//		}
//
//		foreach (Transform mino in tetrimino.transform) {
//			Vector2 pos = Round (mino.position);
//			if (pos.y < gridHeight) {
//				grid[(int)pos.x, (int)pos.y] = mino;
//			}
//		}
//	}


public void UpdateGrid (Tetrimino tetrimino) {
		for (int y = 0; y < gridHeight; ++y) {
			for (int x = 0; x < gridWidth; ++x) {
				if (grid[x,y] != null){
					if (grid [x, y].parent == tetrimino.transform) {
						grid [x, y] = null;
					}
				}
			}
		}
			foreach (Transform mino in tetrimino.transform) {
				Vector2 pos = Round (mino.position);
				if (pos.y < gridHeight) {
					grid[(int)pos.x, (int)pos.y] = mino;
				}
			}
	}

	public Transform GetTransformAtGridPosition (Vector2 pos) {
		if (pos.y > gridHeight -1) {
			return null;
		} else {
			return grid[(int)pos.x, (int)pos.y];
		}
	}

	public void SpawnNextTetrimino () {
		GameObject nextTetrimino = (GameObject)Instantiate (Resources.Load (GetRandomTetrimino (), typeof(GameObject)), new Vector2 (5.0f, 20.0f), Quaternion.identity);
	}

	public bool CheckIsInsideGrid (Vector2 pos) {
		return ((int)pos.x >= 0 && (int)pos.x < gridWidth && (int)pos.y >=0);

	}

	public Vector2 Round (Vector2 pos) {
		return new Vector2 (Mathf.Round(pos.x), Mathf.Round(pos.y));
			}

	string GetRandomTetrimino () {
		int randomTetrimino = Random.Range (1, 8);
		string randomTetriminoName = "Prefabs/Tetrimino_T";
		switch (randomTetrimino) {
		case 1:
			randomTetriminoName = "Prefabs/Tetrimino_T";
			break;
		case 2:
			randomTetriminoName = "Prefabs/Tetrimino_I";
			break;
		case 3:
			randomTetriminoName = "Prefabs/Tetrimino_O";
			break;
		case 4:
			randomTetriminoName = "Prefabs/Tetrimino_J";
			break;
		case 5:
			randomTetriminoName = "Prefabs/Tetrimino_L";
			break;
		case 6:
			randomTetriminoName = "Prefabs/Tetrimino_S";
			break;
		case 7:
			randomTetriminoName = "Prefabs/Tetrimino_Z";
			break;
	
	}
		return randomTetriminoName;
	}

}