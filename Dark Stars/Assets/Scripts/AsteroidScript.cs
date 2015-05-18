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
    private int interval = 160;
    public int MaxInterval = 160;

	// Use this for initialization
	void Start () {
        colour = this.gameObject.GetComponent<MeshRenderer>().material.color;
        colour.a = 0.00f;
        this.gameObject.GetComponent<MeshRenderer>().material.color = colour;
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
        CheckHit();

        if (colour.a < 1.0f)
        {
            colour.a = 0.00f + alphaIncrease;
            this.gameObject.GetComponent<MeshRenderer>().material.color = colour;
            _alphaTimer++;
        }
	}

    void Movement()
    {
    }

    void CheckHit()
    {
        if (_hit)
        {
            HP--;
            Debug.Log("HP is lowered to: " + HP);
            Hit = false;
            if (HP <= 0)
            {
                //Spawn Mineral, Destroy Asteroid and spawn a new one
                Debug.Log("Asteroid Destroyed");
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

    public void Kill()
    {
        Debug.Log("Spawn mineral!");
    }
}
