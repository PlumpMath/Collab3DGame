using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour {

	public GameObject[] tiles;
    public GameObject[] fishes;

    private Transform playerTransform;
	private GameObject player;
	public NewTile spawner;
	public Text score;

    //Constants
    private float playerTilePosition	= 0;
	private float spawnZ 				= 0.0f;
	private int   tilesConstant			= 2;
	private float tileLength			= 150;
	private float time					= 0f;



    void Start () 
	{
		player 			= GameObject.FindGameObjectWithTag ("Player");
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		spawner         = gameObject.GetComponent<NewTile> ();
		tiles           = new GameObject[15];
       

        //Spawning Initial tile prefabs
        for (int i = 0; i < tilesConstant; i++) 
		{
			if (i == 0)
				SpawnTile (false, 0);
		}


	}
	
	void Update () 
	{

		//Update time variable to count seconds
		time += Time.deltaTime;
		//Push time in seconds to player
		player.GetComponent<Player>().setTime((int)Mathf.Floor(time));
		//Update score with current time alive
		score.text = "Time Alive: " + (int)Mathf.Floor(time);


		playerTilePosition = spawnZ - (tilesConstant * tileLength);

        if (playerTransform.position.z > playerTilePosition) 
		{
			SpawnTile (true);
        }

        //Iterate through the tiles
        for (int i = 0; i < tiles.Length; i++) {
			if (tiles [i] != null) {
				if (tiles[i].transform.position.z < playerTransform.position.z - tileLength) {
					Destroy (tiles [i]);
				}
			}
		}
			
    }

	private void SpawnTile(bool fish, int prefabIndex = -1)
	{
		GameObject go;
		//Calls spawner code to create a new tile ahead of object
		go = spawner.SpawnTile (fish, Mathf.FloorToInt(time));
		go.transform.SetParent (transform);


		//Moves new tile into a emtpy spot in the array of game tiles
		for (int i = 0; i < tiles.Length; i++) {
			if (tiles [i] == null) {
				tiles [i] = go;
				break;
			}
		}

		/*
		 * In Start function, 7 tiles are immidetially created. 
		 * This makes spawnZ's position 7 tilesConstant ahead
		*/

		go.transform.position = Vector3.forward * spawnZ;
		spawnZ += tileLength;
	}
}