using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Tiler))]
public class CellularAutomata : MonoBehaviour {

	public float deltaTime;
	private SmartTile[,] tileMap;

	void Awake() {
		GameObject[,] tileMapGo = GetComponent<Tiler> ().GetTileMap ();
		tileMap = new SmartTile[tileMapGo.GetLength (0), tileMapGo.GetLength (1)];
		for (int i = 0; i < tileMapGo.GetLength (0); i++)
			for (int j = 0; j < tileMapGo.GetLength (1); j++)
				tileMap [i, j] = tileMapGo [i, j].GetComponent<SmartTile> ();
	}

	void Start() {
		InvokeRepeating ("ApplyRule", deltaTime, deltaTime);
	}

	void OnDisable() {
		CancelInvoke ();
	}

	private void ApplyRule() {
		
	}
}

