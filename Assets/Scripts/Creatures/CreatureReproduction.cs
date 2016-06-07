using UnityEngine;
using System.Collections;

public enum ReproductionCode {OK, HEALTH, YOUNG, OLD}

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
		
	public ReproductionCode Split() {
		if (!creature.props.IsHealthy ())
			return ReproductionCode.HEALTH;
		else if (creature.props.IsYoung ())
			return ReproductionCode.YOUNG;
		else if (creature.props.IsOld ())
			return ReproductionCode.OLD;
		float currentAge = creature.props.Get ("age");
		pool.Return (gameObject);
		GameObject copyObj1 = pool.Borrow ();
		GameObject copyObj2 = pool.Borrow ();
		copyObj1.transform.position = _GetRandomPosition ();
		copyObj2.transform.position = _GetRandomPosition ();
		Creature copy1 = copyObj1.GetComponent<Creature> ();
		Creature copy2 = copyObj2.GetComponent<Creature> ();
		copy1.props.Reset (Mathf.FloorToInt(currentAge * 0.5f));
		copy2.props.Reset (Mathf.FloorToInt(currentAge * 0.5f));
		copy1.genome.Copy (creature.genome);
		copy2.genome.Copy (creature.genome);
		Genetics.Mutate (copy1.GetComponent<CreatureGenome> ().genome);
		Genetics.Mutate (copy2.GetComponent<CreatureGenome> ().genome);
		creature.events.CreatureSplit ();
		return ReproductionCode.OK;
	}

	public ReproductionCode Mate(Creature other) {
		if (!creature.props.IsHealthy () || !other.props.IsHealthy ())
			return ReproductionCode.HEALTH;
		else if (creature.props.IsYoung () || other.props.IsYoung ())
			return ReproductionCode.YOUNG;
		else if (creature.props.IsOld () || other.props.IsOld ())
			return ReproductionCode.OLD;
		CreatureGenome firstGenome = creature.genome;
		CreatureGenome secondGenome = other.GetComponent <CreatureGenome> ();
		GameObject childObj = pool.Borrow ();
		childObj.transform.position = _GetRandomPosition ();
		Genetics.Join (firstGenome, secondGenome, childObj.GetComponent<CreatureGenome> ());
		childObj.GetComponent<Creature> ().props.Reset (0);
		creature.events.CreatureJoin ();
		return ReproductionCode.OK;
	}

	private Vector3 _GetRandomPosition() {
		return new Vector3 (transform.position.x + Random.Range (-repoductionPosOffset, repoductionPosOffset), 
			transform.position.y + Random.Range (-repoductionPosOffset, repoductionPosOffset),
			transform.position.z);
	}
}
