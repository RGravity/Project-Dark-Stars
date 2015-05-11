﻿using UnityEngine;
using System.Collections;

public class EnemyLaserScript : MonoBehaviour {

    LineRenderer line;
    public int LaserDistance = 50;
    private GameObject target;
    private Transform parent;
    private float angle = 25;

	// Use this for initialization
	void Start () {
        line = gameObject.GetComponent<LineRenderer>();
        line.enabled = false;
        target = GameObject.FindGameObjectWithTag("Player");
        parent = gameObject.transform.parent;
        
	}
	
	// Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.transform.position, parent.position);
        if (distance < LaserDistance && (Vector3.Angle(transform.forward, target.transform.position - transform.position) < angle))
        {
            Debug.Log("FIRUUUUHH ZE LAZOR!!!");
            StopCoroutine("FireLaser");
            StartCoroutine("FireLaser");
        }
    }

    IEnumerator FireLaser()
    {
        line.enabled = true;

        float distance = Vector3.Distance(target.transform.position, parent.position);
        while (distance < LaserDistance && (Vector3.Angle(transform.forward, target.transform.position - transform.position) < angle))
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
                    else if (hit.collider.gameObject.name.Contains("Player"))
                    {
                        hit.rigidbody.AddForceAtPosition(transform.forward * 10, hit.point);
                        GameObject playerHit = GameObject.Find("SpaceShip");
                        playerHit.GetComponent<PlayerController>().Hit = true;
                    }
                }
            }
            else
                line.SetPosition(1, ray.GetPoint(LaserDistance));

            distance = Vector3.Distance(target.transform.position, parent.position);
            yield return null;
        }

        line.enabled = false;
    }
    
}
