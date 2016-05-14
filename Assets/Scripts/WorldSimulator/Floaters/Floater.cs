using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public abstract class Floater : MonoBehaviour {

	[Range(0,10)]
	public float velocity;
	public Vector2 direction;
	private Rigidbody2D body;
	public List<Genetics.GeneType> propsHigh = new List<Genetics.GeneType>();
	public List<Genetics.GeneType> propsLow = new List<Genetics.GeneType>();

	// Use this for initialization
	protected virtual void Awake () {
		body = GetComponent<Rigidbody2D> ();
		body.velocity = direction * velocity;
	}

	// Update is called once per frame
	protected virtual void Update () {

	}

	public void set(float velocity, Vector2 direction) {
		this.velocity = velocity;
		this.direction = direction;
		body.velocity = direction * velocity;
	}



}
