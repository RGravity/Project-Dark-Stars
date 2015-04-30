using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPlayerEnemiesAsteroids : MonoBehaviour {

    //parent of all the gameobjects instantiated
    


    //Player
    public GameObject parentSpaceShip;
    public Object spaceShip;

    //List of Asteroids
    public GameObject parentAsteroids;
    public int amountOfAsteroids = 10;
    public int MinDistanceToPlayer = 30;
    public int MaxDistanceToPlayer = 100;
    public List<Object> AsteroidsList = new List<Object>();

    //list of Enemies
    public GameObject parentSpaceShipEnemies;
    public int amountOfEnemies = 2;
    public List<Object> spaceShipEnemiesList = new List<Object>();

    

	// Use this for initialization
	void Start () {
        SpawnPlayerShip();
        SpawnAsteroids();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SpawnPlayerShip()
    {
        GameObject go = (GameObject)Instantiate(spaceShip, new Vector3(0, 0, 0), Quaternion.identity);
        go.transform.parent = parentSpaceShip.transform;
        go.name = "Spaceship";
    }

    void SpawnAsteroids()
    {
        Vector3 spaceShipPosition = GameObject.Find("Spaceship").transform.position;
        for (int i = 0; i < amountOfAsteroids; i++)
        {
            float asteroidX = 0;
            float asteroidY = 0;
            float asteroidZ = 0;
            int positiveORnegative = 0;

            // 0 = positive , 1 = negative
            positiveORnegative = Random.Range(0, 2);
            switch (positiveORnegative)
            {
                case 0:
                    asteroidX = spaceShipPosition.x + Random.Range(MinDistanceToPlayer, MaxDistanceToPlayer);
                    break;
                case 1:
                    asteroidX = spaceShipPosition.x + Random.Range(-MaxDistanceToPlayer, -MinDistanceToPlayer);
                    break;
                default:
                    break;
            }

            // 0 = positive , 1 = negative
            positiveORnegative = Random.Range(0, 2);
            switch (positiveORnegative)
            {
                case 0:
                    asteroidY = spaceShipPosition.x + Random.Range(MinDistanceToPlayer, MaxDistanceToPlayer);
                    break;
                case 1:
                    asteroidY = spaceShipPosition.x + Random.Range(-MaxDistanceToPlayer, -MinDistanceToPlayer);
                    break;
                default:
                    break;
            }

            // 0 = positive , 1 = negative
            positiveORnegative = Random.Range(0, 2);
            switch (positiveORnegative)
            {
                case 0:
                    asteroidZ = spaceShipPosition.x + Random.Range(MinDistanceToPlayer, MaxDistanceToPlayer);
                    break;
                case 1:
                    asteroidZ = spaceShipPosition.x + Random.Range(-MaxDistanceToPlayer, -MinDistanceToPlayer);
                    break;
                default:
                    break;
            }


            GameObject go = (GameObject)Instantiate(AsteroidsList[Random.Range(0, AsteroidsList.Count)], new Vector3(asteroidX, asteroidY, asteroidZ), Quaternion.identity);
            go.transform.parent = parentAsteroids.transform;
            go.name = "asteroid" + i;
        }
    }
}
