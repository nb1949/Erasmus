using UnityEngine;
using System.Collections;
using System;

public class ContinuousEffect : TimedEffect {

	[Range(1,100)]
	public float deltaTime;

	public override void Apply (){
		genome = GetComponent<CreatureGenome> ();
		InvokeRepeating ("Buff", 0, this.deltaTime);
		Invoke ("CancelBuff", this.terminationTime);
	}

	private void Buff() {
		genome.properties [this.property] += this.value;
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
