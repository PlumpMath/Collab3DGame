using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour {

	private Player Player;
	private Vector3 destination;
	private bool touched = false;
	public ParticleSystem blood;

	// Use this for initialization
	void Start () {
		destination = transform.position;
	}

	void Update () {
		Vector3 temp = Vector3.Lerp (transform.position, destination, 0.1f);
		transform.position = temp;
	}
	
	void OnTriggerEnter(Collider other)
	{

		if (other.tag == "Player" && !touched)
		{
			Player Player = other.GetComponent<Player>();
			destination.y += 40f;
			Player.GetComponent<Player> ().removeLife ();
			touched = true;
			blood.Play ();
			Destroy (gameObject, 5f);
		}


	}
}
