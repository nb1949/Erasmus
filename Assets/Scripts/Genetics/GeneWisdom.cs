using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class GeneWisdom : Gene
	{
		public GeneWisdom (GameObject creature) : base(creature)
		{
			this.type = Genetics.GeneType.WISDOM;
		}

		override protected void onValChange(float oldVal, float newVal){
		}
	}
}

