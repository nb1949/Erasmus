using UnityEngine;
using System.Collections;

public class CreatureReproduction : MonoBehaviour {

	[Range(1, 10)]
	public float repoductionPosOffset;
	private CreatureGenome genome;
	private CreaturesStatistics statistics;

	void Start() {
		genome = GetComponent<CreatureGenome> ();
		statistics = GetComponentInParent <CreaturesStatistics> ();
	}

	public void Reproduce() {
		if (statistics.count <= 6) {
			Vector3 position = 
				new Vector3 (transform.position.x + Random.Range (-repoductionPosOffset, repoductionPosOffset), 
					transform.position.y + Random.Range (-repoductionPosOffset, repoductionPosOffset),
					transform.position.z);
			Transform copy = (Transform)GameObject.Instantiate (this.transform, position, transform.rotation);
			copy.SetParent (this.gameObject.transform.parent);
			copy.gameObject.GetComponent<PlayerMovement> ().enabled = false;
			copy.gameObject.GetComponent<CreatureMovement> ().enabled = true;
			copy.tag = "Creature";

			CreatureGenome genome2 = copy.GetComponent <CreatureGenome> ();
			Genetics.Mutate (ref genome);
			Genetics.Mutate (ref genome2);
		}
	}

	public void Unite(GameObject parent2Creature) {
		CreatureGenome parent2 = parent2Creature.GetComponent <CreatureGenome> ();

		CreatureGenome parent1 = new CreatureGenome (this.genome);
		Genetics.Join (parent1, parent2, ref this.genome);
		Debug.Log ("1. Orig Genome: \n=====================\n" + parent1.asTxt ()); 
		Debug.Log ("2. Mate Genome: \n=====================\n" + parent2.asTxt ()); 
		Debug.Log ("3. new Genome: \n=====================\n" + genome.asTxt ()); 

		Destroy(parent1);
		Destroy (parent2Creature);
	}
}
