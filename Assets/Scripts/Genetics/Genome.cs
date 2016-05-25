using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Genome : SortedList<Genetics.GeneType, Gene>{

	public void SetCreature (GameObject creature){
		foreach(KeyValuePair<Genetics.GeneType, Gene> kvm in this)
			this[kvm.Key].creature = creature;
	}
}

