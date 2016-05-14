using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class GeneSpeed : Gene
	{
		public GeneSpeed (GameObject creature) : base(creature)
		{
			this.type = Genetics.GeneType.SPEED;
		}

		override protected void onValChange(float oldVal, float newVal){
		}
	}
}

