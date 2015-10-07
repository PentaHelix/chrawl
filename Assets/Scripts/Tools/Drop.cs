using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Drop{
	public static GameObject CreatePotion(Transform t, bool parent = false){
		string shape   = PotionGen.RandomShape();
		string effect  = PotionGen.RandomEffect();
		Potion potionC = null;
		Debug.Log(effect);
		Color color   = PotionGen.GetColor(effect);
		GameObject potion = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Objects/Potions/Potion_"+shape)) as GameObject;

		switch(effect){
			case "Healing":
				potionC = potion.AddComponent<PotionOfHealing>();
			break;
			case "Harming":
				potionC = potion.AddComponent<PotionOfHarming>();
			break;
			case "Poison":
				potionC = potion.AddComponent<PotionOfPoison>();
			break;
			case "Burning":
				potionC = potion.AddComponent<PotionOfBurning>();
			break;
			case "Freezing":
				potionC = potion.AddComponent<PotionOfFreezing>();
			break;
			case "Shocking":
				potionC = potion.AddComponent<PotionOfShocking>();
			break;
		}

		if(parent)potion.transform.parent = t;
		potion.transform.position                        = t.position;
		potion.GetComponent<Rigidbody>().velocity        = new Vector3((0.5f-Random.value)*5,Random.value*5,(0.5f-Random.value)*5);
		potion.GetComponent<Rigidbody>().angularVelocity = new Vector3(0.5f-Random.value*5,Random.value*5,0.5f-Random.value*5);
		potionC.shape = shape;
		potionC.effect = effect;
		return potion;
	}

	public static GameObject CreateTome(Transform t, bool parent=false){
		RuneSet r = TomeGen.GetRuneSet();
		GameObject tome = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Magic/Tome")) as GameObject;

		tome.GetComponent<Tome>().runeSet = r;

		Spell spell = tome.transform.Find("Spell").GetComponent<Spell>();
		spell.FromRuneSet(r);

		tome.transform.Find("Rune 1").GetComponent<Renderer>().material.SetTexture("_MainTex", Runes.GetRuneTexture(r.strg));
		tome.transform.Find("Rune 2").GetComponent<Renderer>().material.SetTexture("_MainTex", Runes.GetRuneTexture(r.stat));
		tome.transform.Find("Rune 3").GetComponent<Renderer>().material.SetTexture("_MainTex", Runes.GetRuneTexture(r.proj));
		tome.transform.Find("Rune 4").GetComponent<Renderer>().material.SetTexture("_MainTex", Runes.GetRuneTexture(r.spec));

		spell.FromRuneSet(r);

		if(parent)tome.transform.parent = t;
		tome.transform.localPosition = new Vector3(0,0,0);
		tome.transform.eulerAngles = new Vector3(-90,-90,0);

		tome.transform.name = "Tome";
		return tome;
	}

	public static GameObject CreateScroll(Transform t, bool parent=false){
		int[] runes = ScrollGen.GetRunes();
		GameObject scroll = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Magic/Scroll"), t.position, t.rotation) as GameObject;
		if(parent)scroll.transform.parent = t;
		scroll.transform.Find("Rune 2").GetComponent<Renderer>().material.SetTexture("_MainTex", Runes.GetRuneTexture(runes[0]));
		scroll.transform.Find("Rune 3").GetComponent<Renderer>().material.SetTexture("_MainTex", Runes.GetRuneTexture(runes[1]));
		scroll.transform.name = "Scroll";
		scroll.GetComponent<Scroll>().rune1 = Runes.RUNE_NAME[runes[0]];
		scroll.GetComponent<Scroll>().rune2 = Runes.RUNE_NAME[runes[1]];
		return scroll;
	}

	public static GameObject CreateShopItem(Transform tf, bool parent = false){
		switch((int)(Random.value * 3)){
			// POTION
			case 0:
				GameObject p = CreatePotion(tf, parent);
				p.transform.localEulerAngles = new Vector3(270,180,0);
				p.GetComponent<Rigidbody>().isKinematic = true;
				return p;
			break;

			// TOME
			case 1:
				GameObject t = CreateTome(tf, parent);
				t.transform.localEulerAngles = new Vector3(0,0,180);
				return t;
			break;

			// SCROLL
			case 2:
				GameObject s = CreateScroll(tf, parent);
				s.transform.localEulerAngles = new Vector3(90,90,0);
				return s;
			break;
		}

		return null;
	}
}
