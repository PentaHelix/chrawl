using UnityEngine;
using System.Collections;

public class Tome : MonoBehaviour, ILookAt , IInteractable{
	public RuneSet runeSet;
	public Spell spell;
	public float manaCost;

	void Start () {
		spell = transform.Find("Spell").GetComponent<Spell>();
		manaCost = 8f;
	}
	
	void Update () {
	
	}
	
	public void LookAt(RaycastHit r){
		if(r.distance <= 5f){
			RuneDisplay rd = GameObject.Find("/Player/Head/RuneDisplay").GetComponent<RuneDisplay>();
			rd.FromRuneSet(runeSet);
		}
	}

	public void OnInteract(){
		GameObject.Find("Player/Head/Hands").GetComponent<MagicControls>().AddTome(gameObject);
	}
}
