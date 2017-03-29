using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flock : MonoBehaviour {

    public float speed = 0.1f;
    public float roatationspeed = 4.0f;
    Vector3 AverageHeading;
    Vector3 Averagepos;
    float neighbourDist = 1.0f;

    bool turning = false;
    // Use this for initialization
    void Start () {
        speed = Random.Range(0.5f, 1);
	}
	
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(transform.position,Vector3.zero) >= globalflock.tankSize)
        {
            turning = true;
        }
        else
        {
            turning = false;
        }
        if (turning)
        {
            Vector3 direction = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), roatationspeed * Time.deltaTime);
            speed = Random.Range(0.5f, 1);
        }
        else
        {
            if (Random.Range(0, 5) < 1)
            {
                ApplyR();
            }
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
	}
    void ApplyR()
    {
        GameObject[] gos;
        gos = globalflock.allFish;

        Vector3 vcenter = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.1f;

        Vector3 goal = globalflock.goal;
        float dist;

        int Gsize = 0;
        foreach(GameObject go in gos)
        {
            if(go != this.gameObject){
                dist = Vector3.Distance(go.transform.position, this.transform.position);
                if(dist <= neighbourDist)
                {
                    vcenter += go.transform.position;
                    Gsize++;

                    if(dist < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }
                    flock anotherFlock = go.GetComponent<flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }
        if(Gsize > 0)
        {
            vcenter = vcenter / Gsize + (goal - this.transform.position);
            speed = gSpeed / Gsize;
            Vector3 direction = (vcenter + vavoid) - transform.position;
            if(direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), roatationspeed * Time.deltaTime);
            }
        }

    }
}
