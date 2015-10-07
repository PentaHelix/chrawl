using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Potion : MonoBehaviour, IInteractable {
	private Transform player;
	private Color color;
	private GameObject liquid;

	public string shape  = "";
	public string effect = "";

	public bool breakable = false;
	public bool pickedUp = false;
	public float yMod    = 0;
	public float scale   = 0;

	private PotionInventory pi;

	public static Dictionary<string, float> potionYMod = new Dictionary<string, float>{
		{"Pointed",  0f},
		{"Cylinder", -.006f},
		{"Sphere",   .004f}
	};

	public static Dictionary<string, float> potionScale = new Dictionary<string, float>{
		{"Pointed",  .019f},
		{"Cylinder", .024f},
		{"Sphere",   .0155f}	
	};

	public void Setup(string e) {
		player = GameObject.Find("Player").transform;
		pi = player.GetComponent<PotionInventory>();
		GameObject potion = gameObject;
		liquid = potion.transform.Find("Liquid").gameObject;

		liquid.GetComponent<Renderer>().material = Instantiate(Resources.Load("Materials/Color/Diffuse")) as Material;

		yMod = potionYMod[shape];
		scale = potionScale[shape];

		
		SetColor(PotionGen.GetColor(effect));
	}

	public virtual void Drink(){

	}

	public virtual void Break(GameObject g){
		
	}

	public void SetColor(Color c){
		liquid.GetComponent<Renderer>().material.color = c/255f;
	}

	public void RestoreScale(){
		
	}

	void Update () {

	}

	public void OnInteract(){
		pi.AddPotion(this);
		pickedUp = true;
		breakable = true;
	}

	void OnCollisionEnter(Collision col){
		if(GetComponent<Rigidbody>().velocity.magnitude > 0.8f && breakable){
			GameObject fracture = Instantiate(Resources.Load("Prefabs/Objects/Potions/Potion_"+shape+"_Fractured"), transform.localPosition, transform.localRotation) as GameObject;
			foreach(Transform t in fracture.transform){
				Rigidbody r = t.GetComponent<Rigidbody>();
				if(r != null)r.velocity = GetComponent<Rigidbody>().velocity;
			}
			fracture.transform.Find("Liquid").GetComponent<ParticleSystem>().startColor = PotionGen.GetColor(effect);
			Break(col.gameObject);
			Destroy(fracture, 0.53f);
			Destroy(gameObject);
		}
	}
}
