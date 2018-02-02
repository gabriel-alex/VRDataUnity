using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_FPC : MonoBehaviour {

	private Vector2 m_Input;
	private CharacterController m_CharacterController;
	[SerializeField] private float speed;
	[SerializeField] private float m_GravityMultiplier;
	private Vector3 m_MoveDir = Vector3.zero;
	public bool debug = false;
	private Camera m_cam;

	// Use this for initialization
	void Start () {
		m_CharacterController = GetComponent<CharacterController> ();
		m_cam = Camera.main;

	}
	
	// Update is called once per frame
	void Update () {

		GetInput ();

		Vector3 desiredMove = m_cam.transform.forward * m_Input.y + m_cam.transform.right * m_Input.x;

		/*RaycastHit hitInfo;
		Physics.SphereCast (transform.position, m_CharacterController.radius, Vector3.down, out hitInfo, m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
		desiredMove = Vector3.ProjectOnPlane (desiredMove, hitInfo.normal).normalized;*/

		m_MoveDir.x = desiredMove.x * speed;
		m_MoveDir.z = desiredMove.z * speed;

		m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
		m_CharacterController.Move (m_MoveDir * Time.fixedDeltaTime);


	}
	private void GetInput(){
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		m_Input = new Vector2 (horizontal, vertical);
		/*if (m_Input.sqrMagnitude > 1) {
			m_Input.Normalize ();
		}*/
		if (debug) {
			Debug.Log ("horizontal input value" + m_Input.x + ": Vertical input value" + m_Input.y);
		}
	}
}
