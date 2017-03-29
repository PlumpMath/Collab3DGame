using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour {

    // Use this for initialization

    private Color       oceanColor;
    private GameObject  Player;

    private void Start()
    {
        //Parameters ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax);
        Player      = GameObject.FindGameObjectWithTag("Player");
        oceanColor  = Player.GetComponent<Player>().oceanColor;
    }

    // Update is called once per frame
    private void Update()
    {

        UpdateColor();

    }

    private void UpdateColor()
    {
        oceanColor = Player.GetComponent<Player>().oceanColor;
        GetComponent<Renderer>().material.color = oceanColor;
    }
}
