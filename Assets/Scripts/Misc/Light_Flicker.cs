using UnityEngine;
using System.Collections;

public class Light_Flicker : MonoBehaviour {
	[Range(0,10)]
	public float baseIntensity = 1;
	[Range(0,1)]
	public float flickerIntensity = .5f;
	[Range(0,1)]
	public float flickerSpeed = .2f;
	private float baseFlicker = 1;
	private float flickerDelay = 0;

	void Start () {
		flickerDelay = flickerSpeed;
	}

	void Update () {
		flickerDelay-=Time.deltaTime;
		if(flickerDelay <= 0){
			GetComponent<Light>().intensity = baseIntensity - baseFlicker - flickerIntensity * Random.value;
			flickerDelay = flickerSpeed;
		}
	}
}
