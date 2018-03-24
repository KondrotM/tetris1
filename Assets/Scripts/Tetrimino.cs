using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrimino : MonoBehaviour {

	public bool SwapTwice = false;
	public float fall = 0;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
		//checks for arrow input and moves the mino to the right
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			transform.position += new Vector3 (1, 0, 0);

			//checks if the new position is valid (i.e. not in a wall) and moves the mino back if it is.
			if (CheckIsValidPosition ()) {
				FindObjectOfType<Game> ().UpdateGrid (this);  
			} else {
				transform.position += new Vector3 (-1, 0, 0);
			}
			//repeat for different arrow keys
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				
			transform.position += new Vector3 (-1, 0, 0);

			if (CheckIsValidPosition ()) {
				FindObjectOfType<Game> ().UpdateGrid (this);  
			} else {
				transform.position += new Vector3 (1, 0, 0);
			}
		} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
					
			transform.Rotate (0, 0, 90);
			if (CheckIsValidPosition ()) { 
			} else { 
				transform.Rotate (0, 0, -90);
			}
			//Moves the mino faster if the down arrow key is pressed.
		} else if (Input.GetKey(KeyCode.DownArrow) || Time.time - fall >= FindObjectOfType<Game>().fallSpeed){
			//Checks and executes for deleting full row, game over.
			transform.position += new Vector3(0,-1,0);
			if (CheckIsValidPosition ()) {
				FindObjectOfType<Game> ().UpdateGrid (this);  
			} else {
				transform.position += new Vector3 (0, 1, 0);
				FindObjectOfType<Game> ().DeleteRow ();
				if (FindObjectOfType<Game> ().CheckIsAboveGrid (this)) {
					FindObjectOfType<Game> ().GameOver ();
				}
				enabled = false;
				FindObjectOfType<Game> ().AddScore ();
				FindObjectOfType<Game> ().SpawnNextTetrimino ();
				FindObjectOfType<Game> ().tetLoop = true;
			}
			//sets the current time as the fall time for consistant drop timings.
			fall = Time.time;
		}
			
	}
	bool CheckIsValidPosition (){
		foreach (Transform i in transform) {
			Vector2 pos = FindObjectOfType<Game> ().Round (i.position); 
			//checks whether the object is outside the grid
			if (FindObjectOfType<Game>().CheckIsInsideGrid (pos) == false) {
				return false;
			}
			//checks if the grid position is equal to the real object co-ordinate position
			if (FindObjectOfType<Game> ().GetTransformAtGridPosition (pos) != null && FindObjectOfType<Game> ().GetTransformAtGridPosition (pos).parent != transform) {
				return false;
			}
		}
		return true;
	}

}
