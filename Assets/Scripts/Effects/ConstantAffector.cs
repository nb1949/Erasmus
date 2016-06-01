using UnityEngine;
using System.Collections;

public class ConstantAffector : Affector {

	protected override void _Affect(GameObject creature) {
		Genome creatureGenome = creature.GetComponent<CreatureGenome> ().genome;
		float effect = CalculateEffect (creatureGenome, this.currentValue);
		if (effect != 0) {
			Effect creatureEffect = creature.AddComponent<ConstantEffect> ();
			creatureEffect.Set (this.affectedProperty, effect);
			creatureEffect.Apply ();
		}
	}
}
