using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class GeneSight : Gene
	{
		public GeneSight (GameObject creature) : base(creature)
		{
			this.type = Genetics.GeneType.SIGHT;
		}

		override protected void onValChange(float oldVal, float newVal){
		}
	}
}

