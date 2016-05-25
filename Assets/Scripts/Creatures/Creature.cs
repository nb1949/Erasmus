using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour {

	public CreatureSight sight;
	public CreatureMouth mouth;
	public CreatureInteraction interaction;
	public CreatureMovement movement;
	public CreatureReproduction reproduction;
	public CreatureGenome genome;
	public Transform body;
}
