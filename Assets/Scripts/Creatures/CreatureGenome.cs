using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatureGenome : MonoBehaviour
{

	public SortedList<string, float> properties;
	public Genome genome;
	private CreaturesPool pool;

	[Range(0,100)]
	public float health;
	[Range(0,10)]
	public float weakeningRate;
	[Range(0,10)]
	public float moveSpeed;
	[Range(100, 800)]
	public float rotateSpeed;


	// C'tor
	public CreatureGenome () : base() {
		properties = new SortedList<string, float> (); 
	}

	// Copy c'tor
	public CreatureGenome (CreatureGenome other, GameObject myGameObj) : this() {
		foreach (KeyValuePair<string, float> kvp in other.properties) {
			this.properties.Add (kvp.Key, kvp.Value);
		}
		genome = Gene.instantiateGeneList (myGameObj);
		genome.SetCreature (myGameObj);
		foreach (KeyValuePair<Genetics.GeneType, Gene> kvp in other.genome) {
			this.genome [kvp.Key].Val = kvp.Value.Val;
		}
	}

	void Start(){
		pool = transform.parent.GetComponent<CreaturesPool> ();
		genome = Gene.instantiateGeneList (gameObject);
		//properties
		properties = new SortedList<string, float> ();
		properties.Add ("health", health);
		properties.Add ("moveSpeed", moveSpeed);
		properties.Add ("rotateSpeed", rotateSpeed);

		InvokeRepeating ("Weaken", 0, weakeningRate);
	}

	// Update is called once per frame
	void Update () {
		if (properties["health"] <= 0) {
			CancelInvoke ("Weaken");
			pool.Return (gameObject);
		}
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


	void Weaken (){
		properties ["health"] -= 1f;
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

