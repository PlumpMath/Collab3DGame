using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour {
    //destroy odject over time
    public int timer;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(timer == 0)
        {
            Destroy(gameObject);
        }
        timer--;
	}
}
