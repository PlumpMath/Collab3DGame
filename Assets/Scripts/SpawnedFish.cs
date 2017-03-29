using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedFish : MonoBehaviour {

    // Use this for initialization

	private Color   	fishColor;
    private Player  	Player;

    public GameObject eat;
    private float     	health;
 

	void Start () {
        //Parameters ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax);
        fishColor                               = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        GetComponent<Renderer>().material.color = fishColor;
        health =  Random.Range(10f, 20f);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Player Player = other.GetComponent<Player>();
            Player.EatFishColor(fishColor);
            Player.AffectHealth(health);
            var ate = Instantiate(eat, transform.position, transform.rotation);
            Destroy (gameObject);
        }
       

    }

	//setter Method for fish colour.
	public void setColour(Color colour){
		fishColor = colour;
	}
}
