﻿using UnityEngine;
using System.Collections;

public class MiningLaserScript : MonoBehaviour {

    LineRenderer line;
    public int LaserDistance = 100;
    private bool allowShoot = true;
    public int MaxEnergy = 50;
    private bool releasedButton = false;
    private int energy = 50;

	// Use this for initialization
	void Start () {
        line = gameObject.GetComponent<LineRenderer>();
        line.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1") && allowShoot)
        {
            //Start Shooting
            StartCoroutine("FireLaser");
        }
        else if (!Input.GetButton("Fire1") && allowShoot && energy <= MaxEnergy)
        {
            energy++;

            if (energy > MaxEnergy)
            {
                energy = MaxEnergy;  
            }
        }

        if (!allowShoot)
        {
            StopCoroutine("FireLaser");
            line.enabled = false;
            energy += 4;
            if (energy > MaxEnergy)
            {
                energy = MaxEnergy;
                allowShoot = true;
            }
        }
	}

    IEnumerator FireLaser()
    {
        line.enabled = true;

        while (Input.GetButton("Fire1") && allowShoot)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            line.SetPosition(0, ray.origin);

            if (Physics.Raycast(ray, out hit, LaserDistance))
            {
                line.SetPosition(1, hit.point);
                if (hit.rigidbody)
                {
                    if (hit.collider.gameObject.name.Contains("Asteroid"))
                    {
                        hit.rigidbody.AddForceAtPosition(transform.forward * 10, hit.point); 
                        GameObject asteroidHit = GameObject.Find(hit.collider.gameObject.name);
                        asteroidHit.GetComponent<AsteroidScript>().Hit = true;
                    }
                    else if (hit.collider.gameObject.tag.Contains("Enemy"))
                    {
                        hit.rigidbody.AddForceAtPosition(transform.forward * 10, hit.point);
                        EnemyScript enemyHit = hit.collider.gameObject.GetComponentInParent<EnemyScript>();
                        enemyHit.Hit = true;
                    }
                }
            }
            else
                line.SetPosition(1, ray.GetPoint(LaserDistance));
            energy-= 2;
            if (energy < 0)
            {
                allowShoot = false; 
            }

            yield return null;
        }

        line.enabled = false;
    }
}
