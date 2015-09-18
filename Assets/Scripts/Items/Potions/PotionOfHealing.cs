using UnityEngine;
using System.Collections;

public class PotionOfHealing:Potion{
	public float healAmount;

	void Start(){
		base.Setup("Healing");
		healAmount = 10f;
	}

	override public void Drink(){
		Game.player.GetComponent<Health>().Heal(healAmount);
	}

	override public void Break(GameObject g){
		Enemy e = g.transform.root.GetComponentInChildren<Enemy>();
		if(e==null)return;
		e.Heal(healAmount);
	}
}
