	using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;


public static class Genetics {

	public enum GeneType
	{
		FAT
		,HEIGHT
		,SIGHT
		,FLIGHT
		,WISDOM
		,ARMOR
		,SPEED
		,CRAZINESS
		,LIFE
	}

	public static GeneType[] DNA_GENES = Enum.GetValues (typeof(GeneType)).Cast<GeneType> ().ToArray ();

	/* MORE is used to enhance propety when joining Genomes
	 * Since all DNA attributes are in [-1,1], multiplying any value my MORE 
	 * Enhances it towards some extreme.
	 * TODO: this creates a situtaion where 09. & 0.9 
	 * get "powered up" more in practice. stretch it more evenely */
	public static readonly float MORE = 1.25f;
	public static readonly float MAX = 1f;
	public static readonly float MIN = -1f;
	public static readonly float MAX_MUTATION = 0.15f;
	public static readonly float MAX_TTL_ABS = 2f;
	public static readonly float JOIN_VARIANCE = 0.01f;
	public static readonly float SPLIT_VARIANCE = 0.03f; //for mutation
	public static readonly int DNA_PROPS_TO_MUTATE = 2;


	public static void Mutate(Genome genome){
		Genetics.GeneType[] dnaProps = Genetics._RandomDnaProperties (Genetics.DNA_PROPS_TO_MUTATE);
		Genetics.GeneType prop;
		float origVal;
		for (int i = 0; i < dnaProps.Length; i++) {
			prop = dnaProps [i];
			origVal = genome[prop].Val;
			float mutation = _NextGaussianDouble() * Mathf.Sqrt(SPLIT_VARIANCE); 
			// Keep bounds 
			mutation = Mathf.Clamp(mutation, MIN, MAX);	

			// Dont change too much :)
			if (mutation > MAX_MUTATION)
				mutation = MAX_MUTATION;
			if (mutation < -MAX_MUTATION)
				mutation = -MAX_MUTATION;

			genome[prop].Val =  (origVal + mutation);
		}
	}

	public static CreatureGenome Join (CreatureGenome parent1, CreatureGenome parent2, CreatureGenome child){

		_JoinDna (parent1.genome, parent2.genome, child.genome);
		return child;
	}

	/* For each quality: take the avg between parents, 
	 * make it more extreme and choose from some normal distribution
	 * around it. if it exceed max / min cut it. */
	private static void _JoinDna(Genome parent1, Genome parent2, Genome child){

		foreach (Genetics.GeneType g in DNA_GENES) {
			float g1 = parent1[g].Val;
			float g2 = parent2[g].Val;

			// Take avg, Enhance property
			float avg = (g1 + g2)/2;
			float extreme = avg * MORE; 

			// Take normal distibution, move around extreme.	
			float add = _NextGaussianDouble() * Mathf.Sqrt(JOIN_VARIANCE); 

			// Dont change too much :)
			if (add > MAX_MUTATION)
				add = MAX_MUTATION;
			if (add < -MAX_MUTATION)
				add = -MAX_MUTATION;
			
			extreme += add;

			// Keep bounds
			extreme = Mathf.Clamp(extreme, MIN, MAX);	

			child[g].Val = extreme;
		}
	}


	// From http://stackoverflow.com/questions/5817490/implementing-box-mueller-random-number-generator-in-c-sharp
	// Std normal distribution
	private static float _NextGaussianDouble()
	{
		float u, v, S;

		do{
			u = 2f * UnityEngine.Random.value - 1f;
			v = 2f * UnityEngine.Random.value - 1f;
			S = u * u + v * v;
		}
		while (S >= 1f);

		float fac = Mathf.Sqrt(-2f * Mathf.Log(S) / S);
		return u * fac;
	}
		

	private static Genetics.GeneType[] _RandomDnaProperties(int num){

		if (num >= Genetics.DNA_GENES.Length) {
			Debug.LogError ("asked for too many dna properties");
		} else if (num < 0) {
			Debug.LogError ("asked for negative #dna props");
		}

		List<int> indices = new List<int>();
		int iterations = 0;
		while (indices.Count < num)
		{
			iterations++;
			if (iterations > 100) {
				Debug.LogError ("OVER 100 ITERATIONS! ABORT");
				break;
			}
			int index = UnityEngine.Random.Range(0, Genetics.DNA_GENES.Length);
			if (indices.Count == 0 || !indices.Contains(index))
			{
				indices.Add(index);
			}
		}

		Genetics.GeneType[] dnaProps = new Genetics.GeneType[num];
		for (int i = 0; i < indices.Count; i++)
		{
			int randomIndex = indices[i];
			dnaProps[i] = Genetics.DNA_GENES[randomIndex];
			Debug.Log ("Mutation in " + dnaProps [i] + "!");
		}
		return dnaProps;
	}

}
