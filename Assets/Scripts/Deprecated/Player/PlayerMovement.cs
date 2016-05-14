using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Runtime.Remoting.Messaging;

public class PlayerMovement : MonoBehaviour {

	[Range(0,1)]
	public float epsilon;
	private Collider2D col;
	private bool onTheMove;
	private Vector2 target;
	private RaycastHit2D hit;
	private CreatureGenome genome;

	// Use this for initialization
	void Start () {
		genome = GetComponent<CreatureGenome> ();
		col = GetComponent<Collider2D> ();
		onTheMove = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Vertical")) {
			//col.attachedRigidbody.AddForce (Input.GetAxis ("Vertical") *  genome.properties["moveSpeed"] * Time.deltaTime * transform.up);
			transform.Translate (0, Input.GetAxis ("Vertical") *  genome.properties["moveSpeed"] * Time.deltaTime, 0);
		}

		if (Input.GetButton ("Horizontal")) {
			transform.Rotate (-1 * Input.GetAxis ("Horizontal") * transform.forward * genome.properties["rotateSpeed"] * Time.deltaTime);
		}

		if (Input.GetMouseButtonDown (0)) {
			target = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
			onTheMove = true;
		}

		if (onTheMove && MoveToTarget ())
			onTheMove = false;
	}

	bool RotateToTarget(Vector2 heading) {
		col.attachedRigidbody.velocity = Vector2.zero;
		float angle = (Mathf.Atan2(heading.y,heading.x) - Mathf.PI/2) * Mathf.Rad2Deg;
		Quaternion qTo = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, genome.properties["rotateSpeed"] * Time.deltaTime);
		return (Quaternion.Angle(transform.rotation, qTo) < epsilon);
	}

	bool MoveToTarget() {
		Vector2 position = transform.position;
		Vector2 heading = target - position;
		if(RotateToTarget (heading) && heading.magnitude > 0)
			col.attachedRigidbody.velocity = heading / heading.magnitude * genome.properties["moveSpeed"];
		return (heading.magnitude < epsilon);
	}

	void stopMovement() {
			col.attachedRigidbody.velocity = Vector2.zero;
	}
}
