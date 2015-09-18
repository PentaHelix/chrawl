using UnityEngine;
using System.Collections;

public class Doorway : MonoBehaviour, IInteractable{
	public Transform door;
	public void Start(){
		if(!door)door = transform.Find("Mesh/Door1");
	}

	public void SetDoorStyle(int i){
		//Don't look at that, I'm sure it's great code
		door = transform.Find("Mesh/Door"+i);
		transform.Find("Mesh/Door1").gameObject.active = false;
		transform.Find("Mesh/Door2").gameObject.active = false;
		transform.Find("Mesh/Door"+i).gameObject.active = true;
	}

	public void OnInteract(){
		Tween.TwPosition(door, new Vector3(0,0,0), new Vector3(0,4,0), 1.3f);
		GetComponent<Collider>().enabled = false;
	}
}
