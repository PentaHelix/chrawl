using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Runes{
	public static Dictionary<int, string> RUNE_NAME = new Dictionary<int, string>(){{0,  "Vodcur"},
																					{1,  "One"},
																					{2,  "Two"},
																					{3,  "Three"},
																					{4,  "Four"},
																					{5,  "Five"},

																					{10, "Rotomatur"},
																					{11, "Vitar"},

																					{20, "Accignar"},
																					{21, "Adtomar"},
																					{22, "Glaciar"},

																					{30, "Reqconur"},
																					{31, "Traicatur"},
																					{32, "Clipear"},
																					{33, "Inflidar"}};
	public static Texture2D GetRuneTexture(int id){
		string file;
		if(id > 0 && id < 10){
			file = "Runes/Numbers/"+RUNE_NAME[id];
		}else{
			file = "Runes/"+RUNE_NAME[id];
		}

		return MonoBehaviour.Instantiate(Resources.Load(file)) as Texture2D;
	}
}
