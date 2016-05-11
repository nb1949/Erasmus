using UnityEngine;
using System.Collections;

public class DropsFloater : Floater {

	public float velocity;
	public Vector2 direction;
	private Rigidbody2D body;
	public Drop drop;
	[Range(0,10)]
	public float spawnRate;

	// Use this for initialization
	protected override void Awake () {
		base.Awake ();
		body = GetComponent<Rigidbody2D> ();
		body.velocity = direction * velocity;
		InvokeRepeating ("Spawn", 0, spawnRate);
	}
	
	// Update is called once per frame
	protected override void Update () {
	
	}

	private void Spawn() {
		Vector3 position = 
			new Vector3 (transform.position.x + direction.x * Random.Range (-4, 4),
				transform.position.y - 1 + direction.y * Random.Range (-4, 4), transform.position.z);	
		Drop item = (Drop)Instantiate (drop, position, Quaternion.identity);
		item.Set (velocity/2.0f, 10);
		item.transform.SetParent (gameObject.transform);
	}
}
