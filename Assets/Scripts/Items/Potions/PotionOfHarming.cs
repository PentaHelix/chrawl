using UnityEngine;
using System.Collections;

public class PotionOfHarming:Potion{
	float damageAmount;

	void Start(){
		base.Setup("Harming");
		damageAmount = 4f;
	}

	override public void Drink(){
		Game.player.GetComponent<Health>().Damage(damageAmount);
	}

	override public void Break(GameObject g){
		Enemy e = g.transform.root.GetComponentInChildren<Enemy>();
		if(e==null)return;
		e.Damage(damageAmount);
	}
}
