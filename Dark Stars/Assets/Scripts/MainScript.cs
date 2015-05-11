using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainScript : MonoBehaviour
{

    private bool _spawnNewAsteroid = false;
    public bool SpawnNewAsteroid { set { _spawnNewAsteroid = value; } }

    private bool _spawnNewEnemyship = false;
    public bool SpawnNewEnemyship { set { _spawnNewEnemyship = value; } }

    private GameObject _asteroidToDestroy;
    public GameObject AsteroidToDestroy { set { _asteroidToDestroy = value; } }

    //Player
    public GameObject parentSpaceShip;
    public Object spaceShip;

    //Asteroids
    public GameObject parentAsteroids;
    public int amountOfAsteroids = 10;
    public int MinDistanceToPlayer = 30;
    public int MaxDistanceToPlayer = 100;
    public List<Object> AsteroidsList = new List<Object>();
    private int _numberOfAsteroids = 0;
    private List<string> AsteroidsInSpaceList = new List<string>();

    //Enemies
    public GameObject parentSpaceShipEnemies;
    public int amountOfEnemies = 2;
    public List<Object> enemySpaceshipsList = new List<Object>();
    private int _numberOfEnemies = 0;
    private List<string> enemySpaceshipsInSpaceList = new List<string>();

    //Minerals
    public GameObject parentMinerals;
    public List<Object> mineralsList = new List<Object>();
    private int _numberOfMinerals = 0;



    // Use this for initialization
    void Start()
    {
        SpawnPlayerShip();
        SpawnAsteroids();
        SpawnEnemyShips();
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawnNewAsteroid)
        {
            SpawnAsteroid();
        }

        if (_spawnNewEnemyship)
        {
            SpawnEnemyShip();
        }

        if (_asteroidToDestroy != null)
        {
            SpawnMineral(1);
        }

        //foreach (GameObject enemy in enemySpaceshipsInSpaceList)
        //{
        //    Vector3 spaceShipPosition = GameObject.Find("Spaceship").transform.position;
        //    Vector3 enemyPosition = GameObject.Find(enemy.name).transform.position;

        //    Vector3 distanceDifference = enemyPosition - spaceShipPosition;
        //    float lengthDifference = distanceDifference.magnitude;

        //    if (lengthDifference > MaxDistanceToPlayer)
        //    {
        //        Destroy(GameObject.Find(enemy.name));
        //        SpawnEnemyShip();
        //    }
        //}

        #region AsteroidRespawn
        List<string> tempAsteroidDestroyedList = new List<string>();
        foreach (string asteroid in AsteroidsInSpaceList)
        {
            Vector3 spaceShipPosition = GameObject.Find("Spaceship").transform.position;
            Vector3 asteroidPosition = GameObject.Find(asteroid).transform.position;

            Vector3 distanceDifference = asteroidPosition - spaceShipPosition;
            float lengthDifference = distanceDifference.magnitude;

            if (lengthDifference > MaxDistanceToPlayer)
            {
                tempAsteroidDestroyedList.Add(asteroid);
            }
        }
        foreach (string asteroidDestroyed in tempAsteroidDestroyedList)
        {
            AsteroidsInSpaceList.Remove(asteroidDestroyed);
            Destroy(GameObject.Find(asteroidDestroyed).gameObject);
            SpawnAsteroid();
        }
        #endregion
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
                        asteroidY = spaceShipPosition.y + Random.Range(0, MaxDistanceToPlayer);
                        break;
                    case 1:
                        asteroidY = spaceShipPosition.y + Random.Range(-MaxDistanceToPlayer, 0);
                        break;
                    default:
                        break;
                }

                // 0 = positive , 1 = negative
                positiveORnegative = Random.Range(0, 2);
                switch (positiveORnegative)
                {
                    case 0:
                        asteroidZ = spaceShipPosition.z + Random.Range(0, MaxDistanceToPlayer);
                        break;
                    case 1:
                        asteroidZ = spaceShipPosition.z + Random.Range(-MaxDistanceToPlayer, 0);
                        break;
                    default:
                        break;
                }

                AsteroidPosition = new Vector3(asteroidX, asteroidY, asteroidZ);
                Vector3 DistanceAsteroidToSpaceship = AsteroidPosition - spaceShipPosition;
                lengthAsteroidToSpaceship = DistanceAsteroidToSpaceship.magnitude;

            } while (lengthAsteroidToSpaceship > MaxDistanceToPlayer);

            GameObject go = (GameObject)Instantiate(AsteroidsList[Random.Range(0, AsteroidsList.Count)], new Vector3(asteroidX, asteroidY, asteroidZ), Quaternion.identity);
            go.transform.parent = parentAsteroids.transform;
            go.name = "Asteroid" + _numberOfAsteroids;
            AsteroidsInSpaceList.Add(go.name);

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
        float lengthAsteroidToSpaceship = 0;
        Vector3 AsteroidPosition = new Vector3(asteroidX, asteroidY, asteroidZ);

        do
        {

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
                    asteroidY = spaceShipPosition.y + Random.Range(MinDistanceToPlayer, MaxDistanceToPlayer);
                    break;
                case 1:
                    asteroidY = spaceShipPosition.y + Random.Range(-MaxDistanceToPlayer, -MinDistanceToPlayer);
                    break;
                default:
                    break;
            }

            // 0 = positive , 1 = negative
            positiveORnegative = Random.Range(0, 2);
            switch (positiveORnegative)
            {
                case 0:
                    asteroidZ = spaceShipPosition.z + Random.Range(MinDistanceToPlayer, MaxDistanceToPlayer);
                    break;
                case 1:
                    asteroidZ = spaceShipPosition.z + Random.Range(-MaxDistanceToPlayer, -MinDistanceToPlayer);
                    break;
                default:
                    break;
            }
            AsteroidPosition = new Vector3(asteroidX, asteroidY, asteroidZ);
            Vector3 DistanceAsteroidToSpaceship = AsteroidPosition - spaceShipPosition;
            lengthAsteroidToSpaceship = DistanceAsteroidToSpaceship.magnitude;

        } while (lengthAsteroidToSpaceship > MaxDistanceToPlayer);

        GameObject go = (GameObject)Instantiate(AsteroidsList[Random.Range(0, AsteroidsList.Count)], new Vector3(asteroidX, asteroidY, asteroidZ), Quaternion.identity);
        go.transform.parent = parentAsteroids.transform;
        go.name = "Asteroid" + _numberOfAsteroids;
        AsteroidsInSpaceList.Add(go.name);

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
                        enemyY = spaceShipPosition.y + Random.Range(0, MaxDistanceToPlayer);
                        break;
                    case 1:
                        enemyY = spaceShipPosition.y + Random.Range(-MaxDistanceToPlayer, 0);
                        break;
                    default:
                        break;
                }

                // 0 = positive , 1 = negative
                positiveORnegative = Random.Range(0, 2);
                switch (positiveORnegative)
                {
                    case 0:
                        enemyZ = spaceShipPosition.z + Random.Range(0, MaxDistanceToPlayer);
                        break;
                    case 1:
                        enemyZ = spaceShipPosition.z + Random.Range(-MaxDistanceToPlayer, 0);
                        break;
                    default:
                        break;
                }

                EnemyPosition = new Vector3(enemyX, enemyY, enemyZ);
                Vector3 DistanceEnemyToSpaceship = EnemyPosition - spaceShipPosition;
                lengthEnemyToSpaceship = DistanceEnemyToSpaceship.magnitude;

            } while (lengthEnemyToSpaceship < MaxDistanceToPlayer);

            GameObject go = (GameObject)Instantiate(enemySpaceshipsList[Random.Range(0, enemySpaceshipsList.Count)], new Vector3(enemyX, enemyY, enemyZ), Quaternion.identity);
            go.transform.parent = parentSpaceShipEnemies.transform;
            go.name = "Enemy" + _numberOfEnemies;

            _numberOfEnemies++;
        }
    }

    void SpawnEnemyShip()
    {
        Vector3 spaceShipPosition = GameObject.Find("Spaceship").transform.position;

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
                    enemyY = spaceShipPosition.y + Random.Range(0, MaxDistanceToPlayer);
                    break;
                case 1:
                    enemyY = spaceShipPosition.y + Random.Range(-MaxDistanceToPlayer, 0);
                    break;
                default:
                    break;
            }

            // 0 = positive , 1 = negative
            positiveORnegative = Random.Range(0, 2);
            switch (positiveORnegative)
            {
                case 0:
                    enemyZ = spaceShipPosition.z + Random.Range(0, MaxDistanceToPlayer);
                    break;
                case 1:
                    enemyZ = spaceShipPosition.z + Random.Range(-MaxDistanceToPlayer, 0);
                    break;
                default:
                    break;
            }

            EnemyPosition = new Vector3(enemyX, enemyY, enemyZ);
            Vector3 DistanceEnemyToSpaceship = EnemyPosition - spaceShipPosition;
            lengthEnemyToSpaceship = DistanceEnemyToSpaceship.magnitude;

        } while (lengthEnemyToSpaceship < MaxDistanceToPlayer);

        GameObject go = (GameObject)Instantiate(enemySpaceshipsList[Random.Range(0, enemySpaceshipsList.Count)], new Vector3(enemyX, enemyY, enemyZ), Quaternion.identity);
        go.transform.parent = parentSpaceShipEnemies.transform;
        go.name = "Enemy" + _numberOfEnemies;

        _numberOfEnemies++;
        _spawnNewEnemyship = false;
    }

    void SpawnMineral(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int rnd = Random.Range(0, 54);
            Object ObjectToSpawn;
            if (rnd > 49)
            {
                //Mineral10
                ObjectToSpawn = mineralsList[3];
            }
            else if (rnd > 39)
            {
                //Mineral5
                ObjectToSpawn = mineralsList[2];
            }
            else if (rnd > 24)
            {
                //Mineral2
                ObjectToSpawn = mineralsList[1];
            }
            else
            {
                //Mineral1
                ObjectToSpawn = mineralsList[0];
            }

            GameObject go = (GameObject)Instantiate(ObjectToSpawn, _asteroidToDestroy.transform.position, _asteroidToDestroy.transform.rotation);
            go.transform.localScale = new Vector3(2, 2, 2);
            go.transform.parent = parentSpaceShipEnemies.transform;
            go.name = "Mineral" + _numberOfMinerals;

            if (_asteroidToDestroy != null)
            {
                AsteroidsInSpaceList.Remove(_asteroidToDestroy.name);
                Destroy(_asteroidToDestroy);
                _asteroidToDestroy = null;
            }
            _numberOfMinerals++;

            
        }
        
    }
}
