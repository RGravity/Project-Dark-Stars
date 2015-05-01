using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPlayerEnemiesAsteroids : MonoBehaviour {
    
    private bool _spawnNewAsteroid = false;
    public bool SpawnNewAsteroid { set { _spawnNewAsteroid = value; } }
    //Player
    public GameObject parentSpaceShip;
    public Object spaceShip;

    //List of Asteroids
    public GameObject parentAsteroids;
    public int amountOfAsteroids = 10;
    public int MinDistanceToPlayer = 30;
    public int MaxDistanceToPlayer = 100;
    public List<Object> AsteroidsList = new List<Object>();
    private int _numberOfAsteroids = 0;

    //list of Enemies
    public GameObject parentSpaceShipEnemies;
    public int amountOfEnemies = 2;
    public List<Object> spaceShipEnemiesList = new List<Object>();
    private int _numberOfEnemies = 0;

    

	// Use this for initialization
	void Start () {
        SpawnPlayerShip();
        SpawnAsteroids();
        SpawnEnemyShips();
	}
	
	// Update is called once per frame
	void Update () {
        if (_spawnNewAsteroid)
        {
            SpawnAsteroid();
        }
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
            Vector3 AsteroidPosition = new Vector3(asteroidX, asteroidY, asteroidZ);
            float lengthAsteroidToSpaceship = 0;

            do
            {


                
                int positiveORnegative = 0;

                // 0 = positive , 1 = negative
                positiveORnegative = Random.Range(0, 2);
                switch (positiveORnegative)
                {
                    case 0:
                        asteroidX = spaceShipPosition.x + Random.Range(0, MaxDistanceToPlayer);
                        break;
                    case 1:
                        asteroidX = spaceShipPosition.x + Random.Range(-MaxDistanceToPlayer, 0);
                        break;
                    default:
                        break;
                }

                // 0 = positive , 1 = negative
                positiveORnegative = Random.Range(0, 2);
                switch (positiveORnegative)
                {
                    case 0:
                        asteroidY = spaceShipPosition.x + Random.Range(0, MaxDistanceToPlayer);
                        break;
                    case 1:
                        asteroidY = spaceShipPosition.x + Random.Range(-MaxDistanceToPlayer, 0);
                        break;
                    default:
                        break;
                }

                // 0 = positive , 1 = negative
                positiveORnegative = Random.Range(0, 2);
                switch (positiveORnegative)
                {
                    case 0:
                        asteroidZ = spaceShipPosition.x + Random.Range(0, MaxDistanceToPlayer);
                        break;
                    case 1:
                        asteroidZ = spaceShipPosition.x + Random.Range(-MaxDistanceToPlayer, 0);
                        break;
                    default:
                        break;
                }

                AsteroidPosition = new Vector3(asteroidX, asteroidY, asteroidZ);
                Vector3 DistanceAsteroidToSpaceship = AsteroidPosition - spaceShipPosition;
                lengthAsteroidToSpaceship = DistanceAsteroidToSpaceship.magnitude;

            } while (lengthAsteroidToSpaceship < MaxDistanceToPlayer);

            GameObject go = (GameObject)Instantiate(AsteroidsList[Random.Range(0, AsteroidsList.Count)], new Vector3(asteroidX, asteroidY, asteroidZ), Quaternion.identity);
            go.transform.parent = parentAsteroids.transform;
            go.name = "Asteroid" + _numberOfAsteroids;
            
            _numberOfAsteroids++;
        }
    }

    void SpawnAsteroid()
    {
        float asteroidX = 0;
        float asteroidY = 0;
        float asteroidZ = 0;
        int positiveORnegative = 0;
        Vector3 spaceShipPosition = GameObject.Find("Spaceship").transform.position;

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
        go.name = "Asteroid" + _numberOfAsteroids;
        _numberOfAsteroids++;

        _spawnNewAsteroid = false;
    }

    void SpawnEnemyShips()
    {
        Vector3 spaceShipPosition = GameObject.Find("Spaceship").transform.position;
        for (int i = 0; i < amountOfEnemies; i++)
        {
            float enemyX = 0;
            float enemyY = 0;
            float enemyZ = 0;
            Vector3 EnemyPosition = new Vector3(enemyX, enemyY, enemyZ);
            float lengthEnemyToSpaceship = 0;

            do
            {
                int positiveORnegative = 0;

                // 0 = positive , 1 = negative
                positiveORnegative = Random.Range(0, 2);
                switch (positiveORnegative)
                {
                    case 0:
                        enemyX = spaceShipPosition.x + Random.Range(0, MaxDistanceToPlayer);
                        break;
                    case 1:
                        enemyX = spaceShipPosition.x + Random.Range(-MaxDistanceToPlayer, 0);
                        break;
                    default:
                        break;
                }

                // 0 = positive , 1 = negative
                positiveORnegative = Random.Range(0, 2);
                switch (positiveORnegative)
                {
                    case 0:
                        enemyY = spaceShipPosition.x + Random.Range(0, MaxDistanceToPlayer);
                        break;
                    case 1:
                        enemyY = spaceShipPosition.x + Random.Range(-MaxDistanceToPlayer, 0);
                        break;
                    default:
                        break;
                }

                // 0 = positive , 1 = negative
                positiveORnegative = Random.Range(0, 2);
                switch (positiveORnegative)
                {
                    case 0:
                        enemyZ = spaceShipPosition.x + Random.Range(0, MaxDistanceToPlayer);
                        break;
                    case 1:
                        enemyZ = spaceShipPosition.x + Random.Range(-MaxDistanceToPlayer, 0);
                        break;
                    default:
                        break;
                }

                EnemyPosition = new Vector3(enemyX, enemyY, enemyZ);
                Vector3 DistanceEnemyToSpaceship = EnemyPosition - spaceShipPosition;
                lengthEnemyToSpaceship = DistanceEnemyToSpaceship.magnitude;

            } while (lengthEnemyToSpaceship < MaxDistanceToPlayer);

            GameObject go = (GameObject)Instantiate(spaceShipEnemiesList[Random.Range(0, spaceShipEnemiesList.Count)], new Vector3(enemyX, enemyY, enemyZ), Quaternion.identity);
            go.transform.parent = parentSpaceShipEnemies.transform;
            go.name = "Enemy" + _numberOfEnemies;

            _numberOfEnemies++;
        }
    }
}
