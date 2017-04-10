using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounterScript : MonoBehaviour {

	public GameObject player;
	public GameObject lifeCounter;

	// Use this for initialization
	void Start () {
		//player = GameObject.FindGameObjectWithTag ("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
	}

	public GameObject spawnLife (int num) {
		var temp = Instantiate (lifeCounter);
		temp.transform.parent = player.transform;
		Vector3 pos = temp.transform.position;
		pos.z -= num * 2;
		temp.transform.position = pos;

		return temp;
	}
}
