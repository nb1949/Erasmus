using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class Effect : MonoBehaviour {

	[Range(1,100)]
	public float value;
	public string property;
	protected CreatureStats creature;	

	public void Apply (){
		if (gameObject != null) {
			creature = GetComponent<CreatureStats> ();
			this.ApplyEffect ();
		} else {
			Debug.LogError ("Trying to effect null gameobject! Did you forget to attach an effect to a creature?");
		}
	}

	protected abstract void ApplyEffect();

	public void Set(String property, float value) {
		this.property = property;
		this.value = value;
	}
		

}
