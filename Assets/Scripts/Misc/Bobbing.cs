using UnityEngine;
using System.Collections;

public class Bobbing : MonoBehaviour {
	float timer  = 0.0f;
	[Range(0,1)]
	public float Speed  = 0.18f;
	[Range(0,1)]
	public float Amount = 0.2f;
	float mid    = 1.0f;
	float wave   = 0.0f;

	void Update () {
		wave = 0;
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		if(Mathf.Abs(h) == 0 && Mathf.Abs(v) == 0){
			timer = 0;
		}else{
			wave = Mathf.Sin(timer);
			timer += Speed;
			if(timer > Mathf.PI * 2){
				timer -= Mathf.PI * 2;
			}
		}

		if(wave != 0){
			float trans = wave * Amount;
			float total = Mathf.Abs(h) + Mathf.Abs(v);
			total = Mathf.Clamp(total, 0.0f, 1.0f);
			trans *= total;
			transform.localPosition = new Vector3(0,mid + trans,0);
		}else{
			transform.localPosition = new Vector3(0,mid,0); 
		}
	}
}