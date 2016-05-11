using UnityEngine;
using System.Collections;
using UnityEngine.Tizen;
using System.Net.Sockets;

public class WorldSimulator : MonoBehaviour {

	private CreaturesStatistics statistics;
	private Transform blocks;
	private Transform fields;
	private Transform floaters;
	private Transform creatures;
	private Transform winds;
	[Range(1, 1000), Header("Spawn offests from CoM")]
	public int fieldSpawnOffset;
	[Range(1, 1000)]
	public int floaterSpawnOffset;
	[Range(1, 1000), Header("Field Spawn Interval")]
	public int minFieldSpawnInterval;
	[Range(1, 1000)]
	public int maxFieldSpawnInterval;
	[Range(1, 1000), Header("Floater Spawn Interval")]
	public int minFloaterSpawnInterval;
	[Range(1, 1000)]
	public int maxFloaterSpawnInterval;
	[Range(1, 1000), Header("Max Spawn Instances")]
	public int maxFieldNum;
	[Range(1, 1000)]
	public int maxFloaterNum;
	[Header("Field Types")]
	public Field[] fieldPrefabs;
	[Header("Floater Types")]
	public Floater[] floaterPrefabs;

	[Range(1, 100)]
	public int spawnInvokingRate;

	[Range(1, 10), Header("Light Settings")] 
	public int dayLength;
	[Range(20, 120)]
	public int dayDeltaNum;
	private bool day;
	private float lightCount;

	// Use this for initialization
	void Awake () {
		day = true;
		lightCount = 1;
		blocks = transform.FindChild ("Blocks");
		fields = transform.FindChild ("Fields");
		floaters = transform.FindChild ("Floaters");
		creatures = transform.FindChild ("Creatures");
		winds = transform.FindChild ("Winds");
		statistics = creatures.GetComponent< CreaturesStatistics > ();
		InvokeRepeating("InvokeSpawning", spawnInvokingRate, spawnInvokingRate) ;
		InvokeRepeating ("LightCycle", 0, dayLength);
	}

	private void InvokeSpawning() {
		Invoke("SpawnField", Random.Range (minFieldSpawnInterval, maxFieldSpawnInterval));
		Invoke("SpawnFloater", Random.Range (minFloaterSpawnInterval, maxFloaterSpawnInterval));
	}

	private void SpawnField() {
		if (floaters.childCount < maxFieldNum) {
			Vector2 spawnPosition = statistics.meanPosition + Random.insideUnitCircle * fieldSpawnOffset;
			if (!Physics2D.OverlapCircle (spawnPosition, 10, LayerMask.GetMask (new string[] { "Blocks", "Fields" }))) {
				Field newField = (Field)GameObject.Instantiate (this.fieldPrefabs [Random.Range (0, this.fieldPrefabs.Length - 1)],
					                spawnPosition, Quaternion.identity);
				newField.set (Random.Range (3, 10), Random.Range (10, 20), Random.Range (3, 20), 100, 20, Random.Range (50, 75),
					Random.Range (75, 100), Random.Range (1, 5), Random.Range (5, 10));
				newField.transform.SetParent (fields);
			}
		}
	}


	private void SpawnFloater() {
		if (floaters.childCount < maxFloaterNum) {
			Vector2 spawnPosition = statistics.meanPosition + Random.insideUnitCircle * floaterSpawnOffset;
			Floater newFloater = (Floater)GameObject.Instantiate (this.floaterPrefabs [Random.Range (0, this.floaterPrefabs.Length - 1)],
				                    spawnPosition, Quaternion.identity);
			newFloater.transform.SetParent (floaters);
		}
	}

	private void LightCycle() {
		RenderSettings.ambientLight = Color.white;
		lightCount = Mathf.Floor(++lightCount % dayDeltaNum);
		if (2 * lightCount == dayDeltaNum)
			day = false;
		else if (lightCount == 0)
			day = true;
		else {
			RenderSettings.ambientIntensity = day ? (0.5f - lightCount / dayDeltaNum) : (lightCount / dayDeltaNum - 0.5f);
		}
	}
}
