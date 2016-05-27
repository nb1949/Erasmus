using UnityEngine;
using System.Collections;

public class GeneLife : Gene {

	public GeneLife () : base()
	{
		this.type = Genetics.GeneType.LIFE;
	}

	override protected void OnValChange(float oldVal, float newVal){
		creature.props.e_life = Mathf.CeilToInt (Utils.Remap (newVal, minVal, maxVal, creature.props.e_life * 0.1f,
			creature.props.e_life * 2f));
	}
}
