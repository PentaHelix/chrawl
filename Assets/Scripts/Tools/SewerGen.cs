using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SewerGen:MapGen{
	override public void Make(int level){
		ClearMap();

		Path iPath = new Path(1,1,3,7,1);
		paths.Add(iPath);
		structures.Add(iPath);

		MakeMapData();
		MakeBounds();
		MakeWalls();
	}

	private void MakeMainPath(){
		while(GeneratePath()){}
	}

	private bool GeneratePath(){
		int d = 0;
		int overflow = 0;
		Path pte = paths[paths.Count-1];
		while(++overflow < 1000){
			d = (int)(Random.value * 4);
			switch(d){
				case 0:
					int w = 3;
					int h = Math.max((int)(Random.value*8), 4);
					int x = Random.value > 0.5 ? p.x + p.w : p.x-3;
					int y = p.y-1-h;
				break;
				case 1:
					int w = Math.max((int)(Random.value*8), 4);
					int h = 3;
					int x = p.x + p.w;
					int y = p.y;
				break;

				case 2:
					int w = 3;
					int h = Math.max((int)(Random.value*8), 4);
					int x = Random.value > 0.5 ? p.x + pw : p.x-3;
					int y = p.y+3;
				break;

				case 3:
					int w = Math.max((int)(Random.value*8), 4);
					int h = 3;
					int x = p.x-1-w;
					int y = p.y-1;
				break;
			}
		}
		if(overflow == 1000)return false;
	}

	private 
}
