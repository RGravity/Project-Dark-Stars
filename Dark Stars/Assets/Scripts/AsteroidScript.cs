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
    

	// Use this for initialization
	void Start () {
        colour = this.gameObject.GetComponent<MeshRenderer>().material.color;
        colour.a = 0.00f;
        this.gameObject.GetComponent<MeshRenderer>().material.color = colour;
	}
	
	// Update is called once per frame
	void Update () {
        CheckHit();

        if (colour.a < 1.0f)
        {
            colour.a = 0.00f + alphaIncrease;
            this.gameObject.GetComponent<MeshRenderer>().material.color = colour;
            _alphaTimer++;
        }

        //Debug.Log(this.gameObject.GetComponent<MeshRenderer>().material.color.a);
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
                Debug.Log("Asteroid Destroyed");
                SpawnMineral();
                SpawnNewAsteroid();
            }
        }
    }

    void SpawnMineral()
    {
        GameObject.Find("Main").GetComponent<MainScript>().AsteroidToDestroy = gameObject;
    }

    void SpawnNewAsteroid()
    {
        GameObject.Find("Main").GetComponent<MainScript>().SpawnNewAsteroid = true;
    }
}
