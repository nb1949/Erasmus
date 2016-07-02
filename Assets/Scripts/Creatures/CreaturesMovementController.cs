using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CreaturesMovementController : MonoBehaviour {

	private bool mouseDown;
	private Vector3 mouseStartPos;
	public GameObject arrow;
	public GameObject xIcon;
	public List<GameObject> toDisable;
	public CreaturesPool pool;

	public void ActivateArrowMode() {
		foreach(GameObject go in toDisable)
			go.SetActive (false);
		arrow.SetActive (true);
		xIcon.SetActive (true);
		InvokeRepeating ("ArrowUpdate", 0.05f, Time.unscaledDeltaTime);
	}

	public void DisableArrowMode() {
		arrow.SetActive (false);
		xIcon.SetActive (false);
		foreach(GameObject go in toDisable)
			go.SetActive (true);
		CancelInvoke ("ArrowUpdate");
	}

	private void UpdateCreaturesTarget(Vector3 direction) {
		foreach(GameObject go in pool.GetActive ()){
			Creature creature = go.GetComponent<Creature> ();
			creature.movement.AddDirectionalVector((Vector2)direction);
		}
	}


	// Update is called once per frame
	void ArrowUpdate () {
			Vector3 currentMousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			if (Input.GetMouseButtonDown (0)) {
				mouseDown = true;
				mouseStartPos = currentMousePos;
				arrow.transform.position = mouseStartPos;
			} else if (Input.GetMouseButtonUp (0)) {
				UpdateCreaturesTarget (currentMousePos - mouseStartPos);
				mouseDown = false;
				arrow.transform.localScale = Vector3.zero;
			} else if (mouseDown) {
				Vector3 v3 = currentMousePos - mouseStartPos;
				arrow.transform.position = mouseStartPos + (v3) / 2.0f;
				arrow.transform.localScale = new Vector3 (v3.magnitude / 2.0f, v3.magnitude / 2.0f, arrow.transform.localScale.z);
				arrow.transform.rotation = Quaternion.FromToRotation (Vector3.up, v3);
			}
	}


}
