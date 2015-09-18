using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tween : MonoBehaviour{
	public static Tween instance;
	public static Dictionary<Transform, IEnumerator> rotTweens = new Dictionary<Transform, IEnumerator>();
	public static Dictionary<Transform, IEnumerator> posTweens = new Dictionary<Transform, IEnumerator>();

	void Start(){
		instance = this;
	}

	public static void TwPosition(Transform t, Vector3 v1, Vector3 v2, float dur){
		if(posTweens.ContainsKey(t)){
			instance.StopCoroutine(posTweens[t]);
			posTweens.Remove(t);
		}
		posTweens.Add(t, instance.CPosTween(t, v1, v2, dur));
		instance.StartCoroutine(posTweens[t]);
	}

	public static void TwRotation(Transform t, Vector3 v1, Vector3 v2, float dur){
		if(rotTweens.ContainsKey(t)){
			instance.StopCoroutine(rotTweens[t]);
			rotTweens.Remove(t);
		}
		rotTweens.Add(t, instance.CRotTween(t, v1, v2, dur));
		instance.StartCoroutine(rotTweens[t]);
	}

	private IEnumerator CPosTween(Transform t, Vector3 v1, Vector3 v2, float dur){
		float count = 0;
		while(count < dur){
			count += Time.deltaTime;
			t.localPosition = Vector3.Lerp(v1,v2,count/dur);
			yield return null;
		}
	}

	private IEnumerator CRotTween(Transform t, Vector3 v1, Vector3 v2, float dur){
		float count = 0;
		while(count < dur){
			count += Time.deltaTime;
			t.localEulerAngles = Vector3.Lerp(v1,v2,count/dur);
			yield return null;
		}
	}
}
