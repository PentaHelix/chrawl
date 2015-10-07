using UnityEngine;
using System.Collections;

public class LookRayCast : MonoBehaviour {
	private RaycastHit rayInfo;

	void Update (){
		if(Physics.Raycast(transform.position, transform.forward, out rayInfo)){
			ILookAt i1 = rayInfo.transform.GetComponent<ILookAt>();
			IInteractable i2 = rayInfo.transform.GetComponent<IInteractable>();
			if(i1 != null)i1.LookAt(rayInfo);
			if(i2 != null && Input.GetButtonDown("Interact"))i2.OnInteract();
		}
	}
}