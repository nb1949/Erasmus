﻿using System;
using UnityEngine;

public class GeneCraziness : Gene
{
	public GeneCraziness () : base()
	{
		this.type = Genetics.GeneType.CRAZINESS;
	}

	override protected void onValChange(float oldVal, float newVal){
	}
}

