using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidScript : MonoBehaviour {

    private bool _hit = false;
    public int HP = 100;

    public bool Hit { set { _hit = value; } }
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CheckHit();
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
