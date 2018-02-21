using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragZone : GazeableObject {

	private VRcanvas parentPanel;
	private Transform originalParent;
	private InputMode savedInputMode = InputMode.NONE;

	// Use this for initialization
	void Start () {
		parentPanel = GetComponentInParent<VRcanvas> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnPress(RaycastHit hitInfo){
		base.OnPress (hitInfo);

		originalParent = parentPanel.transform.parent;
		parentPanel.transform.parent = Camera.main.transform;

		savedInputMode = Player.instance.activeMode;
		Player.instance.activeMode = InputMode.DRAG;

	}
	public override void OnRelease(RaycastHit hitInfo){
		base.OnRelease (hitInfo);

		parentPanel.transform.parent = originalParent;
		Player.instance.activeMode = savedInputMode;
	}
}
