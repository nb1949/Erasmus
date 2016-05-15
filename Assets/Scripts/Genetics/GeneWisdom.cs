using System;
using UnityEngine;

public class GeneWisdom : Gene
{
	public GeneWisdom () : base()
	{
		this.type = Genetics.GeneType.WISDOM;
	}

	override protected void onValChange(float oldVal, float newVal){
	}
}

