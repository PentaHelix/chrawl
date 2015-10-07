using UnityEngine;
using System.Collections;

public class Gold : MonoBehaviour {
	private static int maxGold = 24;
	private static Transform goldBar;
	public static int displayGold;

	public static int gold{
		get{
			return _Gold;
		}

		set{
			if(value > maxGold)return;
			goldBar.GetComponent<Renderer>().material.SetFloat("_Gold", value);
			displayGold = value;
			_Gold = value;
		}
	}

	public static int _Gold;



	void Start(){
		goldBar = Game.player.Find("Head/Gold");
		GetComponent<Renderer>().material.mainTexture.mipMapBias = -25;
		gold = 12;
	}


	void Update(){
		goldBar.GetComponent<Renderer>().material.SetFloat("_DisplayGold", displayGold);
		displayGold = 0;
	}
}
