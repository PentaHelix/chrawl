using UnityEngine;
using System.Collections;

public class StatusEffects:MonoBehaviour {
	bool burning = false;
	float burnCooldown = 0f;
	float damageCooldown = 0f;
	float damageTimer = 0.7f;
	float damage = 1f;

	void Update () {
		if(burning)return;
		burnCooldown -= Time.deltaTime;
		damageCooldown -= Time.deltaTime;
		if(burnCooldown < 0 && burning){
			StopBurn();
		}else if(burning){
			if(damageCooldown < 0){
				transform.parent.GetComponent<Health>().Damage(damage);
				damageCooldown = damageTimer;
			}
		}
	}

	public void Burn(float dur){
		if(!burning)transform.Find("Burn").GetComponent<ParticleSystem>().Play();
		burning = true;
		damageCooldown = damageTimer;
		burnCooldown = dur;
	}

	public void StopBurn(){
		transform.Find("Burn").GetComponent<ParticleSystem>().Stop();
		burning = false;
	}
}
