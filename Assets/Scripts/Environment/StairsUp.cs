using UnityEngine;
using System.Collections;

public class LadderUp:MonoBehaviour, IInteractable{
	public void OnInteract(){
		Debug.Log("Mooved up");
	}
}
