using UnityEngine;
using System.Collections;

public class Game:MonoBehaviour{
	public static Transform player;
	public static PocketWatch pocketWatch;
	public static MagicControls magicControls;
	public static GameObject scripts;
	public static int level = 1;


	void Awake() {
		player = GameObject.Find("Player").transform;
		pocketWatch = Game.player.Find("Head/Hands/Right/Hand/PocketWatch").GetComponent<PocketWatch>();
		magicControls = Game.player.Find("Head/Hands").GetComponent<MagicControls>();
		scripts = GameObject.Find("Scripts");
	}
}
