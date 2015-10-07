using UnityEngine;
using System.Collections;

public class Burn : MonoBehaviour {
	bool burning = false;
	float burnCooldown = 0f;
	float damageCooldown = 0f;
	float damage = 1f;
	float damageTimer = 0.7f;
	Enemy e;

	void Start(){
    	e = transform.root.GetComponentInChildren<Enemy>();
	}

	void Update(){
		burnCooldown -= Time.deltaTime;
		if(burnCooldown < 0 && burning){
			StopBurn();
		}else if(burning){
			if(e != null){
				damageCooldown -= Time.deltaTime;
				if(damageCooldown < 0){
					transform.root.GetComponentInChildren<Enemy>().Damage(damage);
					damageCooldown = damageTimer;
				}
			}
		}
	}


	public void StartBurn(float dur){
		if(!burning)GetComponent<ParticleSystem>().Play();
		burning = true;
		burnCooldown = dur;
		damageCooldown = damageTimer;
	}

	public void StopBurn(){
		burning = false;
		GetComponent<ParticleSystem>().Stop();
	}
}
