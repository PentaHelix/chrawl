// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;

// public class MapGenerator : MonoBehaviour {
// 	public GameObject player;
// 	public Material floorMaterial;
// 	public Material wallMaterial;
// 	public int[,] mapData = new int[40,30];
// 	public int[,] chests = new int[40,30];
// 	private List<Room> rooms = new List<Room>();
// 	private List<Room> connectors = new List<Room>();
// 	private bool stop = false;
// 	private int roomCount = 30;
// 	public float freqEnemy = .08f;
// 	public float freqChest = .03f;
// 	void Start () {
// 		var ceil = GameObject.CreatePrimitive(PrimitiveType.Cube);
// 		ceil.transform.localScale = new Vector3(200, 1, 150);
// 		ceil.transform.position = new Vector3(100,6,75);
// 		Material cMat = Instantiate(wallMaterial) as Material;
// 		cMat.mainTextureScale = new Vector2(40,30);
// 		ceil.GetComponent<Renderer>().material = cMat;
// 		Init();
// 	}

// 	void Init(){
// 		Room initialRoom = new Room(18,13,4,4, "Hall");
// 		rooms.Add(initialRoom);
// 		for(int i = 0; i < roomCount; i++){
// 			GenerateRoom();
// 			if(stop == true)break;
// 		}

// 		List<Room> roomsCopy = new List<Room>(rooms);

// 		foreach(Room r in roomsCopy){
// 			if(r.t == "Path" && r.c == 1){
// 				int overflow = 0;
// 				while(!GenerateRoom(r) && overflow <= 100)overflow++;
// 			}
// 		}

// 		MakeMapData();
// 		MakeWalls();
// 		MakeFloors();
// 		MakeStairs();
// 		MiniMap.MakeTexture(mapData);
// 	}

// 	void MakeMapData(){
// 		foreach(Room room in rooms){
// 			for(int y = 0; y < room.h;y++){
// 				for(int x = 0; x < room.w;x++){
// 					mapData[room.x + x, room.y + y] = 1;
// 				}	
// 			}
// 		}
// 	}

// 	void MakeWalls(){
// 		for(int x = 0; x < 40; x++){
// 			for(int y = 0; y < 30; y++){
// 				if(mapData[x,y] != 1){
// 					GameObject cube = (GameObject)GameObject.CreatePrimitive(PrimitiveType.Cube);
// 					cube.transform.localScale = new Vector3(5,10,5);
// 					cube.transform.position = new Vector3(x*5+2.5f,.5f,y*5+2.5f);
// 					cube.GetComponent<Renderer>().material = Instantiate(wallMaterial) as Material;
// 					cube.GetComponent<Renderer>().material.mainTextureScale = new Vector3(1,2,1);
// 				}else{
// 					if(Random.value < freqEnemy){
// 						GameObject gelatCube = (GameObject)Instantiate(Resources.Load("Prefabs/Enemies/Gelationus_Cube"));
// 						gelatCube.transform.position =  new Vector3(x*5+2.5f,3f,y*5+2.5f);
// 						gelatCube.GetComponent<SeekPlayer>().target = transform;
// 					}else if(x >= 1 && x <= 38 && y >= 1 && y <= 28){
// 						if(OnStructure(x,y) != "Hall")continue;
// 						Debug.Log(OnStructure(x,y));
// 						//if((mapData[x+1,y]!=1 && mapData[x-1,y]!=1 && mapData[x,y+1]!=1 && mapData[x,y-1]!=1) || Random.value >= freqChest)continue;
// 						Debug.Log(x + " " + y);
// 						if((mapData[x+1,y]!=1 && mapData[x-1,y]!=1 && mapData[x,y+1]!=1 && mapData[x,y-1]!=1))continue;

// 						GameObject chest = Instantiate(Resources.Load("Prefabs/Objects/Chest"), new Vector3(x*5+2.5f,3f,y*5+2.5f), Quaternion.identity) as GameObject;
// 						chest.transform.eulerAngles += new Vector3(270f,0f,0f);
// 						if(mapData[x+1,y]!=1){

// 						}else if(mapData[x-1,y]!=1){

// 						}else if(mapData[x,y+1]!=1){

// 						}else if(mapData[x,y-1]!=1){

// 						}
// 					}
// 				}
// 			}

// 		}

// 		foreach(Node node in connectors){
// 			GameObject torch = (GameObject)Instantiate(Resources.Load("Prefabs/Objects/Torch"));
// 			float modX = 0;
// 			float modZ = 0;
// 			switch(node.q){
// 				case 0:
// 					modZ = 2.3f;
// 				break;

// 				case 1:
// 					modX = 2.3f;
// 				break;

// 				case 2:
// 					modZ = -2.3f;
// 				break;

// 				case 3:
// 					modX = -2.3f;
// 				break;
// 			}
// 			torch.transform.position = new Vector3(node.x*5+2.5f + modX,3.5f,node.y*5+2.5f + modZ);
// 			torch.transform.eulerAngles = new Vector3(290, (node.q-2) * 90,270);
// 		}
// 		transform.position = new Vector3(100, 4, 75);
// 		InitPathFinding();
// 	}


// 	void MakeFloors(){
// 		foreach(Room r in rooms){
// 			float height = -2f;
// 			if(r.t == "Hall")height = -7f;
// 			if(r.t == "Doorway")continue;
// 			for(int y = r.y; y < r.h + r.y; y++){
// 				for(int x = r.x; x < r.w + r.x; x++){
// 					GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
// 					floor.transform.localScale = new Vector3(5,5,5);
// 					floor.transform.position = new Vector3(x*5 + 2.5f,height,y*5+2.5f);
// 					floor.GetComponent<Renderer>().material = Instantiate(floorMaterial) as Material;
// 				}
// 			}
// 		}
// 	}

// 	void MakeStairs(){
// 		foreach(Node n in connectors){
// 			if(n.s == -1)continue;
// 			GameObject stairs = Instantiate(Resources.Load("Prefabs/Objects/Stairs")) as GameObject;
// 			stairs.transform.localPosition = new Vector3(n.x*5f + 2.5f, -2f, n.y*5f + 2.5f);
// 			switch(n.s){
// 				case 0:
// 					//stairs.transform.localPosition = new Vector3(n.x*5f + 2.5f, -2f, n.y*5f + 2.5f + 5f);
// 					stairs.transform.eulerAngles  = new Vector3(0f,180f,0f);
// 				break;
// 				case 1:
// 					//stairs.transform.localPosition = new Vector3(n.x*5f + 2.5f - 5f, -2f, n.y*5f + 2.5f);
// 					stairs.transform.eulerAngles  = new Vector3(0f,90f,0f);
// 				break;
// 				case 2:
// 					//stairs.transform.localPosition = new Vector3(n.x*5f + 2.5f, -2f, n.y*5f + 2.5f - 5f);
// 					stairs.transform.eulerAngles  = new Vector3(0f,3600f,0f);
// 				break;
// 				case 3:
// 					//stairs.transform.localPosition = new Vector3(n.x*5f + 2.5f + 5f, -2f, n.y*5f + 2.5f);
// 					stairs.transform.eulerAngles  = new Vector3(0f,270f,0f);
// 				break;
// 			}
// 		}
// 	}
	
// 	// 0
// 	//3 1
// 	// 2

// 	bool GenerateRoom(Room rte = null){
// 		Room roomToExtend = rte;
// 		Room room = null;
// 		Vector2 node = new Vector2();
// 		int overFlow = 0;
// 		int dir = 0;
// 		while(overFlow < 1000){
// 			overFlow++;
// 			if(rte == null)roomToExtend = rooms.ElementAt((int)(Mathf.Floor(Random.value * rooms.ToArray().Length)));

// 			string roomType = Random.value > 0.5 ? "Hall" : "Path";

// 			if(roomToExtend.t == "Hall" && roomType == "Hall")roomType = "Path";
// 			if(rte != null)roomType = "Hall";
// 			dir = (int)Mathf.Floor(Random.value * 4);
// 			int x = 0;
// 			int y = 0;
// 			int width = 0;
// 			int height = 0;
// 			int attach = 0;

// 			if(roomToExtend.t == "Node"){
// 				continue;
// 			}

// 			switch(dir){
// 				case 0:
// 				attach = (int)Mathf.Floor(Random.value*roomToExtend.w);
// 				node = new Vector2(roomToExtend.x + attach, roomToExtend.y -1);
// 				if(roomType == "Hall"){
// 					width  = Mathf.Max(3, (int)Mathf.Ceil (Random.value * 7));
// 					height = Mathf.Max(3, (int)Mathf.Ceil (Random.value * 7));
// 					x      = (int)(roomToExtend.x + attach - Mathf.Floor (Random.value * width));
// 					y      = (int)roomToExtend.y - height - 1;
// 				}else{
// 					width  = (int)1;
// 					height = (int)Mathf.Ceil (Random.value * 9);
// 					x      = (int)roomToExtend.x + attach;
// 					y      = (int)roomToExtend.y - height - 1;
// 				}
// 				break;

// 				case 1:
// 				attach = (int)Mathf.Floor(Random.value*roomToExtend.h);
// 				node = new Vector2(roomToExtend.x + roomToExtend.w, roomToExtend.y + attach);
// 				if(roomType == "Hall"){
// 					width  = Mathf.Max(3, (int)Mathf.Ceil (Random.value * 7));
// 					height = Mathf.Max(3, (int)Mathf.Ceil (Random.value * 7));
// 					x      = (int)roomToExtend.x + roomToExtend.w + 1;
// 					y      = (int)roomToExtend.y + attach;
// 				}else{
// 					width  = (int)Mathf.Ceil (Random.value * 9);
// 					height = (int)1;
// 					x      = (int)roomToExtend.x + roomToExtend.w + 1;
// 					y      = (int)roomToExtend.y + attach;
// 				}
// 				break;

// 				case 2:
// 				attach = (int)Mathf.Floor(Random.value*roomToExtend.w);
// 				node = new Vector2(roomToExtend.x + attach, roomToExtend.y + roomToExtend.h);
// 				if(roomType == "Hall"){
// 					width  = Mathf.Max(3, (int)Mathf.Ceil (Random.value * 7));
// 					height = Mathf.Max(3, (int)Mathf.Ceil (Random.value * 7));
// 					x      = (int)(roomToExtend.x + attach - Mathf.Floor (Random.value * width));
// 					y      = (int)roomToExtend.y + height - 1;
// 				}else{
// 					width  = (int)1;
// 					height = (int)Mathf.Ceil (Random.value * 9);
// 					x      = (int)roomToExtend.x + attach;
// 					y      = (int)roomToExtend.y + roomToExtend.h + 1;
// 				}
// 				break;

// 				case 3:
// 				attach = (int)Mathf.Floor(Random.value*roomToExtend.h);
// 				node = new Vector2(roomToExtend.x - 1, roomToExtend.y + attach);
// 				if(roomType == "Hall"){
// 					width  = Mathf.Max(3, (int)Mathf.Ceil (Random.value * 7));
// 					height = Mathf.Max(3, (int)Mathf.Ceil (Random.value * 7));
// 					x      = (int)roomToExtend.x - width - 1;
// 					y      = (int)roomToExtend.y + attach;
// 				}else{
// 					width  = (int)Mathf.Ceil(Random.value * 9);
// 					height = (int)1;
// 					x      = (int)roomToExtend.x - width - 1;
// 					y      = (int)roomToExtend.y + attach;
// 				}
// 				break;
// 			}
// 			room = new Room(x,y,width,height, roomType);
// 			bool invalid = false;
// 			if(roomType == "Hall"){
// 				width = Mathf.Max(width, 3);
// 				height = Mathf.Max(height, 3);
// 			}

// 			foreach(Room r in rooms){
// 				if(room.intersects(r))invalid = true;
// 			}
// 			if(!invalid){
// 				break;
// 			}
// 		}

// 		if(overFlow >= 1000){
// 			stop = true;
// 			return false;
// 		}

// 		rooms.Add(room);
// 		Node connector = new Node((int)node.x, (int)node.y, 1, 1, dir, room.t == roomToExtend.t ? "Node" : "Doorway");
// 		if(roomToExtend.t == "Hall")connector.s = dir;
// 		if(room.t == "Hall")connector.s = OppositeDirection(dir);
// 		connectors.Add(connector);
// 		rooms.Add(connector);
// 		roomToExtend.c++;
// 		return true;
// 	}

// 	void InitPathFinding(){
// 		AstarPath.active.Scan();
// 	}

// 	int OppositeDirection(int d){
// 		if(d==0)return 2;
// 		if(d==1)return 3;
// 		if(d==2)return 0;
// 		if(d==3)return 1;
// 		return -1;
// 	}

// 	string OnStructure(int x, int y){
// 		foreach(Room r in rooms){
// 			if(r.x <= x && r.x + r.w >= x && r.y <= y && r.y + r.h <= y){
// 				return r.t;
// 			}
// 		}
// 		return null;
// 	}
// }

// public class Room{
// 	public int x;
// 	public int y;
// 	public int w;
// 	public int h;
// 	public int c;
// 	public string t;

// 	public Room(int x, int y, int w, int h, string t){
// 		this.x = x;
// 		this.y = y;
// 		this.w = w;
// 		this.h = h;
// 		this.t = t;
// 		//connected rooms
// 		this.c = 1;
// 	}

// 	public bool intersects(Room r){
// 		if(x < 0 || y < 0 || x + w > 40 || y + h > 30)return true;
// 		return !(r.x > x + w || r.x + r.w < x || r.y > y + h || r.y + r.h < y);
// 	}
// }

// public class Node:Room{
// 	//rotation for torches
// 	public int q;
// 	//position for stairs
// 	public int s = -1;

// 	public Node(int x, int y, int w, int h, int q, string t):base(x,y,w,h,t){
// 		this.q = 3-q;
// 	}
// }