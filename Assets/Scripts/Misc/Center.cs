using UnityEngine;
using System.Collections;

public class Center : MonoBehaviour {
	[Range(0, 10)]
	public float speed = .4f;
	void Start(){}
	void Update () {
		if(transform.localPosition != Vector3.zero)transform.localPosition += -transform.localPosition * speed;
		if(transform.localPosition.magnitude < speed / 10f)transform.localPosition = Vector3.zero;
	}
}
