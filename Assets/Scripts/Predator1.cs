using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator1 : MonoBehaviour {

	// Use this for initialization


	private Color   	fishColor;
	private GameObject  player;
	private Player 		playerScript;
	private int 		difference;
	private float 		distance = 10f;
	private float 		timeStart;
	private float 		timeNow;
	private float 		prevBalance;
	private float 		balance;
	private float 		beta;
	private bool 		leave;
	private Vector3 	destination;


	void Start () {
		//Parameters ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax);
		player = GameObject.Find("Player");
		playerScript = player.GetComponent<Player> ();

		//set colour of predator to min colour aspect of player
		setColour ();

		//set start colour
		timeStart = playerScript.getTime();
		timeNow = playerScript.getTime();

		//set start balance
		balance = playerScript.getBalance ();

		//set start beta
		beta = 0.05f;

		GetComponent<Renderer>().material.color = fishColor;
	}

	//setter Method for predator colour.
	public void setColour(){
		int c = playerScript.getMinColour ();
		if (c == 0)
			fishColor = Color.red;
		else if (c == 1)
			fishColor = Color.green;
		else
			fishColor = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
		if (leave) {
			Vector3 temp = Vector3.Lerp (transform.position, destination, 0.01f);
			transform.position = temp;
			return;
		}

		//undate time
		timeNow += Time.deltaTime;

		//update beta
		beta = (5 * playerScript.horSpeed)/1000; 

		//undate balance
		prevBalance = balance;
		balance = playerScript.getBalance ();

		//check if colour was brought back to balance
		if (balance < prevBalance)
			timeStart = timeNow;
			//timeStart += 25f;

		Vector3 goal = player.transform.position;
		goal.z -= (-0.9f * (timeNow - timeStart) + distance);


		transform.position = Vector3.Lerp (transform.position, goal, beta);

		//if predator gets close enough to bite
		if ((player.transform.position - transform.position).magnitude < 6) {
			playerScript.removeLife ();
			DestroyPred ();
		}
	}

	public void DestroyPred() {
		playerScript.setPred1Ded ();
		destination = transform.position;
		destination.z -= 100f;
		leave = true;
		Destroy (gameObject, 3f);
	}

}
