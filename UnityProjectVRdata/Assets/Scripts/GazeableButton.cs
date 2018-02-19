using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GazeableButton : GazeableObject {

	protected VRcanvas parentPanel;

	private string function_name ="GazeableButton";

	// Use this for initialization
	void Start () {
		parentPanel = GetComponentInParent<VRcanvas> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void SetButtonColor(Color buttonColor)
	{
		GetComponent<Image>().color = buttonColor;
	}

	public override void OnPress(RaycastHit hitInfo){
		base.OnPress (hitInfo);
		if (parentPanel != null) {
			parentPanel.SetActiveButton (this);
			Debug.Log ("["+function_name+"] gazeable button pressed"+ this);
		} else {
			Debug.Log ("["+function_name+"] Button not a child of object with VRpanel component", this);
		}
		//SetButtonColor (Color.green);
	}
}
