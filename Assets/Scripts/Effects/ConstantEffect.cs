using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConstantEffect : Effect {

	public override void Apply (){
		genome = GetComponent<CreatureGenome> ();
		string prop;
		float val;
		float finalValue = this.value;
		int sensNum = 0;
		float ttlSensitiveValues = 0f;
		foreach (KeyValuePair<string, float> propVal in genome.dna) {
			prop = propVal.Key;
			val = propVal.Value;
			if (sensitivities.ContainsKey (prop)) {
				float currVal = sensitivities [prop] (val);
				ttlSensitiveValues += currVal;
				sensNum++;
				Debug.Log ("Sensitive to " + prop + "!");
			}
		}
		//give 80% weight to sensitive values avg.
		if (sensNum != 0)
			finalValue = Mathf.CeilToInt (this.value * 0.2f + (ttlSensitiveValues / sensNum) * 0.8f);
		else
			finalValue = this.value;
		Debug.Log("original value: " + this.value + ", sensitive val: " + (ttlSensitiveValues / sensNum).ToString() + "final value: " + finalValue );
		genome.properties [this.property] += finalValue;
		Object.Destroy (this);
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}
}
