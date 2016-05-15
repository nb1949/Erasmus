using System;
using UnityEngine;

public class GeneSpeed : Gene
{
	public GeneSpeed () : base()
	{
		this.type = Genetics.GeneType.SPEED;
	}

	override protected void onValChange(float oldVal, float newVal){
	}
}

