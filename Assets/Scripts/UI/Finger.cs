using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Finger : MonoBehaviour {

	[Range(0,4)]
	public int fingerID;
	[Range(1,10)]
	public int forceMultiplier;
	[Range(0,10)]
	public int maxEffect;
	private HashSet<Collider2D> others;

	void Awake() {
		others = new HashSet<Collider2D>();
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
			others.Add (other);
			InvokeRepeating ("Push", 0f, 0.3f);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag ("Creature")) {
			others.Remove (other);
			CancelInvoke ();
		}
	}

	private void Push() {
		foreach (Collider2D other in others) {
			if (other.attachedRigidbody.velocity.magnitude < maxEffect) {
				Vector3 delta = other.transform.position - transform.position;
				other.attachedRigidbody.AddForce (delta.normalized * forceMultiplier * 100);
			}
		}
	}
}
