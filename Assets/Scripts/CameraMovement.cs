using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour {

	//Countdown timer
	public Text 		countText;
	private float 		countNum = 5;

	private Transform	lookAt;
	private Vector3 	startOffset;
	private Vector3 	moveVector;

	//Constants
	private float animationDuration = 4.0f;
	private float transition 		= 0.0f;
	private Vector3 animationOffset	= new Vector3 (-10, 0, -10);

	// Max and min height clamp of Camera. Relevant for stair climbing, jumping etc.
	private int heightMax = -10;
	private int heightMin = -10;


	private void Start () 
	{
		/*
		 * The original location of the camera is stored. That way, the height and distance between
		 * the camera and the player is maintained. As moveVector is tracking the player, the camera is
		 * "startOffset" displacement away from the player.
		*/

		lookAt 		= GameObject.FindGameObjectWithTag ("Player").transform;
		startOffset	= transform.position - lookAt.position;
	}

	private void Update () 
	{
		moveVector 		= lookAt.position + startOffset;

		// Keep the height of the camera restricted between a max and min

		moveVector.y = Mathf.Clamp(moveVector.y, heightMin, heightMax);

		//moveVector.x = Mathf.Clamp(moveVector.x, 50, 51);

      
		// transition value starts at 0 and reaches 1 when start camera animation is complete

		if (transition > 1.0f) 
		{
			transform.position = moveVector;

			//remove countdown from screen
			countText.text = "";
		} else 
		{
			//
			//	Interpolate between animation initial (position and rotation) and final;
			//
			transform.position = Vector3.Lerp (moveVector + animationOffset, moveVector, transition);
			transition += Time.deltaTime * (1 / animationDuration);

			//Update countdown time
			countNum -= Time.deltaTime * (5 / animationDuration);
			transform.LookAt (lookAt.position);

			//Print int representing countdown
			countText.text = "" + Mathf.Ceil (countNum);
		}
 
	}
}
