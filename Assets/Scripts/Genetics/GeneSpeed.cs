using System;
using UnityEngine;

public class GeneSpeed : Gene
{
	public GeneSpeed () : base()
	{
		this.type = Genetics.GeneType.SPEED;
	}

	override protected void onValChange(float oldVal, float newVal){
		Debug.Log ("speed changed!");
		CreatureStats stats = this.creature.GetComponent<CreatureStats> ();
		stats.setProp("moveSpeed", Utils.Remap (newVal, this.minVal, this.maxVal, 0.5f, 9f));
	}
}

