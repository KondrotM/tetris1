using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrimino : MonoBehaviour {

	float fall = 0;
	public float fallSpeed = 1;
	public int linesCleared;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	
		if (Input.GetKeyDown(KeyCode.RightArrow)){
			
			transform.position += new Vector3(1,0,0);

			if (CheckIsValidPosition ()) {
//				FindObjectOfType<Game> ().UpdateGrid (this);
				FindObjectOfType<Game>().UpdateGrid(this);  
			//	Debug.Log (transform.position);
			} else {
				transform.position += new Vector3 (-1, 0, 0);
			}
		} else if (Input.GetKeyDown(KeyCode.LeftArrow)){
				
			transform.position += new Vector3(-1,0,0);

			if (CheckIsValidPosition ()) {
//				FindObjectOfType<Game> ().UpdateGrid (this);
				FindObjectOfType<Game>().UpdateGrid(this);  
			} else { transform.position += new Vector3 (1, 0, 0);
				}
		} else if (Input.GetKeyDown(KeyCode.UpArrow)){
					
			transform.Rotate (0, 0, 90);
			if (CheckIsValidPosition ()) { 
			//	Debug.Log (transform.position);
			} else { 
				transform.Rotate (0, 0, -90);
			}

		} else if (Input.GetKey(KeyCode.DownArrow) || Time.time - fall >= fallSpeed){
					
			transform.position += new Vector3(0,-1,0);

			if (CheckIsValidPosition ()) {
				FindObjectOfType<Game>().UpdateGrid(this);  
		//		Debug.Log (transform.position);
			} else { transform.position += new Vector3 (0, 1, 0);
				FindObjectOfType<Game> ().DeleteRow ();
				enabled = false;
				FindObjectOfType<Game> ().AddScore ();
				FindObjectOfType<Game> ().SpawnNextTetrimino ();
		//		Debug.Log (transform.position);
					}
			fall = Time.time;
		}
			
	}
	bool CheckIsValidPosition (){
		foreach (Transform i in transform) {
			Vector2 pos = FindObjectOfType<Game> ().Round (i.position); 

			if (FindObjectOfType<Game>().CheckIsInsideGrid (pos) == false) {
				return false;
			}
			if (FindObjectOfType<Game> ().GetTransformAtGridPosition (pos) != null && FindObjectOfType<Game> ().GetTransformAtGridPosition (pos).parent != transform) {
				return false;
			}
		}
		return true;
	}

}
