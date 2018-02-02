using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionDataCollect : MonoBehaviour {

	private string[] positionData;
	private string function_name = "PositionDataCollect";
	[SerializeField] private bool debug = false;
	[SerializeField] private string service_url;
	[SerializeField] private bool random_user_id = false;
	[SerializeField] private string user_ID;
	public Camera cameraObj;

	void Start(){
		if (random_user_id == true) {
			int randNumber = Random.Range (0, 100000);
			user_ID = "User_" + randNumber.ToString("D6");
		} 
		if (debug) {
			Debug.Log ("["+function_name+"]user id: "+user_ID);
		}
	}

	void Update(){
		float[] position = {transform.position.x, transform.position.y, transform.position.z};
		float[] rotation = {cameraObj.transform.position.x, cameraObj.transform.position.y, cameraObj.transform.position.z};
		postData( position, rotation);
	}

	private void postData(float[] pos, float[] rot){
		WWWForm form = new WWWForm();

		form.AddField ("user_id", user_ID);
		form.AddField ("px", pos[0].ToString("G"));
		form.AddField ("py", pos[1].ToString("G"));
		form.AddField ("pz", pos[2].ToString("G"));
		form.AddField ("rx", rot[0].ToString("G"));
		form.AddField ("ry", rot[1].ToString("G"));
		form.AddField ("rz", rot[2].ToString("G"));
		if (debug) {
			Debug.Log ("["+function_name+"] X:"+pos[0].ToString("G")+" Y:"+pos[1].ToString("G")+" Z:"+pos[2].ToString("G"));
		}
		WWW www = new WWW (service_url, form);
		/*yield return www;
		if (!string.IsNullOrEmpty (www.error)) {
			Debug.Log ("["+function_name+"]" + www.error);
		} else if (debug) {
			Debug.Log ("["+function_name+"]Send accomplished");
		}*/
	}

}
