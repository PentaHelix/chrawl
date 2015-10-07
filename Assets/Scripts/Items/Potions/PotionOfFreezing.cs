using UnityEngine;
using System.Collections;
 using UnityStandardAssets.ImageEffects;

public class PotionOfFreezing:Potion{
	VignetteScript v;
	void Start(){
		base.Setup("Freezing");
		v = Game.player.Find("Head").GetComponent<VignetteScript>();
	}

	override public void Drink(){
		Invoke("Revert", 3f);
	}

	override public void Break(GameObject g){

	}

	public void Revert(){
		
	}
}
