using UnityEngine;
using System.Collections;

public class IceDome : MonoBehaviour {
	void Start () {
		Invoke("Shatter", 7f);
	}
	
	void Shatter(){
		Destroy(gameObject);
	}
}
