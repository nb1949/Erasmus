using System;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Creature : MonoBehaviour
	{

		public SortedList<string, float> properties;
		[Range(0,100)]
		public float health = 100f;
		[Range(0,10)]
		public int weakeningRate;
		[Range(0,10)]
		public float moveSpeed;
		[Range(100, 800)]
		public float rotateSpeed;

		public Genome genome;

		// C'tor
		public Creature () : base()
		{
			properties = new SortedList<string, float> (); 
		}

		// Copy c'tor
		public Creature (Creature other) : this()
		{
//			foreach (KeyValuePair<string, float> kvp in other.properties) {
//				this.properties.Add (kvp.Key, kvp.Value);
//			}
//			genome = Gene.instantiateGeneList (gameObject);
//			foreach (KeyValuePair<Genetics.GeneType, Gene> kvp in other.genome) {
//				this.genome [kvp.Key].Val = kvp.Value.Val;
//			}
		}

		void Awake(){

			Debug.Log ("in Awake");

			// Get Gene list.
			genome = Gene.instantiateGeneList (gameObject);

			//properties
			properties = new SortedList<string, float> ();
			properties.Add ("health", health);
			properties.Add ("moveSpeed", moveSpeed);
			properties.Add ("rotateSpeed", rotateSpeed);

			// Print on start
			Debug.Log(this.asTxt());
		
			// Start dying bitch
			InvokeRepeating ("Weaken", 0, weakeningRate);
		}

		// Update is called once per frame
		void Update () {
			if (properties["health"] <= 0) {
				CancelInvoke ("Weaken");
				Destroy (gameObject);
			}
			//DEBUG
			health = (int)properties ["health"];
			moveSpeed = properties ["moveSpeed"];
			rotateSpeed = properties ["rotateSpeed"];
		}

		// Not for genome props!
		public float getProp(string property) {
			return this.properties [property];
		}

		// Not for genome props!
		public void setProp(string property, float value) {
			if (!properties.ContainsKey (property))
				this.properties.Add (property, value);
			else
				this.properties [property] = value;
		}


		void Weaken ()
		{
			this.health -= 1f;
		}

		// Return pretty textual representation of creature stats & genome.
		public string asTxt(){
			string txt = "properties:\n";
			foreach (KeyValuePair<string, float> kvp in this.properties) {
				txt += (kvp.Key + ": " + kvp.Value.ToString () + "\n");
			}
			txt += "Genome:\n";
			foreach (KeyValuePair<Genetics.GeneType, Gene> kvp in this.genome) {
				txt += (kvp.Key.ToString () + ": " + kvp.Value.Val.ToString ());
			}
			return txt;

		}



	}
}

