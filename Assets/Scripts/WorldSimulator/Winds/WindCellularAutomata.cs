using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Tiler))]
public class WindCellularAutomata : MonoBehaviour {

	public float deltaTime;
	public float forceMinValue, forceMaxValue, maxDeltaForce, burstForce;
	private WindTile[,] tileMap;
	private bool started = false;

	void Start() {
		if(!started) {
			GameObject[,] tileMapGo = GetComponent<Tiler> ().GetTileMap ();
			tileMap = new WindTile[tileMapGo.GetLength (0), tileMapGo.GetLength (1)];
			for (int i = 0; i < tileMapGo.GetLength (0); i++)
				for (int j = 0; j < tileMapGo.GetLength (1); j++) {
					tileMap [i, j] = tileMapGo [i, j].GetComponent<WindTile> ();
					tileMap [i, j].af.forceMagnitude = forceMinValue;
				}
			GenerateDirections ();
			InvokeRepeating ("ApplyRule", 0, deltaTime);
			started = true;
		}
	}

	void OnEnable() {
		InvokeRepeating ("ApplyRule", 0, deltaTime);
	}
		
	void OnDisable() {
		CancelInvoke ();
	}

	private void GenerateDirections() {
		tileMap [0, 0].af.forceAngle = Random.Range (0, 359);
		for (int i = 0; i < tileMap.GetLength (0); i++)
			for (int j = 1; j < tileMap.GetLength (1); j++) {
				int offset = (i > 0) ? Mathf.RoundToInt (Random.value) : 0;
				tileMap [i, j].af.forceAngle = (tileMap [i - offset, j - 1].af.forceAngle + Random.Range(-30, 30)) % 360;
			}
	}

	private void ApplyRule() {
		//Step 1 - Add Noise
		for (int i = 0; i < tileMap.GetLength (0); i++)
			for (int j = 0; j < tileMap.GetLength (1); j++) {
				tileMap [i, j].af.forceMagnitude += Random.Range (0, maxDeltaForce);
				tileMap [i, j].af.forceAngle += Random.Range (-10, 10);
			}
		//Step 2 - Update Neighbors
		for (int i = 0; i < tileMap.GetLength (0); i++)
			for (int j = 0; j < tileMap.GetLength (1); j++) {
				tileMap [i, j].tempAngle = (GetAveraged8Angle (i,j) + Random.Range (-5, 5));
				if (tileMap [i, j].af.forceMagnitude > forceMaxValue) {
					tileMap [i, j].tempMagnitude = tileMap [i, j].tempMagnitude - forceMaxValue;
					Update8NeighborsForce (i, j);
				} else {
					tileMap [i, j].tempMagnitude = tileMap [i, j].af.forceMagnitude;
				}
			}
		//Step 3 - Update True Value
		for (int i = 0; i < tileMap.GetLength (0); i++)
			for (int j = 0; j < tileMap.GetLength (1); j++) {
				tileMap [i, j].af.forceMagnitude = tileMap [i, j].tempMagnitude;
				tileMap [i, j].af.forceAngle = tileMap [i, j].tempAngle  % 360;
			}
	}

	private float GetAveraged8Angle(int i, int j) {
		if(i == 0 || i == tileMap.GetLength (0) - 1 || j == 0 || j == tileMap.GetLength (1) - 1)
			return tileMap [i, j].af.forceAngle;
		return (tileMap [i - 1, j - 1].af.forceAngle +
		tileMap [i - 1, j].af.forceAngle +
		tileMap [i - 1, j + 1].af.forceAngle +
		tileMap [i, j - 1].af.forceAngle +
		tileMap [i, j + 1].af.forceAngle +
		tileMap [i + 1, j - 1].af.forceAngle +
		tileMap [i + 1, j].af.forceAngle +
		tileMap [i + 1, j + 1].af.forceAngle) / 8;
	}

	private void Update8NeighborsForce(int i, int j) {
		for (int l = Mathf.Max (i - 1, 0); l <= Mathf.Min (tileMap.GetLength (0) - 1, i + 1); l++)
			for (int k = Mathf.Max (j - 1, 0); k <= Mathf.Min (tileMap.GetLength (1) - 1, j + 1); k++) {
				if (l != i || k != j) {
					tileMap [l,k].tempMagnitude += burstForce;
				}
			}
	}
}

