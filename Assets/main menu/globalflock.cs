using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalflock : MonoBehaviour {

    public GameObject Fishprefab;

    static int fishnum = 50;
    public static int tankSize = 7;
    public static GameObject[] allFish = new GameObject[fishnum];

    public static Vector3 goal = Vector3.zero;

	// Use this for initialization
	void Start () {
		for(int i=0; i<fishnum; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-tankSize, tankSize), Random.Range(-tankSize, tankSize), Random.Range(-tankSize, tankSize));
            allFish[i] = (GameObject)Instantiate(Fishprefab, pos, Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Random.Range(0,10000) < 50)
        {
            goal = new Vector3(Random.Range(-tankSize, tankSize), Random.Range(-tankSize, tankSize), Random.Range(-tankSize, tankSize));

        }
        
    }
}
