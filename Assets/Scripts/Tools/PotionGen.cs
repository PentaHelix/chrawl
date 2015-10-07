using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PotionGen{
	private static List<string> potionShapes = new List<string>{"Cylinder", "Pointed", "Sphere"};
	private static List<string> potionEffects = new List<string>(){"Healing","Harming","Poison","Burning","Freezing","Shocking"};

	public static List<Color> potionColors = new List<Color>(){new Color(0,137,255), 
															   new Color(234,0,2), 
															   new Color(10,214,0), 
															   new Color(169,0,255), 
															   new Color(255, 0, 139), 
															   new Color(0, 248, 255)};

	private static Dictionary<string, Color> colorLookup = new Dictionary<string, Color>();

	public static Color GetColor(string effect){
		return colorLookup[effect];
	}

	public static string RandomEffect(){
		return potionEffects[Random.Range(0,potionEffects.Count)];
	}

	public static Color RandomColor(){
		return potionColors[Random.Range(0,potionColors.Count)];
	}

	public static string RandomShape(){
		return potionShapes[Random.Range(0,potionShapes.Count)];
	}

	public static void SetupPotions(){
		for(int cycles = 0; cycles < 50; cycles++){
			int i1 = Random.Range(0,potionColors.Count);
			int i2 = Random.Range(0,potionColors.Count);
			Color tmp = potionColors[i1];
			potionColors[i1] = potionColors[i2];
			potionColors[i2] = tmp;
		}

		int e = 0;
		foreach(Color c in potionColors){
			colorLookup.Add(potionEffects[e], c);
			e++;
		}
	}
}
