using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class GeneFat : Gene
	{
		public GeneFat (GameObject creature) : base(creature)
		{
			this.type = Genetics.GeneType.FAT;
		}

		override protected void onValChange(float oldVal, float newVal){
		}
	}
}

