using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class ConstantEffect : Effect {

	public override void Apply (){
		if (gameObject != null) {
			creature = GetComponent<Creature> ();
			creature.properties [this.property] += this.value;
			Object.Destroy (this);
		}
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}
}
