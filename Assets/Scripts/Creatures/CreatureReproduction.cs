using UnityEngine;
using System.Collections;

public class CreatureReproduction : MonoBehaviour {

	[Range(1, 10)]
	public float repoductionPosOffset;
	private Creature creature;
	private CreaturesPool pool;

	void Start() {
		creature = GetComponent<Creature> ();
		pool = GetComponentInParent<CreaturesPool> ();
	}
		
	public void Split() {			
		GameObject copyObj = pool.Borrow ();
		copyObj.transform.position =_GetRandomPosition ();
		Creature copy = copyObj.GetComponent<Creature> ();
		copy.genome.Copy (creature.genome);
		Genetics.Mutate (creature.genome.genome);
		Genetics.Mutate (copy.GetComponent<CreatureGenome> ().genome);
	}

	public void Mate(GameObject other) {
		CreatureGenome firstGenome = creature.genome;
		CreatureGenome secondGenome = other.GetComponent <CreatureGenome> ();
		GameObject childObj = pool.Borrow ();
		childObj.transform.position =_GetRandomPosition ();
		Genetics.Join (firstGenome, secondGenome, childObj.GetComponent<CreatureGenome> ());
	}

	private Vector3 _GetRandomPosition() {
		return new Vector3 (transform.position.x + Random.Range (-repoductionPosOffset, repoductionPosOffset), 
			transform.position.y + Random.Range (-repoductionPosOffset, repoductionPosOffset),
			transform.position.z);
	}
}
