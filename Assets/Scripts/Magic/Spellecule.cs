using UnityEngine;
using System.Collections;

public class Spellecule : MonoBehaviour {
	public Vector3 velocity;
	public Enemy homeTowards;
	public Spell parentSpell;
	private float speed = 1f;
	private float speedMod = 0f;
	public int pierces = 0;
	public bool alive = true;

	void Start () {
		transform.Find("DeathParticle").GetComponent<ParticleSystem>().Pause();
	}

	void Update () {
		if(!alive)return;
		transform.position += velocity * (1 + parentSpell.feVelocity);
		if(!parentSpell)return;
		speed *= 1 + (0.5f - parentSpell.feAcceleration);
	}

	public void setLocked(){
		float distance = 100;
		foreach(Enemy e in Object.FindObjectsOfType(typeof(Enemy))){
			if((transform.position - e.transform.position).magnitude < distance && e.alive){
				distance = (transform.position - e.transform.position).magnitude;
				homeTowards = e;
			}
		}
	}

	void Kill(){
		parentSpell.spellecules.Remove(this);
		GetComponent<ParticleSystem>().Stop();
		ParticleSystem p = transform.Find("DeathParticle").GetComponent<ParticleSystem>();
		if(!p.isPlaying)p.Play();
		Destroy(gameObject, parentSpell.feLifetime);
		transform.Find("Light").GetComponent<Fade>().StartFade(parentSpell.feLifetime);
		Destroy(GetComponent<Collider>());
		alive = false;
	}

	void OnDrawGizmos(){
		Gizmos.DrawLine(transform.position, transform.position + velocity);
	}

	void OnTriggerEnter(Collider col){
		if(col.transform.parent != null){
			Enemy e = col.transform.parent.GetComponent<Enemy>();
			if(e != null)e.Damage(parentSpell.feDamage * 10);
		}

		Debug.Log(col.transform.name);
		if(col.transform.name != "Player" && col.transform.name != "Spellecule" && 
		   col.transform.name != "Tome")Kill();
	}
}
