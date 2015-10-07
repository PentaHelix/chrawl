using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spell : MonoBehaviour {
	//EFFECT FLAGS
	//How many projectiles (1-10)
	[Range(0,1)]
	public float feNumProjectiles = 0.0f;
	//How much Homing
	[Range(0,1)]
	public float feHoming = 0.0f;
	//How likely to Chain
	[Range(0,1)]
	public float feChaining = 0.0f;
	//How much freeze
	[Range(0,1)]
	public float feFreeze = 0.0f;
	//How much fire
	[Range(0,1)]
	public float feFire = 0.0f;
	//How much stun
	[Range(0,1)]
	public float feStun = 0.0f;
	//How likely to pierce
	[Range(0,1)]
	public float fePiercing = 0.0f;
	//how much mana cost
	[Range(0,1)]
	public float feMana = 0.0f;
	//how quickly do the spellecules fly
	[Range(0,1)]
	public float feVelocity = 0.0f;
	//how much do the spellecules de/accelerate
	[Range(0,1)]
	public float feAcceleration = 0.0f;
	//how long the spellecules exist
	[Range(0,1)]
	public float feLifetime = 0.0f;
	//how far apart the spellecules are spread
	[Range(0,1)]
	public float feSpread = 0.0f;
	//how much damage do spellecules deal
	[Range(0,1)]
	public float feDamage = 0.0f;


	//VISUAL FLAGS
	[Range(0,1)]
	public float fvRed = 0.0f;

	[Range(0,1)]
	public float fvGreen = 0.0f;

	[Range(0,1)]
	public float fvBlue = 0.0f;

	[Range (0,1)]
	public float fvSize = 0.0f;

	[Range (0,1)]
	public float fvTrail = 0.0f;

	[Range (0,1)]
	public float fvFade = 0.0f;

	public List<Spellecule> spellecules = new List<Spellecule>();

	void Start(){

	}
	
	void Update () {
		if(feHoming != 0){
			foreach(Spellecule sc in spellecules){
				if(sc.homeTowards == null)continue;
				Vector3 distance = (sc.homeTowards.transform.position - sc.transform.position);
				distance = distance.normalized * (100f - distance.magnitude)/10000f * feHoming;
				sc.velocity = (sc.velocity + distance).normalized * 0.1f;
			}
		}
	}

	void Generate(){
		feNumProjectiles = Mathf.Max(0.1f, Random.value);
		//feHoming = Random.value;
		feHoming = 0;
		feChaining = Random.value;
		feFreeze = Random.value;
		feFire = Random.value;
		feStun = Random.value;
		fePiercing = Random.value;
		feMana = Random.value;
		feAcceleration = Random.value;
		feVelocity = Mathf.Max(0.4f,Random.value);
		feLifetime = Mathf.Max(0.55f, Random.value);
		feSpread = Mathf.Max(0.6f, Random.value);
		feDamage = Mathf.Max(0.4f, Random.value);

		fvRed = Random.value;
		fvGreen = Random.value;
		fvBlue = Random.value;
		fvSize = Mathf.Max(0.1f,Random.value);
		fvTrail = Mathf.Max(0.3f,Random.value);
		fvFade = Random.value;


		feHoming = feHoming > 0.4 ? feHoming : 0;
		fePiercing = fePiercing > 0.5 ? fePiercing : 0;
	}

	void Initiate(){
		int numProjectiles = (int)(feNumProjectiles * 10);
		float angleIncrement = 360 / (float)(numProjectiles);
		float angle = 90;
		for(int p = 0; p < numProjectiles; p++){
			GameObject spellecule = Instantiate(Resources.Load("Prefabs/Magic/Spellecule"), transform.position, Quaternion.identity) as GameObject;
			Spellecule sc = spellecule.GetComponent<Spellecule>();
			spellecules.Add(sc);
			sc.parentSpell = this;
			Vector3 vMod = Game.player.GetComponent<CharacterController>().velocity * .4f;

			Vector3 veloc = ((transform.forward + transform.right * (-feSpread/2f + feSpread *p/(numProjectiles-1)))+vMod).normalized * feVelocity;

			if(numProjectiles == 1){
				veloc = transform.forward * feVelocity/3f;
			}

			sc.velocity = veloc;
			angle += angleIncrement;

			//Visual
			spellecule.GetComponent<ParticleSystem>().startSize = fvSize;
			spellecule.GetComponent<ParticleSystem>().startSpeed = fvSize * 40f;
			spellecule.GetComponent<ParticleSystem>().startColor = new Color(fvRed, fvGreen, fvBlue);
			spellecule.GetComponent<ParticleSystem>().startLifetime = fvTrail / 2f;
			spellecule.GetComponent<SphereCollider>().radius = fvSize;
			spellecule.GetComponent<ParticleSystem>().Play();

			spellecule.transform.name = "Spellecule";

			sc.Invoke("Kill", feLifetime * 4f);

			ParticleSystem de = spellecule.transform.Find("DeathParticle").GetComponent<ParticleSystem>();
			de.Stop();
			de.startSize = fvSize;
			de.startColor = new Color(fvRed, fvGreen, fvBlue, 1);
			de.startLifetime = feLifetime;
			de.startSpeed = 1f;

			Light l = spellecule.transform.Find("Light").GetComponent<Light>();
			l.color = new Color(fvRed, fvGreen, fvBlue, 1);
			de.Stop();
			de.startSize = fvSize;
			de.startColor = new Color(fvRed, fvGreen, fvBlue, 1);
			de.startLifetime = feLifetime;
			de.startSpeed = 1f;
			if(feHoming != 0){
				sc.setLocked();
			}
		}
	}

	public void DestroySpellecules(){
		foreach(Spellecule sc in spellecules){
			Destroy(sc.gameObject);
			spellecules.Remove(sc);
		}
	}

	public void Cast(){
		Initiate();
	}

	public void FromRuneSet(RuneSet r){
		string stat = Runes.RUNE_NAME[r.stat];
		string spec = Runes.RUNE_NAME[r.spec];
		feNumProjectiles = r.proj/10f;

		feFire   = 0;
		feStun   = 0;
		feFreeze = 0;

		if(stat == "Accignar"){
			feFire = Mathf.Max(0.4f,Random.value);
		}else if(stat == "Adtomar"){
			feStun = Mathf.Max(0.4f,Random.value);
		}

		feMana = Random.value;
		feAcceleration = Random.value;
		feLifetime = Mathf.Max(0.55f, Random.value);
		feSpread = Mathf.Max(0.6f, Random.value);
		feVelocity = Mathf.Max(0.4f,Random.value);

		feHoming   = 0;
		fePiercing = 0;
		feChaining = 0;

		if(spec == "Traicatur"){
			fePiercing = Mathf.Max(0.4f, Random.value);
		}else if(spec == "Reqconur"){
			//feHoming = Mathf.Max(0.4f, Random.value);
		}

		feDamage = Mathf.Max(0.4f, Random.value);
		fvRed = Random.value;
		fvGreen = Random.value;
		fvBlue = Random.value;

		if(stat == "Accignar"){
			fvRed = Mathf.Max(0.6f, Random.value);
			fvGreen = Mathf.Min(0.3f, Random.value);
			fvBlue = Mathf.Min(0.3f, Random.value);
		}else if(stat == "Adtomar"){
			fvRed = Mathf.Min(0.7f, Random.value);
			fvGreen = fvRed;
			fvBlue = Mathf.Max(0.8f, Random.value);
		}

		fvSize = Mathf.Max(0.1f,Random.value);
		fvTrail = Mathf.Max(0.3f,Random.value);
		fvFade = Random.value;
	}
}
