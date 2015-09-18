using UnityEngine;
using System.Collections;
using Pathfinding;

public class SeekPlayer : MonoBehaviour {
	private Transform target;
	private Vector3 targetPos;
	private CharacterController controller;
	private Path path;
	private Seeker seeker;

	void Start () {
		target = Game.player;
		seeker = GetComponent<Seeker>();
		controller = GetComponent<CharacterController>();
		calcPath();
	}

	void calcPath(){
		path = null;
		seeker.StartPath(transform.position, target.position, OnPathComplete);
	}

	void Update () {
		if(!GetComponent<Enemy>().alive)return;
		if(path == null)return;
		Vector2 dir = new Vector2(path.vectorPath[1].x - transform.position.x, path.vectorPath[1].z - transform.position.z);
		Vector3 move = new Vector3(dir.x, 0f, dir.y).normalized * GetComponent<Enemy>().speed * Time.deltaTime;
		if(Vector3.Distance(transform.localPosition, Game.player.localPosition) > 20)move = Vector3.zero;
		if(GetComponent<Enemy>().Walk()){
			controller.SimpleMove(move);
		}
		if(dir.magnitude < .2f){
			calcPath();
		}
	}

	void OnPathComplete(Path p){
		path = p;
	}
}
