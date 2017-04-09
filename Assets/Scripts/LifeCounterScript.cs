using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounterScript : MonoBehaviour {

	private GameObject player;
	public GameObject lifeCounter;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = player.transform.position;
	}

	public GameObject spawnLife (int num) {
		var temp = Instantiate (lifeCounter);
		temp.transform.parent = player.transform;
		Vector3 pos = temp.transform.position;
		pos.y -= (num - 1) * 10;

		return temp;
	}
}
