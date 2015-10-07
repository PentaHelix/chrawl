using UnityEngine;
using System.Collections;

public class Startup : MonoBehaviour {
	void Start () {
		PotionGen.SetupPotions();
		Drop.CreateScroll(transform, false);
		Scroll.CastRay(new Vector3(0,0,0), new Vector3(10,1,10));
		GetComponent<MapGen>().Make(0);
	}
}
