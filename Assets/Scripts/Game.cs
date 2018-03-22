using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {


	public bool tetLoop = false;
	public double fallSpeed = 1;
	static public int level = 1;
	public float levelGoal = 5f;
	public static int gridWidth = 10;
	public static int gridHeight = 20;
	static public int linesCleared = 0;
	public int tempLines = 0;
	static public int Score = 0;
	public static Transform[,] grid = new Transform[gridWidth, gridHeight];

	private GameObject previewTetrimino;
	private GameObject nextTetrimino;

	private bool gameStarted = false;

	private Vector2 previewTetriminoPosition = new Vector2 (-6.5f, 15);

	//	public static Transform[,] grid = new Transform[gridWidth, gridHeight];
	// Use this for initialization
	void Start () {
		SpawnNextTetrimino ();
//		SpawnHold ();
	}

//	public void SpawnHold () {
//			GameObject nextTetrimino = (GameObject)Instantiate (Resources.Load (GetRandomTetriminoHold (), typeof(GameObject)), new Vector2 (15.0f, 15.0f), Quaternion.identity);
//		}


	public string GetRandomTetriminoHold () {
		int randomTetrimino = Random.Range (1, 8);
		string randomTetriminoName = "Prefabs/Tetrimino_TH";
		string nextMino = "base";
		switch (randomTetrimino) {
		case 1:
			randomTetriminoName = "Prefabs/Tetrimino_TH";
			nextMino = "Prefabs/Tetrimino_T";
			Debug.Log (nextMino);
			break;
		case 2:
			randomTetriminoName = "Prefabs/Tetrimino_IH";
			nextMino = "Prefabs/Tetrimino_I";
			Debug.Log (nextMino);
			break;
		case 3:
			randomTetriminoName = "Prefabs/Tetrimino_OH";
			nextMino = "Prefabs/Tetrimino_O";
			Debug.Log (nextMino);
			break;
		case 4:
			randomTetriminoName = "Prefabs/Tetrimino_JH";
			nextMino = "Prefabs/Tetrimino_J";
			Debug.Log (nextMino);
			break;
		case 5:
			randomTetriminoName = "Prefabs/Tetrimino_LH";
			nextMino = "Prefabs/Tetrimino_L";
			Debug.Log (nextMino);
			break;
		case 6:
			randomTetriminoName = "Prefabs/Tetrimino_SH";
			nextMino = "Prefabs/Tetrimino_S";
			break;
		case 7:
			randomTetriminoName = "Prefabs/Tetrimino_ZH";
			nextMino = "Prefabs/Tetrimino_Z";
			break;

		}
		//asset
		return randomTetriminoName;
	}

	public string GetTetriminoFromHold () {
		string minoName = GetRandomTetriminoHold ();
		minoName = minoName.Remove (minoName.Length - 1);
		Debug.Log (minoName);
		return minoName;
	}


	// Update is called once per frame
	void Update () {
		if (levelGoal <= 0) {
			Game.level += 1;		
//			FindObjectOfType<Game>().level += 1;
			FindObjectOfType<Game>().fallSpeed = 1 * Mathf.Pow(.85f, Game.level);
			//FindObjectOfType<Game>().fallSpeed = 0.2;
			FindObjectOfType<Game>().levelGoal+= 5*level;
			Debug.Log (FindObjectOfType<Game>().fallSpeed);
		}
	}

	public bool CheckIsAboveGrid (Tetrimino tetrimino) {
		for (int x = 0; x < gridWidth; ++x) {
			foreach (Transform mino in tetrimino.transform) {
				Vector2 pos = Round (mino.position);
				if (pos.y > gridHeight - 1) {
					return true;
				}
			}
		}
		return false;
	}

//	public void IncreaseSpeed() {
//		if (linesCleared > level2 ) {
//			level += 1;
//			FindObjectOfType<Tetrimino>().fallSpeed = 1 * Mathf.Pow(0.85f, level);
//			//FindObjectOfType<Tetrimino>().fallSpeed = 0.2;
//			level2 += 5;
//			Debug.Log (FindObjectOfType<Tetrimino>().fallSpeed);
//		}
//	}

	public void AddScore() {
		if (linesCleared == (tempLines + 4)) { 
			Score = (Score + 1200*level);
			tempLines = linesCleared;
		}
		if (linesCleared == (tempLines + 3)) {
			Score = (Score + 300*level);
			tempLines = linesCleared;
		}
		if (linesCleared == (tempLines + 2)) {
			Score = (Score + 100*level);
			tempLines = linesCleared;
		}
		if (linesCleared == (tempLines + 1)) {
			Score = (Score + 40*level);
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
			levelGoal--;
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
		if (!gameStarted) {
			gameStarted = true;
			nextTetrimino = (GameObject)Instantiate (Resources.Load (GetTetriminoFromHold(), typeof(GameObject)), new Vector2 (5.0f, 20.0f), Quaternion.identity);
			previewTetrimino = (GameObject)Instantiate (Resources.Load (GetTetriminoFromHold(), typeof(GameObject)), previewTetriminoPosition, Quaternion.identity);
			previewTetrimino.GetComponent<Tetrimino>().enabled = false;

		} else {
			previewTetrimino.transform.localPosition = new Vector2 (5.0f, 20.0f);
			nextTetrimino = previewTetrimino;
			nextTetrimino.GetComponent<Tetrimino> ().enabled = true;
			previewTetrimino = (GameObject)Instantiate (Resources.Load (GetTetriminoFromHold(), typeof(GameObject)), previewTetriminoPosition, Quaternion.identity);
			previewTetrimino.GetComponent<Tetrimino>().enabled = false;

		}
	}

//	public void SpawnHold () {
//		GameObject nextTetrimino = (GameObject)Instantiate (Resources.Load (GetRandomTetrimino (), typeof(GameObject)), new Vector2 (15.0f, 15.0f), Quaternion.identity);
		//exp = GetComponent<Tetrimino>();
		//Destroy (exp);
//	}

	public bool CheckIsInsideGrid (Vector2 pos) {
		return ((int)pos.x >= 0 && (int)pos.x < gridWidth && (int)pos.y >=0);

	}

	public Vector2 Round (Vector2 pos) {
		return new Vector2 (Mathf.Round(pos.x), Mathf.Round(pos.y));
			}

	public string GetRandomTetrimino () {
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

	public void GameOver () {
		Application.LoadLevel ("Game Over");
	}

}