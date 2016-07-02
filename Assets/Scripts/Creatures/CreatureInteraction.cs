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
	private CreaturesSplitJoinController controller;
	private Creature creature;

	void Awake() {
		mouseDown = false;
		follow = Camera.main.GetComponent<CameraFollow> ();
		creature = GetComponent <Creature> ();

	}

	void Start () {
		selected = false;
		controller = GetComponentInParent <Creatures> ().controller;
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
