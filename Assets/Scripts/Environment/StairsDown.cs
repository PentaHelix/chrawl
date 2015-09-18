using UnityEngine;
using System.Collections;

public class StairsDown : MonoBehaviour{
	public void OnTriggerEnter(Collider col){
		Game.scripts.GetComponent<MapGen>().Make(Game.level + 1);
		Game.level += 1;
	}
}
