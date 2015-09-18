using UnityEngine;
using System.Collections;

public class Mana : MonoBehaviour {
	public float mana = 40f;
	public float maxMana = 40f;
	private float prevMana;
	private float manaRegen = 10;

	public bool Drain(float amount){
		if(mana - amount > 0){
			mana -= amount;
			return true;
		}else{
			return false;
		}
	}

	void Start () {
		mana = 40f;
		maxMana = 40f;
	}



	void Update () {
		if(prevMana == mana && mana < maxMana){
			mana += manaRegen * Time.deltaTime;
			if(mana > maxMana)mana = maxMana;
		}
		prevMana = mana;
	}
}
