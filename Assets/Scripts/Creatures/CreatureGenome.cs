using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatureGenome : MonoBehaviour {

	public Genome genome;
	private Creature creature;

	void Awake(){
		creature = GetComponent<Creature> ();
		genome = Gene.InstantiateGeneList (creature);
	}

	public void Reset (){
		foreach (KeyValuePair<Genetics.GeneType, Gene> kvp in genome)
			this.genome [kvp.Key].Reset ();
	}

	public void Copy (CreatureGenome other){
		foreach (KeyValuePair<Genetics.GeneType, Gene> kvp in other.genome)
			this.genome [kvp.Key].Val = kvp.Value.Val;
	}

	public string toString(){
		string txt = "Genome:\n";
		foreach (KeyValuePair<Genetics.GeneType, Gene> kvp in this.genome) {
			txt += (kvp.Key.ToString () + ": " + kvp.Value.Val.ToString ());
		}
		return txt;
	}



}

