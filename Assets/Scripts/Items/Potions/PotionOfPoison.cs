using UnityEngine;
using System.Collections;

public class PotionOfPoison:Potion{
	void Start(){
		base.Setup("Poison");
	}

	override public void Drink(){

	}

	override public void Break(GameObject g){

	}
}
