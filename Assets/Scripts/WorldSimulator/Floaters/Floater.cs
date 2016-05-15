using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public abstract class Floater : MonoBehaviour {

	public List<Genetics.GeneType> propsHigh = new List<Genetics.GeneType>();
	public List<Genetics.GeneType> propsLow = new List<Genetics.GeneType>();

	// Use this for initialization
	protected virtual void Awake () {
	
	}

	// Update is called once per frame
	protected virtual void Update () {

	}
}
