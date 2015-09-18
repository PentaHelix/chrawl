using UnityEngine;
using System.Collections;

public class PotionOfBurning:Potion{
	float burnDuration;
	void Start(){
		base.Setup("Burning");
		burnDuration = 5f;
	}

	override public void Drink(){
		Debug.Log(Game.player);
		Game.player.Find("Head").GetComponent<StatusEffects>().Burn(burnDuration);
	}

	override public void Break(GameObject g){
		IIgnitable b = g.transform.root.GetComponentInChildren<IIgnitable>();
		if(b == null)return;
		b.OnIgnite(burnDuration);
	}
}
