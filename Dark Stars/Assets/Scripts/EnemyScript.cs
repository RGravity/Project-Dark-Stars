using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]

public class EnemyScript : MonoBehaviour {

    public EnumEnemyShipType Shiptype;

    public GameObject target;

    Vector3 acceleration;
    Vector3 velocty;
    public float EnemySpeed = 10.0f;

    List<GameObject> boids = new List<GameObject>();

    //HP
    private bool _hit = false;
    public int HP = 100;

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
        EnemyMovement();
        CheckHit();
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
            Debug.Log("Enemy " + gameObject.name + " HP is lowered to: " + HP);
            Hit = false;
            if (HP <= 0)
            {
                Debug.Log("Enemy " + gameObject.name + " Killed");
                boids.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
