using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CreatureInteraction : MonoBehaviour {

	public bool selected;
	private CreaturesController controller;
	private TextMesh stats;
	private Creature creature;

	void Start () {
		selected = false;
		creature = GetComponent <Creature> ();
		controller = GetComponentInParent <Creatures> ().controller;
		stats = creature.body.FindChild ("Stats").GetComponent<TextMesh> ();
		stats.gameObject.GetComponent <MeshRenderer> ().sortingLayerName = "Text";
	}

	void OnMouseEnter() {
		stats.text = "Health: " + creature.props.Get("health");

		foreach (Genetics.GeneType gene in Genetics.DNA_GENES){
			stats.text += ("\n" + gene.ToString () + ": " + creature.genome.genome [gene].Val);
		}
	}

	void OnMouseExit() {
		stats.text = "";
	}


	void OnMouseDown() {
	}

	void OnMouseUp(){
		if (controller.splitMode)
			controller.Split (gameObject);
		else if (controller.joinMode) {
			if (selected) {
				controller.Unjoin ();
			} else {
				controller.Join (gameObject);
			}
		}
	}
}
