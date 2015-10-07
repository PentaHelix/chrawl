using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MagicControls : MonoBehaviour {
	private GameObject hand_right;
	private GameObject hand_left;

	private List<GameObject> daggers = new List<GameObject>(3);
	private List<GameObject> tomes = new List<GameObject>(3);
	private List<GameObject> scrolls = new List<GameObject>(3);
	private int currentSpell = 0;

	public const int SPELLTYPE_DAGGER = 0;
	public const int SPELLTYPE_TOME = 1;
	public const int SPELLTYPE_SCROLL = 2;

	void Start () {
		hand_right = transform.Find("Right/Hand").gameObject;
		hand_left = transform.Find("Left/Hand").gameObject;
		AddDagger(Instantiate(Resources.Load("Prefabs/Daggers/Dagger")) as GameObject);
		AddTome();
		AddScroll(Drop.CreateScroll(transform, false));
		daggers[currentSpell].active = true;
		hand_left.GetComponent<Animation>().Play("Hold_Dagger");
	}

	void Update () {
		if(Input.GetButton("Shift"))return;
		if(Input.GetMouseButtonDown(0))CastSpell();
		
		int scroll = (int)(Input.GetAxis("Mouse ScrollWheel") * 10);
		if(scroll == 0)return;
		int lastCurSpell = currentSpell;
		currentSpell += scroll;
		while(currentSpell >= daggers.Count + tomes.Count + scrolls.Count)currentSpell -= daggers.Count + tomes.Count + scrolls.Count;
		while(currentSpell < 0)currentSpell += daggers.Count + tomes.Count + scrolls.Count;
		if(currentSpell == lastCurSpell)return;
		SetPocketwatch();

		hand_left.transform.parent.localPosition = new Vector3(-.2f, -.5f, 0);

		Deactivate(lastCurSpell);

		if(CurrentSpellType() == SPELLTYPE_DAGGER){
			daggers[currentSpell].active = true;
			hand_left.GetComponent<Animation>().Play("Hold_Dagger");
		}else if(CurrentSpellType() == SPELLTYPE_TOME){
			tomes[currentSpell - daggers.Count].active = true;
			hand_left.GetComponent<Animation>().Play("Hold_Tome");
		}else{
			scrolls[currentSpell - daggers.Count - tomes.Count].active = true;
			hand_left.GetComponent<Animation>().Play("Hold_Scroll");
		}
	}

	private void CastSpell(){
		switch(CurrentSpellType()){
			case SPELLTYPE_DAGGER:
				hand_left.GetComponent<Animation>().Play("Swing_Dagger");
				daggers[currentSpell].GetComponent<Dagger>().Attack();
			break;
			case SPELLTYPE_TOME:
				if(Game.player.GetComponent<Mana>().Drain(tomes[currentSpell - daggers.Count].GetComponent<Tome>().manaCost)){
					tomes[currentSpell - daggers.Count].GetComponent<Tome>().spell.Cast();
				}
			break;

			case SPELLTYPE_SCROLL:
				scrolls[currentSpell - daggers.Count - tomes.Count].GetComponent<Scroll>().Cast();
			break;
		}
	}

	public void AddDagger(GameObject d){
		if(daggers.Count == 3)return;
		daggers.Add(d);
		d.transform.parent = Game.player.Find("Head/Hands/Left/Hand/Armature/Bone/Bone.002/Bone.007/Bone.011");
		d.transform.localPosition = new Vector3(-.02f, .03f, 0.01f);
		d.transform.localEulerAngles = new Vector3(-13, -95, 270);
		d.active = false;
	}


	public void AddTome(GameObject t = null){
		if(tomes.Count == 3)return;
		if(t == null){
			t = Drop.CreateTome(hand_left.transform, true);
		}else{
			t.transform.parent = hand_left.transform;
		}
		tomes.Add(t);
		t.transform.localScale = new Vector3(.4f, .4f, .4f);
		t.transform.localPosition = new Vector3(0, 0.15f, 0.1f);
		t.transform.localEulerAngles = new Vector3(293,180,290);
		t.active = false;
	}

	public void AddScroll(GameObject s){
		if(scrolls.Count == 3)return;
		scrolls.Add(s);
		s.transform.parent = hand_left.transform;
		s.transform.localScale = new Vector3(.7f, .7f, .7f);
		s.transform.localPosition = new Vector3(.01f, .036f, .0055f);
		s.transform.localEulerAngles = new Vector3(0,270,280);
		s.active = false;

	}

	public void Deactivate(int i){
		if(i < daggers.Count){
			daggers[i].active = false;
		}else if(i < daggers.Count + tomes.Count){
			tomes[i - daggers.Count].active = false;
		}else{
			scrolls[i - daggers.Count - tomes.Count].active = false;				
		}
	}

	public void SetPocketwatch(){
		switch(CurrentSpellType()){
			case SPELLTYPE_DAGGER:
				Game.pocketWatch.state = "Default";
			break;
			case SPELLTYPE_TOME:
				Game.pocketWatch.state = "Semi_Flipped";
			break;
			case SPELLTYPE_SCROLL:
				Game.pocketWatch.state = "Semi_Flipped";
			break;
		}
	}

	private int CurrentSpellType(){
		if(currentSpell < daggers.Count){
			return SPELLTYPE_DAGGER;
		}else if(currentSpell < daggers.Count + tomes.Count){
			return SPELLTYPE_TOME;
		}else{
			return SPELLTYPE_SCROLL;			
		}
	}
}
