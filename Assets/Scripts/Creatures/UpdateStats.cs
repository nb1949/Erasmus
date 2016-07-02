﻿using UnityEngine;
using System.Collections;

public class UpdateStats : MonoBehaviour {

	private TextMesh stats;
	private Creature creature;

	void Start() {
		stats = GetComponent<TextMesh> ();
		creature = transform.parent.parent.gameObject.GetComponent<Creature> ();
	}
	
	// Update is called once per frame
	void Update () {
		stats.text = "Health: " + creature.props.Get ("health") +
			"\tAge: " + creature.props.Get ("age");
	}
}
