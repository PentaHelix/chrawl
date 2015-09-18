using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ScrollGen{
	private static List<int> pRunes = new List<int>(){30,32,33};
	private static Dictionary<int, int[]> sRunes = new Dictionary<int, int[]>(){{30,new[]{11}},
																				{32,new[]{20,21,22}},
																				{33,new[]{20,21,22}}};
	public static int[] GetRunes(){
		int i = (int)(Random.value * pRunes.Count);
		//int rune1 = pRunes[i];
		//int rune2 = sRunes[pRunes[i]][(int)(Random.value * sRunes[pRunes[i]].Length)];
		int rune1 = 33;
		int rune2 = 22;
		return new[]{rune1, rune2};
	}
}
