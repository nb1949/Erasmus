﻿using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class GeneCraziness : Gene
	{
		public GeneCraziness (GameObject creature) : base(creature)
		{
			this.type = Genetics.GeneType.CRAZINESS;
		}

		override protected void onValChange(float oldVal, float newVal){
		}
	}
}

