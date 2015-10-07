using UnityEngine;
using System.Collections;

public class Bookcase : MonoBehaviour, IIgnitable {
	Texture2D tex;
	void Start(){
		tex = Instantiate(GetComponent<Renderer>().material.mainTexture) as Texture2D;
		int i = 0;
		while(i < 64){
			int w = (int)(Random.value * 2)+1;
			Color c = GetColor();
			i += w;
			for(int x = 0; x < w; x++){
				tex.SetPixel(i + x, 61, c);
				tex.SetPixel(i + x, 62, c);
				tex.SetPixel(i + x, 63, c);
			}
		}
		tex.Apply();
		GetComponent<Renderer>().material.mainTexture = tex;
	}

	private Color GetColor(){
		return new Color(Random.value * 0.5f, Random.value * 0.5f, Random.value * 0.5f);
	}

	public void OnIgnite(float dur){
		transform.root.Find("Fire").GetComponent<ParticleSystem>().Play();
		Invoke("OnExpire", dur);
	}

	public void OnExpire(){
		Destroy(transform.parent.gameObject);
	}
}
