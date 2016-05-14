using UnityEngine;
using System.Collections;
using System;

public class CreatureMouth : MonoBehaviour {

	private CreatureSight sight;
	private CreatureMovement movement;

	// Use this for initialization
	void Start () {
		sight = GetComponent<CreatureSight> ();
		movement = GetComponent<CreatureMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
		Transform food = sight.Seen ("Food", sight.sightDistance);
		if (food != null)
			movement.SetTarget ((Vector2)food.position);
	}
		
	void OnCollisionEnter2D(Collision2D other) {
		if (other.transform.CompareTag ("Food")) {
			other.gameObject.GetComponent<FoodItem> ().Consume (gameObject);
			Destroy (other.gameObject);
		}
	}
}
