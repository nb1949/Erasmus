﻿using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class CreatureReproduction : MonoBehaviour {

	[Range(1, 10)]
	public float repoductionPosOffset;
	private Creature creature;
	private CreaturesStatistics statistics;

	void Start() {
		creature = GetComponent<Creature> ();
		statistics = GetComponentInParent <CreaturesStatistics> ();
	}

	//@Yoel_Todo: Please try to avoid creating\instantiating new instances when rewriting this
	// and instead do both the reproduction and the unification in-place as much as possible.
	public void Reproduce() {
		if (statistics.count <= 6) {
			Vector3 position = 
				new Vector3 (transform.position.x + Random.Range (-repoductionPosOffset, repoductionPosOffset), 
					transform.position.y + Random.Range (-repoductionPosOffset, repoductionPosOffset),
					transform.position.z);
			Transform copy = (Transform)GameObject.Instantiate (this.transform, position, transform.rotation);
			copy.SetParent (this.gameObject.transform.parent);
			Creature creature2 = copy.GetComponent <Creature> ();
			Genetics.Mutate (ref creature.genome);
			Genetics.Mutate (ref creature2.genome);
		}
	}

	public void Unite(GameObject parent2Creature) {
		Creature parent2 = parent2Creature.GetComponent <Creature> ();
		Creature parent1 =  new Creature (this.creature);
		Genetics.Join (parent1, parent2, ref this.creature);
		Debug.Log ("1. Orig Genome: \n=====================\n" + parent1.asTxt ()); 
		Debug.Log ("2. Mate Genome: \n=====================\n" + parent2.asTxt ()); 
		Debug.Log ("3. new Genome: \n=====================\n" + creature.asTxt ()); 

		Destroy(parent1);
		Destroy (parent2Creature);
	}
}
