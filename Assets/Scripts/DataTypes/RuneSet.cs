using UnityEngine;
using System.Collections;

public class RuneSet{
	public int strg;
	public int stat;
	public int proj;
	public int spec;

	public string rID = "";

	public RuneSet(int s1, int s2, int s3, int s4){
		strg = s1;
		stat = s2;
		proj = s3;
		spec = s4;

		rID = s1 + "." + s2 + "." + s3 + "." + s4;
	}
}
