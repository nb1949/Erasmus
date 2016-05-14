﻿using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class GeneFlight : Gene
	{
		public GeneFlight (GameObject creature) : base(creature)
		{
			this.type = Genetics.GeneType.FLIGHT;
		}

		override protected void onValChange(float oldVal, float newVal){
		}
	}
}

