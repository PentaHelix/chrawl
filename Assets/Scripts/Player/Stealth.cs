using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Stealth : MonoBehaviour {
	public bool stealth = false;
	private float vig = 0;
	private VignetteScript v;
	private bool ctrl;

	void Start(){
		v = Game.player.Find("Head").GetComponent<VignetteScript>();
	}

	void Update () {
		if(Input.GetButtonDown("Ctrl"))ctrl = true;
		if(Input.GetButtonUp("Ctrl"))ctrl = false;
		bool c = (ctrl && Game.player.GetComponent<Mana>().Drain(7 * Time.deltaTime));
		if(!c)ctrl = false;
		if(c){
			Game.pocketWatch.state = "Semi_Flipped";
			if(vig < 1.2f){
				vig = Mathf.Min(1.2f, vig + 2.4f * Time.deltaTime);
				v.amount = vig;
			}else if(!stealth){
				stealth = true;
			}
		}else{
			if(vig > 0){
				vig = Mathf.Max(0, vig - 2.4f * Time.deltaTime);
				v.amount = vig;
			}else if(stealth){
				stealth = false;
			}
		}

		if(stealth){
		}
	}
}
