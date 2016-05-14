﻿using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class TimedEffect : Effect {

	[Range(1, 1000)]
	public float terminationTime;

	public override void Apply (){
		creature = GetComponent<Creature> ();
		creature.properties [this.property] += this.value;
		Invoke ("Reverse", this.terminationTime);
	}

	private void Reverse() {
		creature.properties [this.property] -= this.value;
		CancelInvoke ("Reverse");
		Destroy (this);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
