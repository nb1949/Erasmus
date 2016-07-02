using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreatureGraphicsController : MonoBehaviour {

	public Transform damageTransform;
	public GameObject damagePrefab;

	// The damage to show as a popup
	public void CreateDamagePopup(int damage){
		GameObject damageGameObject = (GameObject)Instantiate(damagePrefab,
			damageTransform.position,
			damageTransform.rotation);
		damageGameObject.GetComponent<Text> ().text = damage.ToString();
	}

	// The damage to show as a popup
	public void CreateDamagePopup(float damage){
		this.CreateDamagePopup ((int)damage);
	}
}
