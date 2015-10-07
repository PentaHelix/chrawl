using UnityEngine;
using System.Collections;

public class Dagger : MonoBehaviour {
	private RaycastHit rayInfo;
	public float damage = 3f;

	public void Attack(){
		if(Physics.Raycast(Game.player.Find("Head").position, Game.player.Find("Head").forward, out rayInfo)){
			Enemy e = rayInfo.transform.root.GetComponentInChildren<Enemy>();
			if(e != null && rayInfo.distance < 2f){
				e.Damage(damage);
			}
		}
	}
}
