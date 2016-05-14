using System;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace AssemblyCSharp
{	

	public class Genome : SortedList<Genetics.GeneType, Gene>{}

	public abstract class Gene
	{
		public Genetics.GeneType type;

		// Defaults. can be overriden in inheriting gene classes' c'tors.
		protected float minVal = -1f;
		protected float maxVal = 1f;
		protected float defaultVal = 0f;
		private float val;
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

		protected GameObject creature;

		// Each Gene must know who it belongs to, so it can affect it onChange
		public Gene (GameObject creature)
		{
			this.creature = creature;
			this.reset ();
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
			object[] args = { creature };
			foreach (Type gene in 
				Assembly.GetAssembly(typeof(Gene)).GetTypes()
				.Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Gene))))
			{
				Gene curr = (Gene)Activator.CreateInstance(gene, args);
				Debug.Log ("Adding " + curr.type.ToString () + " To genome"); 
				if (!genes.ContainsKey (curr.type))
					genes.Add (curr.type, curr);
				else
					genes [curr.type] = curr;
			}
			return genes;
		}

		public static Gene createGene(Genetics.GeneType type, GameObject creature){
			switch (type) {
			case Genetics.GeneType.FAT:
				return new GeneFat (creature);
			case Genetics.GeneType.HEIGHT:
				return new GeneHeight (creature);
			case Genetics.GeneType.SIGHT:
				return new GeneSight (creature);
			case Genetics.GeneType.FLIGHT:
				return new GeneFlight (creature);
			case Genetics.GeneType.WISDOM:
				return new GeneWisdom (creature);
			case Genetics.GeneType.ARMOR:
				return new GeneArmor (creature);
			case Genetics.GeneType.SPEED:
				return new GeneSpeed (creature);
			case Genetics.GeneType.CRAZINESS:
				return new GeneCraziness (creature);
			default:
				Debug.LogError ("GENE " + type.ToString() + " NOT IN SWITCH CASE");
				return null;
			}

		}
	}
}

