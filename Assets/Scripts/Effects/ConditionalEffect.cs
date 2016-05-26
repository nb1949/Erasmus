using UnityEngine;
using System.Collections;


/*
 * This will keep hurting target until
 * condition no longe applies.
 */
public class ConditionalEffect : ContinuousEffect {

	public Condition condition;

	protected override void ApplyEffect (){
		InvokeRepeating ("singleApply", 0, this.deltaTime);
	}

	private void singleApply() {
		if (condition.Evaluate ())
			creature.props.Set(this.property, creature.props.Get (this.property) - this.value);
		else {
			CancelInvoke ();
			Destroy (condition);
			Destroy (this);
		}
	}

}
