using UnityEngine;
using System.Collections;

public class ConditionalEffect : ContinuousEffect {

	public Condition condition;

	public override void Apply (){
		genome = GetComponent<CreatureGenome> ();
		InvokeRepeating ("Buff", 0, this.deltaTime);
	}

	private void Buff() {
		if (condition.Evaluate ())
			genome.properties [this.property] += this.value;
		else {
			CancelInvoke ();
			Destroy (condition);
			Destroy (this);
		}
	}


}
