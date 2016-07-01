using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Finger : MonoBehaviour {

	[Range(0,4)]
	public int fingerID;
	[Range(1,10)]
	private HashSet<Creature> others;

	void Awake() {
		others = new HashSet<Creature>();
	}

	// Update is called once per frame
	public void UpdatePosition (int index) {
		transform.position = (Vector2)Camera.main.ScreenToWorldPoint (Input.touches[index].position);
	}

	// Update is called once per frame
	public void UpdatePosition (Vector3 pos) {
		transform.position = (Vector2)Camera.main.ScreenToWorldPoint (pos);	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag ("Creature")) {
			others.Add (other.GetComponent<Creature> ());
			InvokeRepeating ("Push", 0f, 0.02f);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag ("Creature")) {
			others.Remove (other.GetComponent<Creature> ());
			CancelInvoke ();
		}
	}

	void OnDisable() {
		others.Clear ();
		CancelInvoke ();
	}

	private void Push() {
		foreach (Creature other in others) {
			other.movement.AffectMovement (((Vector2)(other.transform.position - transform.position)).normalized);
		}
	}
}
