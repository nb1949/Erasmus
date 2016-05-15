using System;
using UnityEngine;

public class GeneSight : Gene
{
	public GeneSight (GameObject creature) : base(creature)
	{
		this.type = Genetics.GeneType.SIGHT;
	}

	override protected void onValChange(float oldVal, float newVal){
	}
}

