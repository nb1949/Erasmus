using UnityEngine;
using System.Collections;

public class CreatureMovement : MonoBehaviour {

	[Range(0,1)]
	public float epsilon;
	[Range(1,20)]
	public float offset;
	[Range(1,10)]
	public int delta;
	[Range(1,40)]
	public int separationLevel;
	[Range(1,10)]
	private Collider2D col;
	private CreatureGenome genome;
	private CreaturesStatistics statistics;
	private Vector2 target;
	private bool onTheMove;


	void Start() {
		col = GetComponent<Collider2D> ();
		genome = GetComponent<CreatureGenome> ();
		statistics = GetComponentInParent<CreaturesStatistics> ();
		onTheMove = false;
		InvokeRepeating ("RandomizeTarget", 5, delta);
	}

	void Update () {
		if (onTheMove && MoveToTarget ())
			stopMovement ();
		Debug.DrawLine (target - Vector2.up * 0.1f, target + Vector2.up * 0.1f, Color.blue);
		Debug.DrawLine (target - Vector2.right * 0.1f, target + Vector2.right * 0.1f, Color.blue);
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
		Debug.DrawLine ((Vector2)transform.position, (Vector2)transform.position + heading, Color.green);
		if(RotateToTarget (heading))
			col.attachedRigidbody.velocity = heading / heading.magnitude * genome.properties["moveSpeed"];
		return (heading.magnitude < epsilon);
	}


	void RandomizeTarget() {
		if (!onTheMove) {
			target = statistics.centerOfMass + Random.insideUnitCircle * offset;
			onTheMove = true;
		}
	}

	public void setTarget(Vector2 target) {
		this.target = target;
		onTheMove = true;
	}

	void stopMovement() {
		if (onTheMove) {
			col.attachedRigidbody.velocity = Vector2.zero;
			onTheMove = false;
		}
	}
}
