using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator1 : MonoBehaviour {

	// Use this for initialization


	private Color   	fishColor;
	private GameObject  player;
	private Player 		playerScript;
	private float     	health;
	private int 		difference;


	void Start () {
		//Parameters ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax);
		player = GameObject.Find("Player");
		playerScript = player.GetComponent<Player> ();

		fishColor                               = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
		GetComponent<Renderer>().material.color = fishColor;
		health =  Random.Range(10f, 20f);
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.tag == "Player")
		{


		}


	}

	//setter Method for fish colour.
	public void setColour(Color colour){
		fishColor = colour;
	}
	
	// Update is called once per frame
	void Update () {
		difference = playerScript.difference;
	}
}
