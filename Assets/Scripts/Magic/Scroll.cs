using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour, IInteractable{
	public float cost;
	public string rune1;
	public string rune2;

	public void Cast(){
		switch(rune1){

			case "Reqconur":

			break;

			case "Clipear":
				switch(rune2){
					case "Adtomar":

					break;
					case "Accignar":
						
					break;
					case "Glaciar":
						GameObject dome = Instantiate(Resources.Load("Prefabs/Magic/IceDome"), Game.player.position - new Vector3(0, 1.5f, 0), Quaternion.identity) as GameObject;
						dome.transform.localEulerAngles = new Vector3(270, 0, 0);
					break;
				}
			break;

			case "Inflidar":
				switch(rune2){
					case "Adtomar":
						Enemy[] enemies = FindObjectsOfType(typeof(Enemy)) as Enemy[];
						foreach(Enemy e in enemies){
							if(Vector3.Distance(Game.player.position, e.transform.position) < 10f){
								Vector3 dest = e.transform.position;
								dest.y += 2f;
								CastRay(Game.player.position, dest);
								e.Damage(20f);
							}
						}
					break;
					case "Accignar":
						Vector3 position = Game.player.Find("Head").position + Game.player.Find("Head").forward; 
						GameObject f = Instantiate(Resources.Load("Prefabs/Magic/FireBall"), position, Quaternion.identity) as GameObject;
						f.GetComponent<FireBall>().dir = Game.player.Find("Head").forward * 12f;
					break;
					case "Glaciar":
						GameObject blizz = Instantiate(Resources.Load("Prefabs/Magic/Blizzard"), Game.player.position, Quaternion.identity) as GameObject;
						blizz.transform.localEulerAngles = new Vector3(270, 0, 0);
					break;
				}
			break;
		}
	}

	public static void CastRay(Vector3 pos1, Vector3 pos2){
		GameObject beam = Instantiate(Resources.Load("Prefabs/Magic/Beam")) as GameObject;
		beam.GetComponent<Beam>().Cast(pos1, pos2);
	}

	public void OnInteract(){
		Game.magicControls.AddScroll(gameObject);
	}
}
