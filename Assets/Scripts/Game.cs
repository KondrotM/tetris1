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
	private Vector2 previewTetriminoPosition = new Vector2 (-5f, 15);

	void Start () {
		//spawns the first mino in the game, starting the loop which spawns more whenever once hits the ground.
		SpawnNextTetrimino ();
	}




	public string GetRandomTetriminoHold () {

		int randomTetrimino = Random.Range (1, 8);string randomTetriminoName = "Prefabs/Tetrimino_TH";
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
		//checks if the level goal has been reached by clearing lines.
		if (levelGoal <= 0) {
			//makes the game harder
			Game.level += 1;		
			FindObjectOfType<Game>().fallSpeed = 1 * Mathf.Pow(.85f, Game.level);
			FindObjectOfType<Game>().levelGoal+= 5*level;
			Debug.Log (FindObjectOfType<Game>().fallSpeed);
		}
	}

	public bool CheckIsAboveGrid (Tetrimino tetrimino) {
		//loops through all the rows seeing if there is any mino above the grid.
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

	public void AddScore() {
		//increases score based on lines cleared.
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
		//checks if all the squares in a row have been filled
		for (int x = 0; x < gridWidth; ++x) {
			if (grid[x,y] == null) {
				return false;
			}
		}
		//if the row is cleared, lines cleared are updated
		linesCleared++;
		levelGoal--;
		Debug.Log (linesCleared);
		return true;
	}
	//deletes the one square
	public void DeleteMinoAt (int y){
		for (int x = 0; x < gridWidth; ++x) {
			Destroy (grid [x, y].gameObject);
			grid [x, y] = null;
		}
	}

	public void MoveRowDown (int y) {
		//moves down a row if the one underneath it is empty
		for (int x = 0; x < gridWidth; ++x) {
			if (grid [x, y] != null) {
				grid [x, y - 1] = grid [x, y];
				grid [x, y] = null;
				grid [x, y - 1].position += new Vector3 (0, -1, 0);

			}
		}
	}

	public void MoveAllRowDown (int y) {
		//repeats MoveRowDown
		for (int i = y; i < gridHeight; ++i) {
			MoveRowDown(i);

		}
	}


	public void DeleteRow () {
		//repeats delete mino for an entire row
		for (int y = 0; y < gridHeight; ++y) {
			if (IsFullRowAt(y)) {
				DeleteMinoAt(y);
				MoveAllRowDown(y+1);

				--y;
			}
		}
	}

	public void UpdateGrid (Tetrimino tetrimino) {
		//goes through every column
		for (int y = 0; y < gridHeight; ++y) {
			//goes through every row
			for (int x = 0; x < gridWidth; ++x) {
				//checks if the grid from the previous frame has a mino on it
				if (grid[x,y] != null){
					//checks if the mino is still there this frame
					if (grid [x, y].parent == tetrimino.transform) {
						grid [x, y] = null;
					}
				}
			}
		}
		foreach (Transform mino in tetrimino.transform) {
			//rounds the values so that they can snap to grid
			Vector2 pos = Round (mino.position);
			if (pos.y < gridHeight) {
				grid[(int)pos.x, (int)pos.y] = mino;
			}
		}
	}

	public Transform GetTransformAtGridPosition (Vector2 pos) {
		//
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
		//picks a random number between 1 and 8. This corresponds to the mino picked to spawn.
		int randomTetrimino = Random.Range (1, 8);
		//a case function which decides what the next mino will be. It goes through all the cases depending on the random number selected. 
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