using UnityEngine;
using System.Collections;

public class Smooth_Hands : MonoBehaviour {
	private Vector3 preRot;
	private Vector3 curRot;
	private Transform hands;
	[Range(0,1)]
	public float Amount = 0.2f;
	[Range(0,1)]
	public float Speed = 0.2f;
	void Start () {
		curRot = transform.forward.normalized;
		hands = transform.Find("Hands");
	}
	
	void Update () {
		preRot = curRot;
		curRot = transform.forward.normalized;
		Vector3 crossV = Vector3.Cross(preRot, curRot);
		Vector2 transV = new Vector2(-crossV.y, crossV.x);
		if(transform.eulerAngles.y > 90 && transform.eulerAngles.y < 270)transV.y *= -1;
		float angle = Vector3.Angle(preRot, curRot)/180;
		Vector3 finalV = new Vector3(transV.x, transV.y, 0).normalized * angle * Amount;
		hands.localPosition += finalV - hands.localPosition * Speed;
	}
}
