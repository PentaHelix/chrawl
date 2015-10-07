using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour, ILookAt{
	public bool open = false;
	void Start () {
		GameObject tome = Drop.CreateTome(transform.Find("Item"), true);
		tome.transform.localEulerAngles = new Vector3(tome.transform.localEulerAngles.x, 180, tome.transform.localEulerAngles.z);
	}		

	void Update () {
	
	}

	public void LookAt(RaycastHit r){
		if(r.distance <= 3f){
			Open();
			Debug.Log("Lookat");
		}
	}

	void Open(){
		if(open)return;
		open = true;
		GetComponent<Animation>().Play("Open");
		Transform item = transform.Find("Item");
		Vector3 pos = item.localPosition;
		Tween.TwPosition(item.transform, item.localPosition, new Vector3(0,1.5f,0f), 1.2f);
	}
}
