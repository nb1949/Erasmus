using UnityEngine;
using System.Collections;

public class CreatureReproduction : MonoBehaviour {

	[Range(1, 10)]
	public float repoductionPosOffset;
	private Creature creature;
	private CreaturesPool pool;

	void Awake() {
		creature = GetComponent<Creature> ();
	}

	void Start() {
		pool = GetComponentInParent<CreaturesPool> ();
	}
		
	public void Split() {
		pool.Return (gameObject);
		GameObject copyObj1 = pool.Borrow ();
		GameObject copyObj2 = pool.Borrow ();
		copyObj1.transform.position =_GetRandomPosition ();
		copyObj2.transform.position =_GetRandomPosition ();
		Creature copy1 = copyObj1.GetComponent<Creature> ();
		Creature copy2 = copyObj2.GetComponent<Creature> ();
		copy1.genome.Copy (creature.genome);
		copy2.genome.Copy (creature.genome);
		Genetics.Mutate (copy1.GetComponent<CreatureGenome> ().genome);
		Genetics.Mutate (copy2.GetComponent<CreatureGenome> ().genome);
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
