using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Trap : MonoBehaviour {
	void Start () {
	
	}

	void Update () {
	
	}

	public void OnTriggerEnter(Collider col){
		if(col.transform.name == "Player"){
			Game.player.Find("Head").GetComponent<TransformScript>().Ripple(2);
			Game.player.Find("Head").GetComponent<TransformScript>().Tint(2,Color.blue);
			Debug.Log("Trapped!");
		}
	}
}
