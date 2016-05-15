using System;
using UnityEngine;

public class GeneArmor : Gene
{
	public GeneArmor () : base()
	{
		this.type = Genetics.GeneType.ARMOR;
	}

	override protected void onValChange(float oldVal, float newVal){
	}
}


