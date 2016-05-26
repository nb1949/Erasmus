using System;
using UnityEngine;
using System.Collections.Generic;


public enum AffectorType
{
	WHILE_IN_RANGE,
	SINGLE_HIT

}

	public class Affector : MonoBehaviour
	{


	// Defaults. can be changed.
	public List<Sensitivity> sensitivities;
	public AffectorType type = AffectorType.SINGLE_HIT;


	public void Affect (GameObject creature){

		Genome creatureGenome = creature.GetComponent<CreatureGenome> ().genome;
		float damage = CalculateDamage (creatureGenome);
		Effect creatureEffect;
		switch (type) {
		case AffectorType.WHILE_IN_RANGE:
			creatureEffect = creature.AddComponent<ConditionalEffect> ();
			CollidesCondition collidesCond = creature.AddComponent<CollidesCondition> ();
			collidesCond.one = GetComponent<Collider2D> ();
			collidesCond.other = creature.GetComponent<Collider2D> ();
			((ConditionalEffect)creatureEffect).condition = collidesCond;
			((ConditionalEffect)creatureEffect).deltaTime = 4;
			((ConditionalEffect)creatureEffect).terminationTime = 10;
			break;

		case AffectorType.SINGLE_HIT:
			creatureEffect = creature.AddComponent<ConstantEffect> ();
			break;
		
		default:
			Debug.LogError ("You did not specifiy which type of effect you want. Affector will use ConstantEffect as default.");
			creatureEffect = creature.AddComponent<ConstantEffect> ();
			break;
		}

		creatureEffect.Set ("health", damage);
		creatureEffect.Apply ();
	}

	private float CalculateDamage( Genome creatureGenome){
		int sensitivitiesNum = sensitivities.Count;
		if (sensitivitiesNum < 1) {
			Debug.LogError ("This Affector has no sensitivities! it cannot hurt anyone.");
			return 0f;
		}
		float val = 0f;
		foreach (Sensitivity s in sensitivities) {
			Genetics.GeneType gene = s.to;
			Gene currGene = creatureGenome [gene];
			float creatureVal;
			if (s.hitHigh) {
				creatureVal = currGene.Val;
			} else {
				creatureVal = currGene.maxVal - currGene.Val;
			}
			float calculatedVal = Utils.Remap (creatureVal, currGene.minVal, currGene.maxVal, s.min, s.max);
			val += calculatedVal;
		}
		val /= sensitivitiesNum;
		return val;
	}

	}


