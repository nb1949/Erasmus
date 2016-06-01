using UnityEngine;
using System.Collections;

public class CollidesWithAffector : ConditionalAffector
{
	protected override void _Affect(GameObject creature) {
		ConditionalEffect creatureEffect = creature.AddComponent<ConditionalEffect> ();
		creatureEffect.Set (this.affectedProperty, CalculateEffect (creature.GetComponent<CreatureGenome> ().genome,
			this.currentValue));
		creatureEffect.deltaTime = this.deltaTime;
		creatureEffect.terminationTime = this.terminationTime;
		creatureEffect.condition = new CollidesCondition 
			(GetComponent<Collider2D> (), creature.GetComponent<Collider2D> ());
		creatureEffect.Apply ();
	}
}

