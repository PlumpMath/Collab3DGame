using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private CharacterController	controller;
	private Vector3 			moveVector;
	private Vector3 			positionVector;

	//Player Attributes
	public  float 				red;
	public  float 				green;
	public  float 				blue;
	public  ParticleSystem 		smoke;
	private Color 				smokeColour;
    public  Color               oceanColor;
	public int 					difference;
	private int 				timeGoing;
	private int 				lastTime;

	//FrameCounter
	private int 				framz;
	public int					smokeDelay;

	//Constants
	private float 	animationDuration 	= 4.0f;
	public float 	buoyancy 			= 0.5f;
	public float 	horSpeed 			= 10f;
	public float 	verSpeed 			= 10f;
	private float 	verticalVelocity	= 0f;
	public float 	minWidth			= 0f;
	public float 	maxWidth			= 10f;
    public float 	health				= 100f;
	public float 	colourMult			= 10;
	//private int 	difficulty			= 30;



    private void Start () 
	{
		controller = GetComponent<CharacterController> ();
		positionVector = transform.position;

		lastTime = 0;

		//set colour start values
		red 	= 100f;
		green 	= 100f;
		blue 	= 100f;

		//set Player start attributes Colour is between 0 and 1
		smokeColour = new Color(1f, 1f, 1f, 1f);
		framz = 0;
        oceanColor = new Color(0, 0, 1, 1);

	}
	
	private void Update () 
	{

		//See if second has gone by and remove health if yes
		if (timeGoing > lastTime) {
			lastTime = timeGoing;
			health--;
			if (timeGoing % 10 == 0) {
				horSpeed ++;
			}
		}




		//Does colour imbalance exist?
		isBalanced();

		//if all colours reach the bottom, and in balance, push them all back up
		if (red < colourMult + 10 && green < colourMult + 10 && blue < colourMult + 10) {
			red += Mathf.Max (colourMult, 50f);
			green += Mathf.Max (colourMult, 50f);
			blue += Mathf.Max (colourMult, 50f);

		}

		//Keep colour values between 0 and 100 to represent valid colours
		red	= Mathf.Clamp (red, 0, 100);
		green = Mathf.Clamp (green, 0, 100);
		blue = Mathf.Clamp (blue, 0, 100);

		//testing colour change --> it works
		//smokeColour = Random.ColorHSV();

		/*
		 * Restricts player from moving left and right until the camera animation is complete
		*/

		if (Time.time < animationDuration) 
		{
			controller.Move (Vector3.forward * horSpeed * Time.deltaTime);
			return;
		}

		moveVector = Vector3.zero;

		/*
		 * By using axis, we can easily port to differnt input devices. 
		 * It also requires less code. Inside unity, go to edit -> project settings -> Input
		 * Here you can see all the features of using the Horizontal axis.
		 * 
		 * X is for Horizontal Movement
		 * Left and Right
		*/

		//Removing horizontal movement as per Benham
		//moveVector.x = Input.GetAxis("Horizontal") * speed;



		/* 
		 * Y is for vertical movement, such as buoyancy
		 * Here we make a simple buoyancy function. When the object is not touching the ground, 
		 * every second vertical velocity is increased. Otherwise, it is constant
		*/
		moveVector.y = Input.GetAxis("Vertical") * verSpeed;



		if (controller.isGrounded) 
		{
			verticalVelocity = 0f;
			//verticalVelocity = -0.01f;
		} else 
		{
			//verticalVelocity -= buoyancy * Time.deltaTime;
			verticalVelocity = -1 * buoyancy;
		}
		moveVector.y += verticalVelocity;



		// Z is for forward movement
		moveVector.z = horSpeed;



		controller.Move(moveVector * Time.deltaTime);

		//Clamping
		positionVector 		= transform.position;
		//Not Needed now
		//positionVector.x 	= Mathf.Clamp (positionVector.x, minWidth, maxWidth);
		positionVector.y 	= Mathf.Clamp (positionVector.y, minWidth, 2*maxWidth);
		transform.position	= positionVector;


        //Update smoke colour. The if statement allows for fewer particles to be generated, editable from unity interface.
        smokeColour = new Color(red/100f, green/100f, blue/100f, 1f);

        //If health will effect the alpha, comment out above line and use the one below
        //#warning All of this needs to be updated so health is shown by number of bubbles instead of fade
        //smokeColour = new Color(red/100f, green/100f, blue/100f, health/100f);

        #warning Not sure if color will look good on bubbles, maybe expeiment somehow or remove color from bubbles
        //Behnam CHANGE THIS -> smokeDelay is the number of frames that pass before another smoke particle is created

		smokeDelay = (int)((100 - health) / 10);
		smokeDelay = Mathf.Clamp (smokeDelay, 1, 10);
        //A higher number means less smoke/bubbles
        if (framz > smokeDelay) {
			var emitParams = new ParticleSystem.EmitParams ();
			//Debug.Log (emitParams.position);
			emitParams.startColor = smokeColour;
			smoke.Emit (emitParams, 1);
			framz = 0;
		}
		framz++;
		
	}

    public void EatFishColor(Color color)
    {
		//Debug.Log (color.r + " " + color.g + " " + color.b + " ");

		oceanColor += color;

		//hitting fish changes the colour attributes of the player representing colour which changes the smoke colour
		/*red -= colourMult * color.r;
		green -= colourMult * color.g;
		blue -= colourMult * color.b;*/


		//if we want to only remove the largest colour aspect of the fish use the below code instead of above.
		{
			float r = color.r, g = color.g, b = color.b;
			if (r > g && r > b)
				red -= colourMult;
			else if (g > r && g > b)
				green -= colourMult;
			else
				blue -= colourMult;
					
		}

    }

    public void AffectHealth(float healthImpact)
    {
        health += healthImpact;
		health = Mathf.Clamp(health, 0, 100);
    }
		


	//Method here will find the difference between colours and if unbalance is great enough, will take action
	private void isBalanced () {
		int colourOut;
		//is there a colour imbalance?
		if (Mathf.Max (red, green, blue) > Mathf.Min (red, green, blue) + difference) {

			if (red < green && red < blue)
				colourOut = 0;
			else if (green < red && green < blue)
				colourOut = 1;
			else
				colourOut = 2;
		} else
			return;

		//Here we can spawn monsters when the colour impalance is great enough. 
		//The colour of monster to spawn is based on the value colourOut: 0 == red, 1 == green, 2 == blue
	}


	//Setter method to update the time variable
	public void setTime (int newTime) {
		timeGoing = newTime;
	}

	public int getMinColour () {
		int colourOut;
		if (red < green && red < blue)
			colourOut = 0;
		else if (green < red && green < blue)
			colourOut = 1;
		else
			colourOut = 2;

		return colourOut;
	}
}
