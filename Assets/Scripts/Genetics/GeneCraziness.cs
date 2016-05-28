using System;
using UnityEngine;

public class GeneCraziness : Gene
{
	public GeneCraziness () : base()
	{
		this.type = Genetics.GeneType.CRAZINESS;
	}

	override protected void OnValChange(float oldVal, float newVal){
		Animator animator = this.creature.GetComponentInChildren<Animator> ();
		animator.SetFloat ("hair", newVal);
	}
}

