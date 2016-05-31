using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectPool : MonoBehaviour{

	public GameObject pooledObjectPrefab;
	public int capacity;
	public bool increase;
	public int startingNum;
	public int activeCount;

	public List<GameObject> pool;

	void Awake() {
		pool = new List<GameObject> ();
		for (int i = 0; i < capacity; i++) {
			GameObject obj = (GameObject)Instantiate (pooledObjectPrefab, 
				transform.position + (Vector3)(Random.insideUnitCircle * startingNum), Quaternion.identity);
			obj.name = pooledObjectPrefab.name + i;
			obj.transform.SetParent (transform);
			obj.SetActive (false);
			pool.Add (obj);
		}
		activeCount = 0;
	}

	// Use this for initialization
	void Start () {
		for (int i = 0; i < startingNum; i++) {
			GameObject obj = this.Borrow ();
			obj.SetActive (true);
		}
	}

	public GameObject Borrow() {
		foreach (GameObject obj in pool) {
			if (!obj.activeInHierarchy) {
				obj.SetActive (true);
				activeCount++;
				return obj;
			}
		}

		if(increase) {
			GameObject obj = (GameObject)Instantiate(pooledObjectPrefab);
			obj.name = pooledObjectPrefab.name + capacity;
			obj.transform.SetParent (transform);
			pool.Add (obj);
			obj.SetActive (true);
			activeCount++;
			return pool[capacity++];
		}
		return null;
	}

	public void Return(GameObject obj) {
		obj.SetActive (false);
		activeCount--;
	}
}

