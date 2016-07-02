using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CreatureMovement : MonoBehaviour {

	[Range(0,3)]
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
	public float avoidObstacleDistance;
	public bool pause;
	private bool overrrideRandomizer;
	private Collider2D col;
	private Creature creature;
	private CreaturesStatistics statistics;
	public List<string> avoid;
	private Vector2 currentTarget;
	private readonly int RIGHT = 1, LEFT = 2;
	private int direction = 0;
	private Animator animator;


	void Awake() {
		col = GetComponent<Collider2D> ();
		creature = GetComponent<Creature> ();
		animator = creature.animator;
	}

	void Start() {
		statistics = GetComponentInParent<Creatures> ().statistics;
		pause = false;
		InvokeRepeating ("RandomizeTarget", delta, delta);
	}

	void FixedUpdate () {
		if (Time.timeScale > 0 && !pause) {
			Transform seenObstacle = creature.sight.Seen (avoid, Mathf.Min(creature.sight.sightDistance, avoidObstacleDistance));
			if (seenObstacle)
				SetTarget (Quaternion.AngleAxis (Mathf.Sign (Vector2.Angle 
					(transform.up, seenObstacle.position - transform.position) - 180) * avoidObstacleRotation,
					Vector3.forward) * transform.up * Random.Range (minOffset, maxOffset));
			this._SetDirection ();
			if (MoveToTarget ()) { //Arrived destination
				if (overrrideRandomizer) 
					overrrideRandomizer = false;
				RandomizeTarget ();
			}
			Debug.DrawLine (currentTarget - Vector2.up * 0.1f, currentTarget + Vector2.up * 0.1f, Color.blue);
			Debug.DrawLine (currentTarget - Vector2.right * 0.1f, currentTarget + Vector2.right * 0.1f, Color.blue);
		}
	}

	bool RotateToTarget(Vector2 heading) {
			float angle = (Mathf.Atan2 (heading.y, heading.x) - Mathf.PI / 2) * Mathf.Rad2Deg;
			Quaternion qTo = Quaternion.AngleAxis (angle, Vector3.forward);
		if (Quaternion.Angle (transform.rotation, qTo) > epsilon) {
			transform.rotation = Quaternion.RotateTowards (transform.rotation, qTo,
				creature.props.Get ("rotateSpeed") * Time.deltaTime);
			creature.body.localRotation = Quaternion.RotateTowards (creature.body.localRotation, Quaternion.Inverse (transform.rotation),
				creature.props.Get ("rotateSpeed") * Time.deltaTime);
		}
		return (Quaternion.Angle(transform.rotation, qTo) < epsilon);
	}

	bool MoveToTarget() {
		Vector2 position = transform.position;
		Vector2 heading = currentTarget - position;
		Debug.DrawLine (position, (Vector2)transform.position + heading, Color.green);
		RotateToTarget (heading);
		if(col.attachedRigidbody.velocity.magnitude < creature.props.Get("moveSpeed"))
			col.attachedRigidbody.AddForce (transform.up * acceleration);
		if(animator.isInitialized)
			animator.SetFloat ("walkSpeed", col.attachedRigidbody.velocity.magnitude);
		return (heading.magnitude < epsilon);
	}


	void RandomizeTarget() {
		if (!this.overrrideRandomizer) {
			if (Random.value > breatherProb) {
				Vector2 position = (Vector2)transform.position;
				int groupFactor = _GetGroupFactor ();
				ArrayList misses = creature.sight.GetMisses ();
				if (misses.Count < 1) {
					SetTarget (-(Vector2)transform.up * minOffset);
				} else if (Vector2.Distance (position, statistics.meanPosition) > maxOffset * groupFactor) {
					SetTarget (statistics.meanPosition + Random.insideUnitCircle * minOffset * groupFactor);
				} else {
					Vector2 randomDirection = (Vector2)(Vector3)misses [Random.Range (0, misses.Count)];
					SetTarget (position + randomDirection * Random.Range (minOffset, maxOffset) * groupFactor);
				}
			} else {
				if (gameObject.activeInHierarchy)
					StartCoroutine ("Breath");
			}
		}
	}

	private IEnumerator Breath() {
		creature.movement.Pause ();
		yield return new WaitForSeconds (2);
		creature.movement.Play ();
	}

	public void AddDirectionalVector(Vector2 direction) {
		SetTarget ((Vector2)transform.position + direction, true);
	}

	public void SetTarget(Vector2 target, bool overrideRandom = false) {
		this.currentTarget = target;
		if (overrideRandom) {
			overrrideRandomizer = true;
			CancelInvoke ("RandomizeTarget");
		}
	}
		

	public void Pause() {
		this.pause = true;
	}

	public void Play() {
		this.pause = false;
	}

	public void AffectMovement(Vector2 direction) {
		this.currentTarget += direction;
		col.attachedRigidbody.AddForce (direction * 10);
	}

	private int _GetGroupFactor() {
		return 1 + statistics.count / 3;
	}

	private void _SetDirection(){
		if (col.attachedRigidbody.velocity.x > 0.05f) {
			direction = RIGHT;
		}
		if (col.attachedRigidbody.velocity.x < -0.05f) {
			direction = LEFT;
		}
		animator.SetInteger ("walkDirection", direction);
	}

}
