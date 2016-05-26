using System;
using UnityEngine;

public class GeneHeight : Gene
{
	public GeneHeight () : base()
	{
		this.type = Genetics.GeneType.HEIGHT;
	}

	override protected void OnValChange(float oldVal, float newVal){
	}
}

