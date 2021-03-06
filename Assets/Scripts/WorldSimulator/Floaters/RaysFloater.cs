﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RaysFloater : Floater {

	[Range(0, 10)]
	public float hitRate;
	[Range (0, 100)]
	public int decayRate;
	[Range (0, 100)]
	public int halfLife;
	[Range(1,30)]
	public float rayLength;
	[Range(-180, 180)]
	public float rayAngle;
	[Range(1, 20)]
	public int raysDensity;
	public SpriteRenderer sprite;
	private HashSet<GameObject> hits;
	private float spriteXExtent, xStep;
	private Affector affector;

	// Use this for initialization
	protected override void Awake () {
		base.Awake ();
		affector = GetComponent<Affector> ();
		hits = new HashSet<GameObject> ();
		InvokeRepeating ("Beam", 1, hitRate);
		InvokeRepeating ("Decay", decayRate, decayRate);
	}
	
	// Update is called once per frame
	protected override void Update () {
		spriteXExtent = sprite.bounds.extents.x;
		xStep = 2f * spriteXExtent / raysDensity;
		//Debug
		for (int i = 1; i <= raysDensity; i++) {
			Vector3 direction = Quaternion.AngleAxis(rayAngle, Vector3.forward) * -transform.up;
			Vector3 position = new Vector3 (transform.position.x - spriteXExtent + i * xStep, 
				transform.position.y, transform.position.z);
			Debug.DrawRay(position, transform.up + direction * rayLength, Color.red);
		}
	}

	private void Beam() {
		for (int i = 0; i < raysDensity; i++) {
			Vector3 direction = Quaternion.AngleAxis(rayAngle, Vector3.forward) * -transform.up;
			Vector3 position = new Vector3 (transform.position.x - 1.2f * spriteXExtent + i * xStep,
				transform.position.y, transform.position.z);
			RaycastHit2D rayHit = Physics2D.Raycast (position, direction, rayLength, 1 << LayerMask.NameToLayer ("Creatures"));
			if(!RaycastHit2D.Equals (rayHit, default(RaycastHit2D))) {
				GameObject hit = rayHit.transform.gameObject;
				if (!hits.Contains (hit)) {
					affector.Affect (hit);
					hits.Add (hit);
				}
			}
			Debug.DrawRay(position, transform.up + direction * rayLength, Color.red);
		}
		hits.Clear ();
	}



	private void Decay (){
		if (--this.halfLife < 0 && !sprite.isVisible)
			Destroy (this.gameObject);
	}

}
