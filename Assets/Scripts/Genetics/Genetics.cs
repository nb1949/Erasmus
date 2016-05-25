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

	public static readonly string[] PROPERTIES = {"health", "weakeningRate", "moveSpeed", "rotateSpeed"}; //TODO: deprecate this.
	public static GeneType[] DNA_PROPERTIES = Enum.GetValues (typeof(GeneType)).Cast<GeneType> ().ToArray ();

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
	public static readonly float JOIN_VARIANCE = 0.05f;
	public static readonly float SPLIT_VARIANCE = 0.02f; //for mutation
	public static readonly int DNA_PROPS_TO_MUTATE = 2;

	public static CreatureGenome Join (CreatureGenome parent1, CreatureGenome parent2, ref CreatureGenome child){

//		Debug.Log ("Parent 1:\n" + parent1.asTxt() + "\nParent 2:\n" + parent2.asTxt());
		JoinProperties (parent1, parent2, ref child);
		JoinDna (parent1.genome, parent2.genome, ref child.genome);
		return child;
	}

	/* Properties are "regular" qualities that will be averaged. 
	 * TODO: do we really need any such qualities? I left this so not 
	 * to hurt your code.*/
	private static void JoinProperties(CreatureGenome parent1, CreatureGenome parent2, ref CreatureGenome child){
		foreach (string p in PROPERTIES) {
			
			float p3; 
			if (parent1.properties.ContainsKey (p) && parent2.properties.ContainsKey (p)) {
				p3 = (parent1.properties [p] + parent2.properties [p]) / 2;
			} else if (parent1.properties.ContainsKey (p)) {
				p3 = parent1.properties [p];
			} else if (parent2.properties.ContainsKey (p)) {
				p3 = parent2.properties [p];
			} else {
				continue;
			}
			child.setProp (p, p3);
		}
	}

	/* For each quality: take the avg between parents, 
	 * make it more extreme and choose from some normal distribution
	 * around it. if it exceed max / min cut it. */
	private static void JoinDna(Genome parent1, Genome parent2, ref Genome child){

		foreach (Genetics.GeneType q in DNA_PROPERTIES) {
			float q1 = parent1[q].Val;

			// Take avg, Enhance property
			float avg = (q1 + q1)/2;
			float extreme = avg * MORE; 

			// Take normal distibution, move around extreme.	
			float add = NextGaussianDouble() * Mathf.Sqrt(JOIN_VARIANCE); 

			// Dont change too much :)
			if (add > MAX_MUTATION)
				add = MAX_MUTATION;
			if (add < -MAX_MUTATION)
				add = -MAX_MUTATION;
			
			extreme += add;

			// Keep bounds
			extreme = Mathf.Min (MAX, extreme);	
			extreme = Mathf.Max (MIN, extreme);

			child[q].Val = extreme;
		}
	}


	// From http://stackoverflow.com/questions/5817490/implementing-box-mueller-random-number-generator-in-c-sharp
	// Std normal distribution
	// TODO: test this function.
	private static float NextGaussianDouble()
	{
		float u, v, S;

		do
		{
			u = 2f * UnityEngine.Random.value - 1f;
			v = 2f * UnityEngine.Random.value - 1f;
			S = u * u + v * v;
		}
		while (S >= 1f);

		float fac = Mathf.Sqrt(-2f * Mathf.Log(S) / S);
		return u * fac;
	}

	public static void Mutate(ref Genome genome){
		Genetics.GeneType[] dnaProps = Genetics.randomDnaProperties (Genetics.DNA_PROPS_TO_MUTATE);
		Genetics.GeneType prop;
		float origVal;
		for (int i = 0; i < dnaProps.Length; i++) {
			prop = dnaProps [i];
			origVal = genome[prop].Val;
			float mutation = NextGaussianDouble() * Mathf.Sqrt(SPLIT_VARIANCE); 
			// Keep bounds
			mutation = Mathf.Min (MAX, mutation);	
			mutation = Mathf.Max (MIN, mutation);

			// Dont change too much :)
			if (mutation > MAX_MUTATION)
				mutation = MAX_MUTATION;
			if (mutation < -MAX_MUTATION)
				mutation = -MAX_MUTATION;

			genome[prop].Val =  (origVal + mutation);
		}
	}



	private static Genetics.GeneType[] randomDnaProperties(int num){

		if (num >= Genetics.DNA_PROPERTIES.Length) {
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
			int index = UnityEngine.Random.Range(0, Genetics.DNA_PROPERTIES.Length);
			if (indices.Count == 0 || !indices.Contains(index))
			{
				indices.Add(index);
			}
		}

		Genetics.GeneType[] dnaProps = new Genetics.GeneType[num];
		for (int i = 0; i < indices.Count; i++)
		{
			int randomIndex = indices[i];
			dnaProps[i] = Genetics.DNA_PROPERTIES[randomIndex];
			Debug.Log ("Mutation in " + dnaProps [i] + "!");
		}
		return dnaProps;
	}

}
