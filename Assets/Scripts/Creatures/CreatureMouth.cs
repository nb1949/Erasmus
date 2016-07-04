using UnityEngine;
using System.Collections;
using System;

public class CreatureMouth : MonoBehaviour {

	private Creature creature;
	[Range(0,5)]
	public float eatingTime;

	// Use this for initialization
	void Awake () {
		creature = GetComponent<Creature> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Transform food = creature.sight.Seen ("Food", creature.sight.sightDistance);
		if (food != null && creature.props.Get ("health") < creature.props.health) {
			creature.movement.SetTarget ((Vector2)food.position, true);
		}
	}
		
	void OnCollisionEnter2D(Collision2D other) {
		if (other.transform.CompareTag ("Food") && creature.props.Get ("health") < creature.props.health) {
			other.gameObject.GetComponent<FoodItem> ().Consume (gameObject);
			creature.props.Set ("hunger", 0);
			StartCoroutine (Eating ());
		}
	}

	private IEnumerator Eating() {
		creature.movement.Pause ();
		yield return new WaitForSeconds (eatingTime);
		creature.movement.Play ();
	}

}
