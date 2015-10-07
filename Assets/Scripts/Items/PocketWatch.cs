using System;
using UnityEngine;
public class PocketWatch : MonoBehaviour{
	public Texture2D uvTex;
	private float prevHealth = 40f;
	private float prevMana = 40f;
	private float maxHealth = 40f;

	public string prevState = "Default";

	public string state{
		get{
			return _State;
		}

		set{
			prevState = _State;
			GetComponent<Animation>().CrossFade(value, .3f);
			_State = value;
		}
	}

	private string _State = "Default";

	Mana m;

	private void Start(){
		for (int i = 0; i < 40; i++){
			for (int j = 0; j < 320; j++){
				this.uvTex.SetPixel(j, i, Color.red);
				this.uvTex.SetPixel(j, i+40, Color.blue);
			}
		}

		m = Game.player.GetComponent<Mana>();
	}
	private void Update(){
		DrawHealth();
		DrawMana();
	}

	private void DrawHealth(){
		float health = Game.player.GetComponent<Health>().health;
		int num = (int)(health / maxHealth * 320f);
		int num2 = (int)(prevHealth / maxHealth * 320f);
		if (num > num2){
			for (int i = 0; i < 40; i++){
				for (int j = num2; j < num; j++){
					uvTex.SetPixel(j, i, Color.red);
				}
			}
			uvTex.Apply();
		}else if (num < num2){
			for (int k = 0; k < 40; k++){
				for (int l = num2; l > num; l--){
					uvTex.SetPixel(l, k, Color.gray);
				}
			}
			uvTex.Apply();
		}
		prevHealth = health;
	}
	
	private void DrawMana(){
		float mana = m.mana;
		int num = (int)(mana / m.maxMana * 320f);
		int num2 = (int)(prevMana / m.maxMana * 320f);
		if (num > num2){
			for (int i = 0; i < 40; i++){
				for (int j = num2; j < num; j++){
					uvTex.SetPixel(j, i+40, Color.blue);
				}
			}
			uvTex.Apply();
		}else if (num < num2){
			for (int k = 0; k < 40; k++){
				for (int l = num2; l > num; l--){
					uvTex.SetPixel(l, k+40, Color.gray);
				}
			}
			uvTex.Apply();
		}
		prevMana = mana;
	}
}
