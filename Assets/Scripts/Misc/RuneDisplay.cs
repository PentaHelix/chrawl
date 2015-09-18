using UnityEngine;
using System.Collections;

public class RuneDisplay : MonoBehaviour {
	private GameObject rune1;
	private GameObject rune2;
	private GameObject rune3;
	private GameObject rune4;
	public float coolDown = 0f;
	public bool visible = false;
	private string rID = "";

	void Start () {
		rune1 = transform.Find("Rune 1").gameObject;
		rune2 = transform.Find("Rune 2").gameObject;
		rune3 = transform.Find("Rune 3").gameObject;
		rune4 = transform.Find("Rune 4").gameObject;
		SetVisible(false);
	}

	void Update () {
		if(coolDown > 0){
			if(!visible)SetVisible(true);
			coolDown -= Time.deltaTime;
		}else{
			if(visible)SetVisible(false);
		}
	}

	void SetVisible(bool b){
		visible = b;
		rune1.GetComponent<Renderer>().enabled = b;
		rune2.GetComponent<Renderer>().enabled = b;
		rune3.GetComponent<Renderer>().enabled = b;
		rune4.GetComponent<Renderer>().enabled = b;
	}

	public void FromRuneSet(RuneSet r){
		coolDown = 1.5f;
		if(rID == r.rID)return;
		rID = r.rID;
		rune1.GetComponent<Renderer>().material.SetTexture("_MainTex", Runes.GetRuneTexture(r.strg));
		rune2.GetComponent<Renderer>().material.SetTexture("_MainTex", Runes.GetRuneTexture(r.stat));
		rune3.GetComponent<Renderer>().material.SetTexture("_MainTex", Runes.GetRuneTexture(r.proj));
		rune4.GetComponent<Renderer>().material.SetTexture("_MainTex", Runes.GetRuneTexture(r.spec));
	}
}
