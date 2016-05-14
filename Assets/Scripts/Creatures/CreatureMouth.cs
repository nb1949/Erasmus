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
		RaycastHit2D hit = sight.GetSeen ("Food", sight.sightDistance);
		if (!RaycastHit2D.Equals (hit, default(RaycastHit2D)))
			movement.setTarget ((Vector2)hit.transform.position);
	}
		
	void OnCollisionEnter2D(Collision2D other) {
		if (other.transform.CompareTag ("Food")) {
			other.gameObject.GetComponent<FoodItem> ().Consume (gameObject);
			Destroy (other.gameObject);
		}
	}
}
