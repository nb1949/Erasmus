using System;
using UnityEngine;
using System.Collections.Generic;

public abstract class Affector : MonoBehaviour {

	[Range(0, 100)]
	public float baseValue;
	public float currentValue;
	public bool fluctuate;
	[Range(1, 10)]
	public float valueFluctuationFreq;
	public List<Sensitivity> sensitivities;
	public string affectedProperty;
	private float flucX = 0;

	void Awake() {
		if (fluctuate)
			InvokeRepeating ("Fluctuate", valueFluctuationFreq, valueFluctuationFreq);
	}

	private void Fluctuate() {
		flucX += 0.01f;
		this.currentValue = baseValue * Mathf.Abs(Mathf.Sin(0.6f * flucX) + Mathf.Cos (0.4f * flucX)) * 0.5f;
		if (animator != null)
			animator.SetFloat ("value", this.currentValue / baseValue);
	}

	protected abstract void _Affect (GameObject creatureObj);

	public void Affect (GameObject creatureObj){
		this._Affect (creatureObj);

		Creature creature = creatureObj.GetComponent<Creature> ();
		foreach (Sensitivity sensitive in sensitivities) {
			Debug.Log ("Activating trigger: " + sensitive.to.ToString () + "Damage");
			creature.animator.SetTrigger (sensitive.to.ToString () + "Damage");
		}
	}
		

	protected int CalculateEffect(Genome creatureGenome, float value){
		int sensitivitiesNum = sensitivities.Count;
		if (sensitivitiesNum < 1) {
			Debug.LogError ("This Affector has no sensitivities! it cannot hurt anyone.");
			return 0;
		}
		float effect = 0;
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
		effect /= sensitivitiesNum + value;
		return Mathf.FloorToInt (effect);
	}
}
