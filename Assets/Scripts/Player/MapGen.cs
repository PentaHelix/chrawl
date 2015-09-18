using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGen:MonoBehaviour {
	public static int LEVEL = 0;
	[Range(0.0f,100.0f)]
	public int NUM_ROOMS = 30;
	[Range(0.0f,100.0f)]
	public int NUM_ENEMIES = 60;

	public int[,] mapData = new int[40,30];
	public int[,] objData = new int[40,30];

	public List<Structure> structures = new List<Structure>();
	public List<Room> rooms           = new List<Room>();
	public List<Path> paths           = new List<Path>();
	public List<Node> nodes           = new List<Node>();

	public List<Vector2> wallVec = new List<Vector2>{new Vector2(0, -1),new Vector2(1, 0),new Vector2(0, 1),new Vector2(-1, 0)};

	public virtual void Make(int level){
		LEVEL = level;
	}

	public void Place(int x, int y, float h, GameObject g, bool obj=true){
		if(obj)objData[x,y] = 1;
		g.transform.position = new Vector3(x * 5 + 2.5f, h, y * 5 + 2.5f);
		g.hideFlags = HideFlags.HideInHierarchy;
	}

	public virtual void ClearMap(){
		mapData    = new int[40,30];
		objData    = new int[40,30];
		structures = new List<Structure>();
		rooms      = new List<Room>();
		paths      = new List<Path>();
		nodes      = new List<Node>();

		Transform[] transforms = GameObject.FindObjectsOfType(typeof(Transform)) as Transform[];
		foreach(Transform t in transforms){
			if(t.tag != "Persistent" && t.parent == null){
				Destroy(t.gameObject);
			}
		}
	}

	public void MakeMapData(){
		foreach(Structure s in structures){
			for(int y = 0; y < s.h; y++){
				for(int x = 0; x < s.w; x++){
					mapData[s.x + x,s.y + y] = 1;
					if(s.special == "Water")mapData[s.x + x,s.y + y] = 2;
				}
			}
		}
	}

	public void MakeBounds(){
		GameObject c = GameObject.CreatePrimitive(PrimitiveType.Cube);
		c.transform.localScale = new Vector3(200, 1, 150);
		c.transform.localPosition = new Vector3(100, 6, 75);
		Material cM = Instantiate(Resources.Load("Materials/Wall/Wall01")) as Material;
		cM.mainTextureScale = new Vector2(40,30);
		c.GetComponent<Renderer>().material = cM;

		for(int x = 0; x < 40; x++){
			PlaceWall(x,-1);
			PlaceWall(x,30);
		}

		for(int y = 0; y < 30; y++){
			PlaceWall(-1,y);
			PlaceWall(40,y);	
		}
	}

	public void MakeWalls(){
		for(int x = 0; x < 40; x++){
			for(int y = 0; y < 30; y++){
				if(mapData[x,y] == 0){
 					PlaceWall(x,y);
 				}
 			}
 		}
	}

	public static void PlaceWall(int x, int y){
		GameObject cube = Instantiate(Resources.Load("Prefabs/Objects/Cube")) as GameObject;
 		cube.transform.name = "Wall_"+x+"_"+y;
 		cube.transform.localScale = new Vector3(5,10,5);
 		cube.transform.position = new Vector3(x*5+2.5f,.5f,y*5+2.5f);
 		string mat = "Materials/Wall/Wall01";
 		if(Random.value > 0.92){
 			mat+="_"+(int)(Random.value * 3 + 1);
 		}
 		mat = "Materials/Wall/Wall02";
 		cube.GetComponent<Renderer>().material = Instantiate(Resources.Load(mat)) as Material;
 		cube.GetComponent<Renderer>().material.mainTextureScale = new Vector3(1,2,1);
 		cube.transform.hideFlags = HideFlags.HideInHierarchy;
	}

	public int GetMapVal(int x, int y){
		if(x >= 0 && y >= 0 && x < 40 && y < 30){
			return mapData[x,y];
		}else{
			return 0;
		}
	}

	public int Opposite(int d){
		return d+2%4;
	}

	public Structure StructureOn(Vector2 v){
		return StructureOn((int)(v.x), (int)(v.y));
	}

	public Structure StructureOn(int x, int y){
		foreach(Structure s in structures){
			if(s.x <= x && s.x + s.w > x){
				if(s.y <= y && s.y + s.h > y){
					return s;
				}
			}
		}
		return null;
	}

	public int[] StructureDimensions(int x, int y){
		foreach(Structure s in structures){
			if(s.x <= x && s.x + s.w >= x){
				if(s.y <= y && s.y + s.h >= y){
					return new int[]{s.x, s.y, s.w, s.h};
				}
			}
		}
		return null;
	}

	public List<int[]> GetNodes(int x, int y){
		List<int[]> nodes = new List<int[]>();
		Structure s = StructureOn(x,y);
		if(s == null)return null;
		foreach(Node n in s.nodes){
			nodes.Add(new int[]{n.x, n.y, 1, 1});
		}
		return nodes;
	}

	public class Structure{
		public int x;
		public int y;
		public int w;
		public int h;
		public int d;
		public string t;
		public int c = 1;
		public string special = "";
		public List<Node> nodes = new List<Node>();

		public Structure(int x, int y, int w, int h, int d, string t){
			this.x = x;
			this.y = y;
			this.w = w;
			this.h = h;
			this.d = d;
			this.t = t;
		}

		public bool Intersects(Structure s){
			if(x < 0 || y < 0 || x + w > 39 || y + h > 29)return true;
			return !(s.x > x + w || s.x + s.w < x || s.y > y + h || s.y + s.h < y);
		}
	}


	public class Room:Structure{
		public int doorway;
		public Room(int x, int y, int w, int h, int d):base(x,y,w,h,d,"Room"){}
	}

	public class Path:Structure{
		public Path(int x, int y, int w, int h, int d):base(x,y,w,h,d,"Path"){}
	}

	public class Node:Structure{
		public int s = -1;
		public bool dC = false;
		public Node(int x, int y, string t):base(x,y,1,1,0,t){
			
		}
	}
}
