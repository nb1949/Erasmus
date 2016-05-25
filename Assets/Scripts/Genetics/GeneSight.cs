using System;
using UnityEngine;

public class GeneSight : Gene
{
	public GeneSight () : base()
	{
		this.type = Genetics.GeneType.SIGHT;
	}

	override protected void onValChange(float oldVal, float newVal){
		Light l = this.creature.GetComponentInChildren<Light> ();
		l.intensity = Utils.Remap (newVal, minVal, maxVal, 0f, 5f);
	}
}

