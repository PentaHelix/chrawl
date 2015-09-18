using UnityEngine;
using System.Collections;

public class Watcher : Enemy {
	Transform bone1;
	Transform bone2;
	Transform bone3;
	Transform eyePos;

	Quaternion moveVec;
	Vector3 lookVec;

	void Start () {
		bone1 = transform.Find("Armature/Bone/Bone.001/Bone.002/Bone.003/Bone.004");
		bone2 = transform.Find("Armature/Bone/Bone.001/Bone.002/Bone.003/Bone.004/Bone.005");
		bone3 = transform.Find("Armature/Bone/Bone.001/Bone.002/Bone.003/Bone.004/Bone.005/Bone.006");

		eyePos = transform.Find("Armature/Bone/Bone.001/Bone.002/Bone.003/Bone.004/Bone.005/Bone.006/EyePos");

		mesh = transform.Find("Mesh");
	}

	void Update () {
		
	}
}
