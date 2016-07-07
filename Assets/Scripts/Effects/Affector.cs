using System;
using UnityEngine;
using System.Collections.Generic;

public abstract class Affector : MonoBehaviour {

	[Range(0, 100)]
	public float baseValue;
	[Range(0, 1)]
	public float minPrecentageForHit;
	[Range(0, 100)]
	public float currentValue;
	public bool fluctuate;
	[Range(1, 10)]
	public float valueFluctuationFreq;
	public Animator animator;
	public List<Sensitivity> sensitivities;
	public string affectedProperty;
	private float flucX = 0;

	void Awake() {
		if (fluctuate)
			InvokeRepeating ("Fluctuate", valueFluctuationFreq, valueFluctuationFreq);
		else
			currentValue = baseValue;
	}

	private void Fluctuate() {
		flucX += 0.01f;
		this.currentValue = baseValue * Mathf.Abs(Mathf.Sin(0.6f * flucX) + Mathf.Cos (0.4f * flucX)) * 0.5f;
		if (animator != null && animator.isInitialized)
			animator.SetFloat ("value", this.currentValue / baseValue);
	}

	protected abstract void _Affect (GameObject creatureObj);


	public void Affect (GameObject creatureObj){
		if (ValidValueForHit ()) {
			this._Affect (creatureObj);
			Creature creature = creatureObj.GetComponent<Creature> ();
			foreach (Sensitivity sensitive in sensitivities) {
				float currVal = creature.genome.genome[sensitive.to].Val;
				string DamageOrProtection = "Damage";
				if ((sensitive.hitHigh && currVal < 0f) || (!sensitive.hitHigh && currVal > 0f)) {
					DamageOrProtection = "Protection";
				}
				creature.animator.SetTrigger (sensitive.to.ToString () + DamageOrProtection);
			}
		}
	}

	public bool ValidValueForHit() {
		return this.currentValue / this.baseValue > this.minPrecentageForHit;
	}
		

	protected int CalculateEffect(Genome creatureGenome, float value){
		int sensitivitiesNum = sensitivities.Count;
		float effect = 0;
		if (sensitivitiesNum > 0) {
			foreach (Sensitivity sens in sensitivities) {
				Genetics.GeneType gene = sens.to;
				Gene currGene = creatureGenome [gene];
				float creatureVal;
				if (sens.hitHigh)
					creatureVal = currGene.Val;
				else
					creatureVal = currGene.maxVal - currGene.Val;
				effect += Utils.Remap (creatureVal, currGene.minVal, currGene.maxVal, sens.min, sens.max);
			}
			effect /= sensitivitiesNum;
		}
		effect += value;
		return Mathf.RoundToInt (effect);
	}
}
