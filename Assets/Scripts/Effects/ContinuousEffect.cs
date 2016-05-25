using UnityEngine;
using System.Collections;
using System;


/*
 * This will keep hurting target until
 * canceled.
 */
public class ContinuousEffect : TimedEffect {

	[Range(1,100)]
	public float deltaTime;

	protected override void ApplyEffect (){
		InvokeRepeating ("SingleApply", 0, this.deltaTime);
		Invoke ("Cancel", this.terminationTime);
	}

	private void SingleApply() {
		creature.props.Set(this.property, creature.props.Get (this.property) - this.value);
	}

	private void Cancel() {
		CancelInvoke ("SingleApply");
		Destroy (this);
	}
}
