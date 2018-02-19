using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeSystem : MonoBehaviour {

	public bool debug_raycast = false;
	public GameObject reticle;
	public Color inactiveReticleColor = Color.green;
	public Color activeReticleColor = Color.red;

	private GazeableObject currentGazeObject;
	private GazeableObject currentSelectedObject;

	private RaycastHit lastHit;
	// Use this for initialization
	void Start () {

		SetReticleColor (inactiveReticleColor);
		
	}
	
	// Update is called once per frame
	void Update () {

		ProcessGaze ();
		CheckForInput (lastHit);
		
	}

	private void ProcessGaze(){
		Ray raycastRay = new Ray (transform.position, transform.forward);
		RaycastHit hitInfo;

		if (debug_raycast) {
			Debug.DrawRay (raycastRay.origin, raycastRay.direction * 100);
		}

		if (Physics.Raycast (raycastRay, out hitInfo)) {

			GameObject hitObj = hitInfo.collider.gameObject;
			GazeableObject gazeObj = hitObj.GetComponentInParent<GazeableObject> ();

			if (gazeObj != null) {
				if (gazeObj != currentGazeObject) {
					ClearCurrentObject ();
					currentGazeObject = gazeObj;
					currentGazeObject.OnGazeEnter (hitInfo);
					SetReticleColor (activeReticleColor);
				} else {
					currentGazeObject.OnGaze (hitInfo);
				}

			} else {
				ClearCurrentObject ();
			}
			lastHit = hitInfo;

		} else {
			ClearCurrentObject ();
		}
	}
	private void SetReticleColor(Color reticleColor){

		//reticle.GetComponent<Renderer>().material.SetColor("_color", reticleColor);
		reticle.GetComponent<Renderer>().material.color= reticleColor;
		
	}

	private void CheckForInput(RaycastHit hitInfo){
		if (Input.GetMouseButtonDown (0) && currentGazeObject != null) {
			currentSelectedObject = currentGazeObject;
			currentSelectedObject.OnPress (hitInfo);
		} else if (Input.GetMouseButton (0) && currentSelectedObject != null) {
			currentSelectedObject.OnHold (hitInfo);

		} else if (Input.GetMouseButtonUp (0) && currentSelectedObject != null) {
			currentSelectedObject.OnRelease (hitInfo);
			currentSelectedObject = null;
		}
	}
	private void ClearCurrentObject(){
		if (currentGazeObject != null) {
			currentGazeObject.OnGazeExit ();
			SetReticleColor (inactiveReticleColor);
			currentGazeObject = null;
		}
	}
}
