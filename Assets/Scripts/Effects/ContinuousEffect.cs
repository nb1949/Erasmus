using UnityEngine;
using System.Collections;
using System;
using AssemblyCSharp;

public class ContinuousEffect : TimedEffect {

	[Range(1,100)]
	public float deltaTime;

	public override void Apply (){
		creature = GetComponent<Creature> ();
		InvokeRepeating ("Buff", 0, this.deltaTime);
		Invoke ("CancelBuff", this.terminationTime);
	}

	private void Buff() {
		creature.properties [this.property] += this.value;
	}

	private void CancelBuff() {
		CancelInvoke ("Buff");
		Destroy (this);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
