using System;
using UnityEngine;

public class GeneFat : Gene
{
	public GeneFat () : base()
	{
		this.type = Genetics.GeneType.FAT;
	}

	override protected void OnValChange(float oldVal, float newVal){
		CircleCollider2D col = creature.gameObject.GetComponent<CircleCollider2D> ();
		float diff = newVal - oldVal;
		col.attachedRigidbody.mass += diff;
		col.radius += diff / 2;
		creature.body.localScale *= (1 + diff); 

	}
}

