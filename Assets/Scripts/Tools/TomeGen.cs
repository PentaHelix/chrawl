using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class TomeGen{

	public static RuneSet GetRuneSet(){
		int strg = Random.Range(0,2);
		int stat = Random.Range(0,2);
		int spec = Random.Range(0,2);
		int proj = Random.Range(1,6);
		
		return new RuneSet(strg+10,stat+20,proj,spec+30);
	}
}