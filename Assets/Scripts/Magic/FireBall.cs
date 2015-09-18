using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {
	private bool stopped = false;
	public Vector3 dir = new Vector3(0,0,0);

	void Start () {
		Invoke("Explode", 2f);
		transform.Find("Explosion").GetComponent<ParticleSystem>().Stop();
	}

	void Update () {
		if(!stopped){
			transform.Translate(dir * Time.deltaTime);
		}
	}

	void Kill(){
		Debug.Log("Killed");
		Destroy(gameObject);
	}

	void Explode(){
		if(stopped)return;
		stopped = true;
		Invoke("Kill", 1);
		transform.Find("Explosion").GetComponent<ParticleSystem>().Emit(100);
		GetComponent<ParticleSystem>().Stop();
		Enemy[] enemies = FindObjectsOfType(typeof(Enemy)) as Enemy[];
		foreach(Enemy e in enemies){
			if(Vector3.Distance(transform.position, e.transform.position) < 10f){
				e.Damage(12);
			}
		}
	}

	void OnTriggerEnter(Collider col){
		Debug.Log(col.transform.name);
		if(stopped || col.transform.name == "Player")return;
		Enemy e = col.transform.root.GetComponentInChildren<Enemy>();
		if(e){
			e.Damage(40);
		}
		Explode();
	}
}
