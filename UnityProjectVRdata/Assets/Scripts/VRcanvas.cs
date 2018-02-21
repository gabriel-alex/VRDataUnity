using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRcanvas : MonoBehaviour {

	public GazeableButton currentActiveButton;

	public Color unselectedColor = Color.white;
	public Color selectedColor = Color.green;
	// Use this for initialization

	private string function_name ="VRcanvas";

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		LookAtPlayer ();		
	}

	public void SetActiveButton(GazeableButton activeButton){

		if (currentActiveButton != null) {
			currentActiveButton.SetButtonColor(unselectedColor);
		}
		if (activeButton != null && currentActiveButton != activeButton) {
			currentActiveButton = activeButton;
			currentActiveButton.SetButtonColor(selectedColor);
		} else {
			Debug.Log ("["+function_name+"] Resetting");
			currentActiveButton = null;
			Player.instance.activeMode = InputMode.NONE;
		}
	}

	public void LookAtPlayer(){
		Vector3 playerPos = Player.instance.transform.position;
		Vector3 vectoPlayer = playerPos - transform.position;

		Vector3 lookAtPos = transform.position - vectoPlayer;

		transform.LookAt (lookAtPos);
	}
}
