using System;
using UnityEngine;

public class GeneFlight : Gene
{
	public GeneFlight () : base()
	{
		this.type = Genetics.GeneType.FLIGHT;
	}

	override protected void onValChange(float oldVal, float newVal){
	}
}

