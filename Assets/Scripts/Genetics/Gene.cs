using System;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public class Genome : SortedList<Genetics.GeneType, Gene>{

	public void setCreature (GameObject creature){
		foreach(KeyValuePair<Genetics.GeneType, Gene> kvp in this){
			this [kvp.Key].creature = creature;
		}
	}

}

public abstract class Gene
{
	public Genetics.GeneType type;

	// Defaults. can be overriden in inheriting gene classes' c'tors.
	public float minVal = -1f;
	public float maxVal = 1f;
	public float defaultVal = 0f;
	private float val = 0f;
	public float Val {
		get{ return val;}
		set{ 
			value = (value < minVal) ? minVal : value;
			value = (value > maxVal) ? maxVal : value;
			float oldVal = val;
			val = value;
			onValChange (oldVal, val);
		}
	}

	public GameObject creature;

	// Each Gene must know who it belongs to, so it can affect it onChange
	public Gene ()
	{
		//this.reset ();
	}

	public void reset(){
		Val = defaultVal;
	}

	//This method runs whenever value is changed (see "Val" setter func).
	protected abstract void onValChange(float oldVal, float newVal);


	// Create list of all inheriting genes
	// http://stackoverflow.com/questions/5411694/get-all-inherited-classes-of-an-abstract-class#answer-6944605
	public static Genome instantiateGeneList(GameObject creature){
		Genome genes = new Genome();
		object[] args = {};
		foreach (Type gene in 
			Assembly.GetAssembly(typeof(Gene)).GetTypes()
			.Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Gene))))
		{
			Gene curr = (Gene)Activator.CreateInstance(gene, args);
			curr.creature = creature;

			if (!genes.ContainsKey (curr.type))
				genes.Add (curr.type, curr);
			else
				genes [curr.type] = curr;
		}
		return genes;
	}

	public static Gene createGene(Genetics.GeneType type, GameObject creature){
		Gene gene;
		switch (type) {
		case Genetics.GeneType.FAT:
			gene = new GeneFat ();
			break;
		case Genetics.GeneType.HEIGHT:
			gene = new GeneHeight ();
			break;
		case Genetics.GeneType.SIGHT:
			gene = new GeneSight ();
			break;
		case Genetics.GeneType.FLIGHT:
			gene = new GeneFlight ();
			break;
		case Genetics.GeneType.WISDOM:
			gene = new GeneWisdom ();
			break;
		case Genetics.GeneType.ARMOR:
			gene = new GeneArmor ();
			break;
		case Genetics.GeneType.SPEED:
			gene = new GeneSpeed ();
			break;
		case Genetics.GeneType.CRAZINESS:
			gene = new GeneCraziness ();
			break;
		default:
			Debug.LogError ("GENE " + type.ToString() + " NOT IN SWITCH CASE");
			return null;
		}

		gene.creature = creature;
		return gene;

	}
}

