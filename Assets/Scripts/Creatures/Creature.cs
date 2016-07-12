using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour {

	public CreatureSight sight;
	public CreatureMouth mouth;
	public CreatureInteraction interaction;
	public CreatureMovement movement;
	public CreatureReproduction reproduction;
	public CreatureProperties props;
	public CreatureGenome genome;
	public CreatureEvents events;
	public Animator animator;
	public AudioSource audioS;
	public Transform body;

}

