using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnumEnemyShipType
{
    speeder,
    assailant,
    bruiser,
}

public enum EnumMinerals
{
    xenonite1,
    helionite2,
    argonite5,
    neonite10,
}

public enum EnumSkybox
{
    skyboxBlue,
    skyboxGreen,
    skyboxRedBlack,
}

public class MainScript : MonoBehaviour
{

    private bool _spawnNewAsteroid = false;
    public bool SpawnNewAsteroid { set { _spawnNewAsteroid = value; } }

    private GameObject _spawnNewEnemyship;
    public GameObject SpawnNewEnemyship { set { _spawnNewEnemyship = value; } }

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

    private List<EnumMinerals> _mineralsAllowed = new List<EnumMinerals>() { 
        EnumMinerals.xenonite1, 
        EnumMinerals.helionite2, 
        EnumMinerals.argonite5, 
        EnumMinerals.neonite10,
    };
    public List<EnumMinerals> MineralsAllowed { get { return _mineralsAllowed; } set { _mineralsAllowed = value; } }



    // Use this for initialization
    void Start()
    {
        SpawnPlayerShip();
        //SpawnAsteroids(amountOfAsteroids);
        //SpawnRandomEnemyShips(amountOfEnemies);
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawnNewAsteroid)
        {
            SpawnAsteroid();
        }

        if (_spawnNewEnemyship != null)
        {
            SpawnEnemyShips(_spawnNewEnemyship.GetComponent<EnemyScript>().Shiptype, 1);
        }

        if (_asteroidToDestroy != null)
        {
            SpawnMineral(1);
        }

        #region EnemyshipRespawn
        List<string> tempEnemyDestroyedList = new List<string>();
        foreach (string enemy in enemySpaceshipsInSpaceList)
        {
            Vector3 spaceShipPosition = GameObject.Find("Spaceship").transform.position;
            Vector3 enemyPosition = GameObject.Find(enemy).transform.position;

            Vector3 distanceDifference = enemyPosition - spaceShipPosition;
            float lengthDifference = distanceDifference.magnitude;

            if (lengthDifference > MaxDistanceToPlayer+100)
            {
                tempEnemyDestroyedList.Add(enemy);
                
            }
        }
        foreach (string enemyDestroyed in tempEnemyDestroyedList)
        {
            if (GameObject.Find(enemyDestroyed) != null)
            {
                enemySpaceshipsInSpaceList.Remove(enemyDestroyed);
                SpawnEnemyShips(GameObject.Find(enemyDestroyed).GetComponentInChildren<EnemyScript>().Shiptype, 1);
                foreach (GameObject gb in GameObject.FindObjectOfType<EnemyScript>().Boids)
                {
                    gb.GetComponent<EnemyScript>().Boids.Remove(GameObject.Find(enemyDestroyed));
                }
                Destroy(GameObject.Find(enemyDestroyed));
            }
        }
        #endregion

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
        go.GetComponentInChildren<Camera>().farClipPlane = MaxDistanceToPlayer - 100;
    }

    public void SpawnAsteroids(int amount)
    {
        Vector3 spaceShipPosition = GameObject.Find("Spaceship").transform.position;

        for (int i = 0; i < amount; i++)
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
            float randomScale = Random.Range(0.01f, 2.0f);
            go.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            go.transform.rotation = Random.rotation;
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
        go.transform.rotation = Random.rotation;
        go.name = "Asteroid" + _numberOfAsteroids;
        AsteroidsInSpaceList.Add(go.name);

        _numberOfAsteroids++;

        _spawnNewAsteroid = false;
    }

    void SpawnRandomEnemyShips(int amount)
    {
        Vector3 spaceShipPosition = GameObject.Find("Spaceship").transform.position;
        for (int i = 0; i < amount; i++)
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

            } while (lengthEnemyToSpaceship < MinDistanceToPlayer);

            GameObject go;
            switch (Random.Range(0, enemySpaceshipsList.Count))
            {
                case 0:
                    go = (GameObject)Instantiate(enemySpaceshipsList[0], new Vector3(enemyX, enemyY, enemyZ), Quaternion.identity);
                    go.transform.parent = parentSpaceShipEnemies.transform;
                    go.name = EnumEnemyShipType.speeder + "Enemy" + _numberOfEnemies;
                    go.GetComponentInChildren<EnemyScript>().Shiptype = EnumEnemyShipType.speeder;
                    enemySpaceshipsInSpaceList.Add(go.name);
                    break;
                case 1:
                    go = (GameObject)Instantiate(enemySpaceshipsList[1], new Vector3(enemyX, enemyY, enemyZ), Quaternion.identity);
                    go.transform.parent = parentSpaceShipEnemies.transform;
                    go.name = EnumEnemyShipType.assailant + "Enemy" + _numberOfEnemies;
                    go.GetComponentInChildren<EnemyScript>().Shiptype = EnumEnemyShipType.assailant;
                    enemySpaceshipsInSpaceList.Add(go.name);
                    break;
                case 2:
                    go = (GameObject)Instantiate(enemySpaceshipsList[2], new Vector3(enemyX, enemyY, enemyZ), Quaternion.identity);
                    go.transform.parent = parentSpaceShipEnemies.transform;
                    go.name = EnumEnemyShipType.bruiser + "Enemy" + _numberOfEnemies;
                    go.GetComponentInChildren<EnemyScript>().Shiptype = EnumEnemyShipType.bruiser;
                    enemySpaceshipsInSpaceList.Add(go.name);
                    break;
                default:
                    //default is SPEEDER
                    go = (GameObject)Instantiate(enemySpaceshipsList[0], new Vector3(enemyX, enemyY, enemyZ), Quaternion.identity);
                    go.transform.parent = parentSpaceShipEnemies.transform;
                    go.name = EnumEnemyShipType.speeder + "Enemy" + _numberOfEnemies;
                    go.GetComponentInChildren<EnemyScript>().Shiptype = EnumEnemyShipType.speeder;
                    enemySpaceshipsInSpaceList.Add(go.name);
                    break;
            }

            _numberOfEnemies++;
        }
    }

    public void SpawnEnemyShips(EnumEnemyShipType enemyShipType, int amount)
    {
        Vector3 spaceShipPosition = GameObject.Find("Spaceship").transform.position;
        for (int i = 0; i < amount; i++)
        {
            float enemyX = 0;
            float enemyY = 0;
            float enemyZ = 0;
            Vector3 EnemyPosition = new Vector3(enemyX, enemyY, enemyZ);
            float lengthEnemyToSpaceship = 0;
            GameObject go;

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

            } while (lengthEnemyToSpaceship < MinDistanceToPlayer);

            switch (enemyShipType)
            {
                case EnumEnemyShipType.speeder:
                    go = (GameObject)Instantiate(enemySpaceshipsList[0], new Vector3(enemyX, enemyY, enemyZ), Quaternion.identity);
                    go.transform.parent = parentSpaceShipEnemies.transform;
                    go.name = EnumEnemyShipType.speeder + "Enemy" + _numberOfEnemies;
                    enemySpaceshipsInSpaceList.Add(go.name);
                    break;
                case EnumEnemyShipType.assailant:
                    go = (GameObject)Instantiate(enemySpaceshipsList[1], new Vector3(enemyX, enemyY, enemyZ), Quaternion.identity);
                    go.transform.parent = parentSpaceShipEnemies.transform;
                    go.name = EnumEnemyShipType.assailant + "Enemy" + _numberOfEnemies;
                    enemySpaceshipsInSpaceList.Add(go.name);
                    break;
                case EnumEnemyShipType.bruiser:
                    go = (GameObject)Instantiate(enemySpaceshipsList[2], new Vector3(enemyX, enemyY, enemyZ), Quaternion.identity);
                    go.transform.parent = parentSpaceShipEnemies.transform;
                    go.name = EnumEnemyShipType.bruiser + "Enemy" + _numberOfEnemies;
                    enemySpaceshipsInSpaceList.Add(go.name);
                    break;
                default:
                    break;
            }


            _numberOfEnemies++;
            _spawnNewEnemyship = null;
        }
    }

    void SpawnMineral(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Object ObjectToSpawn;
            EnumMinerals mineralType;
            do
            {
                int rnd = Random.Range(0, 54);
                if (rnd > 49)
                {
                    //Mineral10
                    mineralType = EnumMinerals.neonite10;
                    ObjectToSpawn = mineralsList[3];
                }
                else if (rnd > 39)
                {
                    //Mineral5
                    mineralType = EnumMinerals.argonite5;
                    ObjectToSpawn = mineralsList[2];
                }
                else if (rnd > 24)
                {
                    //Mineral2
                    mineralType = EnumMinerals.helionite2;
                    ObjectToSpawn = mineralsList[1];
                }
                else
                {
                    //Mineral1
                    mineralType = EnumMinerals.xenonite1;
                    ObjectToSpawn = mineralsList[0];
                }
            } while (!_mineralsAllowed.Contains(mineralType));

            GameObject go = (GameObject)Instantiate(ObjectToSpawn, _asteroidToDestroy.transform.position, _asteroidToDestroy.transform.rotation);
            go.transform.parent = parentMinerals.transform;
            go.name = mineralType + "Mineral" + _numberOfMinerals;

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
