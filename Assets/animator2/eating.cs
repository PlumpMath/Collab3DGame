using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eating : MonoBehaviour {
    public Animator ano;
    public int timer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (timer < 10)
        {
            ano.SetBool("eating", false);
        }
        else
        {
            timer--;
        }
        
		
	}
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "enemy")
        {
            ano.SetBool("eating", true);
            timer = 30;
        }


    }
}
