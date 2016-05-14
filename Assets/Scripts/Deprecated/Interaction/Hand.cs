using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour
{

	private bool longPress = false;
	private float longPressStart = -1;
	private GameObject hand;
	public GameObject handPrefab;

	// Use this for initialization
	void Awake ()
	{
		hand = Instantiate (handPrefab);
		hand.transform.SetParent (this.transform);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnMouseDrag ()
	{
		if (longPress || (Time.time - longPressStart) > 0.3f) {
			if (hand.activeSelf)
				hand.transform.position = (Vector2)Camera.main.ScreenToWorldPoint (Input.mousePosition);
			else
				OnLongPress ();
		}
	}

	void OnMouseDown ()
	{
		longPressStart = Time.time;
	}

	void OnMouseUp ()
	{
		longPress = false;
		hand.SetActive (false);
	}

	void OnLongPress ()
	{
		longPress = true;
		hand.SetActive (true);
	}
}
