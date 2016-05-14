using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;	

public class CreatureGenome : MonoBehaviour {

	public SortedList<string, float> properties;
	[Range(0,100)]
	public int health;
	[Range(0,10)]
	public int weakeningRate;
	[Range(0,10)]
	public float moveSpeed;
	[Range(100, 800)]
	public float rotateSpeed;

	public SortedList<Genetics.GeneType, float> dna;
	[Range(-1,1)]
	public float fat;
	[Range(-1,1)]
	public float height;
	[Range(-1,1)]
	public float eyes;


	// Use this for initialization
	void Awake () {
		properties = new SortedList<string, float> ();
		dna = new SortedList<Genetics.GeneType, float> ();

		//properties
		properties.Add ("health", health);
		properties.Add ("moveSpeed", moveSpeed);
		properties.Add ("rotateSpeed", rotateSpeed);

		//DNA
		dna.Add (Genetics.GeneType.FAT, fat);
		dna.Add (Genetics.GeneType.HEIGHT, height);
		dna.Add (Genetics.GeneType.SIGHT, eyes);

		InvokeRepeating ("Weaken", 0, weakeningRate);
	}

	public CreatureGenome() : base(){ 	
		properties = new SortedList<string, float> ();
		dna = new SortedList<Genetics.GeneType, float> ();
	}

	//copy c'tor
	public CreatureGenome (CreatureGenome other) {
		properties = new SortedList<string, float> ();
		dna = new SortedList<Genetics.GeneType, float> ();

		//properties
		properties.Add ("health", other.get("health"));
		properties.Add ("moveSpeed", other.get("moveSpeed"));
		properties.Add ("rotateSpeed", other.get("rotateSpeed"));

		//DNA
		dna.Add (Genetics.GeneType.FAT, other.getDna(Genetics.GeneType.FAT));
		dna.Add (Genetics.GeneType.HEIGHT, other.getDna(Genetics.GeneType.HEIGHT));
		dna.Add (Genetics.GeneType.SIGHT, other.getDna(Genetics.GeneType.SIGHT));
	}

	public float get(string property) {
		return this.properties [property];
	}

	public void set(string property, float value) {
		if (!properties.ContainsKey (property))
			this.properties.Add (property, value);
		else
			this.properties [property] = value;
	}


	public float getDna(Genetics.GeneType property) {
		if (!Genetics.DNA_PROPERTIES.Contains (property)) {
			Debug.LogError ("requested illegal dna property " + property);
		}
		return this.dna [property];
	}

	public bool setDna(Genetics.GeneType property, float value) {
		if (!Genetics.DNA_PROPERTIES.Contains (property)) {
			Debug.LogError ("property " + property + " not in Genetics.Qualities");
			return false;
		} else if (value > Genetics.MAX || value < Genetics.MIN){
			Debug.LogError ("property " + property + " has illegal value " + value.ToString());
			return false;
		}

		this.dna [property] = value;
		return true;
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
		fat = dna [Genetics.GeneType.FAT];
		height = dna [Genetics.GeneType.HEIGHT];
		eyes = dna [Genetics.GeneType.SIGHT];
	}

	public string asTxt(){
		string res = "Creature Genome:\n----------------\n";
		res += "Properties: \n";
		foreach (KeyValuePair<string, float> kvp in properties){
			res += ("\t" + kvp.Key + ": " + kvp.Value.ToString() + "\n");
		}
		res += "Dna: \n";
		foreach (KeyValuePair<Genetics.GeneType, float> kvp in dna){
			res += ("\t" + kvp.Key.ToString() + ": " + kvp.Value.ToString() + "\n");
		}
		return res;
	}

	void Weaken ()
	{
		properties["health"] -= 1;
	}
}
