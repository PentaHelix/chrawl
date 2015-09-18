using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour {
	void Start(){
		Invoke("Kill", 0.2f);
	}

	void Kill(){
		Destroy(gameObject);
	}

	public void Cast(Vector3 pos1, Vector3 pos2){
		Vector3 offset = pos2 - pos1;
		Vector3 scale = new Vector3(0.1f, offset.magnitude / 2f, 0.1f);
		Vector3 position = pos1 + (offset/2f);
		GetComponent<Renderer>().material.SetColor("_Color", new Color(0.44f, 0.78f, 0.96f, 0.5f));
		transform.position = position;
		transform.rotation = Quaternion.identity;
		transform.up = offset;
		transform.localScale = scale;
		transform.Find("Particles").GetComponent<ParticleSystem>().Emit((int)(30 * offset.magnitude));
	}
}
