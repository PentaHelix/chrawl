using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MiniMap : MonoBehaviour {
	private Material mat;
	private Texture2D tex;
	private int prevX = 0; 
	private int prevY = 0;
	private int x = 0;
	private int y = 0;
	private List<int[]> revealedStructures = new List<int[]>();
	private List<int[]> revealedNodes = new List<int[]>();
	private static MapGen map;

	void Start(){
		tex = Instantiate(Resources.Load("Textures/MiniMap")) as Texture2D;
		mat = transform.Find("MiniMap").GetComponent<Renderer>().material;
		mat.mainTexture = tex;
	}

	void Update(){
		if(Input.GetButtonDown("Tab")){
			Transform hand = transform.Find("Hands/Right/Hand");
			Tween.TwRotation(hand, hand.localEulerAngles, new Vector3(60, 194, 0), 0.2f);
			Tween.TwPosition(hand, hand.localPosition, new Vector3(0.09f, -0.08f, 0.5f), 0.2f);
			Game.pocketWatch.state = "Flipped";
		}

		if(Input.GetButtonUp("Tab")){
			Transform hand = transform.Find("Hands/Right/Hand");
			Tween.TwRotation(hand, hand.localEulerAngles, new Vector3(15, 194, 0), 0.2f);
			Tween.TwPosition(hand, hand.localPosition, new Vector3(0.14f, -0.16f, 0.7f), 0.2f);
			Game.pocketWatch.state = Game.pocketWatch.prevState;
		}

		if(Input.GetButton("Tab") && !transform.Find("MiniMap").GetComponent<Renderer>().enabled){
			transform.Find("MiniMap").GetComponent<Renderer>().enabled = true;
		}

		if(!Input.GetButton("Tab") && transform.Find("MiniMap").GetComponent<Renderer>().enabled){
			transform.Find("MiniMap").GetComponent<Renderer>().enabled = false;
		}
		
		x = (int)(Mathf.Round((transform.parent.localPosition.x + 2.5f) / 5));
		y = (int)(Mathf.Round((transform.parent.localPosition.z + 2.5f) / 5));
		if(prevX == x && prevY == y)return;
		prevX = x;
		prevY = y;
		int[] s = map.StructureDimensions(x,y);
		if(s != null){
			RevealStructure(s);
		}

		mat.SetInt("_X", x);
		mat.SetInt("_Y", y);

	}

	public static void Init(MapGen m) {
		map = m;
	}

	private void DrawStructure(int[] s, Color c){
		for(int y = s[1]; y < s[3] + s[1]; y++){
			for(int x = s[0]; x < s[2] + s[0]; x++){
				tex.SetPixel(x,y,c);
			}	
		}
		tex.Apply();
	}

	private void RevealStructure(int[] s){
		if(revealedStructures.Contains(s))return;
		revealedStructures.Add(s);
		DrawStructure(s, Color.white);
		if(map.GetNodes(x,y) == null)return;
		foreach(int[] n in map.GetNodes(x,y)){
			if(revealedNodes.Contains(n))continue;
			revealedNodes.Add(n);
			DrawStructure(n, Color.green);
		}
	}

}
