using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CreatureInteraction : MonoBehaviour {

	public bool selected;
	private CreaturesController controller;
	private TextMesh stats;
	private Creature creature;

	void Awake() {
		creature = GetComponent <Creature> ();
		stats = creature.body.FindChild ("Stats").GetComponent<TextMesh> ();
		stats.gameObject.GetComponent <MeshRenderer> ().sortingLayerName = "Text";
	}

	void Start () {
		selected = false;
		controller = GetComponentInParent <Creatures> ().controller;
	}

	void OnMouseEnter() {
		stats.text = "Health: " + creature.props.Get("health");
		stats.text += "\nAge: " + creature.props.Get("age");

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
