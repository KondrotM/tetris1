using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrimino : MonoBehaviour {


	public float fall = 0;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
//		float value2 = 9f;
//		double value1 = Mathf.Pow (value2, 2);
//		Debug.Log (value1);


		//Debug.Log (linesCleared);

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

		} else if (Input.GetKey(KeyCode.DownArrow) || Time.time - fall >= FindObjectOfType<Game>().fallSpeed){
					
			transform.position += new Vector3(0,-1,0);

			if (CheckIsValidPosition ()) {
				FindObjectOfType<Game>().UpdateGrid(this);  
		//		Debug.Log (transform.position);
			} else { transform.position += new Vector3 (0, 1, 0);
				FindObjectOfType<Game> ().DeleteRow ();
				if (FindObjectOfType<Game> ().CheckIsAboveGrid (this)) {
					FindObjectOfType<Game> ().GameOver ();
				}
				enabled = false;
				//FindObjectOfType<Game> ().IncreaseSpeed ();

			
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
