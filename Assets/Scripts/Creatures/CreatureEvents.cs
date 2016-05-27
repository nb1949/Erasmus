using UnityEngine;
using System.Collections;

public class CreatureEvents : MonoBehaviour
{
	public delegate void CreatureAction(Creature creature);
	public delegate void ReproductionAction();
	public static event CreatureAction OnDeath;
	public static event ReproductionAction OnSplit;
	public static event ReproductionAction OnJoin;


	public void CreatureDied(Creature creature) {
		if (OnDeath != null)
			OnDeath (creature);
	}

	public void CreatureSplit() {
		if (OnSplit != null)
			OnSplit ();
	}

	public void CreatureJoin() {
		if (OnJoin != null)
			OnJoin ();
	}
}

