using UnityEngine;
using System.Collections;
using System;

public class CreatureMouth : MonoBehaviour {

	private Creature creature;

	// Use this for initialization
	void Awake () {
		creature = GetComponent<Creature> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Transform food = creature.sight.Seen ("Food", creature.sight.sightDistance);
		if (food != null)
			creature.movement.SetTarget ((Vector2)food.position);
	}
		
	void OnCollisionEnter2D(Collision2D other) {
		if (other.transform.CompareTag ("Food")) {
			other.gameObject.GetComponent<FoodItem> ().Consume (gameObject);
			StartCoroutine (Eating ());
			Destroy (other.gameObject);
		}
	}

	private IEnumerator Eating() {
		creature.movement.Pause ();
		yield return new WaitForSeconds (2);
		creature.movement.Play ();
	}

}
