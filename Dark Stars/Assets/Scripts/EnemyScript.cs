using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]

public class EnemyScript : MonoBehaviour {

    public EnumEnemyShipType Shiptype;

    public GameObject target;

    Vector3 acceleration;
    Vector3 velocty;
    public float EnemySpeed;

    List<GameObject> boids = new List<GameObject>();
    public List<GameObject> Boids { get { return boids; } set { boids = value; } }
    

    //HP
    private bool _hit = false;
    public int HP;

    public bool Hit { set { _hit = value; } }

	// Use this for initialization
	void Start () {

         GameObject[] boidsToAdd = GameObject.FindGameObjectsWithTag("Enemy");
         for (int i = 0; i < boidsToAdd.Length; i++)
         {
             boids.Add(boidsToAdd[i]);
         }
         target = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        //Enemy Movement
        CheckBoids();
        EnemyMovement();
        CheckHit();
	}

    void CheckBoids()
    {
        foreach (GameObject boid in boids)
        {
            if (boid == null)
            {
                boids = new List<GameObject>();
                GameObject[] boidsToAdd = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < boidsToAdd.Length; i++)
                {
                    boids.Add(boidsToAdd[i]);
                }
                return;
            }
        }
    }

    void EnemyMovement()
    {
        Vector3 r1 = Rule_1();
        Vector3 r2 = Rule_2();
        Vector3 r3 = Rule_3();

        acceleration = r1 + r2 + r3;
        velocty += 2 * acceleration * Time.deltaTime;

        if (velocty.magnitude > EnemySpeed)
            velocty = velocty.normalized * EnemySpeed;
        GetComponent<Rigidbody>().velocity = velocty;

        Quaternion desiredRotation = Quaternion.LookRotation(velocty);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 3);
    }

    Vector3 Rule_1()
    {

        Vector3 distance = target.transform.position - transform.position;

        if (distance.magnitude < 3)
            return distance.normalized * -12;
        else
            return distance.normalized * 2;
    }

    Vector3 Rule_2()
    {

        if (!Physics.Raycast(transform.position, transform.forward, 2.0f))
        {
            return -transform.up;
        }

        return Vector3.zero;
    }

    Vector3 Rule_3()
    {

        Vector3 c = Vector3.zero;

        foreach (GameObject g in boids)
        {
            if (g.transform.position != transform.position)
            {
                if ((g.transform.position - transform.position).magnitude < 1.0f)
                {
                    c -= (g.transform.position - transform.position);
                }
            }

        }

        return c * 3.0f;

    }

    void CheckHit()
    {
        if (_hit)
        {
            HP--;
            Hit = false;
            if (HP <= 0)
            {
                boids.Remove(gameObject);

                PlayerController Player = GameObject.FindObjectOfType<PlayerController>();
                if (gameObject.name.Contains(EnumEnemyShipType.speeder.ToString()))
                {
                    Player.AmountOfKilledSpeeders += 1;
                }
                else if (gameObject.name.Contains(EnumEnemyShipType.assailant.ToString()))
                {
                    Player.AmountOfKilledAssailants += 1;
                }
                else if (gameObject.name.Contains(EnumEnemyShipType.bruiser.ToString()))
                {
                    Player.AmountOfKilledBruisers += 1;
                }
                Destroy(gameObject);
            }
        }
    }
}
