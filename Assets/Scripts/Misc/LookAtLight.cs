using UnityEngine;
using System.Collections;

public class LookAtLight : MonoBehaviour, ILookAt {
	public void LookAt(RaycastHit r){
		transform.Find("Light").gameObject.active = true;
	}
}
