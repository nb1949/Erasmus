using UnityEngine;
using System.Collections;

public class LightManager : MonoBehaviour
{
	[Range(1, 10), Header("Light Settings")] 
	public int dayLength;
	[Range(20, 120)]
	public int dayDeltaNum;
	public float minLight = 1f;
	private bool day;
	private float lightCount;


	// Use this for initialization
	void Start (){
		day = true;
		lightCount = 1;
		InvokeRepeating ("LightCycle", 0, dayLength);
	}
	
	private void LightCycle() {
		RenderSettings.ambientLight = Color.white;
		lightCount = Mathf.Floor (++lightCount % dayDeltaNum);
		if (2 * lightCount == dayDeltaNum)
			day = false;
		else if (lightCount == 0)
			day = true;
		else 
			RenderSettings.ambientIntensity = day ? (0.5f - lightCount / dayDeltaNum)+minLight : (lightCount / dayDeltaNum - 0.5f) + minLight;
	}
}

