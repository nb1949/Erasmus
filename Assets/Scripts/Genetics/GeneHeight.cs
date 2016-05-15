using System;
using UnityEngine;

public class GeneHeight : Gene
{
	public GeneHeight () : base()
	{
		this.type = Genetics.GeneType.HEIGHT;
	}

	override protected void onValChange(float oldVal, float newVal){
	}
}

