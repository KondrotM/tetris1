//testing out how buttons work in unity
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Clicky : MonoBehaviour
{
	public Button yourButton;

	void Start()
	{
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		Debug.Log("You have clicked the button!");
	}
}