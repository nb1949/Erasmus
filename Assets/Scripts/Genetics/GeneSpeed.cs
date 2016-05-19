using System;
using UnityEngine;

public class GeneSpeed : Gene
{
	public GeneSpeed () : base()
	{
		this.type = Genetics.GeneType.SPEED;
	}

	override protected void onValChange(float oldVal, float newVal){
//		CreatureStats stats = this.creature.GetComponent<CreatureStats> ();
//		stats.moveSpeed = Utils.Remap (newVal, this.minVal, this.maxVal, 0.5f, 9f);
	}
}

