using System;
using UnityEngine;


[System.Serializable]
	public class Sensitivity
	{
		public Genetics.GeneType to; 
		[Range(-20f,20f)]
		public float min; 
		[Range(-20f,20f)]
		public float max;
		public bool hitHigh;
	}
