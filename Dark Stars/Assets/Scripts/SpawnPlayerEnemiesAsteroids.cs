using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPlayerEnemiesAsteroids : MonoBehaviour {

    //Player
    public Object spaceShip;

    //list of Enemies
    public List<Object> spaceShipEnemiesList = new List<Object>();

    //List of Asteroids
    public List<Object> AsteroidsList = new List<Object>();

	// Use this for initialization
	void Start () {
        SpawnPlayerShip();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SpawnPlayerShip()
    {
        Instantiate(spaceShip, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
