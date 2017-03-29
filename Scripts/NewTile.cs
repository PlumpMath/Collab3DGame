using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTile : MonoBehaviour {

	public GameObject tilePrefab;
    public GameObject fishPrefab;
	private GameObject player;

    // Use this for initialization
    void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	

	public GameObject SpawnTile(bool fish, int timeAlive)
	{
		GameObject go;
		go = Instantiate (tilePrefab) as GameObject;


		//spawn fish
		if (fish) {
			int fishz = Random.Range (3, 7);
			for (int i = 0; i < fishz; i++) {
				if (Random.Range (0, 10000) < timeAlive)
					SpawnFish (go.transform, true);
				else
					SpawnFish (go.transform, false);
			}
		}

		return go;
	}

	private GameObject SpawnFish(Transform tile, bool hard)
    {
        GameObject go;
        go = Instantiate(fishPrefab) as GameObject;

		//Parent of fish is the tile
		go.transform.parent = tile;

		//Set fish colour based on paramaters or difficulty.

		if (hard) {
			int minC = player.GetComponent<Player> ().getMinColour ();
			if (minC == 0)
				go.GetComponent<SpawnedFish> ().setColour (Color.red);
			else if (minC == 1)
				go.GetComponent<SpawnedFish> ().setColour (Color.green);
			else
				go.GetComponent<SpawnedFish> ().setColour (Color.blue);
		}

		go.transform.position = new Vector3(0, Random.Range(0, 6.0f), Random.Range(-50f, 50f));

        return go;
    }

}
