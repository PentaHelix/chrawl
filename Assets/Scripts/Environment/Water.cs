using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {
	void Start () {
	
	}

	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if(col.transform.name != "Player")return;
	}
}
