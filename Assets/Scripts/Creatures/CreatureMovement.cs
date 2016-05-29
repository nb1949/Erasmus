using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CreatureMovement : MonoBehaviour {

	[Range(0,1)]
	public float epsilon;
	[Range(0,1)]
	public float breatherProb;
	[Range(1,20)]
	public float minOffset;
	[Range(1,20)]
	public float maxOffset;
	[Range(0.1f,10)]
	public float delta;
	[Range(1,10)]
	public float acceleration;
	[Range(0, 180)]
	public int avoidObstacleRotation;
	private Collider2D col;
	private Creature creature;
	private CreaturesStatistics statistics;
	private List<string> avoid;
	private Vector2 currentTarget;
	private bool onTheMove;
	public bool pause;
	private float sightDistanceSquared;
	private Animator animator;

	void Awake() {
		col = GetComponent<Collider2D> ();
		creature = GetComponent<Creature> ();
		animator = GetComponentInChildren<Animator> ();
		avoid = new List<string> (2);
		avoid.Add ("Creature");
		avoid.Add ("Block");
	}

	void Start() {
		statistics = GetComponentInParent<Creatures> ().statistics;
		pause = false;
		this._SetOnTheMove (false);
		InvokeRepeating ("RandomizeTarget", delta, delta);
	}

	void FixedUpdate () {
		if (Time.timeScale > 0 && !pause) {
			Transform seen = creature.sight.Seen (avoid, creature.sight.sightDistance);
			if (seen)
				SetTarget (Quaternion.AngleAxis (Mathf.Sign (Vector2.Angle 
					(transform.forward, seen.position - transform.position) - 180) * avoidObstacleRotation,
					Vector3.forward) * transform.up * Random.Range (minOffset, maxOffset));
			if (onTheMove && MoveToTarget ()) 
				this._SetOnTheMove (false);
			Debug.DrawLine (currentTarget - Vector2.up * 0.1f, currentTarget + Vector2.up * 0.1f, Color.blue);
			Debug.DrawLine (currentTarget - Vector2.right * 0.1f, currentTarget + Vector2.right * 0.1f, Color.blue);
		}
	}

	bool RotateToTarget(Vector2 heading) {
		float angle = (Mathf.Atan2(heading.y,heading.x) - Mathf.PI/2) * Mathf.Rad2Deg;
		Quaternion qTo = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo,
			creature.props.Get("rotateSpeed") * Time.deltaTime);
		creature.body.localRotation = Quaternion.RotateTowards(creature.body.localRotation, Quaternion.Inverse (transform.rotation),
			creature.props.Get("rotateSpeed") * Time.deltaTime);
		return (Quaternion.Angle(transform.rotation, qTo) < epsilon);
	}

	bool MoveToTarget() {
		Vector2 position = transform.position;
		Vector2 heading = currentTarget - position;
		Debug.DrawLine (position, (Vector2)transform.position + heading, Color.green);
		RotateToTarget (heading);
		if(col.attachedRigidbody.velocity.magnitude < creature.props.Get("moveSpeed"))
			col.attachedRigidbody.AddForce (transform.up * acceleration);
		return (heading.magnitude < epsilon);
	}


	void RandomizeTarget() {
		if (Random.value > breatherProb) {
			Vector2 position = (Vector2)transform.position;
			if (!onTheMove || (currentTarget - position).magnitude > creature.sight.sightDistance) {
				int groupFactor = 1 + statistics.count / 3;
				ArrayList misses = creature.sight.GetMisses ();
				if (misses.Count < 1)
					SetTarget (-(Vector2)transform.up * minOffset);
				else if (Vector2.Distance (position, statistics.meanPosition) > maxOffset * groupFactor) {
					SetTarget (statistics.meanPosition + Random.insideUnitCircle * maxOffset * groupFactor);
				} else {
					Vector2 randomDirection = (Vector2)(Vector3)misses [Random.Range (0, misses.Count)];
					SetTarget (position + randomDirection * Random.Range (minOffset, maxOffset) * groupFactor);
				}
				this._SetOnTheMove (true);
			}
		}
	}

	public void SetTarget(Vector2 target) {
		this.currentTarget = target;
		this._SetOnTheMove (true);
	}

	public void Pause() {
		this.pause = true;
	}

	public void Play() {
		this.pause = false;
	}

	void OnCollisionEnter2D() {
		this._SetOnTheMove (false);
		SetTarget (transform.position);
	}

	private void _SetOnTheMove(bool val){
		onTheMove = val;
		animator.SetBool ("isWalking", val);
	}

}
