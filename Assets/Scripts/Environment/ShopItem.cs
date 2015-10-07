using UnityEngine;
using System.Collections;

public class ShopItem : MonoBehaviour, ILookAt, IInteractable{
	public int cost = 5;
	public 
	void Start () {
		GameObject go = Drop.CreateShopItem(transform, true);
		go.transform.localPosition = new Vector3(0,0,0);
	}

	public void OnInteract(){
		if(Gold.gold - cost > 0){
			Gold.gold -= cost;
			transform.GetChild(0).GetComponent<IInteractable>().OnInteract();
		}
	}


	public void LookAt(RaycastHit r){
		Gold.displayGold = cost;
	}

}
