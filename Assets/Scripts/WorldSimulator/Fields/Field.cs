using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public abstract class Field : MonoBehaviour
{

	public Collectible collectible;
	[Range (0, 100)]
	public float spawnRate;
	[Range (0, 100)]
	public int capacity;
	[Range (0, 100)]
	public int decayRate;
	[Range (0, 100)]
	public int halfLife;
	[Range (0, 100)]
	public int minHalfLifeForSpawn;
	[Range (1, 100)]
	public int halfLifeMin, halfLifeMax, valueMin, valueMax;
	public List<Collectible> crop;
	private bool spawning;
	private float spread;

	// Use this for initialization
	protected virtual void Awake () {
		spawning = false;
		Bounds bounds = GetComponent<Collider2D> ().bounds;
		spread = Mathf.Min (bounds.extents.x, bounds.extents.y);
		crop = new List<Collectible> ();
		for (int i = 0; i < capacity; i++) {
			Vector3 position = 
				new Vector3 (transform.position.x + Random.Range (-spread, spread),
					transform.position.y + Random.Range (-spread, spread), transform.position.z);	
			Collectible item = (Collectible)Instantiate (collectible, position, Quaternion.identity);
			item.Set (Random.Range (halfLifeMin, halfLifeMax), Random.Range (valueMin, valueMax));
			item.transform.SetParent (gameObject.transform);
			item.gameObject.SetActive (false);
			crop.Add (item);
		}
		InvokeRepeating ("Decay", decayRate, decayRate);
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if (halfLife > minHalfLifeForSpawn && !spawning) {
			this.spawning = true;
			InvokeRepeating ("Spawn", 0, spawnRate);
		} else if (halfLife < minHalfLifeForSpawn) {
			CancelInvoke ("Spawn");
			this.spawning = false;
		}
	}

	public void Set (float spawnRate, int capacity, int decayRate, int halfLife, int minHalfLifeForSpawn,
		int halfLifeMin, int halfLifeMax, int valueMin, int valueMax) {
		this.spawnRate = spawnRate;
		this.capacity = capacity;
		this.decayRate = 1;
		this.halfLife = halfLife;
		this.minHalfLifeForSpawn = minHalfLifeForSpawn;
		this.halfLifeMin = halfLifeMin;
		this.halfLifeMax = halfLifeMax;
		this.valueMin = valueMin;
		this.valueMax = valueMax;
	}

	protected void Decay (){
		if (--this.halfLife < 0 && !GetComponent<Renderer> ().isVisible)
			Object.Destroy (this.gameObject);
		else if(this.halfLife < minHalfLifeForSpawn)
			this.spawning = false;
	}

	protected void Spawn (){
		if (this.spawning)
			foreach (Collectible collect in this.crop) {
				if (!collect.gameObject.activeInHierarchy) {
					collect.gameObject.SetActive (true);
					collect.Set (Random.Range (halfLifeMin, halfLifeMax), Random.Range (valueMin, valueMax));
					break;
				}
			}
	}
}
