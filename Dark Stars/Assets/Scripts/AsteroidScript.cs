using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidScript : MonoBehaviour {

    private bool _hit = false;
    public int HP = 100;

    private float alphaIncrease = 1 / 255;
    private int _alphaTimer = 0; // 0 - 255
    private Color colour;

    public bool Hit { set { _hit = value; } }
    public int MaxInterval = 160;

    float thrust = 1000;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
        CheckHit();
	}

    void Movement()
    {
        rb.AddForce(transform.forward * thrust);
    }

    void CheckHit()
    {
        if (_hit)
        {
            HP--;
            Hit = false;
            if (HP <= 0)
            {
                //Spawn Mineral, Destroy Asteroid and spawn a new one
                SpawnMineral();
                SpawnNewAsteroid();
            }
        }
    }

    void SpawnMineral()
    {
        GameObject.FindObjectOfType<MainScript>().AsteroidToDestroy = gameObject;
    }

    void SpawnNewAsteroid()
    {
        GameObject.FindObjectOfType<MainScript>().SpawnNewAsteroid = true;
    }

    void OnDestroy()
    {
        if (HP <= 0)
        {
            GameObject.Find("Dust").transform.position = gameObject.transform.position;
            GameObject.Find("Dust").GetComponent<DustScript>().ActivateAnimation = true;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponentInChildren<PlayerController>())
        {
            GameObject.FindObjectOfType<PlayerController>().Hit = true; 
        }
    }
}
