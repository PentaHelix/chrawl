using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Damage))]	
public class Enemy : MonoBehaviour {
	public bool alive = true;
	public bool burning = false;
	public bool frozen = false;
	[Range(0,100)]
	public float hp;
	public float speed = 250f;
	public Transform mesh;
	private Color matColor;

	void Start(){
		Init();
		matColor = mesh.GetComponent<Renderer>().material.GetColor("_Color");
	}

	public void Heal(float health){
		hp += health;
	}

	public void Damage(float damage){
		hp-=damage;
		mesh.GetComponent<Renderer>().material.SetColor("_Color", (matColor + Color.red) / 2);
		Invoke("_UnTint", 0.1f);
	}

	void Update () {
		if(hp <= 0 && alive){
			Die();
			DropItem();
			alive = false;
		}
		Tick();
	}

	public virtual void Init(){}
	public virtual void Die(){}
	public virtual void Tick(){}
	public virtual bool Walk(){return false;}

	void Explode(){
		
	}

	void DropItem(){
		if(Random.value > 0.0f){
			Drop.CreatePotion(transform);
		}
	}


	private void _UnTint(){
		if(!mesh)return;
		mesh.GetComponent<Renderer>().material.SetColor("_Color", matColor);
	}
}
