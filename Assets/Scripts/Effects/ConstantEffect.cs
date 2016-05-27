using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConstantEffect : Effect {

	protected override void ApplyEffect (){
		creature.props.Set(this.property, creature.props.Get (this.property) - this.value);
		Destroy (this);
	}
}
