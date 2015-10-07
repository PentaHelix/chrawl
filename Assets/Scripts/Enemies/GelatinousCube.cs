using UnityEngine;
using System.Collections;

public class GelatinousCube : Enemy, IIgnitable, IFreezable {
	override public void Init(){
		hp = 10;
		mesh = transform.Find("Mesh");
	}

	override public void Die(){
		transform.Find("DeathParticles").gameObject.GetComponent<ParticleSystem>().Play();
		GetComponent<Collider>().enabled = false;
		Destroy(transform.Find("Mesh").gameObject);
		Destroy(transform.Find("Armature/Bone/Ice").gameObject);
		Destroy(gameObject, 4f);
	}

	override public bool Walk(){
		Animation a = GetComponent<Animation>();
		if(frozen){
			a.Stop();
			return false;
		}
		if(!a.IsPlaying("Jump")){
			a.Play("Jump");
			return false;
		}

		float time = a["Jump"].normalizedTime % 1;

		if(time > .49f && time < .81f){
			return true;
		}
		
		return false;
	}

	public void OnIgnite(float dur){
		transform.Find("Fire").GetComponent<ParticleSystem>().Play();
		Invoke("OnExpire", dur);
		burning = true;
	}

	public void OnExpire(){
		transform.Find("Fire").GetComponent<ParticleSystem>().Stop();
		burning = false;
	}

	public void OnFreeze(float dur){
		transform.Find("Armature/Bone/Ice").gameObject.SetActive(true);
		Invoke("OnThaw", dur);
		frozen = true;
	}

	public void OnThaw(){
		transform.Find("Armature/Bone/Ice").gameObject.SetActive(false);
		frozen = false;
	}
}
