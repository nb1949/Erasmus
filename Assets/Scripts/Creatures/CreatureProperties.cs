using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatureProperties : MonoBehaviour{

	public SortedList<string, float> properties;
	private CreaturesPool pool;

	[Range(0,100)]
	public float health;
	[Range(0,10)]
	public float weakeningRate;
	[Range(0,10)]
	public float moveSpeed;
	[Range(100, 800)]
	public float rotateSpeed;

	void Awake() {
		properties = new SortedList<string, float> ();
		properties.Add ("health", health);
		properties.Add ("moveSpeed", moveSpeed);
		properties.Add ("rotateSpeed", rotateSpeed);
	}

	// Use this for initialization
	void Start () {
		Reset ();
		pool = transform.parent.GetComponent<CreaturesPool> ();

		InvokeRepeating ("Weaken", 0, weakeningRate);
	}
	
	// Update is called once per frame
	void Update (){
		if (properties["health"] <= 0) {
			CancelInvoke ("Weaken");
			pool.Return (gameObject);
		}
	}

	public void Reset() {
		Set("health", health);
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

	private void Weaken (){
		properties ["health"] -= 1f;
	}

	public string toString() {
		string txt = "Properties:\n";
		foreach (KeyValuePair<string, float> kvp in this.properties) 
			txt += (kvp.Key + ": " + kvp.Value.ToString () + "\n");
		return txt;
	}
}