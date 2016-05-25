using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConstantEffect : Effect {

	protected override void ApplyEffect (){
		creature.properties [this.property] -= this.value;
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}
}
