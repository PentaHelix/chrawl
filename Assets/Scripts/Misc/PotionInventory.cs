using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PotionInventory : MonoBehaviour {
	public const int POTION_CAP = 10;
	public bool potionsVisible;
	private int selectedPotion = 0;
	private float throwSpeedBase = 2f;
	private float throwSpeed = 2f;
	public List<Potion> potions = new List<Potion>();
	void Start () {
		
	}

	void Update () {
		if(potions.Count == 0)return;
		bool shift = Input.GetButton("Shift") && !Input.GetButton("Tab");	
		int scroll = -(int)(Input.GetAxis("Mouse ScrollWheel") * 10);
		if(scroll != 0 && shift){
			if(!potionsVisible){
				potionsVisible = true;
				foreach(Potion p in potions){
					p.transform.localPosition += new Vector3(0f,-0.07f,0f);
				}
			}
			if(potions.Count != 1){
				potions[selectedPotion].transform.localScale /= 1.2f;
				selectedPotion += scroll;
				clampSelected();
				potions[selectedPotion].transform.localScale *= 1.2f;
			}

		}else if(shift && potionsVisible){
			if(Input.GetMouseButtonDown(1)){
				Debug.Log(selectedPotion);
				Potion p = potions[selectedPotion];
				p.Drink();
				potions.Remove(p);
				Destroy(p.gameObject);
				RearrangePotions();
				clampSelected();
				potions[selectedPotion].transform.localScale *= 1.2f;

			}else if(Input.GetMouseButton(0)){
				if(throwSpeed < 25)throwSpeed += 8 * Time.deltaTime;

			}else if(throwSpeed != throwSpeedBase){
				DropPotion(selectedPotion);

			}
		}else if(potionsVisible && !shift){
			foreach(Potion p in potions){
				p.transform.localPosition += new Vector3(0f,0.07f,0f);
			}
			potionsVisible = false;
			throwSpeed = throwSpeedBase;
		}


		if(potionsVisible){

		}
	}

	public int AddPotion(GameObject go){
		return AddPotion(go.GetComponent<Potion>());
	}

	public int AddPotion(Potion potion){
		potions.Add(potion);
		potion.transform.parent = GameObject.Find("Head").transform;

		potion.GetComponent<Collider>().enabled = false;
		potion.GetComponent<Rigidbody>().isKinematic = true;

		potion.transform.localRotation = Quaternion.identity;
		potion.transform.localPosition = new Vector3(-.16f + 0.032f * (potions.Count-1),.125f + potion.yMod,.37f);
		potion.transform.localScale    = new Vector3(potion.scale, potion.scale, potion.scale);
		if(potions.Count == 1)potion.transform.localScale *= 1.2f;

		if(potionsVisible){
			potion.transform.localPosition += new Vector3(0f,-0.07f,0f);
		}

		return potions.Count;
	}

	void DropPotion(int i){
		Potion p = potions[i];

		p.GetComponent<Collider>().enabled = true;
		p.GetComponent<Rigidbody>().isKinematic = false;
		p.transform.parent = null;
		p.transform.localPosition = transform.position + transform.Find("Head").forward*2f + transform.Find("Head").up;
		p.transform.localRotation = Quaternion.identity;
		p.transform.localScale = new Vector3(.4f,.4f,.4f);

		p.GetComponent<Rigidbody>().velocity = (transform.Find("Head").forward*4f + transform.Find("Head").up).normalized * throwSpeed;
		p.GetComponent<Rigidbody>().angularVelocity = new Vector3((.5f-Random.value)*10,(.5f-Random.value)*10,(.5f-Random.value)*10);

		p.pickedUp = false;
		
		throwSpeed = throwSpeedBase;
		potions.Remove(p);

		//selectedPotion -= 1;
		clampSelected();
		potions[selectedPotion].transform.localScale *= 1.2f;

		RearrangePotions();
	}

	void RearrangePotions(){
		int i = 0;
		foreach(Potion p in potions){
			p.transform.localPosition = new Vector3(-.16f + 0.032f * i,p.yMod + .055f,.37f);
			i++;
		}
	}

	private void clampSelected(){
		if(selectedPotion < 0)selectedPotion += potions.Count;
		if(selectedPotion > potions.Count-1)selectedPotion -= potions.Count;
	}
}
