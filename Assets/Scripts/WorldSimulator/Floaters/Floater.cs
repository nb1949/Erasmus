using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public abstract class Floater : MonoBehaviour {

	public List<string> propsHigh = new List<string>();
	public List<string> propsLow = new List<string>();

	// Use this for initialization
	protected virtual void Awake () {
	
	}

	// Update is called once per frame
	protected virtual void Update () {

	}
}
