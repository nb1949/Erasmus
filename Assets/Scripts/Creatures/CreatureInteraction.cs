using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Runtime.ConstrainedExecution;

public class CreatureInteraction : MonoBehaviour {

	public bool selected {
		get{ return _selected;}
		set{ 
			_selected = value;
			creature.animator.SetBool ("Selected", _selected);
		}
	}
	private const float LONG_TIME = 0.2f;
	private bool _selected;
	private bool mouseDown;
	private float lastClickTime;
	private CameraFollow follow;
	private CreaturesController controller;
	private TextMesh stats;
	private Creature creature;

	void Awake() {
		mouseDown = false;
		follow = Camera.main.GetComponent<CameraFollow> ();
		creature = GetComponent <Creature> ();
		stats = creature.body.FindChild ("Stats").GetComponent<TextMesh> ();
		stats.gameObject.GetComponent <MeshRenderer> ().sortingLayerName = "Text";
	}

	void Start () {
		selected = false;
		controller = GetComponentInParent <Creatures> ().controller;
	}

	void OnMouseEnter() {
		if (Time.timeScale > 0) 
			stats.text = "Health: " + creature.props.Get ("health") +
				"\tAge: " + creature.props.Get ("age");

//		foreach (Genetics.GeneType gene in Genetics.DNA_GENES){
//			stats.text += ("\n" + gene.ToString () + ": " + creature.genome.genome [gene].Val);
//		}
	}

	void OnMouseExit() {
		stats.text = "";
	}

	void OnSingleClick() {
		if(controller.ModeIsOn ())
			if (follow.AmIOnZoom (transform))
				follow.ZoomOff ();
			else
				follow.ZoomOn (transform);
	}

	void OnLongClick() {
		if (mouseDown && controller.ModeIsOn () && !follow.IsZoomOn ()) {
			if (controller.splitMode)
				controller.Split (gameObject);
			else if (controller.joinMode)
				if (selected)
					controller.Unjoin ();
				else
					controller.Join (gameObject);
			follow.ZoomOff ();
		}
	}

	void OnMouseDown() {
		mouseDown = true;
		lastClickTime = Time.time;
		Invoke ("OnLongClick", LONG_TIME);
	}

	void OnMouseUp(){
		mouseDown = false;
		CancelInvoke ("OnLongClick");
		if (Time.time - lastClickTime < LONG_TIME)
			OnSingleClick ();
	}
}
