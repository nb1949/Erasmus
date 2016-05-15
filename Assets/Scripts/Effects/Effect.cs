using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class Effect : MonoBehaviour {

	[Range(1,100)]
	public float value;
	public string property;
	protected CreatureStats creature;	
	public delegate float EffectSensitivity(float val);
	protected Dictionary<Genetics.GeneType, EffectSensitivity> sensitivities = new Dictionary<Genetics.GeneType, EffectSensitivity>();


	public abstract void Apply ();

	public void Set(String property, float value) {
		this.property = property;
		this.value = value;
	}

	public bool setSensitivity(Genetics.GeneType prop, EffectSensitivity sensitivity){
		if (Array.IndexOf (Genetics.DNA_PROPERTIES, prop) == -1) {
			Debug.LogError ("No such property in Genetics.DNA_PROPERTIES: " + prop);
			return false;
		} else if (sensitivities.ContainsKey (prop)) {
			Debug.LogError ("This effect already contains sensitiviy for " + prop);
			return false;
		} else {
			sensitivities.Add (prop, sensitivity);
			return true;
		}
	}

	public float strongAgainstHigh(float val){
		// val is in [-1,1]
		float multiplier = val + 1f; 
		return value * multiplier;
	}

	public float strongAgainstLow(float val){
		// val is in [-1,1]
		float multiplier = -val + 1f; 
		return value * multiplier;
	}

}
