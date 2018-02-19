using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeableObject : MonoBehaviour {

	private string function_name ="GazeableObject";

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
	}
	public virtual void OnHold(RaycastHit hitInfo){
		Debug.Log ("["+function_name+"] button hold on " + gameObject.name);
	}
	public virtual void OnRelease(RaycastHit hitInfo){
		Debug.Log ("["+function_name+"] button released on " + gameObject.name);
	}
}
