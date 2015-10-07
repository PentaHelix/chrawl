using UnityEngine;
using System.Collections;

public class Blizzard : MonoBehaviour {
	void Start () {
			
	}

	void Update () {
		transform.localScale += transform.localScale*(1 * Time.deltaTime);
		transform.localEulerAngles += new Vector3(0, 180 * Time.deltaTime, 0);
		Invoke("Stop", 2f);
		Destroy(gameObject, 3.2f);
	}

	void Stop(){
		GetComponent<ParticleSystem>().Stop();

	}

	public void OnTriggerEnter(Collider col){
		IFreezable f = col.transform.root.GetComponentInChildren<IFreezable>();
		if(f != null){
			f.OnFreeze(4f);
		}
	}

}
