using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public static class Genetics {

	//List of all properties
	//TODO: make class Property(name, minVal, maxVal, DefaultVal).
	public static readonly string[] PROPERTIES = {"health", "weakeningRate", "moveSpeed", "rotateSpeed"};
	public static readonly string[] DNA_PROPERTIES = {"fat", "height", "eyes"};

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
	private static Random r = new Random();


	public static CreatureGenome Join (CreatureGenome parent1, CreatureGenome parent2, ref CreatureGenome child){
		JoinProperties (parent1, parent2, ref child);
		JoinDna (parent1, parent2, ref child);
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
			child.set (p, p3);
		}
	}

	/* For each quality: take the avg between parents, 
	 * make it more extreme and choose from some normal distribution
	 * around it. if it exceed max / min cut it. */
	private static void JoinDna(CreatureGenome parent1, CreatureGenome parent2, ref CreatureGenome child){

		foreach (string q in DNA_PROPERTIES) {
			float q1 = parent1.dna [q];
			float q2 = parent2.dna [q];

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

			child.setDna (q, extreme);
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
			u = 2f * Random.value - 1f;
			v = 2f * Random.value - 1f;
			S = u * u + v * v;
		}
		while (S >= 1f);

		float fac = Mathf.Sqrt(-2f * Mathf.Log(S) / S);
		return u * fac;
	}

	public static void Mutate(ref CreatureGenome genome){
		string[] dnaProps = Genetics.randomDnaProperties (Genetics.DNA_PROPS_TO_MUTATE);
		string prop;
		float origVal;
		for (int i = 0; i < dnaProps.Length; i++) {
			prop = dnaProps [i];
			origVal = genome.getDna (prop);
			float mutation = NextGaussianDouble() * Mathf.Sqrt(SPLIT_VARIANCE); 
			// Keep bounds
			mutation = Mathf.Min (MAX, mutation);	
			mutation = Mathf.Max (MIN, mutation);

			// Dont change too much :)
			if (mutation > MAX_MUTATION)
				mutation = MAX_MUTATION;
			if (mutation < -MAX_MUTATION)
				mutation = -MAX_MUTATION;

			genome.setDna (prop, origVal + mutation);
			//Debug.Log ("Mutated " + prop + " from " + origVal.ToString() + " to " + (origVal + mutation).ToString());
		}
	}



	private static string[] randomDnaProperties(int num){

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
			int index = Random.Range(0, Genetics.DNA_PROPERTIES.Length);
			if (indices.Count == 0 || !indices.Contains(index))
			{
				indices.Add(index);
			}
		}

		string[] dnaProps = new string[num];
		for (int i = 0; i < indices.Count; i++)
		{
			int randomIndex = indices[i];
			dnaProps[i] = Genetics.DNA_PROPERTIES[randomIndex];
			Debug.Log ("Mutation in " + dnaProps [i] + "!");
		}
		return dnaProps;
	}

}
