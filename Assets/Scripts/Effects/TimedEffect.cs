using UnityEngine;
using System.Collections;

public class TimedEffect : Effect {
		
	[Range(1, 1000)]
	public float terminationTime;

	protected override void ApplyEffect (){
		creature.props.Set(this.property, creature.props.Get (this.property) - this.value);
		Invoke ("Reverse", this.terminationTime);
	}

	private void Reverse() {
		creature.props.Set(this.property, creature.props.Get (this.property) + this.value);
		CancelInvoke ("Reverse");
		Destroy (this);
	}
}
