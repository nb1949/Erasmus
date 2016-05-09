using UnityEngine;
using System.Collections;
using System;

public class FoodItem : Collectible {

	public String property;

	public override void Consume (GameObject collector) {
		ConstantEffect effect = collector.AddComponent<ConstantEffect> ();
		effect.Set (property, value);
		effect.Apply ();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
