using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeableObject : MonoBehaviour {

	private string function_name ="GazeableObject";

	public bool isTranformable = false;

	private int objectLayer;
	private const int IGNORE_RAYCAST_LAYER = 2;

	Vector3 initialObjectRotation;
	Vector3 initialPlayerRotation; 
	Vector3 initialObectScale;

	public virtual void OnGazeEnter(RaycastHit hitInfo){
		Debug.Log ("["+function_name+"] gaze entered on " + gameObject.name);
	}
	public virtual void OnGaze(RaycastHit hitInfo){
		Debug.Log ("["+function_name+"] gaze hold on " + gameObject.name);
	}
	public virtual void OnGazeExit(){
		Debug.Log ("["+function_name+"] gaze exited on " + gameObject.name);
	}
	public virtual void OnPress(RaycastHit hitInfo){
		Debug.Log ("["+function_name+"] button pressed on " + gameObject.name);
		if (isTranformable) {
			objectLayer = gameObject.layer;
			gameObject.layer = IGNORE_RAYCAST_LAYER;

			initialObjectRotation = transform.rotation.eulerAngles;
			initialPlayerRotation = Camera.main.transform.rotation.eulerAngles;
			initialObectScale = transform.localScale;
		}
	}
	public virtual void OnHold(RaycastHit hitInfo){
		Debug.Log ("["+function_name+"] button hold on " + gameObject.name);
		if (isTranformable) {
			GazeTransformable (hitInfo);

		}
	}
	public virtual void OnRelease(RaycastHit hitInfo){
		Debug.Log ("["+function_name+"] button released on " + gameObject.name);
		if (isTranformable) {
			gameObject.layer = objectLayer;
		}
	}


	public virtual void GazeTransformable(RaycastHit hitInfo){
		switch (Player.instance.activeMode) {
		case InputMode.TRANSLATE:
			GazeTranslate(hitInfo);
			break;
		case InputMode.ROTATE:
			GazeRotate(hitInfo);
			break;
		case InputMode.SCALE:
			GazeScale(hitInfo);
			break;
		}
	}

	public virtual void GazeTranslate(RaycastHit hitInfo){
		if (hitInfo.collider != null && hitInfo.collider.GetComponent<Floor> ()) {
			transform.position = hitInfo.point;
		}
	}
	public virtual void GazeRotate(RaycastHit hitInfo){
		float rotationSpeed = 5.0f;
		Vector3 currentPlayerRotation = Camera.main.transform.rotation.eulerAngles;
		Vector3 currentObjectRotation = transform.rotation.eulerAngles;
		Vector3 rotationDelta = currentPlayerRotation - initialPlayerRotation;
		Vector3 newRotation = new Vector3 (currentObjectRotation.x, initialObjectRotation.y + (rotationDelta.y * rotationSpeed), currentObjectRotation.z);
		transform.rotation = Quaternion.Euler (newRotation);

	}
	public virtual void GazeScale(RaycastHit hitInfo){

		float scaleSpeed = 0.1f;
		float scaleFactor = 1;

		Vector3 currentPlayerRotation = Camera.main.transform.rotation.eulerAngles;
		Vector3 rotationDelta = currentPlayerRotation - initialPlayerRotation;

		if (rotationDelta.x < 0 && rotationDelta.x > -180.0f || rotationDelta.x > 180.0f && rotationDelta.x < 360.0f) {
			if (rotationDelta.x > 180.0f) {
				rotationDelta.x = 360.0f - rotationDelta.x;
			}

			scaleFactor = 1.0f + Mathf.Abs (rotationDelta.x) * scaleSpeed;

		} else {
			if (rotationDelta.x < -180.0f) {
				rotationDelta.x = 360.0f + rotationDelta.x;
			}
			scaleFactor = Mathf.Max (0.1f, 1.0f - (Mathf.Abs (rotationDelta.x) * scaleSpeed) / 180.0f);
		}

		transform.localScale = scaleFactor * initialObectScale;

	}
}
