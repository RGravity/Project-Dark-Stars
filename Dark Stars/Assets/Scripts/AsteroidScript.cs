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
        //GameObject.Find("Main").GetComponent<SpawnPlayerEnemiesAsteroids>().SpawnNewMineral = true;
        //GameObject.Find("Main").GetComponent<SpawnPlayerEnemiesAsteroids>().SpawnMineralAt = transform;
    }

    void SpawnNewAsteroid()
    {
        GameObject.Find("Main").GetComponent<SpawnPlayerEnemiesAsteroids>().SpawnNewAsteroid = true;
    }
}
