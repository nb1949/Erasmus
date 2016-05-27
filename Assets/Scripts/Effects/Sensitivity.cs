using System;
using UnityEngine;


[System.Serializable]
public class Sensitivity
{
	public Genetics.GeneType to; 
	[Range(-5f,5f)]
	public float min; 
	[Range(-10f,10f)]
	public float max;
	public bool hitHigh;
}
