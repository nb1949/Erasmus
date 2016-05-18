using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CreatureMovement : MonoBehaviour {

	[Range(0,1)]
	public float epsilon;
	[Range(1,20)]
	public float minOffset;
	[Range(1,20)]
	public float maxOffset;
	[Range(0.1f,10)]
	public float delta;
	[Range(1,10)]
	public int maxSpeed;
	[Range(0, 180)]
	public int avoidObstacleRotation;
	private Collider2D col;
	private Creature creature;
	private CreaturesStatistics statistics;
	private List<string> avoid;
	private Vector2 currentTarget;
	private bool onTheMove;
	private float sightDistanceSquared;


	void Start() {
		col = GetComponent<Collider2D> ();
		creature = GetComponent<Creature> ();
		statistics = GetComponentInParent<CreaturesStatistics> ();
		avoid = new List<string> (2);
		avoid.Add ("Creature");
		avoid.Add ("Block");
		onTheMove = false;
		InvokeRepeating ("RandomizeTarget", delta, delta);
	}

	void Update () {
		Transform seen = creature.sight.Seen (avoid, creature.sight.sightDistance);
		if (seen) 
				SetTarget (Quaternion.AngleAxis(Mathf.Sign (Vector2.Angle 
					(transform.forward, seen.position - transform.position) - 180) * avoidObstacleRotation,
					Vector3.forward) * transform.up * Random.Range (minOffset, maxOffset));
		if (onTheMove && MoveToTarget ()) 
			onTheMove = false;
		Debug.DrawLine (currentTarget - Vector2.up * 0.1f, currentTarget + Vector2.up * 0.1f, Color.blue);
		Debug.DrawLine (currentTarget - Vector2.right * 0.1f, currentTarget + Vector2.right * 0.1f, Color.blue);
	}

	bool RotateToTarget(Vector2 heading) {
		float angle = (Mathf.Atan2(heading.y,heading.x) - Mathf.PI/2) * Mathf.Rad2Deg;
		Quaternion qTo = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, creature.genome.properties["rotateSpeed"] * Time.deltaTime);
		return (Quaternion.Angle(transform.rotation, qTo) < epsilon);
	}

	bool MoveToTarget() {
		Vector2 position = transform.position;
		Vector2 heading = currentTarget - position;
		Debug.DrawLine (position, (Vector2)transform.position + heading, Color.green);
		RotateToTarget (heading);
		if(col.attachedRigidbody.velocity.magnitude < maxSpeed)
			col.attachedRigidbody.AddForce (transform.up * creature.genome.properties["moveSpeed"]);
		return (heading.magnitude < epsilon);
	}


	void RandomizeTarget() {
		Vector2 position = (Vector2)transform.position;
		if (!onTheMove || (currentTarget - position).magnitude > creature.sight.sightDistance) {
			ArrayList misses = creature.sight.GetMisses ();
			if (misses.Count < 1)
				SetTarget (-(Vector2)transform.up * minOffset);
			else if (Vector2.Distance (position, statistics.meanPosition) > maxOffset * statistics.count) {
				SetTarget (statistics.meanPosition + Random.insideUnitCircle * statistics.count *  maxOffset);
			} else {
				Vector2 randomDirection = (Vector2)(Vector3)misses [Random.Range (0, misses.Count)];
				SetTarget (position + randomDirection * statistics.count * Random.Range (minOffset, maxOffset));
			}
			onTheMove = true;
		}
	}

	public void SetTarget(Vector2 target) {
		this.currentTarget = target;
		onTheMove = true;
	}

	void OnCollisionEnter2D() {
		onTheMove = false;
		RandomizeTarget ();
	}
}
