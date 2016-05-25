using System;
using UnityEngine;

public class GeneWisdom : Gene
{
	public GeneWisdom () : base()
	{
		this.type = Genetics.GeneType.WISDOM;
	}

	override protected void OnValChange(float oldVal, float newVal){
	}
}

