using UnityEngine;
using System.Collections;

public interface IIgnitable{
	void OnIgnite(float dur);
	void OnExpire();
}
