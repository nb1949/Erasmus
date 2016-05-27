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
}
