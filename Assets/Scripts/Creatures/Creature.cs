using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour {

	public CreatureSight sight;
	public 	CreatureMouth mouth;
	public CreatureInteraction interaction;
	public CreatureMovement movement;
	public CreatureReproduction reproduction;
	public CreatureStats stats;
	public CreatureGenome genome;

	void Awake() {
		sight = GetComponent<CreatureSight> ();
		mouth = GetComponent<CreatureMouth> ();
		interaction = GetComponent<CreatureInteraction> ();
		movement = GetComponent<CreatureMovement> ();
		reproduction = GetComponent<CreatureReproduction> ();
		stats = GetComponent<CreatureStats> ();
		genome = GetComponent<CreatureGenome> ();
	}


}
