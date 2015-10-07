using UnityEngine;
using System.Collections;

public interface IFreezable{
	void OnFreeze(float dur);
	void OnThaw();
}
