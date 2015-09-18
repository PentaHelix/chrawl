using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StrongholdGen:MapGen{
	[Range(0.0f,100.0f)]
	public int NUM_TORCHES = 80;

	[Range(0.0f,100.0f)]
	public int NUM_CHESTS = 40;

	[Range(0.0f,100.0f)]
	public int NUM_BOOKCASES = 60;

	[Range(0.0f,100.0f)]
	public int NUM_CHANDELIERS = 20;

	[Range(0.0f,0.1f)]
	public float freqEnemy;

	[Range(0.0f,0.1f)]
	public float freqChest;

	private int[,] mapData = new int[40,30];
	private int[,] objData = new int[40,30];

	private List<Vector4> doorways     = new List<Vector4>();

	private List<int> cellDoors        = new List<int>();

	override public void Make(int level){
		ClearMap();

		Room iRoom = new Room(18,13,5,5,1);
		rooms.Add(iRoom);
		structures.Add(iRoom);

		for(int i = 1; i < NUM_ROOMS; i++){
			if(!GenerateRoom())break;
		}

		List<Path> pCopy = new List<Path>(paths);

		foreach(Path p in pCopy){
		 	if(p.c == 1){
		 		int overflow = 0;
		 		while(!GenerateRoom(p) && overflow < 100)overflow++;
		 		paths.Remove(p);
		 	}
		}

		SetSpecials();

		MiniMap.Init(this);

		MakeBounds();
		MakeMapData();
		MakeWalls();
		MakeFloors();
		MakeCells();
		MakeDoorways();
		MakeSpecials();

		MakeStairways(level);

		MakeStairs();
		MakeTorches();
		MakeChests();
		MakeBookcases();
		MakeChandeliers();

		MakeShops();

		MakeEnemies();
		if(level == 0){
			Game.player.position = new Vector3(100,4,75);
		}else{
			Game.player.position = GameObject.Find("StairsUp(Clone)").transform.position + new Vector3(0,2f,0);
		}
		AstarPath.active.Scan();
	}

	private int[] GetEmpty(Structure s, string t = "Normal"){
		int d = 0;

		List<int[]> valid = new List<int[]>();

		for(int x = -1; x < s.w; x++){
			for(int y = -1; y < s.h; y++){
				if(y == 0 || y == -1)d = 0;
				if(x == s.w-1 || x == s.w)d = 1;
				if(y == s.h-1 || y == s.h)d = 2;
				if(x == 0 || x == -1)d = 3;
				switch(t){
					case "Normal":
						if(x == -1 || x == s.w || y == -1 || y == s.h)continue;
					break;
					case "Edge":
						if(x == -1 || x == s.w || y == -1 || y == s.h)continue;
						if(x != 0 && x != s.w-1 && y != 0 && y != s.h-1)continue;
						//if(StructureOn(new Vector2(x,y) + wallVec[d]) != null)continue;
					break;
					case "Surrounding":
						if(x != -1 && x != s.w && y != -1 && y != s.h)continue;
						//if(StructureOn(x,y) != null)continue;
					break;
				}
				int rX = x + s.x;
				int rY = y + s.y;
				if(rX < 0 || rX >= 40 || rY < 0 || rY >= 30)continue;
				//if(t == "Edge") Debug.Log(rX + " " + rY);
				if(objData[rX , rY] == 1)continue;
				valid.Add(new int[]{x, y, d});
			}	
		}

		if(valid.Count == 0)return null;
		return valid[(int)(valid.Count * Random.value)];
	}

	override public void ClearMap(){
		doorways   = new List<Vector4>();
	}

	private void SetSpecials(){
		List<int> roomIdx = new List<int>(4);
		while(roomIdx.Count < 4){
			int r = (int)(Random.value * rooms.Count);
			if(!roomIdx.Contains(r))roomIdx.Add(r);
		}

		rooms[roomIdx[0]].special = "Item";
		rooms[roomIdx[1]].special = "Shop";
		rooms[roomIdx[2]].special = "Water";
		rooms[roomIdx[3]].special = "Water";
	}

	private void MakeMapData(){
		foreach(Structure s in structures){
			for(int y = 0; y < s.h; y++){
				for(int x = 0; x < s.w; x++){
					mapData[s.x + x,s.y + y] = 1;
					if(s.special == "Water")mapData[s.x + x,s.y + y] = 2;
				}
			}
		}
	}

	private void MakeFloors(){
		for(int x = 0; x < 40; x++){
			for(int y = 0; y < 30; y++){
				GameObject floor;
				Structure s = StructureOn(x,y);
				float height = -7f;
				if(s == null || s.t == "Doorway"){
 					floor = Instantiate(Resources.Load("Prefabs/Objects/Cube")) as GameObject;
					floor.GetComponent<Renderer>().material = Instantiate(Resources.Load("Materials/Wall/Wall_Dark")) as Material;
 					floor.transform.localScale = new Vector3(5,5,5);
 					floor.transform.position = new Vector3(x*5 + 2.5f,height,y*5+2.5f);
					floor.transform.hideFlags = HideFlags.HideInHierarchy;
				}else{
					if(s.d == 1)height = -2f;
					if(s.t == "Path")height = -2f;
					if(s.t == "Node")height = -2f;
					if(mapData[x,y] == 2){
 						floor = Instantiate(Resources.Load("Prefabs/Structures/Water")) as GameObject;
 						objData[x,y] = 1;
 					}else{
 						floor = Instantiate(Resources.Load("Prefabs/Objects/Cube")) as GameObject;
						floor.GetComponent<Renderer>().material = Instantiate(Resources.Load("Materials/Floor/Floor02")) as Material;
 						floor.transform.localScale = new Vector3(5,5,5);
 						if(s.d == 1)height = -2f;

 						if(Random.value > 0.92 && s.special != "Shop"){
 							int id = (int)(Random.value * 4);
 							switch(id){
 								case 0:
		 							GameObject stones = Instantiate(Resources.Load("Prefabs/Structures/Stones")) as GameObject;
		 							Place(x,y,height + 2.4f, stones);
 								break;
 								case 1:
 									GameObject grave = Instantiate(Resources.Load("Prefabs/Structures/Grave")) as GameObject;
 									Place(x,y,height + 3.5f, grave);
 								break;
 								case 2:
 									//GameObject trap = Instantiate(Resources.Load("Prefabs/Structures/Trap")) as GameObject;
		 							//trap.transform.position = new Vector3(x*5 + 2.5f,height + 2.57f,y*5+2.5f);
		 							//trap.transform.hideFlags = HideFlags.HideInHierarchy;
		 							//strDataGround[x,y] = 1;
 								break;
 								case 3:
 									GameObject mush = Instantiate(Resources.Load("Prefabs/Structures/Mushrooms")) as GameObject;
 									Place(x,y,height + 2.57f, mush);
 								break;
 							}
 						}
 					}
 					Place(x,y,height, floor, false);
				}
			}	
		}
	}

	private void MakeCells(){
		foreach(Room r in rooms){
			if(r.c == 1 && r.special != "Water"){
				r.special = "Cell";
				int[] e = GetEmpty(r);
				if(e == null)continue;
				GameObject skeleton = Instantiate(Resources.Load("Prefabs/Structures/Skeleton")) as GameObject;
				Place(e[0] + r.x, e[1] + r.y, r.d*5-4.3f, skeleton);

				e = GetEmpty(r, "Edge");
				if(e == null)continue;
				GameObject torture = Instantiate(Resources.Load("Prefabs/Structures/Torture")) as GameObject;
				Place(e[0] + r.x, e[1] + r.y, r.d*5-2.8f, torture);
				torture.transform.eulerAngles = new Vector3(0,-e[2]*90,0);

				cellDoors.Add(r.doorway);
			}
		}
	}

	private void MakeDoorways(){
		int i = 0;
		foreach(Vector4 v in doorways){
			GameObject d = Instantiate(Resources.Load("Prefabs/Structures/Doorway")) as GameObject;
			Place((int)(v.x), (int)(v.y), 3f, d);
			d.transform.eulerAngles = new Vector3(0,-v.z*90-90,0);
			if(cellDoors.Contains(i))d.GetComponent<Doorway>().SetDoorStyle(2);
			i++;
		}
	}

	private void MakeSpecials(){
		foreach(Room r in rooms){
			if(r.special == "Item"){
				int x = (int)(Random.value * (r.w - 2)) + 1;
				int y = (int)(Random.value * (r.h - 2)) + 1;
				GameObject p = Instantiate(Resources.Load("Prefabs/Structures/Pedestal")) as GameObject;
				Place(r.x + x, r.y + y, r.d * 5f - 3.3f, p);
				Transform item = p.transform.Find("Item");
				Transform scroll = Drop.CreateScroll(item, true).transform;
				scroll.localEulerAngles = new Vector3(0,90,0);
				scroll.localScale = new Vector3(2,2,2);
			}
		}
	}

	private void MakeStairways(int level){
		GameObject stairs = Instantiate(Resources.Load("Prefabs/Objects/StairsDown")) as GameObject;
		int overflow = 0;
		int d = 0;
		while(overflow++ < 10000){
			Room r = rooms[(int)(rooms.Count * Random.value)];
			int[] e = GetEmpty(r, "Surrounding");
			if(e == null)continue;
			int x = e[0] + r.x;
			int y = e[1] + r.y;

			if(StructureOn(x, y) != null)continue;

			Destroy(GameObject.Find("Wall_"+x+"_"+y));
			Place(x,y,r.d == 1 ? -2f:-5f, stairs);
			stairs.transform.eulerAngles = new Vector3(0,90*-e[2],0);
			break;
		}

		stairs = Instantiate(Resources.Load("Prefabs/Objects/StairsUp")) as GameObject;
		overflow = 0;
		d = 0;
		while(overflow++ < 10000){
			Room r = rooms[(int)(rooms.Count * Random.value)];
			int[] e = GetEmpty(r, "Surrounding");
			if(e == null)continue;
			int x = e[0] + r.x;
			int y = e[1] + r.y;

			if(StructureOn(x, y) != null)continue;

			Destroy(GameObject.Find("Wall_"+x+"_"+y));
			Place(x,y,r.d == 1 ? -2f:-5f, stairs);
			stairs.transform.eulerAngles = new Vector3(0,90*-e[2],0);
			break;
		}
	}

	private void MakeStairs(){
		foreach(Node n in nodes){
			if(n.t == "Node")continue;
			GameObject stairs = Instantiate(Resources.Load("Prefabs/Objects/Stairs")) as GameObject;
			Place(n.x, n.y, -2f, stairs);
			switch(n.s){
				case 0:
					stairs.transform.eulerAngles  = new Vector3(0f,180f,0f);
				break;
				case 1:
					stairs.transform.eulerAngles  = new Vector3(0f,90f,0f);
				break;
				case 2:
					stairs.transform.eulerAngles  = new Vector3(0f,360f,0f);
				break;
				case 3:
					stairs.transform.eulerAngles  = new Vector3(0f,270f,0f);
				break;
			}
		}
	}

	private void MakeTorches(){
		for(int i = 0; i < NUM_TORCHES; i++){
			Room r = rooms[(int)(rooms.Count * Random.value)];
			int[] e = GetEmpty(r, "Edge");
			if(e == null)continue;
			GameObject torch = Instantiate(Resources.Load("Prefabs/Objects/Torch")) as GameObject;
			Place(r.x + e[0], r.y + e[1], 3f, torch);
			torch.transform.eulerAngles = new Vector3(0,90*-e[2],0);			
		}
	}

	private void MakeChests(){
		for(int i = 0; i < NUM_CHESTS; i++){
			Room r = rooms[(int)(rooms.Count * Random.value)];
			int[] e = GetEmpty(r, "Edge");
			if(e == null)continue;
			GameObject chest = Instantiate(Resources.Load("Prefabs/Objects/Chest")) as GameObject;
			Place(r.x + e[0], r.y + e[1], r.d*5-4f, chest);
			chest.transform.eulerAngles = new Vector3(0,90*-e[2],0);			
		}
	}

	private void MakeBookcases(){
		for(int i = 0; i < NUM_BOOKCASES; i++){
			Room r = rooms[(int)(rooms.Count * Random.value)];
			int[] e = GetEmpty(r, "Edge");
			if(e == null)continue;
			GameObject bCase = Instantiate(Resources.Load("Prefabs/Structures/Bookcase")) as GameObject;
			Place(r.x + e[0], r.y + e[1], r.d*5-4, bCase);
			bCase.transform.eulerAngles = new Vector3(0,90*-e[2],0);			
		}
	}

	private void MakeChandeliers(){
		for(int i = 0; i < NUM_CHANDELIERS; i++){
			Room r = rooms[(int)(rooms.Count * Random.value)];
			int[] e = GetEmpty(r);
			if(e == null)continue;
			GameObject chandelier = Instantiate(Resources.Load("Prefabs/Structures/Chandelier")) as GameObject;
			Place(r.x + e[0], r.y + e[1], 4.3f, chandelier);
			chandelier.transform.eulerAngles = new Vector3(0,90*-e[2],0);			
		}
	}

	private void MakeEnemies(){
		for(int i = 0; i < NUM_CHANDELIERS; i++){
			Room r = rooms[(int)(rooms.Count * Random.value)];
			int[] e = GetEmpty(r);
			if(e == null)continue;
			GameObject cube = Instantiate(Resources.Load("Prefabs/Enemies/Gelatinous_Cube")) as GameObject;
			Place(r.x + e[0], r.y + e[1], 2, cube);
			cube.transform.eulerAngles = new Vector3(0,90*-e[2],0);		
		}
	}

	private void MakeShops(){
		foreach(Room r in rooms){
			if(r.special != "Shop")continue;
			int overflow = 0;
			while(overflow++ < 1000){
				if(Random.value > 0.5){
					bool yD = Random.value > 0.5;
					int x = (int)(Random.value * (r.w - 2) + r.x);
					int y = (int)((yD ? -1 : r.h) + r.y);
					if(GetMapVal(x,y) != 0 || GetMapVal(x+1,y) != 0)continue;
					GameObject shop = Instantiate(Resources.Load("Prefabs/Structures/Shop")) as GameObject;
					GameObject.Find("Wall_"+x+"_"+y).transform.localScale = new Vector3(5,5,5);
					GameObject.Find("Wall_"+(x+1)+"_"+y).transform.localScale = new Vector3(5,5,5);
					GameObject.Find("Wall_"+x+"_"+y).GetComponent<Renderer>().material.mainTextureScale = new Vector3(1,1,1);
					GameObject.Find("Wall_"+(x+1)+"_"+y).GetComponent<Renderer>().material.mainTextureScale = new Vector3(1,1,1);
					if(r.d == 0){
						shop.transform.position = new Vector3(x*5 + 5, -4.5f, y*5 + 2.5f);
						GameObject.Find("Wall_"+x+"_"+y).transform.position += new Vector3(0,2.5f,0);
						GameObject.Find("Wall_"+(x+1)+"_"+y).transform.position += new Vector3(0,2.5f,0);
					}else{
						shop.transform.position = new Vector3(x*5 + 5, .5f, y*5 + 2.5f);
						GameObject.Find("Wall_"+x+"_"+y).transform.position += new Vector3(0,-2.5f,0);
						GameObject.Find("Wall_"+(x+1)+"_"+y).transform.position += new Vector3(0,-2.5f,0);
					}
					if(yD){
						shop.transform.localEulerAngles = new Vector3(270,270,0);
					}else{
						shop.transform.localEulerAngles = new Vector3(270,90,0);
					}
					break;
				}else{
					bool xD = Random.value > 0.5;
					int x = (int)((xD ? -1 : r.w) + r.x);
					int y = (int)(Random.value * (r.h - 2) + r.y);
					if(GetMapVal(x,y) != 0 || GetMapVal(x,y+1) != 0)continue;
					GameObject shop = Instantiate(Resources.Load("Prefabs/Structures/Shop")) as GameObject;
					shop.transform.position = new Vector3(x*5 + 2.5f, 0.5f, y*5 + 5);
					GameObject.Find("Wall_"+x+"_"+y).transform.localScale = new Vector3(5,5,5);
					GameObject.Find("Wall_"+x+"_"+(y+1)).transform.localScale = new Vector3(5,5,5);
					GameObject.Find("Wall_"+x+"_"+y).GetComponent<Renderer>().material.mainTextureScale = new Vector3(1,1,1);
					GameObject.Find("Wall_"+x+"_"+(y+1)).GetComponent<Renderer>().material.mainTextureScale = new Vector3(1,1,1);
					if(r.d == 0){
						shop.transform.position = new Vector3(x*5 + 2.5f, -4.5f, y*5 + 5);
						GameObject.Find("Wall_"+x+"_"+(y+1)).transform.position += new Vector3(0,2.5f,0);
						GameObject.Find("Wall_"+x+"_"+y).transform.position += new Vector3(0,2.5f,0);
					}else{
						shop.transform.position = new Vector3(x*5 + 2.5f, .5f, y*5 + 5);
						GameObject.Find("Wall_"+x+"_"+y).transform.position += new Vector3(0,-2.5f,0);
						GameObject.Find("Wall_"+x+"_"+(y+1)).transform.position += new Vector3(0,-2.5f,0);
					}
					if(xD){
						shop.transform.localEulerAngles = new Vector3(270,0,0);
					}else{
						shop.transform.localEulerAngles = new Vector3(270,180,0);
					}

					break;
				}
			}
		}
	}

	private bool GenerateRoom(Structure baseStruct = null){
		Structure ste = null;
		Structure str = null;
		Vector2 n = new Vector2();
		int overflow = 0;
		string t  = "";
		int d = 0;
		int a = 0;
		while(++overflow < 1000){
			ste = baseStruct ?? structures[(int)(Random.value * structures.Count)];
			t = Random.value > 0.3 ? "Room" : "Path";
			if(ste.t == "Room" && t == "Room")t = "Path";
			if(ste.t == "Node" || ste.t == "Doorway")continue;
			if(baseStruct != null)t = "Room";
			d = (int)Mathf.Floor(Random.value * 4);
			int x = 0;
			int y = 0;
			int w = 0;
			int h = 0;

			switch(d){
				case 0:
					a = (int)(Random.value*ste.w);
					n = new Vector2(ste.x + a, ste.y - 1);

					if(t == "Room"){
						w = Mathf.Max(3, (int)(Random.value * 7));
						h = Mathf.Max(3, (int)(Random.value * 7));
						x = (int)(ste.x + a - Random.value * (w-1));
						y = (int)ste.y - h - 1;
					}else{
						w = (int)1;
						h = (int)Mathf.Max(4, Random.value * 9);
						x = (int)ste.x + a;
						y = (int)ste.y - h - 1;
					}
				break;

				case 1:
					a = (int)(Random.value*ste.h);
					n = new Vector2(ste.x + ste.w, ste.y + a);
					if(t == "Room"){
						w = Mathf.Max(3, (int)(Random.value * 7));
						h = Mathf.Max(3, (int)(Random.value * 7));
						x = (int)ste.x + ste.w + 1;
						y = (int)ste.y + a;
					}else{
						w = (int)Mathf.Max(4, Random.value * 9);
						h = (int)1;
						x = (int)ste.x + ste.w + 1;
						y = (int)(ste.y + a - Random.value * (h-1));
					}
				break;

				case 2:
					a = (int)(Random.value*ste.w);
					n = new Vector2(ste.x + a, ste.y + ste.h);
					if(t == "Room"){
						w = Mathf.Max(3, (int)(Random.value * 7));
						h = Mathf.Max(3, (int)(Random.value * 7));
						x = (int)(ste.x + a - Random.value * (w-1));
						y = (int)ste.y + ste.h + 1;
					}else{
						w = (int)1;
						h = (int)Mathf.Max(4, Random.value * 9);
						x = (int)ste.x + a;
						y = (int)ste.y + ste.h + 1;
					}
				break;

				case 3:
					a = (int)(Random.value*ste.h);
					n = new Vector2(ste.x - 1, ste.y + a);
					if(t == "Room"){
						w = Mathf.Max(3, (int)(Random.value * 7));
						h = Mathf.Max(3, (int)(Random.value * 7));
						x = (int)ste.x - w - 1;
						y = (int)(ste.y + a - Random.value * (h-1));
					}else{
						w = (int)Mathf.Max(4, Random.value * 9);
						h = (int)1;
						x = (int)ste.x - w - 1;
						y = (int)ste.y + a;
					}
				break;
			}

			if(t == "Room"){
				str = new Room(x,y,w,h, Random.value > 0.7 ? 0 : 1);
			}else if(t == "Path"){
				str = new Path(x,y,w,h, Random.value > 0.7 ? 0 : 1);
			}

			bool invalid = false;
			foreach(Structure s in structures){
				if(str.Intersects(s))invalid = true;
			}
			if(!invalid){
				//Debug.Log(d);
				break;
			}
		}

		if(overflow == 1000){
			return false;
		}

		if(t == "Room"){
			Room r = (Room)(str);
			rooms.Add(r);
		}

		if(t == "Path"){
			paths.Add((Path)(str));
		}

		if(t == "Room" && ste.t == "Path"){
			doorways.Add(new Vector4((n - wallVec[d]).x, (n - wallVec[d]).y, Opposite(d), 1));
			((Room)(str)).doorway = doorways.Count-1;
		}else if(t == "Path" && ste.t == "Room"){
			doorways.Add(new Vector4((n + wallVec[d]).x, (n + wallVec[d]).y, d, 1));
			((Room)(ste)).doorway = doorways.Count-1;
		}
		structures.Add(str);
		string nType = "";

		if((str.t == "Room" && ste.t == "Path" && str.d == 0) || (str.t == "Path" && ste.t == "Room" && ste.d == 0)){
			nType = "Connector";
		}else{
			nType = "Node";
		}

		Node node = new Node((int)(n.x), (int)(n.y), nType);
		if(ste.t == "Room")node.s = d;
		if(str.t == "Room")node.s = Opposite(d);
		nodes.Add(node);
		structures.Add(node);
		str.nodes.Add(node);
		ste.nodes.Add(node);
		ste.c++;
		return true;
	}
}
