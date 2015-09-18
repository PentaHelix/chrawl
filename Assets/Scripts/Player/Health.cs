using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public float health;
	public float maxHealth;
	public bool invincible;

	public void Heal(float heal){
		health += heal;
		if(health > maxHealth)health = maxHealth;
	}

	public void Damage(float dam){
		health -= dam;
	}

	void Start () {
		health = 40f;
		maxHealth = 40f;
		invincible = false;
	}

	void Update () {
	
	}

	void OnCollisionStay(Collision col){
		if(!col.transform.parent)return;
		Damage dmg = col.transform.parent.GetComponent<Damage>();
		if(dmg != null && !invincible){
			health -= dmg.damage;
			invincible = true;
			Invoke("RevertInvincible", 2f);
		}
	}

	void RevertInvincible(){
		invincible = false;
	}
}
