using System;
using UnityEngine;

public class GeneArmor : Gene
{
	public GeneArmor () : base()
	{
		this.type = Genetics.GeneType.ARMOR;
	}

	override protected void OnValChange(float oldVal, float newVal){
		Animator animator = this.creature.GetComponentInChildren<Animator> ();
		animator.SetFloat ("armor", newVal);
	}
}


