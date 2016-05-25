using System;
using UnityEngine;

public class GeneSpeed : Gene
{
	public GeneSpeed () : base()
	{
		this.type = Genetics.GeneType.SPEED;
	}

	override protected void OnValChange(float oldVal, float newVal){
		Debug.Log ("speed changed!");
		creature.props.Set ("moveSpeed", Utils.Remap (newVal, this.minVal, this.maxVal, 0.5f, 9f));
	}
}

