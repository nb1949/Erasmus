using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public int minSpawnRate;
	public int maxSpawnRate;
	public float spawnOffset;
	public float avoidOffset;
	public LayerMask avoid;
	public Transform parent;
	public GameObject spawnedObject;
	public CreaturesStatistics creaturesStatistics;

	void InvokeNextSpawn() {
		Invoke ("Spawn", Random.Range (minSpawnRate, maxSpawnRate));
	}

	public void Spawn() {
		Vector2 spawnPosition = creaturesStatistics.meanPosition + 
			Random.insideUnitCircle.normalized * Camera.main.orthographicSize * 2
			+ Random.insideUnitCircle * spawnOffset;
		if (!Physics2D.OverlapCircle (spawnPosition, avoidOffset, avoid)) {
			GameObject spawned = (GameObject)GameObject.Instantiate (spawnedObject, spawnPosition, Quaternion.identity);
			spawned.transform.SetParent (parent);
		}
		InvokeNextSpawn ();
	}

	void OnDisable() {
		CancelInvoke ();
	}

	void OnEnable() {
		InvokeNextSpawn ();
	}


}
