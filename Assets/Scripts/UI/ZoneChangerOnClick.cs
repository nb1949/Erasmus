using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ZoneChangerOnClick : MonoBehaviour {

	public int zone;
	public List<GameObject> zones;
	public List<string> zoneNames;
	public List<Sprite> zoneSprites;
	public Text text;

	public void ChangeZone() {
		zones[zone].SetActive (false);
		zone = (zone + 1) % zones.Count;
		text.text = zoneNames [zone];
		GetComponent<Image> ().sprite = zoneSprites [zone];
		zones[zone].SetActive (true);
	}
}
