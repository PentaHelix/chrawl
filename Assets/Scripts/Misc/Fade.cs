using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {
	private bool fading = false;
	private float intensStep = 0;
	private Light l;

	void Start(){
		l = GetComponent<Light>();
	}

	void Update () {
		if(!fading)return;
		if(l.intensity < 0)fading = false;
		l.intensity -= intensStep * Time.deltaTime;
	}

	public void StartFade(float dur){
		fading = true;
		intensStep = GetComponent<Light>().intensity / dur;
	}
}
