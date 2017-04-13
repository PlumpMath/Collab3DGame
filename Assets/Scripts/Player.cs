using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    //comment graham branch
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
	private float 	something 			= 20.0f;
	public float 	buoyancy 			= 0.5f;
	public float 	horSpeed 			= 10f;
	public float 	verSpeed 			= 10f;
	private float 	verticalVelocity	= 0f;
	public float 	minHeight			= -20f;
	public float 	maxHeight			= 0f;
    public float 	health				= 100f;
	public float 	colourMult			= 10;
	//private int 	difficulty			= 30;

	//Predator Spawns
	private bool 		predator1 	= false;
	private float 		balance 	= 0f;
	private GameObject 	predator;
	public GameObject 	predPrefab;

	//Life counter
	private int startLives = 3;
	private int lives;
	public GameObject lifeCounter;
	private GameObject[] livesArray = new GameObject[3];

    //Sound after kia
    public GameObject deathAudio;



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

		something = 4f;

		//set lives
		lives = startLives;

		//update life counter
		showLives();

	}
	
	private void Update () 
	{
        

        //check to see if all lives have been lost
        if (lives < 1)
			resetLevel ();
		//if health is less then 1 lose a life
		if (health < 1)
			removeLife ();

		//See if second has gone by and remove health if yes
		if (timeGoing > lastTime) {
			lastTime = timeGoing;
			health -= 2;
			if (timeGoing % 10 == 0) {
				horSpeed ++;
			}
		}


		//Update oceanColour to be the most needed colour by the player
		oceanColor = Vector4.Lerp(oceanColor, getMaxColourVal(), 0.1f);


		//Update balance of colours
		balance = Mathf.Max (red, green, blue) - Mathf.Min (red, green, blue);

		if (balance > difference && !predator1) {
			predator1 = true;
			predator = Instantiate (predPrefab) as GameObject;
			Vector3 temp = predator.transform.position;
			temp.z -= 20;
			predator.transform.position = temp;
		}

		//If balance is regained
		if (balance < difference * 2 / 3 && predator1) {
			predator.GetComponent<Predator1> ().DestroyPred ();
			predator = null;
		}


		//if all colours reach the bottom, and in balance, push them all back up
		if (red < colourMult + 5 || green < colourMult + 5 || blue < colourMult + 5) {
			red += Mathf.Max (colourMult, 10f);
			green += Mathf.Max (colourMult, 10f);
			blue += Mathf.Max (colourMult, 10f);

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

		if (timeGoing < something) 
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

        //Scott added animation
#warning we need up rotation/bubble burst

        // Z is for forward movement
        moveVector.z = horSpeed;



		controller.Move(moveVector * Time.deltaTime);

		//Clamping
		positionVector 		= transform.position;
		//Not Needed now
		//positionVector.x 	= Mathf.Clamp (positionVector.x, minWidth, maxWidth);
		positionVector.y 	= Mathf.Clamp (positionVector.y, -50f, 12f);
		transform.position	= positionVector;

		//check if player has fallen below the screen
		if (positionVector.y < -39f) {
			Vector3 temp = transform.position;
			temp.y = -12f;
			transform.position = temp;
			removeLife ();
		}

        //Update smoke colour. The if statement allows for fewer particles to be generated, editable from unity interface.
        smokeColour = new Color(red / 100f, green / 100f, blue / 100f, 1f);

        //If health will effect the alpha, comment out above line and use the one below
        //smokeColour = new Color(red/100f, green/100f, blue/100f, health/100f);


        smokeDelay = (int)((100 - health) / 10);
        smokeDelay = Mathf.Clamp(smokeDelay, 1, 10);
        //A higher number means less smoke/bubbles
        if (framz > smokeDelay)
        {
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

		//oceanColor += color;

		//hitting fish changes the colour attributes of the player representing colour which changes the smoke colour
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
		health = Mathf.Clamp(health, 0, 50);
    }


	//Setter method to update the time variable
	public void setTime (int newTime) {
		timeGoing = newTime;
	}

	//getter method to get time variable
	public float getTime () {
		return timeGoing;
	}

	//Setter method called when predator 'dies'
	public void setPred1Ded () {
		predator1 = false;
	}

	//Getter method for balance value
	public float getBalance() {
		return balance;
	}

	//returns the smallest colour value. 0 == red, 1 == green, 2 == blue
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

	//returns the largest colour.
	public Vector4 getMaxColourVal () {
		if (red > green && red > blue)
			return new Vector4 (.9f, 0f, .1f, 1f);
		else if (green > red && green > blue)
			return new Vector4 (0f, .9f, .1f, 1f);
		else
			return new Vector4 (0f, 0f, 1f, 1f);
	}


	//method to remove lives
	public void removeLife () {


		//Reset colour balance to 100, 100, 100
		red = 100f;
		green = 100f;
		blue = 100f;

		//Reset health to 100
		health = 100;

		//remove one life
		lives--;

		//update life counter
		showLives();

        GameObject death = Instantiate(deathAudio);
        death.transform.parent = this.transform;

	}

	//Method to display lives
	public void showLives () {
		for (int i = 0; i < startLives; i++) {
			if (i < lives) {
				if (livesArray [i] == null)
					livesArray [i] = GetComponent<LifeCounterScript> ().spawnLife (i);
			}
			else {
				if (livesArray [i] != null)
					Destroy (livesArray [i]);
				livesArray [i] = null;
			}		
		}
	}

	//reset method to restart round when deded
	public void resetLevel () {
		PlayerPrefs.SetInt ("score", timeGoing);
		SceneManager.LoadScene ("Reset");
	}




}
