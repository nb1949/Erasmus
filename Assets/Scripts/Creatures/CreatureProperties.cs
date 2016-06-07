using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatureProperties : MonoBehaviour{

	public SortedList<string, float> properties;
	private Creature creature;
	private CreaturesPool pool;

	[Range(0,100)]
	public float health;
	public float e_life;
	[Range(0,10)]
	public float agingRate;
	[Range(0,10)]
	public float HungerRate;
	[Range(0,10)]
	public float moveSpeed;
	[Range(100, 800)]
	public float rotateSpeed;

	private bool active;

	void Awake() {
		properties = new SortedList<string, float> ();
		creature = GetComponent<Creature> ();
		Reset ();
	}

	// Use this for initialization
	void Start () {
		Reset ();
		pool = GetComponentInParent<CreaturesPool> ();
		InvokeRepeating ("Age", 0, agingRate);
		InvokeRepeating ("Hunger", 0, HungerRate);
	}
	
	// Update is called once per frame
	void Update (){
		if (active) {
			if (properties ["age"] > e_life || properties ["health"] <= 0) {
					creature.events.CreatureDied (creature);
				CancelInvoke ("Age");
				CancelInvoke ("Hunger");
				active = false;
				pool.Return (gameObject);
			}
		}
	}

	public void Reset() {
		active = true;
		Set("health", health);
		Set("age", 0f);
		Set("moveSpeed", moveSpeed);
		Set("rotateSpeed", rotateSpeed);
	}

	public void Copy(CreatureProperties other) {
		foreach (KeyValuePair<string, float> kvp in other.properties)
			this.properties.Add (kvp.Key, kvp.Value);
	}

	public void Set(string property, float value) {
		if (!properties.ContainsKey (property))
			this.properties.Add (property, value);
		else
			this.properties [property] = value;
	}

	public float Get(string property) {
		return this.properties [property];
	}

	public bool IsHealthy() {
		return properties ["health"] > (0.85f * health);
	}

	public bool IsOld() {
		return (0.8f * e_life) < properties ["age"];
	}

	public bool IsYoung() {
		return properties ["age"] < (0.1f * e_life);
	}

	private void Age (){
		properties ["age"] += 1f;
	}

	private void Hunger (){
		properties ["health"] -= 1f;
	}

	public string toString() {
		string txt = "Properties:\n";
		foreach (KeyValuePair<string, float> kvp in this.properties) 
			txt += (kvp.Key + ": " + kvp.Value.ToString () + "\n");
		return txt;
	}
}