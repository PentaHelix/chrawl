using UnityEngine;
using System.Collections;

public class Cultist:Enemy, IIgnitable, IFreezable{
	private bool walking = false;
	private Transform p;
	
	override public void Init(){
		hp    = 12;
		p     = Game.player;
		speed = 160f;
		mesh = transform.Find("Mesh");
	}

	override public void Tick(){
		Vector2 d = new Vector2(p.localPosition.x - transform.localPosition.x, p.localPosition.z - transform.localPosition.z);
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.Atan2(d.x, d.y) * Mathf.Rad2Deg - 90, transform.localEulerAngles.z);
	}

	override public void Die(){
		transform.Find("DeathParticle").GetComponent<ParticleSystem>().Play();
		Destroy(transform.Find("Mesh").gameObject);
		Destroy(gameObject, 1f);
	}

	override public bool Walk(){
		Animation a = GetComponent<Animation>();
		if(Vector3.Distance(transform.localPosition, Game.player.localPosition) < 2f){
			a.Play("Idle");
			return false;
		}
		if(!a.IsPlaying("Walk"))a.Play("Walk");
		return true;
	}

	public void OnIgnite(float dur){
		transform.root.Find("Fire").GetComponent<ParticleSystem>().Play();
		Invoke("OnExpire", dur);
	}

	public void OnExpire(){

	}

	public void OnFreeze(float dur){
		transform.Find("Ice").gameObject.SetActive(true);
		frozen = true;
	}

	public void OnThaw(){
		transform.Find("Ice").gameObject.SetActive(false);
		frozen = false;
	}
}
