using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CreatureProperties : MonoBehaviour{

	public SortedList<string, float> properties;
	private Creature creature;
	private CreaturesPool pool;

	public Image healthBar;
	[Range(0,100)]
	public float health;
	public float e_life;
	[Range(0,10)]
	public float agingRate;
	[Range(0,10)]
	public float HungerRate;
	[Range(1,360)]
	public float CriticalHunger;
	[Range(0,10)]
	public float moveSpeed;
	[Range(100, 800)]
	public float rotateSpeed;

	private bool active;

	void Awake() {
		properties = new SortedList<string, float> ();
		creature = GetComponent<Creature> ();
		Reset (0);
	}

	// Use this for initialization
	void Start () {
		pool = GetComponentInParent<CreaturesPool> ();
	}
		
	
	// Update is called once per frame
	void Update (){
		if (active) {
			healthBar.transform.localScale = new Vector3 ((IsDying() ? Mathf.Min(Get("health"), 20) : Get("health")) / health,
				healthBar.transform.localScale.y, healthBar.transform.localScale.z);
			if (properties ["age"] > e_life || properties ["health"] <= 0) {
					creature.events.CreatureDied (creature);
				active = false;
				pool.Return (gameObject);
			}
		}
	}

	void OnEnable() {
		InvokeRepeating ("Age", 0, agingRate);
		InvokeRepeating ("Hunger", 0, HungerRate);
	}

	void OnDisable() {
		CancelInvoke ("Age");
		CancelInvoke ("Hunger");
	}

	public void Reset(float age) {
		active = true;
		Set("health", health);
		Set("hunger", 0);
		Set("age", age);
		Set("moveSpeed", moveSpeed);
		Set("rotateSpeed", rotateSpeed);
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
		return properties ["health"] > (0.5f * health);
	}

	public bool IsOld() {
		return (0.9f * e_life) < properties ["age"];
	}

	public bool IsYoung() {
		return properties ["age"] < (0.1f * e_life);
	}

	public bool IsDying() {
		return e_life < properties ["age"] + 10;
	}

	private void Age (){
		properties ["age"] += 1f;
	}

	private void Hunger (){
		properties ["hunger"] += 1f;
		if (properties ["hunger"] > CriticalHunger) {
			if(creature.animator.isInitialized)
				creature.animator.SetBool ("Hungry", true);
			properties ["health"] -= 1f;
		} else {
			if(creature.animator.isInitialized)
				creature.animator.SetBool ("Hungry", false);
		}
	}

	public string toString() {
		string txt = "Properties:\n";
		foreach (KeyValuePair<string, float> kvp in this.properties) 
			txt += (kvp.Key + ": " + kvp.Value.ToString () + "\n");
		return txt;
	}
}