using System;
using UnityEngine;
using System.Collections.Generic;

public class CreatureGraphics : MonoBehaviour
{

	//for image changes
	private GameObject bodySprite;
	private GameObject eyesSprite;
	private GameObject leftLegsSprite;
	private GameObject rightLegsSprite;

	public List<Sprite> bodySprites;
	public List<Sprite> eyesSprites;
	public List<Sprite> leftLegsSprites;
	public List<Sprite> rightLegsSprites;

	void Awake(){
		// For quick sprite changing
		bodySprite = GameObject.Find ("/BodySprite");
		eyesSprite = GameObject.Find ("/EyesSprite");
		leftLegsSprite = GameObject.Find ("/LegsLeftSprite");
		rightLegsSprite = GameObject.Find ("/LegsRightSprite");
	}

	public void eyesSmall(){
		eyesSprite.GetComponent<SpriteRenderer> ().sprite = eyesSprites [0];
	}
	public void eyesBig(){
		eyesSprite.GetComponent<SpriteRenderer> ().sprite = eyesSprites [1];
	}


}

