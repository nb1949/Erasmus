using System;
using UnityEngine;

public class GeneSight : Gene
{
	public GeneSight () : base()
	{
		this.type = Genetics.GeneType.SIGHT;
	}

	override protected void OnValChange(float oldVal, float newVal){
		Light selfLight = this.creature.body.GetComponentInChildren<Light> ();
		selfLight.intensity = Utils.Remap (newVal, minVal, maxVal, 0f, 3f);

		Animator animator = this.creature.GetComponentInChildren<Animator> ();
		animator.SetFloat ("sight", newVal);
	}
}

