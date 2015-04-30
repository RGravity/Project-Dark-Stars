using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidScript : MonoBehaviour {

    public bool Hit = false;
    public int HP = 100;
    public List<Object> Minerals = new List<Object>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CheckHit();
	}

    void CheckHit()
    {
        if (Hit)
        {
            HP--;
            Debug.Log("HP is lowered to: " + HP);
            Hit = false;
            if (HP <= 0)
            {
                Debug.Log("Asteroid Destroyed");
                SpawnMineral();

                SpawnNewAsteroid();
                Destroy(gameObject);
            }
        }
    }

    void SpawnMineral()
    {
        //SpawnMineralHERE
        int rnd = Random.Range(0, 54);
        Object ObjectToSpawn;
        if (rnd > 49)
        {
            //Mineral10
            ObjectToSpawn = Minerals[3];
        }
        else if (rnd > 39)
        {
            //Mineral5
            ObjectToSpawn = Minerals[2];
        }
        else if (rnd > 24)
        {
            //Mineral2
            ObjectToSpawn = Minerals[1];
        }
        else
        {
            //Mineral1
            ObjectToSpawn = Minerals[0];
        }
        GameObject go = (GameObject)Instantiate(ObjectToSpawn, transform.position, transform.rotation);
        go.transform.localScale = new Vector3(2,2,2);
    }

    void SpawnNewAsteroid()
    {
        ////TODO: Communicate to Michiel's Script to Spawn 1 new Asteroid somewhere...
        GameObject.Find("Main").GetComponent<SpawnPlayerEnemiesAsteroids>().SpawnNewAsteroid = true;
    }
}
